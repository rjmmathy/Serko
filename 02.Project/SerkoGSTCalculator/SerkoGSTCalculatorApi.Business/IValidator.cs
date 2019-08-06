using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using SerkoGSTCalculatorApi.Model;

namespace SerkoGSTCalculatorApi.Business
{
    /// <summary>
    /// Interface for validator
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Check if given value is a Number
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>Boolean value whether value is number or not</returns>
        bool IsNumber(string value);

        /// <summary>
        /// Check if given node exists in the given xml string
        /// </summary>
        /// <param name="xDoc">XML document</param>
        /// <param name="nodeName">XML node name to check</param>
        /// <returns>Boolean value whether node exists in the xml or not</returns>
        bool CheckNodeExists(XDocument xDoc, string nodeName);

        /// <summary>
        /// Check if given timestamp matches with the Authorization header
        /// </summary>
        /// <param name="context">Http Context object</param>
        /// <param name="timestamp">Timestamp details</param>
        /// <returns>Validation reponse data</returns>
        ValidationData ValidateTimestamp(HttpContext context, DateTime? timestamp);

        #region Validation Response Data
        /// <summary>
        /// Get the validation response data
        /// </summary>
        /// <param name="responseCode">Response Code</param>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="message">Response message</param>
        /// <param name="data">dynamic data</param>
        /// <returns>Validation Response Data object</returns>
        ValidationData GetValidationResponseData(int responseCode, bool responseStatus, string message = "", dynamic data = null);
        #endregion
    }
}
