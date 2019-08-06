using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SerkoGSTCalculatorApi.Business;
using SerkoGSTCalculatorApi.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SerkoGSTCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GSTCalculatorController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly ICore core;
        private readonly IValidator validator;

        public GSTCalculatorController(IConfiguration config, ICore core, IValidator validator)
        {
            this.config = config;
            this.core = core;
            this.validator = validator;
        }

        [HttpPost("GetCostCalculationResult")]
        public ResponseData GetCostCalculationResult([FromBody]RequestData request)
        {
            CalculateResponse cost = new CalculateResponse();
            string message = string.Empty;

            try
            {
                ValidationData validResponse = validator.ValidateTimestamp(this.HttpContext, request.TimeStampValue);
                if (!validResponse.ResponseStatus)
                {
                    return core.GetResponseData(validResponse.ResponseCode, validResponse.Message);
                }
                validResponse = core.ValidateGivenMessage(request.MailText);
                if (!validResponse.ResponseStatus)
                {
                    return core.GetResponseData(validResponse.ResponseCode, validResponse.Message);
                }

                double GSTPercent = Convert.ToDouble(config.GetSection("AppSettings")["GSTPercentage"].ToString());
                if (!string.IsNullOrEmpty(request.GSTPercentage.ToString()) && request.GSTPercentage > 0)
                {
                    GSTPercent = request.GSTPercentage;
                }

                double total = core.GetTotalCost(validResponse.Data);
                if (total <= 0)
                {
                    return core.GetResponseData(1002, Messages.Error.TotalIsZero);
                }
                cost.CostExcludingGST = core.GetCostExclGST(total, GSTPercent);
                cost.GSTCost = core.GetGSTAdded(total, cost.CostExcludingGST);

                message = Messages.Success.PrdtCostResult.Replace("{totalCost}", total.ToString()).Replace("{gstPercent}", GSTPercent.ToString())
                    .Replace("{gstadded}", cost.GSTCost.ToString()).Replace("{costExclGST}", cost.CostExcludingGST.ToString());

                string InputText = validResponse.Data.ToString().Replace("&lt;", "<").Replace("&gt;", ">");
                return core.GetResponseData(2000, message, InputText, cost);
            }
            catch(Exception e)
            {
                message = Messages.Error.UnExpectedError + ", Exception :"+ e.ToString();
                return core.GetResponseData(1001, message);
            }
        }
    }
}