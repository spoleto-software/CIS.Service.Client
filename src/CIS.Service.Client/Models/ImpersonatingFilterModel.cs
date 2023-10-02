namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with filter and groupBy information for object searching with user impersonating feature.
    /// </summary>
    public class ImpersonatingFilterModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public FilterModel FilterModel { get; set; }
    }
}
