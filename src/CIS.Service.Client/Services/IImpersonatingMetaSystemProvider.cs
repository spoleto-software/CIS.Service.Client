using System.Collections.Generic;
using System.Threading.Tasks;
using CIS.Service.Client.Interfaces;
using CIS.Service.Client.MetaSystem;
using CIS.Service.Client.Models;

namespace CIS.Service.Client.Services
{
    /// <summary>
    /// The meta-system provider with user impersonating feature.
    /// </summary>
    public interface IImpersonatingMetaSystemProvider
    {
        /// <summary>
        /// Async loads the available attribute list for the specified user.
        /// </summary>
        Task<List<MetaAttribute>> LoadAttributes<T>(ImpersonatingUser user) where T : IdentityObject;

        /// <summary>
        /// Async loads the available attribute list for the specified user.
        /// </summary>
        Task<List<MetaAttribute>> LoadAttributes(ImpersonatingUser user, string objectClassName);

        /// <summary>
        /// Async loads the available attribute list for the context object and specified user.
        /// </summary>
        Task<List<MetaAttribute>> LoadAttributes<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject;
    }
}
