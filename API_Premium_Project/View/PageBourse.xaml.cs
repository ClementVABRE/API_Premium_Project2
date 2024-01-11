using API_Premium_Project.Service;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static API_Premium_Project.Service.Api_Argent;
using NewsAPI;

namespace API_Premium_Project.View
{
    /// <summary>
    /// Logique d'interaction pour PageBourse.xaml
    /// </summary>
    public partial class PageBourse : UserControl
    {
        Api_Actu api_actu;
        Money_list money_list;
        Api_Argent api_argent;
        Pays_list pays_list;
        public string ApiUrl;
        public string ApiUrl2;
        public PageBourse()
        {
            InitializeComponent();
            api_actu = new Api_Actu();
            pays_list = new Pays_list();
            money_list = new Money_list();
            api_argent = new Api_Argent();
            ApiUrl2 = "https://newsapi.org/v2/top-headlines?country=fr&apiKey=e373d9e558a54e80b0435171b91c8025";
            ApiUrl = "https://v6.exchangerate-api.com/v6/15d970716364c58deb6e73c8/latest/USD";
            GetValueMoney(ApiUrl);
            GetActu(ApiUrl2);


            CB_Pays_Actu.ItemsSource = pays_list.LsActu;


            CB_Pays_1.ItemsSource = money_list.LsMoney;
            CB_Pays_1.SelectedItem = money_list.SelectedCurrency;
            CB_Pays_1.SelectionChanged += ComboBox_SelectionChanged;

            CB_Pays_2.ItemsSource = money_list.LsMoney;
            CB_Pays_2.SelectedItem = money_list.SelectedCurrency;
            CB_Pays_2.SelectionChanged += ComboBox_SelectionChanged;

        }

    
        private async void UpdateMoneyData(string SelectedMoney)
        {
            // On définit l'URL de l'API
            ApiUrl = $"https://v6.exchangerate-api.com/v6/15d970716364c58deb6e73c8/latest/{SelectedMoney}";
            // On récupère les données météo
            await GetValueMoney(ApiUrl); // Attendez la fin de la requête pour éviter des problèmes asynchrones
        }


        public async Task GetValueMoney(string apiUrl)
        {

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Root moneyData = JsonConvert.DeserializeObject<Root>(content);

                // Assuming 'money' is an instance of the Money class
                money_list.ExchangeRates.Clear();


                foreach (var property in moneyData.conversion_rates.GetType().GetProperties())
                {
                    money_list.ExchangeRates.Add(property.Name, Convert.ToDouble(property.GetValue(moneyData.conversion_rates)));
                }

                TB_Money.Text = money_list.ExchangeRates["EUR"].ToString();
            }
            else
            {
                MessageBox.Show("api pas bien recu");
            }
        }



        private void BTN_Convert_Click(object sender, RoutedEventArgs e)
        {
            ConvertCurrency();
        }

        private void ConvertCurrency()
        {
            if (CB_Pays_1.SelectedItem != null && CB_Pays_2.SelectedItem != null)
            {
                string fromCurrency = CB_Pays_1.SelectedItem.ToString();
                string toCurrency = CB_Pays_2.SelectedItem.ToString();

                if (double.TryParse(TB_Valeur.Text, out double amountToConvert))
                {
                    // Perform the conversion using the exchange rates
                    double exchangeRateFrom = money_list.ExchangeRates[fromCurrency];
                    double exchangeRateTo = money_list.ExchangeRates[toCurrency];

                    double convertedAmount = amountToConvert * (exchangeRateTo / exchangeRateFrom);

                    // Display the result

                    TB_Valeur2.Text = convertedAmount.ToString("N2") + " " + toCurrency;

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



        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                if (comboBox.SelectedItem != null)
                {
                    string selectedCurrency = comboBox.SelectedItem.ToString();

                    // Check if the text has been modified, if yes, trigger the update
                    if (!string.IsNullOrEmpty(TB_Valeur.Text))
                    {
                        UpdateMoneyData(selectedCurrency);
                    }
                }
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();

        }
        public async void GetActu(string apiUrl2) // On récupère les données météo
        {
            try
            {
                HttpClient client = new HttpClient(); // On crée un objet HttpClient

                HttpResponseMessage response = await client.GetAsync(apiUrl2); // On récupère les données de
                if (response.IsSuccessStatusCode) // Si la requête a réussi
                {
                    var content = await response.Content.ReadAsStringAsync(); // On récupère le contenu de la réponse
                    dynamic actudata = JsonConvert.DeserializeObject(content); // On désérialise le contenu de la répons

                    TB_Actu1_Gauche.Text = ":  " + actudata.root.statut.ToString();

                }


                else
                {
                    MessageBox.Show("Erreur lors de la récupération des données actualités."); // On affiche un message d'erreur
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

    }
    }











