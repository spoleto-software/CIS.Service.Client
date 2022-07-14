using CIS.Service.Client.Models;

namespace CIS.Service.Client.Converters
{
    /// <summary>
    /// The additional convertations for all CRUD.
    /// </summary>
    public interface IModelConverter
    {
        bool CanConvertType<T>();

        T CreateConvert<T>(ContainerModel originalContainer, T model);

        T ReadConvert<T>(ContainerModel originalContainer, T model);

        T UpdateConvert<T>(ContainerModel originalContainer, T model);

        T DeleteConvert<T>(ContainerModel originalContainer, T model);
    }
}
