using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace API_Premium_Project.View
{
    /// <summary>
    /// Logique d'interaction pour PageNasa.xaml
    /// </summary>
    public partial class PageNasa : UserControl
    {
        private const string ApiKey = "YaYdZmxafsLZuLjoCWLISvMFYxO1bLMehwGb4MwW";
        private string ApiUrl => $"https://api.nasa.gov/planetary/apod?api_key={ApiKey}";

        private int currentImageIndex = 0;
        public PageNasa()
        {
            InitializeComponent();
        }
        private async void OnGetSpaceImageClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BitmapImage image = await GetSpaceImageFromApi();
                spaceImage.Source = image;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<BitmapImage> GetSpaceImageFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var spaceImageData = Newtonsoft.Json.JsonConvert.DeserializeObject<SpaceImageData>(responseData);

                    return new BitmapImage(new Uri(spaceImageData.Url));
                }
                else
                {
                    throw new Exception($"Erreur de l'API : {response.StatusCode}");
                }
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
    }

    public class SpaceImageData
    {
        public string Url { get; set; }
    }


 
}

