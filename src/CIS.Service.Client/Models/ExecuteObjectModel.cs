﻿using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The model with information for the method execution on the specified object.
    /// </summary>
    public class ExecuteObjectModel<T> where T : IdentityObject
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExecuteObjectModel()
        {
        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        public ExecuteObjectModel(T obj, string method, object[] args)
        {
            Object = obj;
            Method = method;

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
        /// Gets or sets the primary identifier.
        /// </summary>
        public T Object { get; set; }

        /// <summary>
        /// Gets or sets the method name.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the method arguments.
        /// </summary>
        public WebValue[] Args { get; set; }

        /// <summary>
        /// Returns the string representation.
        /// </summary>
        public override string ToString() => Method;
    }
}
