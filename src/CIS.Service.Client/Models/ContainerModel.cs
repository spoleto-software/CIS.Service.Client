using System;
using System.Text.Json.Serialization;
using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Models
{
    public class ContainerModel
    {
        /// <summary>
        /// Gets or sets the key of the object.
        /// </summary>
        [JsonPropertyName("Key")]
        public Guid? Identity { get; set; }

        /// <summary>
        /// Gets the name of the object class.
        /// </summary>
        [JsonPropertyName("ObjectClassName")]
        public string ObjectClassName { get; set; }

        ///// <summary>
        ///// Gets the assembly name.
        ///// </summary>
        //public string AssemblyName { get; set; }

        ///// <summary>
        ///// Gets the real type name in specified <see cref="AssemblyName"/>.
        ///// </summary>
        //public string RealTypeName { get; set; }

        [JsonPropertyName("ObjectState")]
        public ChangeState ObjectState { get; set; }

        [JsonPropertyName("Constructed")]
        public bool Constructed { get; set; }
    }

    public class ContainerModel<T> : ContainerModel where T : IBody
    {
        public ContainerModel()
        {
            ObjectClassName = typeof(T).Name;
        }

        [JsonPropertyName("Body")]
        public T Body { get; set; }

        public override string ToString() => $"{ObjectClassName}: {Body}";
    }
}
