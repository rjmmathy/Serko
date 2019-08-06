using System;
using System.Collections.Generic;
using System.Text;

namespace SerkoGSTCalculatorApi.Model
{
    public class CalculateResponse
    {
        public double GSTCost { get; set; }
        public double CostExcludingGST { get; set; }
    }
}
