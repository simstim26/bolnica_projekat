using Bolnica_aplikacija.Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for IzmenaBolesti.xaml
    /// </summary>
    public partial class IzmenaBolesti : UserControl
    {
        public static bool aktivan;
        private static Grid gridRecept;
        private static Grid gridLekovi;
        public IzmenaBolesti()
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
            aktivan = true;
            this.txtNaziv.Text = PacijentKontroler.getBolestTerapija().nazivBolesti;
            this.txtTerapija.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
            this.txtIzvestaj.Text = PacijentKontroler.getBolestTerapija().izvestaj;
            this.lblRJmbg.Content = PacijentKontroler.getPacijent().jmbg;
            this.lblRImePrezime.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            this.lblRDatumR.Content = PacijentKontroler.getPacijent().datumRodjenja.ToString("dd.MM.yyyy.");
            gridRecept = gridIzmenaTerapije;
            gridLekovi = gridOdabirLeka;
            this.btnPotvrdi.IsEnabled = false;
        }

        public static Grid getGridRecept()
        {
            return gridRecept;
        }

        public static Grid getGridLekovi()
        {
            return gridLekovi;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.azuriranjeIzvestajaZaTermin(txtIzvestaj.Text, PacijentKontroler.getBolestTerapija().idTermina);

        }

        private void txtIzvestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnPotvrdi.IsEnabled = !String.IsNullOrWhiteSpace(txtIzvestaj.Text);
        }

        private void btnIzmeniTerapiju_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Visible;
            txtDijagnoza.Text = PacijentKontroler.getBolestTerapija().nazivBolesti;
            txtNazivLeka.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
            Terapija terapija = TerapijaKontroler.nadjiTerapijuPoId(PacijentKontroler.getBolestTerapija().idTerapije);
            txtTrajanje.Text = (terapija.trajanje).ToString();
            txtNacinUpotrebe.Text = terapija.nacinUpotrebe;
            txtKol.Text = (LekKontroler.nadjiLekPoId(terapija.idLeka).kolicina).ToString();
            txtDatum.Text = terapija.datumPocetka.ToString("dd.MM.yyyy.");
        }

        private void btnDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Hidden;
            this.gridOdabirLeka.Visibility = Visibility.Visible;
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSveSemTrenutnogNaTerapiji(
                TerapijaKontroler.nadjiTerapijuPoId(PacijentKontroler.getBolestTerapija().idTerapije).idLeka);
        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                Lek lek = (Lek)dataGridLekovi.SelectedItem;
                PacijentKontroler.getBolestTerapija().idLeka = lek.id;
                PacijentKontroler.getBolestTerapija().nazivTerapije = lek.naziv;
                this.txtTerapija.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
                this.gridIzmenaTerapije.Visibility = Visibility.Visible;
                this.gridOdabirLeka.Visibility = Visibility.Hidden;
                txtNazivLeka.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
                txtNacinUpotrebe.Text = lek.getNacinUpotrebeString();
                txtKol.Text = lek.kolicina.ToString();
                txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            }
        }

        private void btnPotvrdiRecept_Click(object sender, RoutedEventArgs e)
        {
            this.gridIzmenaTerapije.Visibility = Visibility.Hidden;
            TerapijaKontroler.azurirajTerapiju(PacijentKontroler.getBolestTerapija().idTerapije, PacijentKontroler.getBolestTerapija().idLeka,
                txtNacinUpotrebe.Text, Convert.ToInt32(txtTrajanje.Text), DateTime.Now);
        }
    }
}
