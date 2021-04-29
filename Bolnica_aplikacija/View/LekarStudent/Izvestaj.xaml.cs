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
    /// Interaction logic for Izvestaj.xaml
    /// </summary>
    public partial class Izvestaj : UserControl
    {
        public static bool aktivan { get; set; }
        private static Grid uput;
        private static Grid recept;
        private static Grid odabirLeka;
        private static Grid azuriranje;
        private static Grid lekari;
        private static Grid zakazivanje;
        public Izvestaj()
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
            PacijentInfo.aktivanPacijentInfo = false;
            LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            aktivan = true;
            lblJmbg.Content = PacijentKontroler.getPacijent().jmbg;
            lblImePrezime.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblUpucuje.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + ", "
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);     
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + ", " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
            txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");
            lblRJmbg.Content = PacijentKontroler.getPacijent().jmbg;
            lblRImePrezime.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblRDatumR.Content = PacijentKontroler.getPacijent().datumRodjenja.ToString("dd.MM.yyyy");

            podesiStaticGridPolja();


            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();

        }

        private void podesiStaticGridPolja()
        {
            uput = this.gridUput;
            recept = this.gridRecept;
            azuriranje = this.gridAzuriranje;
            odabirLeka = this.gridOdabirLeka;
            lekari = this.gridOdabirLekaraUput;
            zakazivanje = this.gridZakazivanje;
        }

        public static void podesiKretanjeZaDugmeNazad()
        {
            if (uput.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
                Izvestaj.uput.Visibility = Visibility.Hidden;

            }
            else if (recept.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
                Izvestaj.recept.Visibility = Visibility.Hidden;
            }
            else if (azuriranje.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
                azuriranje.Visibility = Visibility.Hidden;
            }
            else if (odabirLeka.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
                odabirLeka.Visibility = Visibility.Hidden;
                recept.Visibility = Visibility.Visible;
            }
            else if (lekari.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
                lekari.Visibility = Visibility.Hidden;
                uput.Visibility = Visibility.Visible;
            }
            else if (zakazivanje.Visibility == Visibility.Visible)
            {
                LekarProzor.getGlavnaLabela().Content = "Odabir lekara";
                zakazivanje.Visibility = Visibility.Hidden;
                lekari.Visibility = Visibility.Visible;
            }
            else
            {
                LekarProzor.getX().Content = new PacijentInfo();
                aktivan = false;
            }
        }

        private void btnUput_Click(object sender, RoutedEventArgs e)
        {
            this.gridUput.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";

        }

        private void btnRecept_Click(object sender, RoutedEventArgs e)
        {
            this.gridRecept.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.gridAzuriranje.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Ažuriranje inventara";
        }

        private void btnDodavanjeLeka_Click(object sender, RoutedEventArgs e)
        {
            this.gridRecept.Visibility = Visibility.Hidden;
            this.gridOdabirLeka.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir leka";
        }

        private void btnPotvrdaIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            String nazivBolesti = this.txtDijagnozaIzvestaj.Text;
            String izvestajSaTermina = this.txtIzvestaj.Text;

            TerminKontroler.dodavanjeIzvestajaZaTermin(nazivBolesti, izvestajSaTermina);
            aktivan = false;
            LekarProzor.getX().Content = new PacijentInfo();

        }

        private void btnPonistiIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            LekarProzor.getX().Content = new PacijentInfo();
        }

        private void gridRecept_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(this.txtDijagnozaIzvestaj.Text) && String.IsNullOrWhiteSpace(this.txtDijagnoza.Text))
            {
                txtDijagnoza.Text = txtDijagnozaIzvestaj.Text;
            }
            else if(String.IsNullOrWhiteSpace(this.txtDijagnozaIzvestaj.Text) && !String.IsNullOrWhiteSpace(this.txtDijagnoza.Text))
            {
                txtDijagnozaIzvestaj.Text = txtDijagnoza.Text;
            }
            else if (!String.IsNullOrWhiteSpace(this.txtDijagnozaIzvestaj.Text) && !String.IsNullOrWhiteSpace(this.txtDijagnoza.Text))
            {
                txtDijagnoza.Text = txtDijagnozaIzvestaj.Text;
                
            }

        }

        private void btnPotvrdiRecept_Click(object sender, RoutedEventArgs e)
        {
            int trajanje = Convert.ToInt32(txtTrajanje.Text);
            String nacinUpotrebe = txtNacinUpotrebe.Text;

            TerapijaKontroler.dodajTerapiju(DateTime.Now, trajanje, nacinUpotrebe, PacijentKontroler.getPacijent().id, 
                ((Lek)dataGridLekovi.SelectedItem).id, TerminKontroler.getTermin().idTermina);
            this.gridRecept.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";

        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            this.gridOdabirLeka.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
            this.gridRecept.Visibility = Visibility.Visible;
            txtNazivLeka.Text = ((Lek)dataGridLekovi.SelectedItem).naziv;
            txtKol.Text = ((Lek)dataGridLekovi.SelectedItem).kolicina + "mg";
            txtNacinUpotrebe.Text = ((Lek)dataGridLekovi.SelectedItem).getNacinUpotrebeString();
        }

        private void btnIzaberiLekara_Click(object sender, RoutedEventArgs e)
        {
            this.gridOdabirLekaraUput.Visibility = Visibility.Visible;
            this.gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Odabir lekara";
        }

        private void btnZakazivanjePregledaUput_Click(object sender, RoutedEventArgs e)
        {
            this.gridZakazivanje.Visibility = Visibility.Visible;
            this.gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            this.gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje termina";
        }
    }
}
