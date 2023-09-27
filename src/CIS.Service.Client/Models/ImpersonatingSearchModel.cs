namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with filter, order and other information for object searching with user impersonating feature.
    /// </summary>
    public class ImpersonatingSearchModel
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the search model.
        /// </summary>
        public SearchModel SearchModel { get; set; }
    }
}
