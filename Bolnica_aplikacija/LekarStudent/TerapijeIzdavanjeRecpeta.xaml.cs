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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for TerapijeIzdavanjeRecpeta.xaml
    /// </summary>
    public partial class TerapijeIzdavanjeRecpeta : UserControl
    {
        private static Grid gridLekovi;
        public static bool aktivan;
        public TerapijeIzdavanjeRecpeta()
        {
            InitializeComponent();
            lblRDatumR.Content = PacijentKontroler.getPacijent().datumRodjenja;
            lblRImePrezime.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblRJmbg.Content = PacijentKontroler.getPacijent().jmbg;

            txtDijagnoza.Text = PacijentKontroler.getBolestTerapija().nazivBolesti;
            txtKol.Text = PacijentKontroler.getBolestTerapija().kolicina;
            txtNacinUpotrebe.Text = TerapijaKontroler.nadjiTerapijuPoId(PacijentKontroler.getBolestTerapija().idTerapije).nacinUpotrebe;
            txtNazivLeka.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
            txtTrajanje.Text = TerapijaKontroler.nadjiTerapijuPoId(PacijentKontroler.getBolestTerapija().idTerapije).trajanje.ToString();
            txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSveSemTrenutnogNaTerapiji(PacijentKontroler.getBolestTerapija().idLeka);
            gridLekovi = gridDodavanjeLeka;
            aktivan = true;
            UvidUTerapije.aktivan = false;
        }

        public static Grid getGridLekovi()
        {
            return gridLekovi;
        }

        private void btnDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeLeka.Visibility = Visibility.Visible;
        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridLekovi.SelectedIndex != -1)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItem;
                PacijentKontroler.getBolestTerapija().idLeka = lek.id;
                PacijentKontroler.getBolestTerapija().idTermina = null;
                PacijentKontroler.getBolestTerapija().izvestaj = null;
                PacijentKontroler.getBolestTerapija().kolicina = lek.kolicina.ToString();
                PacijentKontroler.getBolestTerapija().nazivTerapije = lek.naziv;
                txtKol.Text = PacijentKontroler.getBolestTerapija().kolicina;
                txtNacinUpotrebe.Text = lek.getNacinUpotrebeString();
                txtNazivLeka.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
                txtTrajanje.Text = TerapijaKontroler.nadjiTerapijuPoId(PacijentKontroler.getBolestTerapija().idTerapije).trajanje.ToString();
                gridDodavanjeLeka.Visibility = Visibility.Hidden;
            }
        }

        private void btnPotvrdiRecept_Click(object sender, RoutedEventArgs e)
        {
            if(PacijentKontroler.getBolestTerapija().idTermina == null)
            {
                TerapijaKontroler.dodajTerapijuIzRecepta(DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text, 
                    PacijentKontroler.getBolestTerapija().idLeka, PacijentKontroler.getPacijent().id, null, PacijentKontroler.getBolestTerapija().idBolesti);
            }
            else
            {
                TerapijaKontroler.azurirajTerapiju(PacijentKontroler.getBolestTerapija().idTerapije, PacijentKontroler.getBolestTerapija().idLeka
                    , txtNacinUpotrebe.Text, Convert.ToInt32(txtTrajanje.Text), DateTime.Now);
            }

            Content = new UvidUTerapije();
        }

        private void btnPonistiOdaberLeka_Click(object sender, RoutedEventArgs e)
        {
            gridDodavanjeLeka.Visibility = Visibility.Hidden;
        }
    }
}
