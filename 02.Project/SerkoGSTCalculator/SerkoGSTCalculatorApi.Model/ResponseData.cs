using System;

namespace SerkoGSTCalculatorApi.Model
{
    /// <summary>
    /// Response data model
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// Gets or sets the response code
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the message
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the data
        /// </summary>
        /// <value>
        /// The data
        /// </value>
        public dynamic Data { get; set; }

        /// <summary>
        /// Gets or sets the input text
        /// </summary>
        /// <value>
        /// The input text.
        /// </value>
        public string InputText { get; set; }
    }
}
