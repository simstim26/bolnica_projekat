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
    /// Interaction logic for PogledajInventar.xaml
    /// </summary>
    public partial class PogledajInventar : UserControl
    {
        public static DataGrid dataGridProstorijeInventar;
        public PogledajInventar()
        {
            InitializeComponent();
            dataGridInventarProstorije.ItemsSource = ProstorijaKontroler.dobaviStavkeIzProstorije(
                (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem);
            dataGridProstorijeInventar = dataGridInventarProstorije;
        }

        private void btnOtkaziPrikaz_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }

        private void btnPremestiUDruguProstoriju_Click(object sender, RoutedEventArgs e)
        {
            izaberiStavku.Visibility = Visibility.Hidden;
            if (dataGridInventarProstorije.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PremestiUDruguProstoriju());
            }
            else
            {
                izaberiStavku.Visibility = Visibility.Visible;
            }
            
        }

        public static DataGrid dobaviDataGridProstorijaInventar()
        {
            return dataGridProstorijeInventar;
        }
    }
}
