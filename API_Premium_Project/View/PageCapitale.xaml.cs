using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace API_Premium_Project.View
{
    public partial class PageCapitale : UserControl
    {
        private const string ApiBaseUrl = "https://restcountries.com/v2/";
        private string randomCountry;

        public PageCapitale()
        {
            InitializeComponent();
            LoadRandomEuropeanCountry();
        }

        private void LoadRandomEuropeanCountry()
        {
            randomCountry = GetRandomEuropeanCountry();
            txtCountry.Text = randomCountry;
            txtUserGuess.Text = string.Empty;
            TB_Captiale.Text = string.Empty;
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string userGuess = txtUserGuess.Text;

            if (string.IsNullOrEmpty(userGuess))
            {
                MessageBox.Show("Veuillez saisir votre réponse avant de cliquer sur Rechercher.");
                return;
            }

            string capital = await GetCapitalAsync(randomCountry);

            if (string.Equals(userGuess, capital, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bonne réponse !");
            }
            else
            {
                MessageBox.Show($"Mauvaise réponse. La capitale est {capital}.");
            }

            LoadRandomEuropeanCountry();
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private async Task<string> GetCapitalAsync(string countryName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"{ApiBaseUrl}name/{countryName}";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var countryInfo = JsonConvert.DeserializeObject<JArray>(json).FirstOrDefault();

                        if (countryInfo != null)
                        {
                            string capital = countryInfo["capital"]?.ToString();
                            return capital;
                        }
                        else
                        {
                            MessageBox.Show($"Aucune information trouvée pour {countryName}");
                            return string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Erreur lors de la récupération des données pour {countryName}");
                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                    return string.Empty;
                }
            }
        }

        private string GetRandomEuropeanCountry()
        {
            string apiUrl = "https://restcountries.com/v3.1/region/europe";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string json = response.Content.ReadAsStringAsync().Result;
                        var europeanCountries = JsonConvert.DeserializeObject<JArray>(json);

                        if (europeanCountries.Count == 0)
                        {
                            MessageBox.Show("Aucun pays européen trouvé.");
                            return string.Empty;
                        }

                        Random random = new Random();
                        int randomIndex = random.Next(0, europeanCountries.Count);
                        string randomCountry = europeanCountries[randomIndex]["name"]["common"]?.ToString();

                        return randomCountry;
                    }
                    else
                    {
                        MessageBox.Show("Erreur lors de la récupération de la liste des pays européens.");
                        return string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur : {ex.Message}");
                    return string.Empty;
                }
            }
        }


    }
}
