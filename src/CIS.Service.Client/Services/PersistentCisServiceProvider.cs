using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using CIS.Service.Client.Converters;
using CIS.Service.Client.Extensions;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The default service provider.
    /// </summary>
    public class PersistentCisServiceProvider : BaseCisServiceProvider, IPersistentCisServiceProvider
    {
        private const string _controllerName = "api/persistent";

        public PersistentCisServiceProvider(ILogger<PersistentCisServiceProvider> logger, IOptions<WebApiOption> settings, IHttpClientFactory httpClientFactory, ICisServiceTokenProvider tokenProvider, IContainerModelConverter converter = null)
            : base(logger, settings, httpClientFactory, tokenProvider)
        {
        }

        public async Task<T> LoadObjectByFilter<T>(SearchModel searchModel = null) where T : IdentityObject
        {
            var singleSearchModel = ToSingleSearchModel(searchModel);

            var objList = await LoadObjectListAsync<T>(singleSearchModel);
            foreach (var obj in objList)
                return obj;

            return null;
        }

        public async Task<List<T>> LoadObjectListAsync<T>(SearchModel searchModel = null) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/LoadObjectListSimple");

            var jsonModel = JsonHelper.ToJson(searchModel);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<List<T>> LoadObjectListSPAsync<T>(string spName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/LoadObjectListSP");

            var webCriteria = new WebLoadingSPCriteria(spName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<List<T>> LoadObjectListSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/LoadObjectListSPByDictionary");

            var webCriteria = new WebLoadingNamedSPCriteria(spName, namedArgs);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<T> CreateAsync<T>(T creatingObject) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(creatingObject);

            return await InvokeAsync<T>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task<T> ReadAsync<T>(Guid id) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var obj = await InvokeAsync<T>(Settings, uri, HttpMethod.Get);
            if (obj == null)
                return null;

            return obj;
        }

        public async Task UpdateAsync<T>(T updatingObject) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{updatingObject.Identity.Value:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(updatingObject);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Put, jsonModel);
        }

        public async Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{updatingObjectId:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var updatingValues = updateFields.ToDictionaryValues();
            var jsonModel = JsonHelper.ToJson(updatingValues);

            await InvokeAsync<object>(Settings, uri, new HttpMethod("PATCH"), jsonModel);
        }

        public async Task DeleteAsync<T>(Guid id) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Delete);
        }

        public async Task ExecuteSPAsync<T>(string spName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteSP");

            var webCriteria = new WebLoadingSPCriteria(spName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task ExecuteSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteSPByDictionary");

            var webCriteria = new WebLoadingNamedSPCriteria(spName, namedArgs);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task Execute<T>(Guid objectId, string methodName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteById");

            var executeModel = new ExecuteModel(objectId, methodName, args);
            var jsonModel = JsonHelper.ToJson(executeModel);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task Execute<T>(T obj, string methodName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteByObject");

            var executeModel = new ExecuteObjectModel<T>(obj, methodName, args);
            var jsonModel = JsonHelper.ToJson(executeModel);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }
    }
}
