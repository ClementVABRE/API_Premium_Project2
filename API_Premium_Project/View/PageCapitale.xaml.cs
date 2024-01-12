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
           
        }
        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string countryName = txtCountry.Text;
            if (!string.IsNullOrEmpty(countryName))
            {
                string capital = await GetCapitalAsync(countryName);
                TB_Captiale.Text = capital;
            }
            else
            {
                MessageBox.Show("Veuillez saisir le nom du pays.");
            }
        }

        private async Task<string> GetCapitalAsync(string countryName)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string apiUrl = $"https://restcountries.com/v2/name/{countryName}";
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

        
    }
}


