using API_Premium_Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logique d'interaction pour PageTraducteur.xaml
    /// </summary>
    public partial class PageTraducteur : UserControl
    {
        private DeepLTranslator deepLTranslator;
        public PageTraducteur()
        {
            InitializeComponent();
            deepLTranslator = new DeepLTranslator();

            // Appelez une méthode pour remplir la ComboBox avec les langues disponibles
            PopulateLanguagesComboBox();
        }

        private void PopulateLanguagesComboBox()
        {
            // Liste de langues avec leur nom et leur code
            List<Language> languages = new List<Language>
            {
                new Language { Name = "Allemand", Code = "DE" },
                new Language { Name = "Anglais", Code = "EN" },
                new Language { Name = "Espagnol", Code = "ES" },
                new Language { Name = "Italien", Code = "IT" },
                new Language { Name = "Néerlandais", Code = "NL" },
                new Language { Name = "Polonais", Code = "PL" },
                new Language { Name = "Portugais", Code = "PT" },
                new Language { Name = "Russe", Code = "RU" },
                new Language { Name = "Japonais", Code = "JA" },
                new Language { Name = "Chinois", Code = "ZH" }

                // Ajoutez d'autres langues selon vos besoins
            };

            // Associez la liste de langues à la ComboBox
            TargetLanguageComboBox.ItemsSource = languages;

            // Définissez la propriété d'affichage pour afficher le nom de la langue
            TargetLanguageComboBox.DisplayMemberPath = "Name";

            // Définissez la propriété de valeur pour stocker le code de la langue
            TargetLanguageComboBox.SelectedValuePath = "Code";

            // Sélectionnez la première langue par défaut
            TargetLanguageComboBox.SelectedIndex = 0;
        }
        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Window.GetWindow(this).Close();
        }
        private async void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;

            // Récupérez le code de langue à partir de la ComboBox
            string targetLanguage = TargetLanguageComboBox.SelectedValue.ToString();

            string translation = await deepLTranslator.TranslateText(inputText, targetLanguage);

            if (translation != null)
            {
                TranslationTextBlock.Text = translation;
            }
            else
            {
                MessageBox.Show("Erreur lors de la traduction.");
            }
        }
    }

    // Classe représentant une langue avec son nom et son code
    public class Language
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
