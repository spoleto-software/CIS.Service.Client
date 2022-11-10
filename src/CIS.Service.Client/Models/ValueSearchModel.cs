namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with filter, order and other information for object value searching (the value of the specified column).
    /// </summary>
    public class ValueSearchModel : FilterModel
    {
        /// <summary>
        /// Gets or sets the specified column.
        /// </summary>
        public string Column { get; set; }

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

        /// <summary>
        /// Gets or sets the flag indicating whether to return only unique values.
        /// </summary>
        public bool Distinct { get; set; }
    }
}
