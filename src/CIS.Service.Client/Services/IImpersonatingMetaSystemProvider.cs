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
        Task<List<MetaAttribute>> LoadAttributesAsync<T>(ImpersonatingUser user) where T : IdentityObject;

        /// <summary>
        /// Async loads the available attribute list for the specified user.
        /// </summary>
        Task<List<MetaAttribute>> LoadAttributesAsync(ImpersonatingUser user, string objectClassName);

        /// <summary>
        /// Async loads the available attribute list for the context object and specified user.
        /// </summary>
        Task<List<MetaAttribute>> LoadAttributesAsync<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject;

        /// <summary>
        /// Async loads the accessible meta-system info for the specified user.
        /// </summary>
        Task<MetaClass> LoadMetaClassAsync<T>(ImpersonatingUser user) where T : IdentityObject;

        /// <summary>
        /// Async loads the accessible meta-system info for the specified user.
        /// </summary>
        Task<MetaClass> LoadMetaClassAsync(ImpersonatingUser user, string objectClassName);

        /// <summary>
        /// Async loads the accessible meta-system info for the context object and specified user.
        /// </summary>
        Task<MetaClass> LoadMetaClassAsync<T>(ImpersonatingUser user, T contextObject) where T : IdentityObject;

        /// <summary>
        /// Async loads the accessible meta-system info list for the specified user.
        /// </summary>
        Task<List<MetaClass>> LoadMetaClassListAsync(ImpersonatingUser user, List<string> objectClassNames, bool withAttributes = false);
    }
}
