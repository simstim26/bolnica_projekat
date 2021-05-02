using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
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
        private static Grid zakazivanjeOperacije;
        private static Grid odabirProstorije;
        private static Grid prikazInventaraProstorije;
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

            btnZakaziOperaciju.IsEnabled = true;
            btnDodajProstoriju.IsEnabled = false;
            btnUkloniProstoriju.IsEnabled = false;
            btnPotvrdi.IsEnabled = false;
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
            zakazivanjeOperacije = this.gridZakazivanjeOperacije;
            odabirProstorije = this.gridOdabirProstorija;
            prikazInventaraProstorije = this.gridPrikazInventara;
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
            else if(zakazivanjeOperacije.Visibility == Visibility.Visible)
            {
                if (odabirProstorije.Visibility == Visibility.Visible)
                {
                    LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
                    odabirProstorije.Visibility = Visibility.Hidden;
                }
                else if (prikazInventaraProstorije.Visibility == Visibility.Visible)
                {
                    LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";
                    prikazInventaraProstorije.Visibility = Visibility.Hidden;
                    odabirProstorije.Visibility = Visibility.Visible;
                }
                else
                {
                    LekarProzor.getGlavnaLabela().Content = "Odabir lekara";
                    zakazivanjeOperacije.Visibility = Visibility.Hidden;
                    lekari.Visibility = Visibility.Visible;
                }
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
            dataGridLekari.ItemsSource = LekarKontroler.ucitajLekareSaSpecijalizacijom();
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

        private void btnPotvrdaIzbora_Click(object sender, RoutedEventArgs e)
        {
            gridPitanjeOZakazivanju.Visibility = Visibility.Visible;
            if(((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije.Equals("Opšta praksa"))
            {
                btnZakaziOperaciju.IsEnabled = false;
            }
        }

        private void btnZakaziPregled_Click(object sender, RoutedEventArgs e)
        {
            gridZakazivanje.Visibility = Visibility.Visible;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje pregleda";
            dataGridSlobodniTerminiLekara.ItemsSource = TerminKontroler.ucitajPregledaZaIzabranogLekara(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara);
        }

        private void btnOdbijZakazivanje_Click(object sender, RoutedEventArgs e)
        {
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
            TerminKontroler.getTermin().idUputLekara = ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara;
            TerminKontroler.azurirajTermin(TerminKontroler.getTermin());
        }

        private void btnZakaziOperaciju_Click(object sender, RoutedEventArgs e)
        {
            gridZakazivanjeOperacije.Visibility = Visibility.Visible;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";
            Termin termin = new Termin();
            termin.datum = (DateTime)datum.SelectedDate;
            termin.idLekara = ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara;
            String[] satnica = txtVreme.Text.Split(':');
            termin.satnica = termin.datum + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));
            termin.tip = TipTermina.OPERACIJA;
            termin.idProstorije = "";
            dataGridProstorije.ItemsSource = TerminKontroler.nadjiSlobodneProstorijeZaTermin(LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara), termin);
        }

        private void btnUkloniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            txtProstorija.Text = "Prostorija...";
            dataGridProstorije.SelectedIndex = -1;
            btnDodajProstoriju.IsEnabled = true;
            btnUkloniProstoriju.IsEnabled = false;
            btnZakaziOperaciju.IsEnabled = false;
        }


        private void btnPrikazInventara_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedIndex != -1)
            {
                gridOdabirProstorija.Visibility = Visibility.Hidden;
                gridPrikazInventara.Visibility = Visibility.Visible;
                LekarProzor.getGlavnaLabela().Content = "Prikaz inventara";
                dataGridInventar.ItemsSource = ((Prostorija)dataGridProstorije.SelectedItem).Stavka;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPotvrdaZakazivanja_Click(object sender, RoutedEventArgs e)
        {
            PacijentKontroler.zakaziTerminPacijentu(((PacijentTermin)dataGridSlobodniTerminiLekara.SelectedItem).id);
            txtLekarUput.Text = LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).ime + " " +
                LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).prezime + ", " 
                + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije;
            TerminKontroler.veziTermin(((PacijentTermin)dataGridSlobodniTerminiLekara.SelectedItem).id);

            gridZakazivanje.Visibility = Visibility.Hidden;
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Visible;

            radioBtnPregled.IsChecked = true;
            radioBtnOperacija.IsEnabled = false;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            termin.idTermina = "";
            termin.idPacijenta = PacijentKontroler.getPacijent().id;
            termin.datum = (DateTime)datum.SelectedDate;
            termin.idLekara = ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara;
            termin.idProstorije = ((Prostorija)dataGridProstorije.SelectedItem).id;
            termin.jeZavrsen = false;
            String[] satnica = txtVreme.Text.Split(':');
            DateTime sat = DateTime.Now.Add(new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));
            termin.satnica = termin.datum + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));
            termin.tip = TipTermina.OPERACIJA;
            termin.jeHitan = (bool)cBoxHitna.IsChecked;
            String idNovog = TerminKontroler.napraviTermin(termin);
            TerminKontroler.veziTermin(idNovog);
            radioBtnOperacija.IsChecked = true;
            radioBtnPregled.IsEnabled = false;
            txtLekarUput.Text = LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).ime + " " +
                LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).prezime + ", "
                + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije;
            
            gridZakazivanjeOperacije.Visibility = Visibility.Hidden;
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Visible;
        }

        private void btnPotvrdiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProstorije.SelectedIndex != -1)
            {
                txtProstorija.Text = ProstorijaKontroler.nadjiProstorijuPoId(((Prostorija)dataGridProstorije.SelectedItem).id).sprat + " " +
                    ProstorijaKontroler.nadjiProstorijuPoId(((Prostorija)dataGridProstorije.SelectedItem).id).broj;
                gridOdabirProstorija.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPotvrdiUput_Click(object sender, RoutedEventArgs e)
        {
            TerminKontroler.getTermin().izvestajUputa = txtUputIzvestaj.Text;
            TerminKontroler.azurirajTermin(TerminKontroler.getTermin());
            gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
        }

        private void txtVreme_TextChanged(object sender, TextChangedEventArgs e)
        {
            omoguciDugme();
            dozvoliUnosProstorije();
        }

        private void txtProstorija_TextChanged(object sender, TextChangedEventArgs e)
        {
            omoguciDugme();
            dozvoliUnosProstorije();
            if (!txtProstorija.Text.Equals("Prostorija..."))
            {
                btnUkloniProstoriju.IsEnabled = true;
            }
            else
            {
                btnUkloniProstoriju.IsEnabled = false;
            }
        }

        private void dozvoliUnosProstorije()
        {
            if (String.IsNullOrWhiteSpace(txtVreme.Text) && datum.SelectedDate == null)
            {
                btnDodajProstoriju.IsEnabled = false;
            }
            else
            {
                btnDodajProstoriju.IsEnabled = true;
            }
        }

        private void omoguciDugme()
        {
            if (String.IsNullOrWhiteSpace(txtVreme.Text) && !txtProstorija.Text.Equals("Prostorija...") && datum.SelectedDate == null)
            {
                btnPotvrdi.IsEnabled = false;
            }
            else
            {
                btnPotvrdi.IsEnabled = true;
            }
        }
    }
}
