using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class Money_list
    {
        public List<string> LsMoney;
        public string SelectedCurrency { get; set; }
        public Dictionary<string, double> ExchangeRates { get; set; }

        public Money_list()
        {
            LsMoney = new List<string>
        {
            "USD", "EUR", "GBP", "JPY", "CHF", "CAD", "AUD", "CNY", "SEK", "NZD", "MXN", "SGD", "HKD"
        };
            SelectedCurrency = "USD";
            ExchangeRates = new Dictionary<string, double>();
        }
    }
}

