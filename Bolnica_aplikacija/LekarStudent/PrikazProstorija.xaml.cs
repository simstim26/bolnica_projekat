using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for PrikazProstorija.xaml
    /// </summary>
    public partial class PrikazProstorija : UserControl
    {
        public static bool aktivan;
        public PrikazProstorija()
        {
            InitializeComponent();
            aktivan = true;
            ZakaziTermin.aktivan = false;
            LekarProzor.getGlavnaLabela().Content = "Promena prostorije";
            ucitajPodatke();
        }

        private void ucitajPodatke()
        {

            dataProstorije.ItemsSource = TerminKontroler.nadjiSlobodneProstorijeZaTermin(KorisnikKontroler.getLekar(), TerminKontroler.getTermin());
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            Content = new ZakaziTermin(ZakaziTermin.getTipAkcije()); 
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataProstorije.SelectedIndex != -1)
            {
                Prostorija prostorija = (Prostorija)dataProstorije.SelectedItem;
                TerminKontroler.promeniProstorijuTermina(TerminKontroler.getTermin().idTermina, prostorija.id);
                LekarProzor.getX().Content = new PacijentInfo();
                PacijentInfo.getPregledTab().SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
