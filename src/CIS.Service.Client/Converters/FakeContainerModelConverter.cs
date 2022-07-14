using CIS.Service.Client.Models;
using System.Collections.Generic;

namespace CIS.Service.Client.Converters
{
    public class FakeContainerModelConverter : IContainerModelConverter
    {
        ContainerModel<T> IContainerModelConverter.CreateConvert<T>(ContainerModel<T> originalContainer)
            => originalContainer;

        ContainerModel<T> IContainerModelConverter.DeleteConvert<T>(ContainerModel<T> originalContainer)
            => originalContainer;

        ContainerModel<T> IContainerModelConverter.ReadConvert<T>(ContainerModel<T> originalContainer)
            => originalContainer;

        List<ContainerModel<T>> IContainerModelConverter.ReadConvert<T>(List<ContainerModel<T>> originalContainerList)
            => originalContainerList;

        ContainerModel<T> IContainerModelConverter.UpdateConvert<T>(ContainerModel<T> originalContainer)
            => originalContainer;
    }
}
