using System;
using System.Net.Http;
using System.Threading.Tasks;
using CIS.Service.Client.Extensions;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Models;
using Microsoft.Extensions.Logging;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The token provider based on Kerberos/Negotiate.
    /// </summary>
    public class CisServiceNegotiateTokenProvider : ICisServiceTokenProvider
    {
        /// <summary>
        /// NegotiateName
        /// </summary>
        public const string NegotiateName = "Negotiate";

        private readonly ILogger<CisServiceNegotiateTokenProvider> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="httpClientFactory"></param>
        public CisServiceNegotiateTokenProvider(ILogger<CisServiceNegotiateTokenProvider> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<T> InvokeAsync<T>(Uri uri, string requestJsonContent, bool throwOnError)
        {
            using var client = _httpClientFactory.CreateClient(CisServiceNegotiateTokenProvider.NegotiateName);
            client.ConfigureHttpClient();

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            requestMessage.ConfigureRequestMessage();

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

            var uri = new Uri(new Uri(settings.WebAPITokenEndpointAddress), "api/negotiatetoken");

            var loginModel = new LoginModel
            {
                ClientCode = settings.ClientCode,
                ClientSecret = settings.ClientSecret,
                UserId = settings.UserId,
                LoginType = settings.LoginType,
                UserName = settings.UserName,
                
            };
            var requestJsonContent = JsonHelper.ToJson(loginModel);

            return await InvokeAsync<JsonTokenModel>(uri, requestJsonContent, true);
        }

        public JsonTokenModel RefreshToken(WebApiOption settings, JsonTokenModel originalToken)
            => RefreshTokenAsync(settings, originalToken).GetAwaiter().GetResult();

        public async Task<JsonTokenModel> RefreshTokenAsync(WebApiOption settings, JsonTokenModel originalToken)
        {
            _logger.LogInformation("Refresh CIS2 Web Api token.");

            var uri = new Uri(new Uri(settings.WebAPITokenEndpointAddress), "api/negotiatetoken/refresh");

            var loginModel = new RefreshTokenModel
            {
                ClientCode = settings.ClientCode,
                ClientSecret = settings.ClientSecret,
                RefreshToken = originalToken.RefreshToken
            };
            var requestJsonContent = JsonHelper.ToJson(loginModel);

            return await InvokeAsync<JsonTokenModel>(uri, requestJsonContent, false);
        }
    }
}
