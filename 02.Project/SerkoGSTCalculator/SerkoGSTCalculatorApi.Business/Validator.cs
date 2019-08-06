using Microsoft.AspNetCore.Http;
using SerkoGSTCalculatorApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Xml.Linq;

namespace SerkoGSTCalculatorApi.Business
{
    /// <summary>
    /// Helper for Validator
    /// </summary>
    /// <seealso cref="SerkoGSTCalculatorApi.Business.IValidator" />
    public class Validator : IValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validator" /> class.
        /// </summary>
        public Validator()
        {
        }

        /// <summary>
        /// Check if given value is a Number
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>Boolean value whether value is number or not</returns>
        public bool IsNumber(string value)
        {
            return double.TryParse(value, out double res);
        }

        /// <summary>
        /// Check if given node exists in the given xml string
        /// </summary>
        /// <param name="xDoc">XML document</param>
        /// <param name="nodeName">XML node name to check</param>
        /// <returns>Boolean value whether node exists in the xml or not</returns>
        public bool CheckNodeExists(XDocument xDoc, string nodeName)
        {
            var nodeValue = xDoc.Descendants(nodeName).FirstOrDefault();
            return (nodeValue != null) ? true : false;
        }

        /// <summary>
        /// Check if given timestamp matches with the Authorization header
        /// </summary>
        /// <param name="context">Http Context object</param>
        /// <param name="timestamp">Timestamp details</param>
        /// <returns>Validation reponse data</returns>
        public ValidationData ValidateTimestamp(HttpContext context, DateTime? timestamp)
        {
            try
            {
                if (string.IsNullOrEmpty(timestamp.ToString()))
                {
                    return this.GetValidationResponseData(1006, false, Messages.Error.TimestampEmpty);
                }
                var claimIdentity = context.User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claimList = claimIdentity.Claims;
                var currentTimestamp = claimList
                                .Where(c => c.Type == ClaimTypes.Name)
                                .Select(c => c.Value)
                                .FirstOrDefault();
                if (!string.IsNullOrEmpty(currentTimestamp) && (Convert.ToDateTime(currentTimestamp) == timestamp))
                {
                    return this.GetValidationResponseData(2000, true, Messages.Success.TimestampMatch);
                }
                else
                {
                    return this.GetValidationResponseData(1007, false, Messages.Error.TimestampMisMatch);
                }
            }
            catch(Exception e)
            {
                return this.GetValidationResponseData(1007, false, Messages.Error.TimestampMisMatch);
            }
        }

        #region Validation Response Data
        /// <summary>
        /// Get the validation response data
        /// </summary>
        /// <param name="responseCode">Response Code</param>
        /// <param name="responseStatus">Response Status</param>
        /// <param name="message">Response message</param>
        /// <param name="data">dynamic data</param>
        /// <returns>Validation Response Data object</returns>
        public ValidationData GetValidationResponseData(int responseCode, bool responseStatus, string message, dynamic data = null)
        {
            ValidationData resp = new ValidationData();
            resp.ResponseCode = responseCode;
            resp.ResponseStatus = responseStatus;
            resp.Message = message;
            resp.Data = data;
            return resp;
        }
        #endregion
    }
}
