using Bolnica_aplikacija.Kontroler;
using Model;
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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for Potvrda.xaml
    /// </summary>
    public partial class Potvrda : Window
    {
        String ime;
        public Potvrda(String ime)
        {
            InitializeComponent();
            this.ime = ime;
            textBlockBrisanje.Text = ime + "?";
            (ProstorijePogled.dobaviGridProstorija()).Opacity = 0.50;
            /*this.NavigationService.Navigate(this);*/

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ime == "prostoriju")
            {
                var prostorija = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
                ProstorijaKontroler.ObrisiProstoriju(prostorija.id);
                this.Close();
                (ProstorijePogled.dobaviGridProstorija()).Opacity = 1;
                ProstorijePogled.dobaviDataGridProstorija().ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
                
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            (ProstorijePogled.dobaviGridProstorija()).Opacity = 1;
            this.Close();
        }
    }
}
