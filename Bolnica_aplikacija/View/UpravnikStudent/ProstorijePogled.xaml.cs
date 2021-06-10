using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.View.UpravnikStudent;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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
    /// Interaction logic for ProstorijePogled.xaml
    /// </summary>
    public partial class ProstorijePogled : UserControl
    {
        public static DataGrid dataGridProstorije;
        public static Grid gridProstorije;
        public ProstorijePogled()
        {
            InitializeComponent();
            dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
            dataGridProstorije = dataGridProstorija;
            gridProstorije = gridProstorija;
        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new DodajProstoriju());
        }

        private void btnIzmeniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            izaberite.Visibility = Visibility.Hidden;
            if (dataGridProstorija.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new IzmeniProstoriju());
            }
            else
            {
                izaberite.Visibility = Visibility.Visible;
            }
        }

        private void btnIzbrisiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            izaberite.Visibility = Visibility.Hidden;
            if (dataGridProstorija.SelectedIndex != -1)
            {
                Potvrda potvrda = new Potvrda("prostoriju");
                potvrda.ShowDialog();
            }
            else
            {
                izaberite.Visibility = Visibility.Visible;
            }
        }

        private void btnPogledajInventar_Click(object sender, RoutedEventArgs e)
        {
            izaberite.Visibility = Visibility.Hidden;
            if (dataGridProstorija.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PogledajInventar());
            }
            else
            {
                izaberite.Visibility = Visibility.Visible;
            }
        }

        private void btnZakaziRenoviranje_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ZakaziRenoviranje());
        }

        public static DataGrid dobaviDataGridProstorija()
        {
            return dataGridProstorije;
        }

        public static Grid dobaviGridProstorija()
        {
            return gridProstorije;
        }

        private void btnZakaziRenoviranje_Copy_Click(object sender, RoutedEventArgs e)
        {
            BiranjeDatumaZaIzvestaj biranje = new BiranjeDatumaZaIzvestaj();
            biranje.Show();
        }

        private void dataGridProstorija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
