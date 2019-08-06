using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using SerkoGSTCalculatorApi.Model;

namespace SerkoGSTCalculatorApi.Business
{
    /// <summary>
    /// Helper for Core
    /// </summary>
    /// <seealso cref="SerkoGSTCalculatorApi.Business.ICore" />
    public class Core : ICore
    {
        /// <summary>
        /// The Validator
        /// </summary>
        private readonly IValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Core" /> class.
        /// </summary>
        /// <param name="validator">The Validator.</param>
        public Core(IValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Validates the message text.
        /// </summary>
        /// <param name="text">The message text.</param>
        /// <returns>Validation data object</returns>
        public ValidationData ValidateGivenMessage(string text)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(text))
            {
                return validator.GetValidationResponseData(1005, isValid, Messages.Error.EmptyMailText);
            }
            string errorMessage = string.Empty;
            Regex emailRegex = new Regex(@"(<)+\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*>", RegexOptions.IgnoreCase);
            MatchCollection emailMatches = emailRegex.Matches(text);
            foreach (Match emailMatch in emailMatches)
            {
                string replacestring = emailMatch.Value.Replace("<", "&lt;").Replace(">", "&gt;");
                text = text.Replace(emailMatch.Value, replacestring);
            }

            string textXml = @"<root>" + text + "</root>";
            try
            {
                XDocument xdoc = new XDocument();
                xdoc = XDocument.Parse(textXml);

                isValid = validator.CheckNodeExists(xdoc, "total");
                if (!isValid)
                {
                    errorMessage = Messages.Error.TagNotExists.Replace("{tag}", "total");
                    return validator.GetValidationResponseData(1004, isValid, errorMessage);
                }

                isValid = validator.CheckNodeExists(xdoc, "cost_centre");

                if (!isValid)
                {
                    xdoc.Descendants("expense").First().Add(new XElement("cost_centre", "UNKNOWN"));
                    return validator.GetValidationResponseData(2000, true, string.Empty, xdoc);
                }
                else
                {
                    var nodeValue = xdoc.Descendants("cost_centre").FirstOrDefault().Value;
                    if (string.IsNullOrEmpty(nodeValue.ToString()))
                    {
                        xdoc.Descendants("expense").FirstOrDefault().SetElementValue("cost_centre", "UNKONWN");
                    }

                    return validator.GetValidationResponseData(2000, isValid, errorMessage, xdoc);
                }
            }
            catch (Exception ex)
            {
                return validator.GetValidationResponseData(1003, isValid, Messages.Error.TextFormatError);
            }
        }

        /// <summary>
        /// Get the product total cost
        /// </summary>
        /// <param name="xDoc">The XDocument object with the given text</param>
        /// <returns>Product total cost</returns>
        public double GetTotalCost(XDocument xDoc)
        {
            string total = xDoc.Descendants("total").FirstOrDefault().Value;
            return string.IsNullOrEmpty(total) ? 0 : Convert.ToDouble(total);
        }

        /// <summary>
        /// Get the product cost excluding GST
        /// </summary>
        /// <param name="total">Product total cost</param>
        /// <param name="gstPercent">gst % rate</param>
        /// <returns>Product cost excluding GST</returns>
        public double GetCostExclGST(double total, double gstPercent)
        {
            return Math.Round(total / (1 + (gstPercent / 100)), 2);
        }

        /// <summary>
        /// Get the GST cost added to the total product cost
        /// </summary>
        /// <param name="total">Product total cost</param>
        /// <param name="prdtCostExclGST">Product cost excluding GST</param>
        /// <returns>GST cost added to the total product cost</returns>
        public double GetGSTAdded(double total, double prdtCostExclGST)
        {
            return Math.Round(total - prdtCostExclGST, 2);
        }

        /// <summary>
        /// Get the response data
        /// </summary>
        /// <param name="responseCode">Response Code</param>
        /// <param name="message">Response message</param>
        /// <param name="inputText">Given Input text</param>
        /// <param name="data">dynamic data</param>
        /// <returns>Response Data object</returns>
        public ResponseData GetResponseData(int responseCode, string message, string inputText = "", dynamic data = null)
        {
            ResponseData resp = new ResponseData();
            resp.ResponseCode = responseCode;
            resp.InputText = inputText;
            resp.Message = message;
            resp.Data = data;
            return resp;
        }
    }
}
