using Spoleto.Common.Objects;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with the <see cref="ValueSearchModel"/> and the specified column type.
    /// </summary>
    public class ValueSearchCriteria
    {
        /// <summary>
        /// Gets or sets the specified column type.
        /// </summary>
        public WebType ColumnType { get; set; }

        /// <summary>
        /// Gets or sets the search model.
        /// </summary>
        public ValueSearchModel SearchModel { get; set; }
    }
}
