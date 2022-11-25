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
        Task<long> GetCountObjectListAsync<T>(FilterModel filterModel = null);

        Task<T> LoadObjectByFilterAsync<T>(SearchModel searchModel = null) where T : IdentityObject;

        Task<List<T>> LoadObjectListAsync<T>(SearchModel searchModel = null) where T : IdentityObject;

        /// <summary>
        /// Async loading the object list from Stored Procedures
        /// </summary>
        /// <remarks>
        /// It works on MS SQL.
        /// </remarks>
        /// <param name="spName">The name of Stored Procedure.</param>
        /// <param name="args">Unnamed params.</param>
        /// <returns>The persistent object list of specified class.</returns>
        Task<List<T>> LoadObjectListSPAsync<T>(string spName, params object[] args) where T : IdentityObject;

        /// <summary>
        /// Async loading the object list from Stored Procedures
        /// </summary>
        /// <remarks>
        /// It works on MS SQL.
        /// </remarks>
        /// <param name="spName">The name of Stored Procedure.</param>
        /// <param name="namedArgs">Params name with values</param>
        /// <returns>The persistent object list of specified class.</returns>
        Task<List<T>> LoadObjectListSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject;

        /// <summary>
        /// Async loading the persistent object list of specified class from Table Valued Function.
        /// </summary>
        /// <param name="funcName">The name of Table Valued Function</param>
        /// <param name="args">The arguments of Table Valued Function.</param>
        /// <returns>The persistent object list of specified class.</returns>
        Task<List<T>> LoadObjectListFnAsync<T>(string funcName, params object[] args) where T : IdentityObject;

        /// <summary>
        /// Async loading the persistent object list of specified class from the specified function on C# code.
        /// </summary>
        Task<List<T>> LoadObjectListCodeFnAsync<T>(string funcName, params object[] args) where T : IdentityObject;

        /// <summary>
        /// Async loads the object value of specified column list from the data source.
        /// </summary>
        Task<List<TV>> LoadObjectValueListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject;
        
        /// <summary>
        /// Async loads the list of the object value of specified column with correspoing identity from the data source.
        /// </summary>
        Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV, TFrom>(ValueSearchModel valueSearchModel) where TFrom : IdentityObject;

        /// <summary>
        /// Async loads the object value of specified column list from the data source.
        /// </summary>
        Task<List<TV>> LoadObjectValueListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel);

        /// <summary>
        /// Async loads the list of the object value of specified column with correspoing identity from the data source.
        /// </summary>
        Task<List<ObjectValue<TV>>> LoadObjectValueKeyListAsync<TV>(string objectClassName, ValueSearchModel valueSearchModel);

        Task<T> CreateAsync<T>(T creatingObject) where T : IdentityObject;

        Task<T> ReadAsync<T>(Guid id) where T : IdentityObject;

        Task UpdateAsync<T>(T updatingObject, bool throwIfNotFound = true) where T : IdentityObject;

        Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields, bool throwIfNotFound = true) where T : IdentityObject;

        Task UpdateOnlyAsync<T>(Guid updatingObjectId, Dictionary<string, object> updateFields, bool throwIfNotFound = true) where T : IdentityObject;

        Task DeleteAsync<T>(Guid id, bool throwIfNotFound = false) where T : IdentityObject;

        Task ExecuteSPAsync<T>(string spName, params object[] args) where T : IdentityObject;

        Task ExecuteSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IdentityObject;

        /// <summary>
        /// Executes the custom method on the specified object.
        /// </summary>
        Task<string> ExecuteAsync<T>(Guid objectId, string methodName, params object[] args) where T : IdentityObject;

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
        Task<string> ExecuteAsync<T>(Expression<Func<T>> obj, string methodName, params object[] args) where T : IdentityObject;

        /// <summary>
        /// Fast insertion of objects into the database.
        /// </summary>
        /// <remarks>
        /// MS SQL uses SqlBulkCopy.<br/>
        /// PostgreSQL uses COPY FROM STDIN (FORMAT BINARY).<br/>
        /// Other dialect providers have fallback as foreach (Insert).
        /// </remarks>
        Task BulkInsertAsync<T>(List<T> creatingObjectList) where T : IdentityObject;
    }
}
