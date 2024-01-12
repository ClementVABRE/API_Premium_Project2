using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Premium_Project.Service
{
    public class Money_list
    {
        public List<string> LsMoney; // Création d'une liste de monnaie
        public string SelectedCurrency { get; set; } // Création d'une monnaie sélectionnée
        public Dictionary<string, double> ExchangeRates { get; set; } // Création d'un dictionnaire de taux de change

        public Money_list() // Constructeur
        {
            LsMoney = new List<string> // On définit la liste de monnaie
        {
            "USD", "EUR", "GBP", "JPY", "CHF", "CAD", "AUD", "CNY", "SEK", "NZD", "MXN", "SGD", "HKD" // On définit les monnaies
        };
            SelectedCurrency = "USD"; // On définit la monnaie sélectionnée
            ExchangeRates = new Dictionary<string, double>(); // On définit le dictionnaire de taux de change
        }
    }
}

