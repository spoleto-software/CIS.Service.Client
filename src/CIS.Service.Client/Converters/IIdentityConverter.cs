using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Converters
{
    public class IIdentityConverter : IModelConverter
    {
        bool IModelConverter.CanConvertType<T>()
            => typeof(IIdentity).IsAssignableFrom(typeof(T));

        T IModelConverter.CreateConvert<T>(ContainerModel originalContainer, T model)
            => model;

        T IModelConverter.ReadConvert<T>(ContainerModel originalContainer, T model)
        {
            ((IIdentity)model).Identity = originalContainer.Identity;

            return model;
        }

        T IModelConverter.UpdateConvert<T>(ContainerModel originalContainer, T model)
            => model;

        T IModelConverter.DeleteConvert<T>(ContainerModel originalContainer, T model)
            => model;
    }
}
