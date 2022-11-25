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

        public async Task<long> GetCountObjectListAsync<T>(FilterModel filterModel = null)
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/GetCountObjectListSimple");

            filterModel ??= new ();
            var jsonModel = JsonHelper.ToJson(filterModel);

            var count = await InvokeAsync<long>(Settings, uri, HttpMethod.Post, jsonModel);

            return count;
        }

        public async Task<T> LoadObjectByFilterAsync<T>(SearchModel searchModel = null) where T : IdentityObject
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

            searchModel ??= new SearchModel();
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

        public async Task<List<T>> LoadObjectListFnAsync<T>(string funcName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/LoadObjectListFn");

            var webCriteria = new WebLoadingSPCriteria(funcName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<List<T>> LoadObjectListCodeFnAsync<T>(string funcName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/LoadObjectListCodeFn");

            var webCriteria = new WebLoadingSPCriteria(funcName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public Task<List<TV>> LoadObjectValueListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject
            => LoadObjectValueListAsync<TV>(typeof(TFrom).Name, valueSearchModel);

        public Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject
            => LoadObjectValueKeyListAsync<TV>(typeof(TFrom).Name, valueSearchModel);

        public async Task<List<TV>> LoadObjectValueListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel)
        {
            if (valueSearchModel == null)
                throw new ArgumentNullException(nameof(valueSearchModel));

            var valueSearchCriteria = new ValueSearchCriteria
            {
                SearchModel = valueSearchModel,
                ColumnType = new WebType(typeof(TV))
            };

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{objectClassName}/LoadObjectValueListSimple");

            var jsonModel = JsonHelper.ToJson(valueSearchCriteria);

            var objList = await InvokeAsync<List<TV>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel)
        {
            if (valueSearchModel == null)
                throw new ArgumentNullException(nameof(valueSearchModel));

            var valueSearchCriteria = new ValueSearchCriteria
            {
                SearchModel = valueSearchModel,
                ColumnType = new WebType(typeof(TV))
            };

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{objectClassName}/LoadObjectValueKeyListSimple");

            var jsonModel = JsonHelper.ToJson(valueSearchCriteria);

            var objList = await InvokeAsync<List<ObjectValue<TV>>>(Settings, uri, HttpMethod.Post, jsonModel);

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

        public async Task UpdateAsync<T>(T updatingObject, bool throwIfNotFound = true) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{updatingObject.Identity.Value:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(updatingObject);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Put, jsonModel, throwIfNotFound: throwIfNotFound);
        }

        public Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields, bool throwIfNotFound = true) where T : IdentityObject
        {
            var updatingValues = updateFields.ToDictionaryValues();

            return UpdateOnlyAsync<T>(updatingObjectId, updatingValues, throwIfNotFound);
        }

        public async Task UpdateOnlyAsync<T>(Guid updatingObjectId, Dictionary<string, object> updateFields, bool throwIfNotFound = true) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{updatingObjectId:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(updateFields);

            await InvokeAsync<object>(Settings, uri, new HttpMethod("PATCH"), jsonModel, throwIfNotFound: throwIfNotFound);
        }

        public async Task DeleteAsync<T>(Guid id, bool throwIfNotFound = false) where T : IdentityObject
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Delete, throwIfNotFound: throwIfNotFound);
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

        public async Task<string> ExecuteAsync<T>(Guid objectId, string methodName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteById");

            var executeModel = new ExecuteModel(objectId, methodName, args);
            var jsonModel = JsonHelper.ToJson(executeModel);

            var result = await InvokeAsync<string>(Settings, uri, HttpMethod.Post, jsonModel);
            return result;
        }

        public async Task<string> ExecuteAsync<T>(Expression<Func<T>> obj, string methodName, params object[] args) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/ExecuteByObject");

            var objValues = obj.ToDictionaryValues();
            if (objValues == null)
                throw new ArgumentNullException("Incorrect initialization of object in Execute method");

            var executeModel = new ExecuteObjectModel<T>(objValues, methodName, args);
            var jsonModel = JsonHelper.ToJson(executeModel);

            var result = await InvokeAsync<string>(Settings, uri, HttpMethod.Post, jsonModel);
            return result;
        }

        public async Task BulkInsertAsync<T>(List<T> creatingObjectList) where T : IdentityObject
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/{typeof(T).Name}/BulkInsertSimple");

            var jsonModel = JsonHelper.ToJson(creatingObjectList);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }
    }
}
