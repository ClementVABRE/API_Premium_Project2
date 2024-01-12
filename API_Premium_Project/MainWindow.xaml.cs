using API_Premium_Project.View;
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

namespace API_Premium_Project
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BTN_Bourse_Click(object sender, RoutedEventArgs e)
        {
            Ecran.Children.Clear();
            PageBourse pageBourse = new PageBourse();
            Ecran.Children.Add(pageBourse);
        }

        private void BTN_Nasa_Click(object sender, RoutedEventArgs e)
        {
            Ecran.Children.Clear();
            PageNasa pageNasa = new PageNasa();
            Ecran.Children.Add(pageNasa);
        }

        private void BTN_Capital_Click(object sender, RoutedEventArgs e)
        {
            Ecran.Children.Clear();
            PageCapitale pageCapitale = new PageCapitale();
            Ecran.Children.Add(pageCapitale);

        }

        private void BTN_Traducteur_Click(object sender, RoutedEventArgs e)
        {
            Ecran.Children.Clear();
            PageTraducteur pageTraducteur = new PageTraducteur();
            Ecran.Children.Add(pageTraducteur);
        }
    }
}
