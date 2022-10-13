using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CIS.Service.Client.Exceptions;
using CIS.Service.Client.Extensions;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The base service provider.
    /// </summary>
    public abstract class BaseCisServiceProvider
    {
        private static readonly HashSet<string> _objectNotFoundMessages = new()
        {
            "\"The object is not found.\"",
            "\"The object for patching is not found.\"",
            "\"The object for editing is not found.\"",
            "\"The object for deleting is not found.\""
        };

        private readonly ILogger _logger;
        private readonly WebApiOption _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICisServiceTokenProvider _tokenProvider;
        private JsonTokenModel _token;

        /// <summary>
        /// Gets the settings.
        /// </summary>
        protected WebApiOption Settings => _settings;

        protected BaseCisServiceProvider(ILogger logger, IOptions<WebApiOption> settings, IHttpClientFactory httpClientFactory, ICisServiceTokenProvider tokenProvider)
        {
            _logger = logger;
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(WebApiOption));
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        private async Task InitHeadersAsync(HttpRequestMessage requestMessage, WebApiOption settings)
        {
            requestMessage.ConfigureRequestMessage();
            var token = await GetTokenAsync(settings);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
        }

        private async Task<JsonTokenModel> GetTokenAsync(WebApiOption settings) => _token ??= await _tokenProvider.GetTokenAsync(settings);

        protected async Task<T> InvokeAsync<T>(WebApiOption settings, Uri uri, HttpMethod method, string requestJsonContent = null, bool throwIfNotFound = true)
        {
            using var client = _httpClientFactory.CreateClient();
            client.ConfigureHttpClient();

            using var requestMessage = new HttpRequestMessage(method, uri);
            await InitHeadersAsync(requestMessage, settings);
            if (requestJsonContent != null)
            {
                requestMessage.Content = new StringContent(requestJsonContent, DefaultSettings.Encoding, DefaultSettings.ContentType);
            }

            using var responseMessage = await client.SendAsync(requestMessage);

            if (responseMessage.IsSuccessStatusCode)
            {
                var result = await responseMessage.Content.ReadAsStringAsync();

                var objectResult = JsonHelper.FromJson<T>(result);
                return objectResult;
            }

            var errorResult = await responseMessage.Content.ReadAsStringAsync();
            if (!String.IsNullOrEmpty(errorResult))
            {
                var isTokenExpiredAsync = await IsTokenExpiredAsync(responseMessage);
                if (isTokenExpiredAsync)
                {
                    _token = await _tokenProvider.RefreshTokenAsync(settings, _token);
                    //если рефреш токен будет истекший, то вернется null и _token станет равным null и в методе GetTokenAsync по новой будет создан.

                    return await InvokeAsync<T>(settings, uri, method, requestJsonContent);
                }

                if (responseMessage.Content.Headers.ContentType.MediaType == ContentTypes.ApplicationJson)
                {
                    if (responseMessage.StatusCode == HttpStatusCode.NotFound
                       && _objectNotFoundMessages.Contains(errorResult))
                    {
                        if (throwIfNotFound)
                        {
                            var notFoundException = new NotFoundException(errorResult);
                            notFoundException.InitializeException(responseMessage);

                            throw notFoundException;
                        }

                        return default;
                    }

                    _logger.LogError(errorResult);
                    Exception exception;
                    if ((int)responseMessage.StatusCode == (int)CustomHttpStatusCode.ExceptionContent)
                    {
                        var exceptionContent = JsonHelper.FromJson<ExceptionContent>(errorResult);

                        exception = (ServiceException)exceptionContent;
                    }
                    else
                        exception= new Exception(errorResult);

                    exception.InitializeException(responseMessage);

                    throw exception;
                }
                else
                {
                    _logger.LogError(errorResult);
                    
                    var exception = new Exception(errorResult);
                    exception.InitializeException(responseMessage);

                    throw exception;
                }
            }
            else
            {
                _logger.LogError(responseMessage.ReasonPhrase);

                var exception = new Exception(responseMessage.ReasonPhrase);
                exception.InitializeException(responseMessage);

                throw exception;
            }
        }

        private async Task<bool> IsTokenExpiredAsync(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.Forbidden
                && response.Content.Headers.ContentType?.MediaType == ContentTypes.TextPlain)
            {
                var result = await response.Content.ReadAsStringAsync();
                if (result == CustomResponses.LifetimeValidationFailedResponse)
                {
                    return true;
                }
            }

            return false;
        }

        protected static SearchModel ToSingleSearchModel(SearchModel originalSearchModel)
        {
            var singleSearchModel = new SearchModel { Rows = 1 };

            if (originalSearchModel != null)
            {
                singleSearchModel.Order = originalSearchModel.Order;
                singleSearchModel.Offset = originalSearchModel.Offset;
                singleSearchModel.Filter = originalSearchModel.Filter;
                singleSearchModel.GroupBy = originalSearchModel.GroupBy;
                singleSearchModel.Select = originalSearchModel.Select;
                singleSearchModel.ExecuteExpression = originalSearchModel.ExecuteExpression;
            }

            return singleSearchModel;
        }
    }
}
