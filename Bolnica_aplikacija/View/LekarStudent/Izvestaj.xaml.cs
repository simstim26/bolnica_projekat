using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public static Grid lekari { get; set; }
        private static Grid zakazivanje;
        private static Grid zakazivanjeOperacije;
        private static Grid odabirProstorije;
        private static Grid prikazInventaraProstorije;
        private static Grid pitanjeOZakazivanju;
        private static Grid pitanjeOUputu;
        private static Grid bolnickoLecenje;
        public static Grid pretragaLekara { get; set; }
        private static FrameworkElement fm = new FrameworkElement();
        public Izvestaj(String idPacijenta, String idTermina)
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
            PacijentInfo.aktivanPacijentInfo = false;
            LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            String[] id = { idPacijenta, idTermina };
            this.DataContext = id;
            fm.DataContext = this.DataContext;
            aktivan = true;

            Pacijent pacijent = PacijentKontroler.nadjiPacijenta(idPacijenta);
            lblJmbg.Content = pacijent.jmbg;
            lblImePrezime.Content = pacijent.ime + " " + pacijent.prezime;

            lblUpucuje.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + ", "
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);     
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + ", " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
            txtDatum.Text = DateTime.Now.ToString("dd.MM.yyyy.");

            lblRJmbg.Content = pacijent.jmbg;
            lblRImePrezime.Content = pacijent.ime + " " + pacijent.prezime;
            lblRDatumR.Content = pacijent.datumRodjenja.ToString("dd.MM.yyyy");
            lblRPol.Content = pacijent.pol;

            podesiStaticGridPolja();

            btnZakaziOperaciju.IsEnabled = true;
            btnDodajProstoriju.IsEnabled = false;
            btnUkloniProstoriju.IsEnabled = false;
            btnPotvrdi.IsEnabled = false;
            dataGridLekovi.ItemsSource = LekKontroler.ucitajSve();
            radioBtnPregled.IsChecked = true;

        }

        public static FrameworkElement getFM()
        {
            return fm;
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
            pitanjeOZakazivanju = this.gridPitanjeOZakazivanju;
            pitanjeOUputu = this.gridPitanjeOUputu;
            bolnickoLecenje = this.gridBolnickoLecenje;
            pretragaLekara = this.gridPretragaLekar;
        }

        public static void podesiKretanjeZaDugmeNazad()
        {
            if(pitanjeOZakazivanju.Visibility == Visibility.Visible)
            {
                pitanjeOZakazivanju.Visibility = Visibility.Hidden;
            }
            else if(pitanjeOUputu.Visibility == Visibility.Visible)
            {
                pitanjeOUputu.Visibility = Visibility.Hidden;
            }
            else if(bolnickoLecenje.Visibility == Visibility.Visible)
            {
                bolnickoLecenje.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja"; 
            }
            else if (uput.Visibility == Visibility.Visible)
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
                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);
                aktivan = false;
            }
        }

        private void btnUput_Click(object sender, RoutedEventArgs e)
        {
            //this.gridUput.Visibility = Visibility.Visible;
            this.gridPitanjeOUputu.Visibility = Visibility.Visible;
           // dataGridLekari.ItemsSource = LekarKontroler.ucitajLekareSaSpecijalizacijom();
           // LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";

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

            TerminKontroler.dodavanjeIzvestajaZaTermin(((String[])PacijentInfo.getFM().DataContext)[1], this.txtDijagnozaIzvestaj.Text,
               this.txtIzvestaj.Text);

            aktivan = false;
            MessageBox.Show("Uspešno ste završili termin!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);

        }

        private void btnPonistiIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);
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
            TerapijaKontroler.dodajTerapiju(new TerapijaDTO("", ((Lek)dataGridLekovi.SelectedItem).id, ((String[])fm.DataContext)[0], "",
                ((String[])fm.DataContext)[1], DateTime.Now, Convert.ToInt32(txtTrajanje.Text), txtNacinUpotrebe.Text));

            MessageBox.Show("Uspešno izdat recept!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.gridRecept.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
        }

        private void btnPotvdiOdabirLeka_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLekovi.SelectedIndex != -1)
            {
                if (PacijentKontroler.proveriAlergijuNaLekZaPacijenta(((String [])fm.DataContext)[0], ((Lek)dataGridLekovi.SelectedItem).id))
                {
                  MessageBox.Show("Pacijent je alergičan na dati lek.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    azurirajPrikazZaRecept();
                }
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lek!", "Upozorenje.", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void azurirajPrikazZaRecept()
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
            if (dataGridLekari.SelectedIndex != -1)
            {
                gridPitanjeOZakazivanju.Visibility = Visibility.Visible;
                btnZakaziOperaciju.IsEnabled = !((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije.Equals("Opšta praksa");
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati lekara!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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

            txtLekarUput.Text = LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).ime + " " +
                LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).prezime + ", "
                + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";

            TerminKontroler.azurirajLekaraZaUput(((String[])fm.DataContext)[1], ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara);

            if (LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).idSpecijalizacije.Equals("0"))
            {
                radioBtnOperacija.IsEnabled = false;
            }
            else
            {
                radioBtnOperacija.IsEnabled = true;
            }
        }

        private void btnZakaziOperaciju_Click(object sender, RoutedEventArgs e)
        {
            gridZakazivanjeOperacije.Visibility = Visibility.Visible;
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
            gridUput.Visibility = Visibility.Hidden;
            lblLekarOperacija.Content = ((LekarSpecijalizacija)dataGridLekari.SelectedItem).imeLekara + " " + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).prezimeLekara + ", " + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije;
            lblPacijent.Content = PacijentKontroler.nadjiPacijenta(((String [])fm.DataContext)[0]).ime + " " + PacijentKontroler.nadjiPacijenta(((String[])fm.DataContext)[0]).prezime;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";

            String[] satnica = txtVreme.Text.Split(':');
            /*DateTime datum, DateTime satnica, String idLekara, String idProstorije, TipTermina tip*/
            dataGridProstorije.ItemsSource = TerminKontroler.nadjiSlobodneProstorijeZaTermin(LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara), 
                new TerminDTO((DateTime)datum.SelectedDate, (DateTime)datum.SelectedDate + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0)),
                ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara, "", TipTermina.OPERACIJA));
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
            if (dataGridSlobodniTerminiLekara.SelectedIndex != -1)
            {
                PacijentKontroler.zakaziTerminPacijentu(((String[])fm.DataContext)[0], ((PacijentTermin)dataGridSlobodniTerminiLekara.SelectedItem).id);
                txtLekarUput.Text = LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).ime + " " +
                    LekarKontroler.nadjiLekaraPoId(((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara).prezime + ", "
                    + ((LekarSpecijalizacija)dataGridLekari.SelectedItem).nazivSpecijalizacije;

                TerminKontroler.azurirajUputTermina(((String[])fm.DataContext)[1], ((PacijentTermin)dataGridSlobodniTerminiLekara.SelectedItem).id);

                MessageBox.Show("Uspešno zakazan pregled!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

                gridZakazivanje.Visibility = Visibility.Hidden;
                gridPitanjeOZakazivanju.Visibility = Visibility.Hidden;
                gridOdabirLekaraUput.Visibility = Visibility.Hidden;
                gridUput.Visibility = Visibility.Visible;

                radioBtnPregled.IsChecked = true;
                radioBtnOperacija.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {

            String[] satnica = txtVreme.Text.Split(':');
            DateTime sat = (DateTime)datum.SelectedDate + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));

            TerminKontroler.azurirajUputTermina(((String[])fm.DataContext)[1], TerminKontroler.napraviTermin(new TerminDTO(TipTermina.OPERACIJA,
                (DateTime)datum.SelectedDate, sat, false, "", ((Prostorija)dataGridProstorije.SelectedItem).id, ((String[])fm.DataContext)[0], ((LekarSpecijalizacija)dataGridLekari.SelectedItem).idLekara,
                null, null, null, null, null, null, TipTermina.PREGLED, (bool)cBoxHitna.IsChecked)));

            MessageBox.Show("Uspešno zakazana operacija!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

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
            TerminKontroler.azurirajIzvestajUputa(((String[])fm.DataContext)[1], proveriOznaceniRadioButton(),txtUputIzvestaj.Text);
            MessageBox.Show("Uspešno izdat uput!","Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
        }

        private TipTermina proveriOznaceniRadioButton()
        {
            return (bool)radioBtnPregled.IsChecked ? TipTermina.PREGLED : TipTermina.OPERACIJA;
        }

        private void txtVreme_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
            if (rx.IsMatch(txtVreme.Text))
            {
                omoguciDugme();
                omoguciDugmeZaProstoriju();
                lblGreska.Visibility = Visibility.Hidden;
            }
            else
            {
                btnPotvrdi.IsEnabled = false;
                btnDodajProstoriju.IsEnabled = false;
                lblGreska.Visibility = Visibility.Visible;
            }
        }

        private void txtProstorija_TextChanged(object sender, TextChangedEventArgs e)
        {
            omoguciDugme();
            omoguciDugmeZaProstoriju();
        }


        private void omoguciDugme()
        {
            if (!String.IsNullOrWhiteSpace(txtVreme.Text) && (datum.SelectedDate != null && DateTime.Compare((DateTime)datum.SelectedDate, DateTime.Now.Date) >= 0) && !txtProstorija.Text.Equals("Prostorija..."))
            {
                btnPotvrdi.IsEnabled = true;
            }
            else
            {
                btnPotvrdi.IsEnabled = false;
            }
        }

        private void omoguciDugmeZaProstoriju()
        {
            if (!String.IsNullOrWhiteSpace(txtVreme.Text) && (datum.SelectedDate != null && DateTime.Compare((DateTime)datum.SelectedDate, DateTime.Now.Date) >= 0))
            {
                btnDodajProstoriju.IsEnabled = true;
            }
            else
            {
                btnDodajProstoriju.IsEnabled = false;
            }

            if (txtProstorija.Text.Equals("Prostorija..."))
            {
                btnUkloniProstoriju.IsEnabled = false;
            }
            else
            {
                btnUkloniProstoriju.IsEnabled = true;
            }
        }

        private void btnPonistiIzbor_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirLekaraUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa"; 
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            gridZakazivanjeOperacije.Visibility = Visibility.Hidden;
            gridOdabirLekaraUput.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir lekara";
        }

        private void btnIzdavanjeUputaPregledOperacija_Click(object sender, RoutedEventArgs e)
        {
            this.gridUput.Visibility = Visibility.Visible;
            this.gridPitanjeOUputu.Visibility = Visibility.Hidden;
            dataGridLekari.ItemsSource = LekarKontroler.ucitajLekareSaSpecijalizacijom();
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
        }

        private void btnIzdavanjeUputaBolnickoLecenje_Click(object sender, RoutedEventArgs e)
        {
            gridBolnickoLecenje.Visibility = Visibility.Visible;
            gridPitanjeOUputu.Visibility = Visibility.Hidden;
            dataGridBolnickeSobe.ItemsSource = ProstorijaKontroler.pronadjiSlobodneBolnickeSobe();
            LekarProzor.getGlavnaLabela().Content = "Izdavanje uputa";
        }

        private void btnPotvrdiBLecenje_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridBolnickeSobe.SelectedIndex != -1)
            {
                BolnickoLecenjeKontroler.napraviUputZaBolnickoLecenje(new BolnickoLecenjeDTO("", (DateTime)datumBLecenje.SelectedDate, Convert.ToInt32(txtTrajanjeBLecenje.Text),
                    false, ((String[])fm.DataContext)[0], ((Prostorija)dataGridBolnickeSobe.SelectedItem).id, ((String[])fm.DataContext)[1]));
                ProstorijaKontroler.azurirajBrojZauzetihKreveta(((Prostorija)dataGridBolnickeSobe.SelectedItem).id);
                MessageBox.Show("Uspešno izdat uput za bolničko lečenje!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                gridBolnickoLecenje.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati bolničku sobu!", "Upozorenje!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPonistiBLecenje_Click(object sender, RoutedEventArgs e)
        {
            gridBolnickoLecenje.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";

        }

        private void txtTrajanjeBLecenje_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            if (rx.IsMatch(txtTrajanjeBLecenje.Text))
            {
                lblGreskaBLecenje.Visibility = Visibility.Visible;
                btnPotvrdiBLecenje.IsEnabled = false;
            }
            else
            {
                lblGreskaBLecenje.Visibility = Visibility.Hidden;
                btnPotvrdiBLecenje.IsEnabled = true;
                potvrdaBLecenjeEnabled(true, (datumBLecenje.SelectedDate != null && DateTime.Compare((DateTime)datumBLecenje.SelectedDate, DateTime.Now.Date) >= 0), (dataGridBolnickeSobe.SelectedIndex != -1));

            }
        }

        private void datumBLecenje_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DateTime.Compare((DateTime)datumBLecenje.SelectedDate, DateTime.Now.Date) < 0)
            {
                lblGreskaDatumBLecenje.Visibility = Visibility.Visible;
                btnPotvrdiBLecenje.IsEnabled = false;
            }
            else
            {
                Regex rx = new Regex("[^0-9]+");
                lblGreskaDatumBLecenje.Visibility = Visibility.Hidden;
                potvrdaBLecenjeEnabled(!rx.IsMatch(txtTrajanjeBLecenje.Text), true, (dataGridBolnickeSobe.SelectedIndex != -1));

            }
        }

        private void dataGridBolnickeSobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaBLecenjeEnabled(!rx.IsMatch(txtTrajanjeBLecenje.Text), (datumBLecenje.SelectedDate != null && DateTime.Compare((DateTime)datumBLecenje.SelectedDate, DateTime.Now.Date) >= 0), (dataGridBolnickeSobe.SelectedIndex != -1));

        }

        private void potvrdaBLecenjeEnabled(bool trajanjeOK, bool datumOK, bool bSobaOk)
        {
            btnPotvrdiBLecenje.IsEnabled = trajanjeOK && datumOK && bSobaOk;

        }

        private void txtTrajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            if (rx.IsMatch(txtTrajanje.Text))
            {
                lblGreskaTrajanjeRecept.Visibility = Visibility.Visible;
                btnPotvrdiRecept.IsEnabled = false;
            }
            else
            {
                lblGreskaTrajanjeRecept.Visibility = Visibility.Hidden;
                potvrdaReceptaEnabled(true && (!String.IsNullOrWhiteSpace(txtTrajanje.Text)), !String.IsNullOrWhiteSpace(txtDijagnoza.Text), !String.IsNullOrWhiteSpace(txtNazivLeka.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
            }
        }

        private void txtDijagnoza_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaReceptaEnabled((!String.IsNullOrWhiteSpace(txtTrajanje.Text)) && !rx.IsMatch(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtDijagnoza.Text), !String.IsNullOrWhiteSpace(txtNazivLeka.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
        }

        private void txtNazivLeka_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaReceptaEnabled((!String.IsNullOrWhiteSpace(txtTrajanje.Text)) && !rx.IsMatch(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtDijagnoza.Text), !String.IsNullOrWhiteSpace(txtNazivLeka.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
        }

        private void txtNacinUpotrebe_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            potvrdaReceptaEnabled((!String.IsNullOrWhiteSpace(txtTrajanje.Text)) && !rx.IsMatch(txtTrajanje.Text), !String.IsNullOrWhiteSpace(txtDijagnoza.Text), !String.IsNullOrWhiteSpace(txtNazivLeka.Text), !String.IsNullOrWhiteSpace(txtNacinUpotrebe.Text));
        }

        private void potvrdaReceptaEnabled(bool trajanjeOK, bool dijagnozaOk, bool nazivLekaOK, bool nacinUpotrebeOK)
        {
            btnPotvrdiRecept.IsEnabled = trajanjeOK && dijagnozaOk && nazivLekaOK && nacinUpotrebeOK;
        }

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lblGreskaDatumOperacija.Visibility = DateTime.Compare((DateTime)datum.SelectedDate, DateTime.Now.Date) < 0 ? Visibility.Visible : Visibility.Hidden; 
            omoguciDugme();
            omoguciDugmeZaProstoriju();    
        }

        private void btnPonistiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Hidden;
            gridZakazivanjeOperacije.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
        }

        private void btnPonistavanjeZakazivanja_Click(object sender, RoutedEventArgs e)
        {
            gridZakazivanje.Visibility = Visibility.Hidden;
            gridOdabirLekaraUput.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir lekara";
        }

        private void btnPonistiOdaberLeka_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirLeka.Visibility = Visibility.Hidden;
            gridRecept.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Izdavanje recepta";
        }

        private void txtUputIzvestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUputIzvestaj.Text) || String.IsNullOrWhiteSpace(txtLekarUput.Text))
            {
                btnPotvrdiUput.IsEnabled = false;
            }
            else
            {
                btnPotvrdiUput.IsEnabled = true;
            }
        }

        private void txtLekarUput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUputIzvestaj.Text) || String.IsNullOrWhiteSpace(txtLekarUput.Text))
            {
                btnPotvrdiUput.IsEnabled = false;
            }
            else
            {
                btnPotvrdiUput.IsEnabled = true;
            }
        }

        private void btnPonistiUput_Click(object sender, RoutedEventArgs e)
        {
            gridUput.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Pisanje izveštaja";
        }

        private void txtIzvestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnPotvrdaIzvestaj.IsEnabled = (String.IsNullOrWhiteSpace(txtDijagnozaIzvestaj.Text) || String.IsNullOrWhiteSpace(txtIzvestaj.Text)) ? false : true;
        }

        private void txtDijagnozaIzvestaj_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnPotvrdaIzvestaj.IsEnabled = (String.IsNullOrWhiteSpace(txtDijagnozaIzvestaj.Text) || String.IsNullOrWhiteSpace(txtIzvestaj.Text)) ? false : true;
        }

        private void gridOdabirLekaraUput_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(gridOdabirLekaraUput.Visibility == Visibility.Hidden)
            {
                LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            }
            else
            {
                LekarProzor.getPretraga().Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtKriterijum.Text))
            {
                Regex rx = new Regex("[0-9]+");
                if (rx.IsMatch(txtKriterijum.Text))
                {
                    lblGreskaPretraga.Visibility = Visibility.Visible;
                }
                else
                {
                    lblGreskaPretraga.Visibility = Visibility.Hidden;
                    dataGridLekari.ItemsSource = LekarKontroler.pretraziLekare(txtKriterijum.Text);

                }
            }
            else
            {
                dataGridLekari.ItemsSource = LekarKontroler.ucitajLekareSaSpecijalizacijom();
            }
        }
    }
}
