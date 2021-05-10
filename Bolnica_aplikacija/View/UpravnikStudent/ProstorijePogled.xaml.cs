using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.View.UpravnikStudent;
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
    /// Interaction logic for ProstorijePogled.xaml
    /// </summary>
    public partial class ProstorijePogled : UserControl
    {
        public static DataGrid dataGridProstorije;
        public ProstorijePogled()
        {
            InitializeComponent();
            dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
            dataGridProstorije = dataGridProstorija;
        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new DodajProstoriju());
        }

        private void btnIzmeniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorija.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new IzmeniProstoriju());
            }
        }

        private void btnIzbrisiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnPogledajInventar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnZakaziRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public static DataGrid dobaviDataGridProstorija()
        {
            return dataGridProstorije;
        }
    }
}
