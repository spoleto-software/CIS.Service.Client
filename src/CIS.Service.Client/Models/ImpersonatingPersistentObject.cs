using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The persistent object with user impersonating feature.
    /// </summary>
    public class ImpersonatingPersistentObject<T> where T : IdentityObject
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        public T Object { get; set; }
    }
}
