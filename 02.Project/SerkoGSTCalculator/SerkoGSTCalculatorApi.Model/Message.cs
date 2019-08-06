using System;
using System.Collections.Generic;
using System.Text;

namespace SerkoGSTCalculatorApi.Model
{
    /// <summary>
    /// Messages class
    /// </summary>
    public class Messages
    {
        /// <summary>
        /// Error messages
        /// </summary>
        public struct Error
        {
            public const string EmptyMailText = "Sorry! There is no text to process.";
            public const string TotalIsZero = "Cost of Product cannot be zero";
            public const string TagNotExists = "{tag} does not exist in the given text";
            public const string TextFormatError = "Sorry! The given text is in the wrong format.";
            public const string UnExpectedError = "Sorry for the inconvenience caused. Please contact the admin.";
            public const string TimestampMisMatch = "TimeStamp error! Sorry, the timestamp provided is invalid.";
            public const string TimestampEmpty = "TimeStamp error! Timestamp data is missing.";
        }

        /// <summary>
        /// Success messages
        /// </summary>
        public struct Success
        {
            public const string SuccessText = "Success";
            public const string TimestampMatch = "TimeStamp matches.";
            public const string PrdtCostResult = "The total cost of the product is {totalCost}. For the given GST% Rate of {gstPercent}% applied, the total GST added is {gstadded}. The original cost of the product without GST is {costExclGST}";
        }
    }
}
