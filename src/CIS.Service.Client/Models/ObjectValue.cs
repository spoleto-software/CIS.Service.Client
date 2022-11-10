using System;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The object key + value of some object property.
    /// </summary>
    public class ObjectValue<T>
    {
        /// <summary>
        /// Gets or sets the object key.
        /// </summary>
        public Guid? Identity { get; set; }

        /// <summary>
        /// Gets or sets the object value of some property.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Return the string representation of this object.
        /// </summary>
        public override string ToString() => $"{(Identity == null ? "null" : Identity.Value.ToString("D"))}: {Value}";
    }
}
