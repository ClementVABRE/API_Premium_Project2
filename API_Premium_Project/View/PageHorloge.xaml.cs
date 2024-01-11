using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static API_Premium_Project.Service.Api_Horloge;

namespace API_Premium_Project.View
{
    public partial class PageHorloge : UserControl
    {
        private List<string> timeZones;
        private DispatcherTimer timer;

        public PageHorloge()
        {
            InitializeComponent();
            InitializeComboBox();

            // Initialiser et démarrer le timer
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1); // Mettre à jour chaque seconde
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeComboBox()
        {
            // Récupérer la liste des fuseaux horaires disponibles
            timeZones = GetAvailableTimeZones();

            // Peupler la ComboBox avec la liste des fuseaux horaires
            CB_Pays_Horloge.ItemsSource = timeZones;
        }

        private List<string> GetAvailableTimeZones()
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://worldtimeapi.org/api/timezone";
                HttpResponseMessage response = client.GetAsync(apiUrl).Result;

                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<string>>(result);
                }
                else
                {
                    MessageBox.Show("Erreur lors de la récupération des fuseaux horaires.");
                    return new List<string>();
                }
            }
        }




        private async Task<string> GetTimeForTimeZone(string timeZone)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://worldtimeapi.org/api/timezone/{timeZone}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    WorldTimeApiResponse timeApiResponse = JsonConvert.DeserializeObject<WorldTimeApiResponse>(result);

                    // Formater la date pour afficher uniquement l'heure (HH:mm:ss)
                    DateTime currentTime = DateTime.Parse(timeApiResponse.datetime);
                    string formattedTime = currentTime.ToString("HH:mm:ss");

                    return formattedTime;
                }
                else
                {
                    return "Erreur lors de la récupération de l'heure actuelle.";
                }
            }
        }


        private async void CB_Pays_Horloge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTimeZone = CB_Pays_Horloge.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedTimeZone))
            {
                // Ne mettez pas à jour directement ici
            }
        }



        private async void Timer_Tick(object sender, EventArgs e)
        {
            string selectedTimeZone = CB_Pays_Horloge.SelectedItem as string;

            if (!string.IsNullOrEmpty(selectedTimeZone))
            {
                string currentTime = await GetTimeForTimeZone(selectedTimeZone);
                TB_Horloge.Text = currentTime;
            }
        }



        private void BTN_Retour_Click2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
    }
}
