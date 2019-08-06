using System;
using System.Collections.Generic;
using System.Text;

namespace SerkoGSTCalculatorApi.Model
{
    /// <summary>
    /// Request data model
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// Gets or sets the mail text
        /// </summary>
        /// <value>
        /// The mail text.
        /// </value>
        public string MailText { get; set; }

        /// <summary>
        /// Gets or sets the time stamp
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime? TimeStampValue { get; set; }

        /// <summary>
        /// Gets or sets the gst %
        /// </summary>
        /// <value>
        /// The gst %.
        /// </value>
        public double GSTPercentage { get; set; }
    }
}
