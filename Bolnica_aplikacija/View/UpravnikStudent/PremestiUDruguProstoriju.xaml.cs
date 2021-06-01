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
    /// Interaction logic for PremestiUDruguProstoriju.xaml
    /// </summary>
    public partial class PremestiUDruguProstoriju : UserControl
    {
        public PremestiUDruguProstoriju()
        {
            //TODO: UBACITI STAVKE KAD SE UBACE
            InitializeComponent();
            var prostorija = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
            //var stavka = (Stavka)dataGridInventarProstorije.SelectedItem;

            //textBoxStavkaZaPremestanjeU.Text = stavka.naziv;
            textBoxKolicinaZaPremestanjeU.Clear();
            comboBoxProstorijeZaPremestanjeU.SelectedIndex = -1;
            datumPocetkaU.SelectedDate = null;
            datumKrajaU.SelectedDate = null;




            /*if (stavka.jeStaticka == true)
            {
                textStaticka1U.Visibility = Visibility.Visible;
                textStaticka2U.Visibility = Visibility.Visible;
                datumPocetkaU.Visibility = Visibility.Visible;
                datumKrajaU.Visibility = Visibility.Visible;
            }
            else
            {
                textStaticka1U.Visibility = Visibility.Hidden;
                textStaticka2U.Visibility = Visibility.Hidden;
                datumPocetkaU.Visibility = Visibility.Hidden;
                datumKrajaU.Visibility = Visibility.Hidden;
            }*/

            Dictionary<string, string> prostorije = new Dictionary<string, string>();
            var neobrisaneProstorije = ProstorijaKontroler.ucitajNeobrisane();
            foreach (Prostorija p in neobrisaneProstorije)
            {
                if (p.id != prostorija.id)
                {
                    prostorije.Add(p.id, p.broj + " " + p.sprat);
                }

            }
            comboBoxProstorijeZaPremestanjeU.ItemsSource = prostorije;
        }

        private void btnPremestiU_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnOtkaziPremestanjeU_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }
    }
}
