using System.Text.Json.Serialization;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The container with full information for loading data from stored procedures with unnamed arguments.
    /// </summary>
    public class WebLoadingSPCriteria
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WebLoadingSPCriteria()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public WebLoadingSPCriteria(string storedProcedureName, object[] args)
        {
            SPName = storedProcedureName;

            if (args?.Length > 0)
            {
                Args = new WebValue[args.Length];
                for (var i = 0; i < args.Length; i++)
                {
                    var arg = args[i];
                    Args[i] = new WebValue(@arg);
                }
            }
        }

        /// <summary>
        /// Gets or sets the stored procedure name.
        /// </summary>
        [JsonPropertyName("SPName")]
        public string SPName { get; set; }

        /// <summary>
        /// Gets or sets the unnamed arguments.
        /// </summary>
        [JsonPropertyName("Args")]
        public WebValue[] Args { get; set; }
    }
}
