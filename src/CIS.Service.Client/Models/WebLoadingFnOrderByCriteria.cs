using System.Text.Json.Serialization;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The container with full information for loading data from table valued functions with the specified orderBy field names with unnamed arguments.
    /// </summary>
    public class WebLoadingFnOrderByCriteria : WebLoadingSPCriteria
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WebLoadingFnOrderByCriteria()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public WebLoadingFnOrderByCriteria(string storedProcedureName, string orderByFieldNames, object[] args)
            : base(storedProcedureName, args)
        {
            OrderByFieldNames = orderByFieldNames;
        }

        /// <summary>
        /// Gets or sets the orderBy field names.
        /// </summary>
        [JsonPropertyName("OrderByFieldNames")]
        public string OrderByFieldNames { get; set; }
    }
}
