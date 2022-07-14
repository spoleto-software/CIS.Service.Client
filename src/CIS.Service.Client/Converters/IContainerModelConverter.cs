using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;
using System.Collections.Generic;

namespace CIS.Service.Client.Converters
{
    /// <summary>
    /// The additional convertations for all CRUD.
    /// </summary>
    public interface IContainerModelConverter
    {
        ContainerModel<T> CreateConvert<T>(ContainerModel<T> originalContainer) where T : IBody;

        ContainerModel<T> ReadConvert<T>(ContainerModel<T> originalContainer) where T : IBody;

        List<ContainerModel<T>> ReadConvert<T>(List<ContainerModel<T>> originalContainerList) where T : IBody;

        ContainerModel<T> UpdateConvert<T>(ContainerModel<T> originalContainer) where T : IBody;

        ContainerModel<T> DeleteConvert<T>(ContainerModel<T> originalContainer) where T : IBody;
    }
}
