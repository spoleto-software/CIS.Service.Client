using CIS.Service.Client.Interfaces;
using CIS.Service.Client.Models;
using System.Collections.Generic;

namespace CIS.Service.Client.Converters
{
    /// <summary>
    /// The additional convertations for all CRUD.
    /// </summary>
    public class ContainerModelConverter : IContainerModelConverter
    {
        private readonly IEnumerable<IModelConverter> _modelConverters;

        public ContainerModelConverter(IEnumerable<IModelConverter> modelConverters = null)
        {
            _modelConverters = modelConverters ?? new IModelConverter[1] { new IIdentityConverter() };
        }

        public ContainerModel<T> CreateConvert<T>(ContainerModel<T> originalContainer) where T : IBody
        {
           var createContainer = new ContainerModel<T>
            {
                Constructed = true,
                Body = originalContainer.Body,
                Identity = originalContainer.Identity,
                ObjectClassName = originalContainer.ObjectClassName,
                ObjectState = ChangeState.Added
            };

            if (typeof(Interfaces.IConvertible).IsAssignableFrom(typeof(T)))
            {
                foreach (var modelConverter in _modelConverters)
                {
                    if (modelConverter.CanConvertType<T>())
                    {
                        createContainer.Body = modelConverter.CreateConvert(createContainer, createContainer.Body);
                    }
                }
            }

            return createContainer;
        }

        public ContainerModel<T> ReadConvert<T>(ContainerModel<T> originalContainer) where T : IBody
        {
            foreach (var modelConverter in _modelConverters)
            {
                if (modelConverter.CanConvertType<T>())
                {
                    originalContainer.Body = modelConverter.ReadConvert(originalContainer, originalContainer.Body);
                }
            }

            return originalContainer;
        }

        public List<ContainerModel<T>> ReadConvert<T>(List<ContainerModel<T>> originalContainerList) where T : IBody
        {
            if (typeof(Interfaces.IConvertible).IsAssignableFrom(typeof(T)))
            {
                foreach (var obj in originalContainerList)
                    ReadConvert(obj);
            }

            return originalContainerList;
        }


        public ContainerModel<T> UpdateConvert<T>(ContainerModel<T> originalContainer) where T : IBody
        {
            var updateContainer = new ContainerModel<T>
            {
                Constructed = true,
                Body = originalContainer.Body,
                Identity = originalContainer.Identity,
                ObjectClassName = originalContainer.ObjectClassName,
                ObjectState = ChangeState.Changed
            };

            if (typeof(Interfaces.IConvertible).IsAssignableFrom(typeof(T)))
            {
                foreach (var modelConverter in _modelConverters)
                {
                    if (modelConverter.CanConvertType<T>())
                    {
                        updateContainer.Body = modelConverter.UpdateConvert(updateContainer, updateContainer.Body);
                    }
                }
            }

            return updateContainer;
        }

        public ContainerModel<T> DeleteConvert<T>(ContainerModel<T> originalContainer) where T : IBody
        {
            var deleteContainer = new ContainerModel<T>
            {
                Constructed = true,
                Body = originalContainer.Body,
                Identity = originalContainer.Identity,
                ObjectClassName = originalContainer.ObjectClassName,
                ObjectState = ChangeState.Removed
            };

            if (typeof(Interfaces.IConvertible).IsAssignableFrom(typeof(T)))
            {
                foreach (var modelConverter in _modelConverters)
                {
                    if (modelConverter.CanConvertType<T>())
                    {
                        deleteContainer.Body = modelConverter.DeleteConvert(deleteContainer, deleteContainer.Body);
                    }
                }
            }

            return deleteContainer;
        }
    }
}
