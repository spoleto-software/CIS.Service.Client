namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with the <see cref="ValueSearchModel"/> and the specified column type with user impersonating feature.
    /// </summary>
    public class ImpersonatingValueSearchCriteria
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the search model.
        /// </summary>
        public ValueSearchCriteria ValueSearchCriteria { get; set; }
    }
}
