using SerkoGSTCalculatorApi.Model;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerkoGSTCalculatorApi.Business
{
    /// <summary>
    /// Interface for core
    /// </summary>
    public interface ICore
    {
        #region ProductCostCalculation
        /// <summary>
        /// Get the total cost
        /// </summary>
        /// <param name="xDoc">The XDocument object with the given text</param>
        /// <returns>Product total cost</returns>
        double GetTotalCost(XDocument xDoc);

        /// <summary>
        /// Get the cost excluding GST
        /// </summary>
        /// <param name="total">Product total cost</param>
        /// <param name="gstPercent">gst % rate</param>
        /// <returns>Product cost excluding GST</returns>
        double GetCostExclGST(double total, double gstPercent);

        /// <summary>
        /// Get the GST cost added to the total product cost
        /// </summary>
        /// <param name="total">Product total cost</param>
        /// <param name="prdtCostExclGST">Product cost excluding GST</param>
        /// <returns>GST cost added to the total product cost</returns>
        double GetGSTAdded(double total, double prdtCostExclGST);
        #endregion

        #region Validate Input

        /// <summary>
        /// Validates the message text.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <returns>Validation data object</returns>
        ValidationData ValidateGivenMessage(string text);

        #endregion

        #region Response Data
        /// <summary>
        /// Get the response data
        /// </summary>
        /// <param name="responseCode">Response Code</param>
        /// <param name="message">Response message</param>
        /// <param name="inputText">Given Input text</param>
        /// <param name="data">dynamic data</param>
        /// <returns>Response Data object</returns>
        ResponseData GetResponseData(int responseCode, string message, string inputText = "", dynamic data = null);
        #endregion
    }
}
