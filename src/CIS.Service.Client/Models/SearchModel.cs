namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with filter, order and other information for object searching.
    /// </summary>
    public class SearchModel : FilterModel
    {
        /// <summary>
        /// Gets or sets the expression to execute.<br/>
        /// If this property is <b>initialized</b>, then other propertes are <b>ignored</b>.
        /// </summary>
        public string ExecuteExpression { get; set; }

        /// <summary>
        /// Gets or sets the select statement.
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// Gets or sets the group by.
        /// </summary>
        public string GroupBy { get; set; }

        /// <summary>
        /// Gets or sets the offset in the object collection.
        /// </summary>
        public int? Offset { get; set; }

        /// <summary>
        /// Gets or sets the count of objects from the object collection.
        /// </summary>
        public int? Rows { get; set; }
    }
}
