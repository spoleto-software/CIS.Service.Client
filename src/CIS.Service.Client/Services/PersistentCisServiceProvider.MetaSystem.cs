using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.MetaSystem;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    public partial class PersistentCisServiceProvider : IImpersonatingMetaSystemProvider
    {
        public Task<List<MetaAttribute>> LoadAttributesAsync<T>(ImpersonatingUser user) where T : IdentityObject
            => LoadAttributesAsync(user, typeof(T).Name);

        public async Task<List<MetaAttribute>> LoadAttributesAsync(ImpersonatingUser user, string objectClassName)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{objectClassName}/LoadAttributes");

            var jsonModel = JsonHelper.ToJson(user);

            var attributeList = await InvokeAsync<List<MetaAttribute>>(Settings, uri, HttpMethod.Post, jsonModel);

            return attributeList;
        }

        public async Task<List<MetaAttribute>> LoadAttributesAsync<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (contextObject == null)
                throw new ArgumentNullException(nameof(contextObject));


            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{typeof(T).Name}/LoadAttributesByContext");

            var impersonatingContextObject = new ImpersonatingPersistentObject<T>
            {
                User = user,
                Object = contextObject
            };

            var jsonModel = JsonHelper.ToJson(impersonatingContextObject);

            var attributeList = await InvokeAsync<List<MetaAttribute>>(Settings, uri, HttpMethod.Post, jsonModel);

            return attributeList;
        }

        public Task<MetaClass> LoadMetaClassAsync<T>(ImpersonatingUser user) where T : IdentityObject
            => LoadMetaClassAsync(user, typeof(T).Name);

        public async Task<MetaClass> LoadMetaClassAsync(ImpersonatingUser user, string objectClassName)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{objectClassName}/LoadMetaClass");

            var jsonModel = JsonHelper.ToJson(user);

            var metaClass = await InvokeAsync<MetaClass>(Settings, uri, HttpMethod.Post, jsonModel);

            return metaClass;
        }

        public async Task<MetaClass> LoadMetaClassAsync<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (contextObject == null)
                throw new ArgumentNullException(nameof(contextObject));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{typeof(T).Name}/LoadMetaClassByContext");

            var impersonatingContextObject = new ImpersonatingPersistentObject<T>
            {
                User = user,
                Object = contextObject
            };

            var jsonModel = JsonHelper.ToJson(impersonatingContextObject);

            var metaClass = await InvokeAsync<MetaClass>(Settings, uri, HttpMethod.Post, jsonModel);

            return metaClass;
        }

        public async Task<List<MetaClass>> LoadMetaClassListAsync(ImpersonatingUser user, List<string> objectClassNames, bool withAttributes = false)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (objectClassNames == null)
                throw new ArgumentNullException(nameof(objectClassNames));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}PersistentClass/LoadMetaClassList");
            
            var impersonatingPersistentClassList = new ImpersonatingPersistentClassList
            {
                User = user,
                ObjectClassNames = objectClassNames,
                WithAttributes = withAttributes
            };

            var jsonModel = JsonHelper.ToJson(impersonatingPersistentClassList);

            var metaClassList = await InvokeAsync<List<MetaClass>>(Settings, uri, HttpMethod.Post, jsonModel);

            return metaClassList;
        }
    }
}
