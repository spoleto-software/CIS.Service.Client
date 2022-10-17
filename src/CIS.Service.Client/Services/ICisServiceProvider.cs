using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The service provider.
    /// </summary>
    public interface ICisServiceProvider
    {
        Task<ContainerModel<T>> LoadObjectByFilter<T>(SearchModel searchModel = null) where T : IBody;

        Task<List<ContainerModel<T>>> LoadObjectListAsync<T>(SearchModel searchModel = null) where T : IBody;

        Task<List<ContainerModel<T>>> LoadObjectListSPAsync<T>(string spName, params object[] args) where T : IBody;

        Task<List<ContainerModel<T>>> LoadObjectListSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IBody;

        Task<ContainerModel<T>> CreateAsync<T>(ContainerModel<T> creatingObject) where T : IBody;

        Task<ContainerModel<T>> ReadAsync<T>(Guid id) where T : IBody;

        Task UpdateAsync<T>(ContainerModel<T> updatingObject, bool throwIfNotFound = true) where T : IBody;

        Task UpdateOnlyAsync<T>(Guid updatingObjectId, Expression<Func<T>> updateFields, bool throwIfNotFound = true) where T : IBody;

        Task UpdateOnlyAsync<T>(Guid updatingObjectId, Dictionary<string, object> updateFields, bool throwIfNotFound = true) where T : IBody;

        Task DeleteAsync<T>(Guid id, bool throwIfNotFound = false) where T : IBody;

        Task ExecuteSPAsync<T>(string spName, params object[] args) where T : IBody;

        Task ExecuteSPAsync<T>(string spName, Dictionary<string, object> namedArgs) where T : IBody;

    }
}
