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
    public class CisServiceProvider : BaseCisServiceProvider, ICisServiceProvider
    {
        private const string _controllerName = "api/lightpersistent";

        private readonly IContainerModelConverter _converter;

        public CisServiceProvider(ILogger<CisServiceProvider> logger, IOptions<WebApiOption> settings, IHttpClientFactory httpClientFactory, ICisServiceTokenProvider tokenProvider, IContainerModelConverter converter = null)
            : base(logger, settings, httpClientFactory, tokenProvider)
        {
            _converter = converter ?? new ContainerModelConverter();
        }

        public async Task<ContainerModel<T>> LoadObjectByFilter<T>(SearchModel searchModel = null) where T : IBody
        {
            var singleSearchModel = ToSingleSearchModel(searchModel);

            var objList = await LoadObjectListAsync<T>(singleSearchModel);
            foreach (var obj in objList)
                return obj;

            return null;
        }

        public async Task<List<ContainerModel<T>>> LoadObjectListAsync<T>(SearchModel searchModel = null) where T : IBody
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/LoadObjectList/{typeof(T).Name}");

            var jsonModel = JsonHelper.ToJson(searchModel);

            var objList = await InvokeAsync<List<ContainerModel<T>>>(Settings, uri, HttpMethod.Post, jsonModel);
            objList = _converter.ReadConvert(objList);

            return objList;
        }

        public async Task<List<ContainerModel<T>>> LoadObjectListSPAsync<T>(string spName, params object[] args) where T : IBody
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/LoadObjectListSP/{typeof(T).Name}");

            var webCriteria = new WebLoadingSPCriteria(spName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<ContainerModel<T>>>(Settings, uri, HttpMethod.Post, jsonModel);
            objList = _converter.ReadConvert(objList);

            return objList;
        }

        public async Task<List<ContainerModel<T>>> LoadObjectListSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IBody
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/LoadObjectListSPByDictionary/{typeof(T).Name}");

            var webCriteria = new WebLoadingNamedSPCriteria(spName, namedArgs);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            var objList = await InvokeAsync<List<ContainerModel<T>>>(Settings, uri, HttpMethod.Post, jsonModel);
            objList = _converter.ReadConvert(objList);

            return objList;
        }

        public async Task<ContainerModel<T>> CreateAsync<T>(ContainerModel<T> creatingObject) where T : IBody
        {
            var relativeUri = $"{_controllerName}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var createContainer = _converter.CreateConvert(creatingObject);
            var jsonModel = JsonHelper.ToJson(createContainer);

            return await InvokeAsync<ContainerModel<T>>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task<ContainerModel<T>> ReadAsync<T>(Guid id) where T : IBody
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var obj = await InvokeAsync<ContainerModel<T>>(Settings, uri, HttpMethod.Get);
            if (obj == null)
                return null;

            obj = _converter.ReadConvert(obj);

            return obj;
        }

        public async Task UpdateAsync<T>(ContainerModel<T> updatingObject) where T : IBody
        {
            var relativeUri = $"{_controllerName}/{updatingObject.Identity.Value:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var updateContainer = _converter.UpdateConvert(updatingObject);
            var jsonModel = JsonHelper.ToJson(updateContainer);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Put, jsonModel);
        }

        public async Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields) where T : IBody
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{updatingObjectId:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var updatingValues = updateFields.ToDictionaryValues();
            var jsonModel = JsonHelper.ToJson(updatingValues);

            await InvokeAsync<object>(Settings, uri, new HttpMethod("PATCH"), jsonModel);
        }

        public async Task DeleteAsync<T>(Guid id) where T : IBody
        {
            var relativeUri = $"{_controllerName}/{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Delete);
        }

        public async Task ExecuteSPAsync<T>(string spName, params object[] args) where T : IBody
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/ExecuteSP/{typeof(T).Name}");

            var webCriteria = new WebLoadingSPCriteria(spName, args);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task ExecuteSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IBody
        {
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_controllerName}/ExecuteSPByDictionary/{typeof(T).Name}");

            var webCriteria = new WebLoadingNamedSPCriteria(spName, namedArgs);
            var jsonModel = JsonHelper.ToJson(webCriteria);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Post, jsonModel);
        }
    }
}
