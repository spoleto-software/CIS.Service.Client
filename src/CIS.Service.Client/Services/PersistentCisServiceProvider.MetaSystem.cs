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
        public async Task<List<MetaAttribute>> LoadAttributes<T>(ImpersonatingUser user) where T : IdentityObject
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var uri = new Uri(new Uri(Settings.WebAPIEndpointAddress), $"{_impersonatingControllerName}{typeof(T).Name}/LoadAttributes");

            var jsonModel = JsonHelper.ToJson(user);

            var attributeList = await InvokeAsync<List<MetaAttribute>>(Settings, uri, HttpMethod.Post, jsonModel);

            return attributeList;
        }

        public async Task<List<MetaAttribute>> LoadAttributes<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject
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
    }
}
