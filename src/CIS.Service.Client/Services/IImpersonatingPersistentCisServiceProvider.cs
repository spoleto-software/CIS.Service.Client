using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The service provider based on original PersistentObjects with user impersonating feature.
    /// </summary>
    public interface IImpersonatingPersistentCisServiceProvider
    {
        Task<T> LoadObjectByFilterAsync<T>(ImpersonatingUser user, SearchModel searchModel = null) where T : IdentityObject;

        Task<List<T>> LoadObjectListAsync<T>(ImpersonatingUser user, SearchModel searchModel = null) where T : IdentityObject;

        ///// <summary>
        ///// Async loads the object value of specified column list from the data source.
        ///// </summary>
        //Task<List<TV>> LoadObjectValueListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject;
        
        ///// <summary>
        ///// Async loads the list of the object value of specified column with correspoing identity from the data source.
        ///// </summary>
        //Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject;

        ///// <summary>
        ///// Async loads the object value of specified column list from the data source.
        ///// </summary>
        //Task<List<TV>> LoadObjectValueListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel);

        ///// <summary>
        ///// Async loads the list of the object value of specified column with correspoing identity from the data source.
        ///// </summary>
        //Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel);

        Task<T> CreateAsync<T>(ImpersonatingUser user, T creatingObject) where T : IdentityObject;

        Task<T> ReadAsync<T>(ImpersonatingUser user, Guid id) where T : IdentityObject;

        Task UpdateAsync<T>(ImpersonatingUser user, T updatingObject, bool throwIfNotFound = true) where T : IdentityObject;

        Task UpdateOnlyAsync<T>(ImpersonatingUser user, Guid updatingObjectId, Expression<Func<T>> updateFields, bool throwIfNotFound = true) where T : IdentityObject;

        Task UpdateOnlyAsync<T>(ImpersonatingUser user, Guid updatingObjectId, Dictionary<string, object> updateFields, bool throwIfNotFound = true) where T : IdentityObject;

        Task DeleteAsync<T>(ImpersonatingUser user, Guid id, bool throwIfNotFound = false) where T : IdentityObject;
    }
}
