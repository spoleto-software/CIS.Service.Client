using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The container with full information for loading data from stored procedures with named arguments.
    /// </summary>
    public class WebLoadingNamedSPCriteria
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WebLoadingNamedSPCriteria()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public WebLoadingNamedSPCriteria(string storedProcedureName, Dictionary<string, object> namedArgs)
        {
            SPName = storedProcedureName;

            if (namedArgs?.Count > 0)
            {
                NamedArgs = new Dictionary<string, WebValue>();
                foreach (var namedArg in namedArgs)
                {
                    NamedArgs.Add(namedArg.Key, new WebValue(namedArg.Value));
                }
            }
        }

        /// <summary>
        /// Gets or sets the stored procedure name.
        /// </summary>
        [JsonPropertyName("SPName")]
        public string SPName { get; set; }

        /// <summary>
        /// Gets or sets the named arguments.
        /// </summary>
        [JsonPropertyName("NamedArgs")]
        public Dictionary<string, WebValue> NamedArgs { get; set; }
    }
}
