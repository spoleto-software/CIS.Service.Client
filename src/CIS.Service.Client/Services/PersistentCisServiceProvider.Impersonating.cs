using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Threading.Tasks;
using CIS.Service.Client.Extensions;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;
using Spoleto.Common.Objects;

namespace CIS.Service.Client.Services
{
    public partial class PersistentCisServiceProvider : IImpersonatingPersistentCisServiceProvider
    {
        private const string _impersonatingControllerName = "api/persistent/impersonating";

        public async Task<long> GetCountObjectListAsync<T>(ImpersonatingUser user, FilterModel filterModel = null)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{typeof(T).Name}/GetCountObjectList");

            filterModel ??= new();
            var impersonatingFilterModel = new ImpersonatingFilterModel
            {
                User = user,
                FilterModel = filterModel
            };

            var jsonModel = JsonHelper.ToJson(impersonatingFilterModel);

            var count = await InvokeAsync<long>(Settings, uri, HttpMethod.Post, jsonModel);

            return count;
        }

        public async Task<T> LoadObjectByFilterAsync<T>(ImpersonatingUser user, SearchModel searchModel = null) where T : IdentityObject
        {
            var singleSearchModel = ToSingleSearchModel(searchModel);

            var objList = await LoadObjectListAsync<T>(user, singleSearchModel);
            foreach (var obj in objList)
                return obj;

            return null;
        }

        public async Task<List<T>> LoadObjectListAsync<T>(ImpersonatingUser user, SearchModel searchModel = null) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            searchModel ??= new();
            var impersonatingSearchModel = new ImpersonatingSearchModel
            {
                User = user,
                SearchModel = searchModel
            };

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{typeof(T).Name}/LoadObjectList");

            var jsonModel = JsonHelper.ToJson(impersonatingSearchModel);

            var objList = await InvokeAsync<List<T>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public Task<List<TV>> LoadObjectValueListAsync<TV, TFrom>(ImpersonatingUser user, ValueSearchModel valueSearchModel) where TFrom : IdentityObject
            => LoadObjectValueListAsync<TV>(user, typeof(TFrom).Name, valueSearchModel);

        public Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV, TFrom>(ImpersonatingUser user, ValueSearchModel valueSearchModel) where TFrom : IdentityObject
            => LoadObjectValueKeyListAsync<TV>(user, typeof(TFrom).Name, valueSearchModel);

        public async Task<List<TV>> LoadObjectValueListAsync<TV>(ImpersonatingUser user, string objectClassName, ValueSearchModel valueSearchModel)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (valueSearchModel == null)
                throw new ArgumentNullException(nameof(valueSearchModel));

            var valueSearchCriteria = new ValueSearchCriteria
            {
                SearchModel = valueSearchModel,
                ColumnType = new WebType(typeof(TV))
            };

            var impersonatingValueSearchCriteria = new ImpersonatingValueSearchCriteria
            {
                User = user,
                ValueSearchCriteria = valueSearchCriteria
            };

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{objectClassName}/LoadObjectValueList");

            var jsonModel = JsonHelper.ToJson(impersonatingValueSearchCriteria);

            var objList = await InvokeAsync<List<TV>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV>(ImpersonatingUser user, string objectClassName, ValueSearchModel valueSearchModel)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (valueSearchModel == null)
                throw new ArgumentNullException(nameof(valueSearchModel));

            var valueSearchCriteria = new ValueSearchCriteria
            {
                SearchModel = valueSearchModel,
                ColumnType = new WebType(typeof(TV))
            };

            var impersonatingValueSearchCriteria = new ImpersonatingValueSearchCriteria
            {
                User = user,
                ValueSearchCriteria = valueSearchCriteria
            };

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{objectClassName}/LoadObjectValueKeyList");

            var jsonModel = JsonHelper.ToJson(impersonatingValueSearchCriteria);

            var objList = await InvokeAsync<List<ObjectValue<TV>>>(Settings, uri, HttpMethod.Post, jsonModel);

            return objList;
        }

        public async Task<T> CreateAsync<T>(ImpersonatingUser user, T creatingObject) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var relativeUri = $"{_impersonatingControllerName}{typeof(T).Name}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var impersonatingObject = new ImpersonatingPersistentObject<T>
            {
                User = user,
                Object = creatingObject
            };

            var jsonModel = JsonHelper.ToJson(impersonatingObject);

            return await InvokeAsync<T>(Settings, uri, HttpMethod.Post, jsonModel);
        }

        public async Task<T> ReadAsync<T>(ImpersonatingUser user, Guid id) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var relativeUri = $"{_impersonatingControllerName}{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(user);

            var obj = await InvokeAsync<T>(Settings, uri, HttpMethod.Get, jsonModel, throwIfNotFound: false);
            if (obj == null)
                return null;

            return obj;
        }

        public async Task UpdateAsync<T>(ImpersonatingUser user, T updatingObject, bool throwIfNotFound = true) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var relativeUri = $"{_impersonatingControllerName}{typeof(T).Name}/{updatingObject.Identity.Value:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var impersonatingObject = new ImpersonatingPersistentObject<T>
            {
                User = user,
                Object = updatingObject
            };

            var jsonModel = JsonHelper.ToJson(impersonatingObject);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Put, jsonModel, throwIfNotFound: throwIfNotFound);
        }

        public Task UpdateOnlyAsync<T>(ImpersonatingUser user, Guid updatingObjectId, Expression<Func<T>> updateFields, bool throwIfNotFound = true) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var updatingValues = updateFields.ToDictionaryValues();

            return UpdateOnlyAsync<T>(user, updatingObjectId, updatingValues, throwIfNotFound);
        }

        public async Task UpdateOnlyAsync<T>(ImpersonatingUser user, Guid updatingObjectId, Dictionary<string, object> updateFields, bool throwIfNotFound = true) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var relativeUri = $"{_impersonatingControllerName}{typeof(T).Name}/{updatingObjectId:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var impersonatingUpdatingValues = new ImpersonatingUpdatingValues
            {
                User = user,
                UpdatingValues = updateFields
            };

            var jsonModel = JsonHelper.ToJson(impersonatingUpdatingValues);

            await InvokeAsync<object>(Settings, uri, new HttpMethod("PATCH"), jsonModel, throwIfNotFound: throwIfNotFound);
        }

        public async Task DeleteAsync<T>(ImpersonatingUser user, Guid id, bool throwIfNotFound = false) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var relativeUri = $"{_impersonatingControllerName}{typeof(T).Name}/{id:D}";
            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), relativeUri);

            var jsonModel = JsonHelper.ToJson(user);

            await InvokeAsync<object>(Settings, uri, HttpMethod.Delete, jsonModel, throwIfNotFound: throwIfNotFound);
        }
    }
}
