using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CIS.Service.Client.Extensions;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Models;
using Microsoft.Extensions.Logging;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The token provider based on Login and Password.
    /// </summary>
    public class CisServiceCredentialsTokenProvider : ICisServiceTokenProvider
    {
        private readonly ILogger<CisServiceNegotiateTokenProvider> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClientFactory"></param>
        public CisServiceCredentialsTokenProvider(ILogger<CisServiceNegotiateTokenProvider> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<T> InvokeAsync<T>(Uri uri, WebApiOption option, string requestJsonContent, bool throwOnError)
        {
            using var client = _httpClientFactory.CreateClient();
            client.ConfigureHttpClient();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.ConfigureRequestMessage();

            var byteArray = System.Text.Encoding.UTF8.GetBytes($"{option.UserName}:{option.Password}");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            if (requestJsonContent != null)
                requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);

            using var responseMessage = await client.SendAsync(requestMessage);
            var result = await responseMessage.Content.ReadAsStringAsync();
            if (responseMessage.IsSuccessStatusCode)
            {
                var tokenModel = JsonHelper.FromJson<T>(result);
                return tokenModel;
            }

            if (throwOnError)
            {
                Exception exception;
                if (!string.IsNullOrEmpty(result))
                {
                    _logger.LogError(result);

                    if ((int)responseMessage.StatusCode == (int)CustomHttpStatusCode.ExceptionContent)
                    {
                        var exceptionContent = JsonHelper.FromJson<ExceptionContent>(result);

                        exception = (ServiceException)exceptionContent;
                    }
                    else
                    {
                        exception = new Exception(result);
                    }
                }
                else
                {
                    _logger.LogError(responseMessage.ReasonPhrase);
                    exception = new Exception(responseMessage.ReasonPhrase);
                }

                exception.InitializeException(responseMessage);

                throw exception;
            }

            return default;
        }

        public JsonTokenModel GetToken(WebApiOption settings)
            => GetTokenAsync(settings).GetAwaiter().GetResult();

        public async Task<JsonTokenModel> GetTokenAsync(WebApiOption settings)
        {
            _logger.LogInformation("Get CIS2 Web Api token.");

            var uri = new Uri(new Uri(settings.WebAPITokenEndpointAddress), "api/token");

            var loginModel = new LoginModelCredentials
            {
                ClientCode = settings.ClientCode,
                ClientSecret = settings.ClientSecret,
                LoginType = settings.LoginType,
                UserName = settings.UserName,
                Password = settings.Password
            };
            var requestJsonContent = JsonHelper.ToJson(loginModel);

            return await InvokeAsync<JsonTokenModel>(uri, settings, requestJsonContent, true);
        }

        public JsonTokenModel RefreshToken(WebApiOption settings, JsonTokenModel originalToken)
            => RefreshTokenAsync(settings, originalToken).GetAwaiter().GetResult();

        public async Task<JsonTokenModel> RefreshTokenAsync(WebApiOption settings, JsonTokenModel originalToken)
        {
            _logger.LogInformation("Refresh CIS2 Web Api token.");

            var uri = new Uri(new Uri(settings.WebAPITokenEndpointAddress), "api/token/refresh");

            var loginModel = new RefreshTokenModel
            {
                ClientCode = settings.ClientCode,
                ClientSecret = settings.ClientSecret,
                RefreshToken = originalToken.RefreshToken
            };
            var requestJsonContent = JsonHelper.ToJson(loginModel);

            return await InvokeAsync<JsonTokenModel>(uri, settings, requestJsonContent, false);
        }
    }
}
