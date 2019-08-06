using System;
using System.Collections.Generic;
using System.Text;

namespace SerkoGSTCalculatorApi.Model
{
    /// <summary>
    /// Validation data model
    /// </summary>
    public class ValidationData
    {
        /// <summary>
        /// Gets or sets the response code
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the response status
        /// </summary>
        /// <value>
        /// The response status.
        /// </value>
        public bool ResponseStatus { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        /// <value>
        /// The response message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        /// <value>
        /// The response data.
        /// </value>
        public dynamic Data { get; set; }
    }
}
