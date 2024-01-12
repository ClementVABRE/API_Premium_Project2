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
 
    public partial class PageNasa : UserControl
    { 
        private const string ApiKey = "YaYdZmxafsLZuLjoCWLISvMFYxO1bLMehwGb4MwW"; // Clé API
        private string ApiUrl => $"https://api.nasa.gov/planetary/apod?api_key={ApiKey}"; // URL de l'API

        private int currentImageIndex = 0; // Index de l'image actuelle
        public PageNasa() // Constructeur
        {
            InitializeComponent();
        }
        private async void OnGetSpaceImageClick(object sender, RoutedEventArgs e) // Fonction qui permet de récupérer l'image de l'espace
        {
            try
            {
                BitmapImage image = await GetSpaceImageFromApi(); // On récupère l'image de l'espace
                spaceImage.Source = image; // On définit la source de l'image
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Erreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<BitmapImage> GetSpaceImageFromApi() // Fonction qui permet de récupérer l'image de l'espace
        {
            using (HttpClient client = new HttpClient()) // On instancie un nouveau client HTTP
            {
                HttpResponseMessage response = await client.GetAsync(ApiUrl); // On récupère la réponse de l'API

                if (response.IsSuccessStatusCode)   // Si la réponse est bonne
                { 
                    string responseData = await response.Content.ReadAsStringAsync(); // On récupère les données de la réponse
                    var spaceImageData = Newtonsoft.Json.JsonConvert.DeserializeObject<SpaceImageData>(responseData); // On désérialise les données

                    return new BitmapImage(new Uri(spaceImageData.Url)); // On retourne l'image
                }
                else
                {
                    throw new Exception($"Erreur de l'API : {response.StatusCode}"); // On retourne une erreur
                }
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e) // Bouton Retour
        {
            MainWindow mainWindow = new MainWindow(); // On instancie une nouvelle fenêtre
            mainWindow.Show(); // On affiche la fenêtre
            Window.GetWindow(this).Close(); // On ferme la fenêtre actuelle
        }
    }

    public class SpaceImageData     // Classe qui permet de récupérer les données de l'image de l'espace
    {
        public string Url { get; set; }     // URL de l'image
    }


 
}

