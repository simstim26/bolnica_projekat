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
        private Termin termin;
        public PrikazProstorija(Termin termin)
        {
            InitializeComponent();
            this.termin = termin;
            ucitajPodatke();
        }

        private void ucitajPodatke()
        {

            dataProstorije.ItemsSource = KorisnikKontroler.getLekar().PrikazProstorija(termin);
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            Content = new ZakaziTermin(ZakaziTermin.getTipAkcije(), ZakaziTermin.getIzabraniTermin()); 
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataProstorije.SelectedIndex != -1 && ZakaziTermin.getTipAkcije() == 0)
            {
                Prostorija prostorija = (Prostorija)dataProstorije.SelectedItem;
                KorisnikKontroler.getLekar().AzurirajProstorijuTermina(termin.idTermina, prostorija.id);
                Content = new ZakaziTermin(ZakaziTermin.getTipAkcije(), ZakaziTermin.getIzabraniTermin());
            }
            else if(dataProstorije.SelectedIndex != -1 && ZakaziTermin.getTipAkcije() == 1)
            {
                Prostorija prostorija = (Prostorija)dataProstorije.SelectedItem;
                KorisnikKontroler.getLekar().AzurirajProstorijuTermina(ZakaziTermin.getIzabraniTermin().id, prostorija.id);
                LekarProzor.getX().Content = new LekarTabovi();
                LekarTabovi.getX().Content = new PacijentInfo();
                LekarTabovi.getTab().SelectedIndex = 1;
                PacijentInfo.getPregledTab().SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
