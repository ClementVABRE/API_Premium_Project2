using API_Premium_Project.Service;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static API_Premium_Project.Service.Api_Argent;

namespace API_Premium_Project.View
{
   
    public partial class PageBourse : UserControl
    {
      
        Money_list money_list; // Création d'une nouvelle liste de monnaie
        Api_Argent api_argent; // Création d'une nouvelle API
         
        public string ApiUrl; // Création d'une nouvelle URL
      
        public PageBourse()
        {
            InitializeComponent();
           
            
            money_list = new Money_list(); // On instancie la liste de monnaie
            api_argent = new Api_Argent(); // On instancie l'API
            ApiUrl = "https://v6.exchangerate-api.com/v6/15d970716364c58deb6e73c8/latest/USD"; // On définit l'URL de l'API
            GetValueMoney(ApiUrl); // On récupère les données de l'API
          


         


            CB_Pays_1.ItemsSource = money_list.LsMoney; // On définit la source de la liste déroulante
            CB_Pays_1.SelectedItem = money_list.SelectedCurrency; // On définit la monnaie sélectionnée
            CB_Pays_1.SelectionChanged += ComboBox_SelectionChanged;    // On définit l'évènement de changement de sélection

            CB_Pays_2.ItemsSource = money_list.LsMoney; // On définit la source de la liste déroulante
            CB_Pays_2.SelectedItem = money_list.SelectedCurrency; // On définit la monnaie sélectionnée
            CB_Pays_2.SelectionChanged += ComboBox_SelectionChanged;   // On définit l'évènement de changement de sélection

        }


        private async void UpdateMoneyData(string SelectedMoney) // Fonction qui permet de mettre à jour les données de l'API
        {
            // On définit l'URL de l'API
            ApiUrl = $"https://v6.exchangerate-api.com/v6/15d970716364c58deb6e73c8/latest/{SelectedMoney}";
            
            await GetValueMoney(ApiUrl); // Attendez la fin de la requête pour éviter des problèmes asynchrones
        }


        public async Task GetValueMoney(string apiUrl) // Fonction qui permet de récupérer les données de l'API
        {
             
            HttpClient client = new HttpClient(); // On instancie un nouveau client HTTP
            HttpResponseMessage response = await client.GetAsync(apiUrl); // On récupère la réponse de l'API

            if (response.IsSuccessStatusCode) // Si la réponse est bonne
            {
                var content = await response.Content.ReadAsStringAsync(); // On récupère le contenu de la réponse
                Root moneyData = JsonConvert.DeserializeObject<Root>(content);  // On désérialise le contenu de la réponse

                // Assuming 'money' is an instance of the Money class
                money_list.ExchangeRates.Clear(); // On vide la liste de monnaie


                foreach (var property in moneyData.conversion_rates.GetType().GetProperties()) // On parcourt les propriétés de la liste de monnaie
                {
                    money_list.ExchangeRates.Add(property.Name, Convert.ToDouble(property.GetValue(moneyData.conversion_rates))); // On ajoute les propriétés à la liste de monnaie
                }

                TB_Money.Text = money_list.ExchangeRates["EUR"].ToString(); // On affiche le taux de change de l'euro
            }
            else
            {
                MessageBox.Show("api pas bien recu"); // Sinon on affiche un message d'erreur
            }
        }



        private void BTN_Convert_Click(object sender, RoutedEventArgs e) // Bouton Convertir
        {
            ConvertCurrency(); // On convertit la monnaie
        }

        private void ConvertCurrency() // Fonction qui permet de convertir la monnaie
        {
            if (CB_Pays_1.SelectedItem != null && CB_Pays_2.SelectedItem != null) // Si les deux monnaies sont sélectionnées
            {
                string fromCurrency = CB_Pays_1.SelectedItem.ToString();    // On définit la monnaie de départ
                string toCurrency = CB_Pays_2.SelectedItem.ToString();     // On définit la monnaie d'arrivée
                 
                if (double.TryParse(TB_Valeur.Text, out double amountToConvert)) // Si la valeur est un nombre
                {
                    
                    double exchangeRateFrom = money_list.ExchangeRates[fromCurrency]; // On définit le taux de change de la monnaie de départ
                    double exchangeRateTo = money_list.ExchangeRates[toCurrency]; // On définit le taux de change de la monnaie d'arrivée

                    double convertedAmount = amountToConvert * (exchangeRateTo / exchangeRateFrom); // On convertit la monnaie

                    TB_Valeur2.Text = convertedAmount.ToString("N2") + " " + toCurrency; // On affiche la monnaie convertie

                }
                else
                {
                    MessageBox.Show("Invalid input. Please enter a valid numeric value."); 
                }
            }
            else
            {
                MessageBox.Show("Please select currencies for conversion.");
            }
        }



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) // Fonction qui permet de changer la sélection de la monnaie
        {
            if (sender is ComboBox comboBox) // Si la source est une liste déroulante
            {
                if (comboBox.SelectedItem != null) // Si la monnaie est sélectionnée
                {
                    string selectedCurrency = comboBox.SelectedItem.ToString();     // On définit la monnaie sélectionnée

                    if (!string.IsNullOrEmpty(TB_Valeur.Text)) // Si la valeur n'est pas nulle
                    {
                        UpdateMoneyData(selectedCurrency); // On met à jour les données de l'API
                    }
                }
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e) // Bouton Retour
        {
            MainWindow mainWindow = new MainWindow(); // On instancie la fenêtre principale
            mainWindow.Show(); // On affiche la fenêtre principale
            Window.GetWindow(this).Close(); // On ferme la fenêtre actuelle

        }

        private void Btn_link_Click(object sender, RoutedEventArgs e) // Bouton Lien
        {
            
            string stockChartUrl = "https://www.boursorama.com/bourse/devises/taux-de-change-euro-dollar-EUR-USD/"; // On définit l'URL du site

            System.Diagnostics.Process.Start(stockChartUrl); // On ouvre le site
        }



    }
}











