using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class Pays_list
    {
        public List<string> LsActu;
        public string SelectedCountry { get; set; }
        public Dictionary<string, double> ExchangeRates { get; set; }

        public Pays_list()
        {
            LsActu = new List<string>
            {
            "fr", "us", "gb", "jp", "ch", "ca", "au", "cn", "se", "nz", "mx", "sg", "hk"
        };
            SelectedCountry = "fr";
            ExchangeRates = new Dictionary<string, double>();
        }

    }

}
