using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The service provider based on original PersistentObjects.
    /// </summary>
    public interface IPersistentCisServiceProvider
    {
        Task<T> LoadObjectByFilter<T>(SearchModel searchModel = null) where T : IdentityObject;

        Task<List<T>> LoadObjectListAsync<T>(SearchModel searchModel = null) where T : IdentityObject;

        Task<List<T>> LoadObjectListSPAsync<T>(string spName, params object[] args) where T : IdentityObject;

        Task<List<T>> LoadObjectListSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject;

        Task<T> CreateAsync<T>(T creatingObject) where T : IdentityObject;

        Task<T> ReadAsync<T>(Guid id) where T : IdentityObject;

        Task UpdateAsync<T>(T updatingObject) where T : IdentityObject;

        Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields) where T : IdentityObject;

        Task DeleteAsync<T>(Guid id) where T : IdentityObject;

        Task ExecuteSPAsync<T>(string spName, params object[] args) where T : IdentityObject;

        Task ExecuteSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject;

        /// <summary>
        /// Executes the custom method on the specified object.
        /// </summary>
        Task<string> Execute<T>(Guid objectId, string methodName, params object[] args) where T : IdentityObject;

        /// <summary>
        /// Executes the custom method on the specified object.<br/>
        /// The object has to be passed, it will be changed or added to the session.
        /// </summary>
        /// <remarks>
        ///  
        /// await persistentProvider.Execute(() => new SaleSlip
        ///     {
        ///         Identity = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041812345"),
        ///         DateEnd = DateTime.Parse("2022-06-02"),
        ///         Note = "Test slip",
        ///     },
        ///     "CloseSlip", false);
        ///     
        /// </remarks>
        Task<string> Execute<T>(Expression<Func<T>> obj, string methodName, params object[] args) where T : IdentityObject;
    }
}
