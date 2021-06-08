using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PacijentStudent;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.View.SekretarStudent;
using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for SekretarProzor.xaml
    /// </summary>
    public partial class SekretarProzor : Window

    {
        private static Pacijent pacijent;
        private List<Pacijent> sviPacijenti = new List<Pacijent>();
        private bool flagIzmeni;
        private String idPacijenta;
        private int tipAkcijeTermini; // 0 - dodaj, 1 - izmeni, 2 - ukloni
       // private int tipAkcijeObavestenja; // 0 - dodaj, 1 - izmeni, 2 - ukloni
        private String imePrezimePacijenta;
        private PacijentTermin izabraniTermin;
       // private Obavestenje izabranoObavestenje;
        private List<Alergija> alergije;
        private bool jeHitanSlucaj;
        private String tipHitanSlucaj;
        private String tipSpecijalizacije;
        private bool imaGodisnjiOdmor;
        private int brojSlobodnihDana;
        private int brojZauzetihDana;

        public SekretarProzor()
        {
            InitializeComponent();
            CenterWindow();

            lblSekretar.Content = KorisnikKontroler.GetSekretar().ime + " " + KorisnikKontroler.GetSekretar().prezime;
            
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Hidden;
            this.HitanSlucajGrid.Visibility = Visibility.Hidden;
            this.LekariGrid.Visibility = Visibility.Hidden;
            frame.Visibility = Visibility.Hidden;

        }
        private void CenterWindow()
        {

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

        }

        private void odjavaButton_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        #region Rukovanje Pacijentima
        private void pacijenti_Click(object sender, RoutedEventArgs e)
        {

            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Visible;

            ucitajPacijenteTabela();

        }

        //Povratak
        private void ButtonPovratak_Click(object sender, RoutedEventArgs e)
        {
            this.PocetniEkranGrid.Visibility = Visibility.Visible;
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Hidden;

            //Termini
            slobodniTerminiLabel.Visibility = Visibility.Hidden;
            potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Hidden;
            TabelaTerminiPacijenta.ItemsSource = new List<Termin>();

            omoguciKoriscenjaPolja(false);
            ocistiPolja();
            buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;

        }

        private void dodajPacijentaButton_Click(object sender, RoutedEventArgs e)
        {
            podesiDodavanjePacijenta();

        }

        private void izmeniPacijentaButton_Click(object sender, RoutedEventArgs e)
        {

            if (TabelaPacijenti.SelectedIndex != -1)
            {
                //IZMENA
                flagIzmeni = true;

                pacijent = (Pacijent)TabelaPacijenti.SelectedItem;
                PacijentKontroler.nadjiPacijenta(pacijent.id);
                dataGridAlergije.ItemsSource = PacijentKontroler.procitajAlergije(pacijent.id);

                buttonOdustaniDodavanje.Visibility = Visibility.Visible;
                buttonPotvrdiDodavanje.Visibility = Visibility.Visible;

                ocistiPolja();
                omoguciKoriscenjaPolja(true);
                textKorisnickoIme.IsEnabled = false;
                lozinka.IsEnabled = false;

                this.idPacijenta = pacijent.id;
                textKorisnickoIme.Text = pacijent.korisnickoIme;
                lozinka.Password = pacijent.lozinka;
                textJMBG.Text = pacijent.jmbg;
                textIme.Text = pacijent.ime;
                textPrezime.Text = pacijent.prezime;
                textAdresa.Text = pacijent.adresa;
                textDatumRodj.Text = pacijent.datumRodjenja.Date.ToString("MM/dd/yyyy");
                textEmail.Text = pacijent.email;
                textTelefon.Text = pacijent.brojTelefona;

                alergije = (List<Alergija>)dataGridAlergije.ItemsSource;

                Console.WriteLine(dataGridAlergije.IsEnabled);

                if (pacijent.jeGost)
                {
                    checkboxGostujuciNalog.IsChecked = true;
                    textAdresa.Clear();
                    textAdresa.IsEnabled = false;
                    textEmail.Clear();
                    textEmail.IsEnabled = false;
                    checkboxGostujuciNalog.IsEnabled = true;

                }
                else
                {
                    checkboxGostujuciNalog.IsChecked = false;
                    checkboxGostujuciNalog.IsEnabled = false;
                }
            }

        }

        private void obrisiPacijentaButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaPacijenti.SelectedIndex != -1)
            {
                Pacijent izabraniPacijent = (Pacijent)TabelaPacijenti.SelectedItem;
                String idZaBrisanje = izabraniPacijent.id;
                PacijentKontroler.ObrisiPacijenta(idZaBrisanje);

            }

            ucitajPacijenteTabela();
        }

        private void infoPacijentiButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaPacijenti.SelectedIndex != -1)
            {
                omoguciKoriscenjaPolja(false);
                ocistiPolja();
                pacijent = (Pacijent)TabelaPacijenti.SelectedItem;

                this.PacijentInfoGrid.Visibility = Visibility.Visible;
                buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
                buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;

                textJMBG.Text = pacijent.jmbg;
                textIme.Text = pacijent.ime;
                textPrezime.Text = pacijent.prezime;
                textAdresa.Text = pacijent.adresa;
                textEmail.Text = pacijent.email;
                textTelefon.Text = pacijent.brojTelefona;

                String datum = pacijent.datumRodjenja.ToString("dd/MM/yyyy");
                textDatumRodj.Text = datum;

                textKorisnickoIme.Text = pacijent.korisnickoIme;
                lozinka.Password = pacijent.lozinka;

                PacijentKontroler.nadjiPacijenta(pacijent.id);
                alergije = PacijentKontroler.procitajAlergije(pacijent.id);
                dataGridAlergije.ItemsSource = alergije;

                if (pacijent.jeGost)
                {
                    checkboxGostujuciNalog.IsChecked = true;
                }
                else
                {
                    checkboxGostujuciNalog.IsChecked = false;
                    textAdresa.IsEnabled = false;
                    textEmail.IsEnabled = false;
                }
            }

        }


        private void buttonPotvrdiDodavanje_Click(object sender, RoutedEventArgs e)
        {
            Sekretar sekretar = KorisnikKontroler.GetSekretar();
            bool pacGost;
            String pacKorisnickoIme = textKorisnickoIme.Text;
            String pacLozinka = lozinka.Password.ToString();
            String pacIdBolnice = sekretar.idBolnice;
            String pacJmbg = textJMBG.Text;
            String pacIme = textIme.Text;
            String pacPrezime = textPrezime.Text;
            DateTime pacDatumRodjenja = Convert.ToDateTime(textDatumRodj.Text);
            String pacAdresa = textAdresa.Text;
            String pacEmail = textEmail.Text;
            String pacTelefon = textTelefon.Text;

            if ((bool)checkboxGostujuciNalog.IsChecked)
            {
                pacGost = true;
            }
            else
            {
                pacGost = false;
            }


            if (proveriIspravnostPolja(idPacijenta, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon))
            {

                if (!flagIzmeni)
                {

                    Console.WriteLine(pacGost);
                    if (!pacGost)
                    {
                        alergije = (List<Alergija>)dataGridAlergije.ItemsSource;
                        PacijentDTO pacijentDTO = new PacijentDTO(pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon);
                        PacijentKontroler.NapraviPacijenta(pacijentDTO, alergije);
                        ucitajPacijenteTabela();
                    }
                    else
                    {
                        alergije = (List<Alergija>)dataGridAlergije.ItemsSource;
                        PacijentDTO pacijentDTO = new PacijentDTO(pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, "", "", pacTelefon);
                        PacijentKontroler.NapraviPacijenta(pacijentDTO, alergije);
                        ucitajPacijenteTabela();
                    }

                }
                else
                {
                    alergije = (List<Alergija>)dataGridAlergije.ItemsSource;
                    foreach (Alergija alergija in alergije)
                    {
                        alergija.idPacijenta = idPacijenta;
                    }

                    PacijentDTO pacijentDTO = new PacijentDTO(pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon);
                    pacijentDTO.id = idPacijenta;
                    PacijentKontroler.AzurirajPacijenta(pacijentDTO, alergije);
                    ucitajPacijenteTabela();
                }

                omoguciKoriscenjaPolja(false);
                ocistiPolja();
                textAdresa.IsEnabled = false;
                textEmail.IsEnabled = false;

                buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
                buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;

                alergije.Clear();
                dataGridAlergije.Items.Refresh();

                if (jeHitanSlucaj)
                {
                    PacijentGrid.Visibility = Visibility.Hidden;
                    HitanSlucajGrid.Visibility = Visibility.Visible;
                    podesiDugmadPacijenti(true);
                    ucitajPacijenteTabelaHitanSlucaj();
                }

            }
            else
            {
                return;
            }

        }

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {

            odustaniOdRukovanjaPacijentom();

        }

        private void ucitajPacijenteTabela()
        {
            sviPacijenti = PacijentKontroler.ProcitajPacijente();
            TabelaPacijenti.ItemsSource = sviPacijenti;
            TabelaPacijenti.Items.Refresh();

        }



        private void omoguciKoriscenjaPolja(bool flag)
        {

            textJMBG.IsEnabled = flag;
            textIme.IsEnabled = flag;
            textPrezime.IsEnabled = flag;
            textAdresa.IsEnabled = flag;
            textDatumRodj.IsEnabled = flag;
            textEmail.IsEnabled = flag;
            textTelefon.IsEnabled = flag;
            textTelefon.IsEnabled = flag;
            textKorisnickoIme.IsEnabled = flag;
            lozinka.IsEnabled = flag;
            checkboxGostujuciNalog.IsEnabled = flag;
            dataGridAlergije.IsEnabled = flag;
            ___btnDodajRed_.IsEnabled = flag;
        }

        private void ocistiPolja()
        {

            textJMBG.Clear();
            textIme.Clear();
            textPrezime.Clear();
            textAdresa.Clear();
            textDatumRodj.Clear();
            textEmail.Clear();
            textTelefon.Clear();
            textKorisnickoIme.Clear();
            lozinka.Clear();
            checkboxGostujuciNalog.IsChecked = false;
        }



        private void proveraPopunjenosti()
        {
            if (!flagIzmeni)
            {
                if (!(bool)checkboxGostujuciNalog.IsChecked)
                {
                    this.buttonPotvrdiDodavanje.IsEnabled = !string.IsNullOrWhiteSpace(this.textIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textPrezime.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textJMBG.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textDatumRodj.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textAdresa.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textEmail.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textTelefon.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textKorisnickoIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.lozinka.Password.ToString());

                }
                else
                {
                    this.buttonPotvrdiDodavanje.IsEnabled = !string.IsNullOrWhiteSpace(this.textIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textPrezime.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textJMBG.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textDatumRodj.Text) &&
                                                             string.IsNullOrWhiteSpace(this.textAdresa.Text) &&
                                                             string.IsNullOrWhiteSpace(this.textEmail.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textTelefon.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textKorisnickoIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.lozinka.Password.ToString());
                }

            }
            else
            {
                if (!(bool)checkboxGostujuciNalog.IsChecked)
                {

                    this.buttonPotvrdiDodavanje.IsEnabled = !string.IsNullOrWhiteSpace(this.textIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textPrezime.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textJMBG.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textDatumRodj.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textAdresa.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textEmail.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textTelefon.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textKorisnickoIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.lozinka.Password.ToString());

                }
                else
                {

                    this.buttonPotvrdiDodavanje.IsEnabled = !string.IsNullOrWhiteSpace(this.textIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textPrezime.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textJMBG.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textDatumRodj.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textTelefon.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.textKorisnickoIme.Text) &&
                                                            !string.IsNullOrWhiteSpace(this.lozinka.Password.ToString());
                }
            }



        }
        private void textJMBG_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textPrezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textDatumRodj_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textAdresa_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void textKorisnickoIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void lozinka_PasswordChanged(object sender, RoutedEventArgs e)
        {
            proveraPopunjenosti();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            textAdresa.Clear();
            textAdresa.IsEnabled = false;
            textEmail.Clear();
            textEmail.IsEnabled = false;
            proveraPopunjenosti();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            textAdresa.IsEnabled = true;
            textEmail.IsEnabled = true;
            proveraPopunjenosti();
        }

        #endregion

        #region Validacija pacijent
        private bool proveriIspravnostPolja(String id, bool gost, String korisnickoIme, String loznika, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon)
        {

            //PROVERA JMBG
            if ((!Regex.IsMatch(jmbg, @"[0-9]{13}$")) || (jmbg.Length != 13))
            {
                textJMBG.Clear();
                return false;
            }


            foreach (Pacijent pac in sviPacijenti)
            {

                if (pac.id.Equals(id))
                {

                }
                else
                {
                    if (pac.jmbg.Equals(jmbg))
                    {
                        textJMBG.Clear();
                        return false;
                    }

                }

            }


            if (!flagIzmeni) // Ako kreiramo pacijenta i vec postoji Korisnicko ime
            {

                foreach (Pacijent pac in sviPacijenti)
                {
                    if (pac.korisnickoIme.Equals(korisnickoIme))
                    {
                        textKorisnickoIme.Clear();
                        return false;
                    }
                }
            }

            //Provera imena
            foreach (char c in ime)
            {
                if (!Char.IsLetter(c))
                {
                    textIme.Clear();
                    return false;
                }

            }

            //Provera prezimena
            foreach (char c in prezime)
            {
                if (!Char.IsLetter(c))
                {
                    textPrezime.Clear();
                    return false;
                }

            }

            //Provera datuma rodjenja
            if (!Regex.IsMatch(datumRodj.ToString(), @"[0-9]{2}[/][0-9]{2}[/][0-9]{4}"))
            {
                //textDatumRodj.Clear();
            }

            if (!gost)
            {
                //Provera E-mail adrese
                if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    textEmail.Clear();
                    return false;
                }

            }


            //Provera broja telefona
            foreach (char c in telefon)
            {
                if (Char.IsLetter(c))
                {
                    textTelefon.Clear();
                    return false;
                }

            }

            if (!flagIzmeni) // Ako kreiramo pacijenta i vec postoji korisnicko ime
            {

                foreach (Pacijent pac in sviPacijenti)
                {
                    if (pac.korisnickoIme.Equals(korisnickoIme))
                    {
                        textKorisnickoIme.Clear();
                        return false;
                    }
                }
            }


            return true;

        }

        #endregion
      
        #region Rukovanje terminima

        private void ucitajTerminePacijenta()
        {

            pacijent = (Pacijent)TabelaPacijentiTermini.SelectedItem;
            idPacijenta = pacijent.id;
            imePrezimePacijenta = pacijent.ime + " " + pacijent.prezime;
            PacijentKontroler.nadjiPacijenta(pacijent.id);
            TabelaTerminiPacijenta.ItemsSource = PacijentKontroler.prikazPacijentovihTermina(idPacijenta);

            lblUpozorenje.Visibility = Visibility.Hidden;

        }

        private void terminiButton_Click(object sender, RoutedEventArgs e)
        {
            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Visible;

            sviPacijenti = PacijentKontroler.ProcitajPacijente();
            TabelaPacijentiTermini.ItemsSource = sviPacijenti;
            TabelaPacijentiTermini.Items.Refresh();
        }

        private void prikaziTabeluSlobodnihTermina()
        {
            TabelaSlobodnihTermina.Visibility = Visibility.Visible;
            slobodniTerminiLabel.Visibility = Visibility.Visible;
            lblPacijent.Visibility = Visibility.Visible;
            lblPacijent.Content = imePrezimePacijenta;
        }
        private void zakaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            lblUpozorenje.Visibility = Visibility.Hidden;
            if (TabelaPacijentiTermini.SelectedIndex != -1)
            {
                List<PacijentTermin> slobodniTermini = PacijentKontroler.ucitajSlobodneTermine(0, true);
                TabelaSlobodnihTermina.ItemsSource = slobodniTermini;

                if (slobodniTermini.Count() != 0)
                {
                    tipAkcijeTermini = 0;
                    prikaziTabeluSlobodnihTermina();

                    if (TabelaSlobodnihTermina.SelectedIndex == -1)
                    {
                        potvrdiZakazivanjeTerminaButton.IsEnabled = false;
                    }

                    TabelaSlobodnihTermina.Items.Refresh();
                    potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Visible;

                }
                else
                {
                    sakrijTabeluSlobodnihTermina();
                    nemaTerminaObavestenje();
                }
            }
            else
            {
                lblUpozorenje.Visibility = Visibility.Visible;
                lblUpozorenje.Content = "* Molimo odaberite pacijenta kojem želite zakazati termin!";
            }


        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {

            if (TabelaTerminiPacijenta.SelectedIndex != -1)
            {
                tipAkcijeTermini = 2;
                izabraniTermin = (PacijentTermin)TabelaTerminiPacijenta.SelectedItem;
                if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                {
                    lblUpozorenje.Visibility = Visibility.Visible;
                    lblUpozorenje.Content = "* Nije moguće izvršiti otkazivanje termina 24h pred termin!";
                }
                else
                {

                    PotvrdaGrid.Visibility = Visibility.Visible;

                }

            }
            else
            {
                lblUpozorenje.Visibility = Visibility.Visible;
                lblUpozorenje.Content = "* Molimo odaberite termin koji želite da otkažete!";
            }

        }

        private void IzmeniTerminButton_Click(object sender, RoutedEventArgs e)
        {
            tipAkcijeTermini = 1;

            if (TabelaTerminiPacijenta.SelectedIndex != -1)
            {
                izabraniTermin = (PacijentTermin)TabelaTerminiPacijenta.SelectedItem;

                List<PacijentTermin> sviTermini = PacijentKontroler.ucitajSlobodneTermine(0, true);
                List<PacijentTermin> slobodniTermini = new List<PacijentTermin>();

                if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                {
                    sakrijTabeluSlobodnihTermina();
                    lblUpozorenje.Visibility = Visibility.Visible;
                    lblUpozorenje.Content = "* Nije moguće izmeniti termin za manje od 24h!";
                }
                else
                {

                    if (izabraniTermin.napomena.Equals("Pregled"))
                    {
                        foreach (PacijentTermin termin in sviTermini)
                        {
                            if (termin.napomena.Equals("Pregled"))
                            {
                                slobodniTermini.Add(termin);
                            }
                        }
                    }
                    else
                    {
                        foreach (PacijentTermin termin in sviTermini)
                        {
                            if (termin.napomena.Equals("Operacija"))
                            {
                                slobodniTermini.Add(termin);
                            }
                        }

                    }

                    if (slobodniTermini.Count != 0)
                    {
                        TabelaSlobodnihTermina.Visibility = Visibility.Visible;
                        slobodniTerminiLabel.Visibility = Visibility.Visible;
                        lblPacijent.Visibility = Visibility.Visible;
                        lblPacijent.Content = imePrezimePacijenta;

                        TabelaSlobodnihTermina.ItemsSource = slobodniTermini;
                        TabelaSlobodnihTermina.Items.Refresh();

                        potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Visible;
                        potvrdiZakazivanjeTerminaButton.IsEnabled = false;

                    }
                    else
                    {
                        nemaTerminaObavestenje();
                    }
                }



            }
            else
            {
                lblUpozorenje.Visibility = Visibility.Visible;
                lblUpozorenje.Content = "* Molimo odaberite termin koji želite da izmenite.";
            }

            if (TabelaSlobodnihTermina.SelectedIndex == -1)
            {
                potvrdiZakazivanjeTerminaButton.IsEnabled = false;
            }
            else
            {
                potvrdiZakazivanjeTerminaButton.IsEnabled = true;
            }


        }

        private void potvrdiZakazivanjeTerminaButton_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaGrid.Visibility = Visibility.Visible;
        }

        private void ucitajPacijenteUTabeluTermina()
        {
            ucitajTerminePacijenta();
            TabelaSlobodnihTermina.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(1, true);
            TabelaSlobodnihTermina.Items.Refresh();
        }

        private void sakrijGridZaPotvrdu()
        {
            PotvrdaGrid.Visibility = Visibility.Hidden;
            potvrdiZakazivanjeTerminaButton.IsEnabled = false;
        }


        private void btnPotvrdiIzbor_Click(object sender, RoutedEventArgs e)
        {
            if (tipAkcijeTermini == 0)
            {
                izabraniTermin = (PacijentTermin)TabelaSlobodnihTermina.SelectedItem;
                String idSelektovanog = izabraniTermin.id;
                PacijentKontroler.zakaziTerminPacijentu(idPacijenta, idSelektovanog);

                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje termina (Pacijent)", "Zakazan je teremin (Pacijent)", idPacijenta, "pacijent");
                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje termina (Lekar)", "Zakazan je teremin (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(izabraniTermin.id), "lekar");

                ucitajPacijenteUTabeluTermina();
                sakrijGridZaPotvrdu();
                sakrijTabeluSlobodnihTermina();
            }
            else if (tipAkcijeTermini == 1)
            {
                izabraniTermin = (PacijentTermin)TabelaTerminiPacijenta.SelectedItem;
                PacijentTermin noviTermin = (PacijentTermin)TabelaSlobodnihTermina.SelectedItem;
                //TerminKontroler.sacuvajTermin(izabraniTermin.id);
                PacijentKontroler.azurirajTerminPacijentu(izabraniTermin.id, noviTermin.id);

                NotifikacijaKontroler.napraviNotifikaciju("Izmena termina (Pacijent)", "Izmenjen je teremin (Pacijent)", idPacijenta, "pacijent");
                NotifikacijaKontroler.napraviNotifikaciju("Izmena termina (Lekar)", "Izmenjen je teremin (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(izabraniTermin.id), "lekar");

                ucitajPacijenteUTabeluTermina();
                sakrijGridZaPotvrdu();
                sakrijTabeluSlobodnihTermina();
            }
            else
            {
                PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);

                NotifikacijaKontroler.napraviNotifikaciju("Otkazivanje termina (Pacijent)", "Otkazan je teremin (Pacijent)", idPacijenta, "pacijent");
                NotifikacijaKontroler.napraviNotifikaciju("Otkazivanje termina (Lekar)", "Otkazan je teremin (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(izabraniTermin.id), "lekar");

                ucitajPacijenteUTabeluTermina();
                sakrijGridZaPotvrdu();
                sakrijTabeluSlobodnihTermina();

            }

        }

        private void btnOdustaniOdIzborra_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaGrid.Visibility = Visibility.Hidden;
            potvrdiZakazivanjeTerminaButton.IsEnabled = false;
        }

        private void ___btnDodajRed__Click(object sender, RoutedEventArgs e)
        {
            alergije.Add(new Alergija("", ""));
            dataGridAlergije.ItemsSource = alergije;
            dataGridAlergije.Items.Refresh();
        }

        private void TabelaPacijenti1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabelaPacijentiTermini.SelectedIndex != -1)
            {
                ucitajTerminePacijenta();
                sakrijTabeluSlobodnihTermina();
            }

        }
        private void TabelaTerminiPacijenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sakrijTabeluSlobodnihTermina();

        }

        private void TabelaSlobodnihTermina_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (TabelaSlobodnihTermina.SelectedIndex != -1)
            {
                potvrdiZakazivanjeTerminaButton.IsEnabled = true;
            }
            else
            {
                potvrdiZakazivanjeTerminaButton.IsEnabled = false;
            }

        }

        private void sakrijTabeluSlobodnihTermina()
        {

            TabelaSlobodnihTermina.Visibility = Visibility.Hidden;
            slobodniTerminiLabel.Visibility = Visibility.Hidden;
            lblPacijent.Visibility = Visibility.Hidden;
            potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Hidden;
            lblUpozorenje.Visibility = Visibility.Hidden;
        }

        private void nemaTerminaObavestenje()
        {
            lblUpozorenje.Visibility = Visibility.Visible;
            lblUpozorenje.Content = "* Nema slobodnih termina!";
        }


        private void PacijentGrid_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            Alergija alergija = (Alergija)dataGridAlergije.Items.GetItemAt(dataGridAlergije.SelectedIndex);

        }

        #endregion

        #region Rukovanje Obavestenjima (MVVM)

        private void btnObavestenja_Click(object sender, RoutedEventArgs e)
        {
            frame.Visibility = Visibility.Visible;
            frame.Content = new SekretarObavestenja(this);

            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Hidden;
            this.HitanSlucajGrid.Visibility = Visibility.Hidden;

        }
        #endregion

        #region Hitan Slucaj
        private void btnHitanSlucaj_Click(object sender, RoutedEventArgs e)
        {

            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Hidden;
            this.HitanSlucajGrid.Visibility = Visibility.Visible;
            this.dataGridSlobodniTerminiHItanSlucaj.Visibility = Visibility.Hidden;

            ucitajPacijenteTabelaHitanSlucaj();

        }

        private void ucitajPacijenteTabelaHitanSlucaj()
        {
            sviPacijenti = PacijentKontroler.ProcitajPacijente();
            dataGridPacijentiHitanSlucaj.ItemsSource = sviPacijenti;
            dataGridPacijentiHitanSlucaj.Items.Refresh();

        }

        private void dataGridPacijentiHitanSlucaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPacijentiHitanSlucaj.SelectedIndex != -1)
            {
                pacijent = (Pacijent)dataGridPacijentiHitanSlucaj.SelectedItem;

                btnPregled.IsEnabled = true;
                btnOperacija.IsEnabled = true;
            }
        }

        private void btnDodajGosta_Click(object sender, RoutedEventArgs e)
        {
            jeHitanSlucaj = true;
            HitanSlucajGrid.Visibility = Visibility.Hidden;
            PacijentGrid.Visibility = Visibility.Visible;
            ucitajPacijenteTabela();
            podesiDodavanjePacijenta();
            podesiDugmadPacijenti(false);
        }

        public void podesiDugmadPacijenti(bool jeUkljucen)
        {
            btnPovratakPacijent.IsEnabled = jeUkljucen;
            btnDodajPacijenta.IsEnabled = jeUkljucen;
            btnIzmeniPacijenta.IsEnabled = jeUkljucen;
            btnObrisiPacijenta.IsEnabled = jeUkljucen;
            btnInfoPacijenta.IsEnabled = jeUkljucen;
        }

        public void podesiDodavanjePacijenta()
        {
            flagIzmeni = false;

            omoguciKoriscenjaPolja(true);
            ocistiPolja();

            buttonOdustaniDodavanje.Visibility = Visibility.Visible;
            buttonPotvrdiDodavanje.Visibility = Visibility.Visible;
            buttonPotvrdiDodavanje.IsEnabled = false;

            alergije = new List<Alergija>();
            dataGridAlergije.ItemsSource = alergije;

            if (jeHitanSlucaj)
            {
                checkboxGostujuciNalog.IsChecked = true;
                checkboxGostujuciNalog.IsEnabled = false;
            }

        }

        public void odustaniOdRukovanjaPacijentom()
        {
            omoguciKoriscenjaPolja(false);
            ocistiPolja();

            textAdresa.IsEnabled = false;
            textEmail.IsEnabled = false;

            buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;

            if (jeHitanSlucaj)
            {
                PacijentGrid.Visibility = Visibility.Hidden;
                HitanSlucajGrid.Visibility = Visibility.Visible;
                podesiDugmadPacijenti(true);
                jeHitanSlucaj = false;
            }

        }

        private void btnPovratakHitanSlucaj_Click(object sender, RoutedEventArgs e)
        {
            HitanSlucajGrid.Visibility = Visibility.Hidden;
            PocetniEkranGrid.Visibility = Visibility.Visible;
            lblUpozorenjeTermin.Visibility = Visibility.Hidden;
            dataGridSlobodniTerminiHItanSlucaj.Visibility = Visibility.Hidden;
            lblNajbliziTermini.Visibility = Visibility.Hidden;

            btnZakaziHitanPregled.IsEnabled = false;
            btnPregled.IsChecked = false;
            btnOperacija.IsChecked = false;

        }

        private void lstBoxItemOpstaPraksa_Selected(object sender, RoutedEventArgs e)
        {
            tipSpecijalizacije = "0";
            ucitajSlobodneTermineHitanSlucaj(tipSpecijalizacije);
        }

        private void lstBoxItemKardiohirurg_Selected(object sender, RoutedEventArgs e)
        {

            tipSpecijalizacije = "1";
            ucitajSlobodneTermineHitanSlucaj(tipSpecijalizacije);

        }

        private void ucitajSlobodneTermineHitanSlucaj(String idSpecijalizacije)
        {
            List<PacijentTermin> termini = new List<PacijentTermin>();
            Console.WriteLine(tipHitanSlucaj);
            termini = TerminKontroler.ucitajTermineZaHitanSlucaj(tipHitanSlucaj, idSpecijalizacije);

            if (termini.Count == 0)
            {
                lblUpozorenjeTermin.Visibility = Visibility.Visible;
            }

            dataGridSlobodniTerminiHItanSlucaj.Visibility = Visibility.Visible;
            lblNajbliziTermini.Visibility = Visibility.Visible;
            dataGridSlobodniTerminiHItanSlucaj.ItemsSource = termini;
            dataGridSlobodniTerminiHItanSlucaj.Items.Refresh();
        }


        private void btnPregled_Checked(object sender, RoutedEventArgs e)
        {
            tipHitanSlucaj = "Pregled";
            lstBoxItemOpstaPraksa.IsEnabled = true;
            lblUpozorenjeTermin.Visibility = Visibility.Hidden;
        }

        private void btnOperacija_Checked(object sender, RoutedEventArgs e)
        {
            tipHitanSlucaj = "Operacija";
            lstBoxItemOpstaPraksa.IsEnabled = false;
            lblUpozorenjeTermin.Visibility = Visibility.Hidden;
        }

        private void dataGridPacijentiHitanSlucaj_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridPacijentiHitanSlucaj.SelectedIndex != -1 && dataGridSlobodniTerminiHItanSlucaj.SelectedIndex != -1)
            {
                btnZakaziHitanPregled.IsEnabled = true;
            }
        }


        private void dataGridSlobodniTerminiHItanSlucaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridSlobodniTerminiHItanSlucaj.SelectedIndex != -1 && dataGridPacijentiHitanSlucaj.SelectedIndex != -1)
            {
                btnZakaziHitanPregled.IsEnabled = true;
            }
        }

        private void btnZakaziHitanPregled_Click(object sender, RoutedEventArgs e)
        {
            pacijent = (Pacijent)dataGridPacijentiHitanSlucaj.SelectedItem;
            idPacijenta = pacijent.id;
            izabraniTermin = (PacijentTermin)dataGridSlobodniTerminiHItanSlucaj.SelectedItem;

            Termin termin = TerminKontroler.nadjiTerminPoId(izabraniTermin.id);

            if (termin.idPacijenta.Equals(""))
            {
                PacijentKontroler.nadjiPacijenta(pacijent.id);
                String idSelektovanogTermina = izabraniTermin.id;
                PacijentKontroler.zakaziTerminPacijentu(idPacijenta, idSelektovanogTermina);
                TerminKontroler.oznaciHitanSlucaj(idSelektovanogTermina);
                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje hitnog slucaja (Pacijent)", "Hitan Slucaj (Pacijent)", pacijent.id, "pacijent");
                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje hitnog slucaja (Lekar)", "Hitaj slucaj (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(izabraniTermin.id), "lekar");

                dataGridSlobodniTerminiHItanSlucaj.ItemsSource = null;
                dataGridSlobodniTerminiHItanSlucaj.Items.Refresh();

                btnZakaziHitanPregled.IsEnabled = false;
            }
            else
            {
                String idPacijentaZaPomeranje = termin.idPacijenta;
                PacijentKontroler.pomeriTerminNaPrviSlobodan(idPacijentaZaPomeranje, termin.idTermina, tipHitanSlucaj, tipSpecijalizacije);
                PacijentKontroler.nadjiPacijenta(pacijent.id);
                String idSelektovanogTermina = izabraniTermin.id;
                PacijentKontroler.zakaziTerminPacijentu(idPacijenta, idSelektovanogTermina);
                TerminKontroler.oznaciHitanSlucaj(idSelektovanogTermina);

                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje hitnog slucaja uz pomeranje (Pacijent)", "Hitan Slucaj (Pacijent)", pacijent.id, "pacijent");
                NotifikacijaKontroler.napraviNotifikaciju("Zakazivanje hitnog slucaja uz pomeranje (Lekar)", "Hitaj slucaj (Lekar)", TerminKontroler.nadjiIdLekaraZaTermin(izabraniTermin.id), "lekar");

                dataGridSlobodniTerminiHItanSlucaj.ItemsSource = null;
                dataGridSlobodniTerminiHItanSlucaj.Items.Refresh();

                btnZakaziHitanPregled.IsEnabled = false;

            }
        }
#endregion

        #region Rukovanje lekarima
        

        private void lekariButton_Click(object sender, RoutedEventArgs e)
        {
            PocetniEkranGrid.Visibility = Visibility.Hidden;
            PacijentGrid.Visibility = Visibility.Hidden;
            TerminiGrid.Visibility = Visibility.Hidden;
            HitanSlucajGrid.Visibility = Visibility.Hidden;
            LekariGrid.Visibility = Visibility.Visible;

            //Ucitavanje podataka
            sakrijDodatnaPoljaLekara();
            ucitajLekareUTabelu();

        }

        private void btnPovratakLekari_Click(object sender, RoutedEventArgs e)
        {
            PocetniEkranGrid.Visibility = Visibility.Visible;
            LekariGrid.Visibility = Visibility.Hidden;
            ocistiPoljaLekara();
        }

        // DODAJ LEKARA      

        private void btnDodajLekara_Click(object sender, RoutedEventArgs e)
        {
            if (proveriValidnostPoljaLekara(txtBoxImeLekara.Text, txtBoxPrezimeLekara.Text, txtBoxJMBG.Text, txtBoxEmail.Text, txtBoxTelefon.Text,
                                                txtBoxBrojZdravKnjizice.Text, txtBoxPocetakRadnogVremena.Text, txtBoxKrajRadnogVremena.Text,
                                                txtBoxBrojZauzetihDana.Text, txtBoxPocetakGodisnjegOdmora.Text))
            {
                LekarDTO noviLekar = ucitajUnetePodatkeULekara();
                LekarKontroler.napraviLekara(noviLekar);
                ocistiPoljaLekara();
                ucitajLekareUTabelu();
            }          
        }

        // IZMENI LEKARA
        private void btnIzmeniLekara_Click(object sender, RoutedEventArgs e)
        {

            if (proveriValidnostPoljaLekara(txtBoxImeLekara.Text, txtBoxPrezimeLekara.Text, txtBoxJMBG.Text, txtBoxEmail.Text, txtBoxTelefon.Text,
                                               txtBoxBrojZdravKnjizice.Text, txtBoxPocetakRadnogVremena.Text, txtBoxKrajRadnogVremena.Text,
                                               txtBoxBrojZauzetihDana.Text, txtBoxPocetakGodisnjegOdmora.Text))
            {

                LekarSpecijalizacija selektovanLekar = (LekarSpecijalizacija)dataGridLekari.SelectedItem;
                              
                if (!imaGodisnjiOdmor)
                {
                    checkBoxGodisnjiOdmor.IsEnabled = true;
                    LekarDTO izmeniLekarDTO = ucitajUnetePodatkeULekara();
                    izmeniLekarDTO.jeNaGodisnjemOdmoru = false;
                    izmeniLekarDTO.brojSlobodnihDana += izmeniLekarDTO.brojZauzetihDana;
                    izmeniLekarDTO.brojZauzetihDana = 0;
                    LekarKontroler.izmeniLekara(izmeniLekarDTO);

                }
                else
                {
                    LekarDTO izmeniLekarDTO = ucitajUnetePodatkeULekara();
                    LekarKontroler.izmeniLekara(izmeniLekarDTO);
                }

                ocistiPoljaLekara();
                ucitajLekareUTabelu();
            }
           
        }

        // IZBRISI LEKARA

        private void btnIzbrisiLekara_Click(object sender, RoutedEventArgs e)
        {
            LekarSpecijalizacija selektovanLekar = (LekarSpecijalizacija)dataGridLekari.SelectedItem;
            String idLekara = selektovanLekar.idLekara;
            LekarKontroler.obrisiLekara(idLekara);
            ocistiPoljaLekara();
            ucitajLekareUTabelu();

        }

        
        //POMOCNE FUNKCIJE
        private LekarDTO ucitajUnetePodatkeULekara()
        {

            Sekretar sekretar = KorisnikKontroler.GetSekretar();
            String idBolnice = sekretar.idBolnice;
            String ime = txtBoxImeLekara.Text;
            String prezime = txtBoxPrezimeLekara.Text;
            String jmbg = txtBoxJMBG.Text;
            DateTime lekarDatumRodjenja = Convert.ToDateTime(txtBoxDatumRodjenja.Text);
            String mestoRodjenja = txtBoxMestoRodjenja.Text;
            String drzavaRodjenja = txtBoxDrzavaRodjenja.Text;
            String pol = selektovanPol();
            String adresa = txtBoxAdresaS.Text;
            String email = txtBoxEmail.Text;
            String telefon = txtBoxTelefon.Text;
            String korisnickoIme = txtBoxKorisnickoIme.Text;
            String lozinka = passwordBoxLozinkaLekar.Password;
            String brojZdravstveneKnjizice = txtBoxBrojZdravKnjizice.Text;
            String bracnoStatus = selektovanBracniStatus();
            DateTime pocetakRadnogVremena = DateTime.ParseExact(txtBoxPocetakRadnogVremena.Text, "HH:mm", null);
            DateTime krajRadnogVremena = DateTime.ParseExact(txtBoxKrajRadnogVremena.Text, "HH:mm", null);
            String idSpecijalizacije = specijalizacija.SelectedIndex.ToString();
            DateTime pocetakGodisnjegOdmora = new DateTime();


            if (imaGodisnjiOdmor)
            {
                brojZauzetihDana = Int32.Parse(txtBoxBrojZauzetihDana.Text);
                pocetakGodisnjegOdmora = Convert.ToDateTime(txtBoxPocetakGodisnjegOdmora.Text);
            }


            LekarDTO lekarDTO = new LekarDTO();

            if (!flagIzmeni)
            {
                lekarDTO = new LekarDTO(idBolnice, ime, prezime, jmbg, lekarDatumRodjenja, mestoRodjenja, drzavaRodjenja,
                                                 pol, adresa, email, telefon, korisnickoIme, lozinka, brojZdravstveneKnjizice,
                                                 "lekar", bracnoStatus, pocetakRadnogVremena, krajRadnogVremena, idSpecijalizacije,
                                                 0.0, false, new List<Notifikacija>(), 30, 0, new DateTime(), false);
            }
            else
            {
                LekarSpecijalizacija selektovanLekar = (LekarSpecijalizacija)dataGridLekari.SelectedItem;
                String idLekara = selektovanLekar.idLekara;
                Lekar lekar = LekarKontroler.nadjiLekaraPoId(idLekara);

                lekarDTO = new LekarDTO(idBolnice, ime, prezime, jmbg, lekarDatumRodjenja, mestoRodjenja, drzavaRodjenja,
                                        pol, adresa, email, telefon, korisnickoIme, lozinka, brojZdravstveneKnjizice,
                                        "lekar", bracnoStatus, pocetakRadnogVremena, krajRadnogVremena, idSpecijalizacije,
                                         lekar.prosecnaOcena, lekar.jeNaGodisnjemOdmoru, lekar.notifikacije, lekar.brojSlobodnihDana,
                                         lekar.brojZauzetihDana, lekar.pocetakGodisnjegOdmora, lekar.jeLogickiObrisan);
                lekarDTO.id = lekar.id;

                if (imaGodisnjiOdmor)
                {
                    lekarDTO.pocetakGodisnjegOdmora = pocetakGodisnjegOdmora;
                    lekarDTO.jeNaGodisnjemOdmoru = true;
                    lekarDTO.brojZauzetihDana = brojZauzetihDana;
                    lekarDTO.brojSlobodnihDana -= brojZauzetihDana;
                }

            }

            return lekarDTO;
        }

        private String selektovanPol()
        {
            String pol = "";
            if ((bool)radioBtnMusko.IsChecked)
            {
                pol = "Musko";
            }
            else if ((bool)radioBtnZensko.IsChecked)
            {
                pol = "Zensko";
            }
            else
            {
                pol = "Ostalo";
            }

            return pol;
        }

        private String selektovanBracniStatus()
        {
            String bracniStatus = "";
            if ((bool)radioBtnUBraku.IsChecked)
            {
                bracniStatus = "U braku";
            }
            else if ((bool)radioBtnNijeUBraku.IsChecked)
            {
                bracniStatus = "Nije u braku";
            }
            else
            {
                bracniStatus = "Razveden/a";
            }

            return bracniStatus;
        }
        private void ucitajLekareUTabelu()
        {
            List<LekarSpecijalizacija> lekari = LekarKontroler.ucitajLekareSaSpecijalizacijom();
            dataGridLekari.ItemsSource = lekari;
            dataGridLekari.Items.Refresh();
        }

        private void ocistiPoljaLekara()
        {

            txtBoxImeLekara.Clear();
            txtBoxPrezimeLekara.Clear();
            txtBoxJMBG.Clear();
            txtBoxDatumRodjenja.Clear();
            txtBoxMestoRodjenja.Clear();
            txtBoxDrzavaRodjenja.Clear();
            //buttoni
            txtBoxAdresaS.Clear();
            txtBoxEmail.Clear();
            txtBoxTelefon.Clear();
            txtBoxKorisnickoIme.Clear();
            passwordBoxLozinkaLekar.Clear();
            txtBoxBrojZdravKnjizice.Clear();
            //status
            specijalizacija.SelectedIndex = -1;
            txtBoxPocetakRadnogVremena.Clear();
            txtBoxKrajRadnogVremena.Clear();

            checkBoxGodisnjiOdmor.IsChecked = false;
            btnOdustaniLekar.IsEnabled = false;

        }

        // LISTENERI ZA PROVERE POPUNJENOSTI

        private void proveriPopunjenostPoljaLekara()
        {
            if (!flagIzmeni)
            {
                this.btnDodajLekara.IsEnabled = jesuPopunjenaObaveznaPolja();
                this.btnOdustaniLekar.IsEnabled = true;
            }
            else
            {
                this.btnIzmeniLekara.IsEnabled = jesuPopunjenaObaveznaPolja();

                if (imaGodisnjiOdmor)
                {
                    this.btnIzmeniLekara.IsEnabled = !string.IsNullOrWhiteSpace(this.txtBoxBrojZauzetihDana.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxPocetakGodisnjegOdmora.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxImeLekara.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxPrezimeLekara.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxJMBG.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxDatumRodjenja.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxEmail.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxBrojZdravKnjizice.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxKorisnickoIme.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.passwordBoxLozinkaLekar.Password) &&
                                                        !(this.specijalizacija.IsSelectionBoxHighlighted) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxPocetakRadnogVremena.Text) &&
                                                        !string.IsNullOrWhiteSpace(this.txtBoxKrajRadnogVremena.Text);
                }

                this.btnOdustaniLekar.IsEnabled = true;
            }
        }


        private bool jesuPopunjenaObaveznaPolja()
        {
            return !string.IsNullOrWhiteSpace(this.txtBoxImeLekara.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxPrezimeLekara.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxJMBG.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxDatumRodjenja.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxEmail.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxBrojZdravKnjizice.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxKorisnickoIme.Text) &&
                                            !string.IsNullOrWhiteSpace(this.passwordBoxLozinkaLekar.Password) &&
                                            !(this.specijalizacija.IsSelectionBoxHighlighted) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxPocetakRadnogVremena.Text) &&
                                            !string.IsNullOrWhiteSpace(this.txtBoxKrajRadnogVremena.Text);
        }
        private void txtBoxImeLekara_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxPrezimeLekara_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxJMBG_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxDatumRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxMestoRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxDrzavaRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxAdresaS_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxKorisnickoIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxBrojZdravKnjizice_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxZanimanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxPocetakRadnogVremena_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxKrajRadnogVremena_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }


        private void txtBoxPocetakGodOdmora_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void txtBoxBrojSlobodnihDana_TextChanged(object sender, TextChangedEventArgs e)
        {
            proveriPopunjenostPoljaLekara();
        }

        private void checkBoxGodisnjiOdmor_Checked(object sender, RoutedEventArgs e)
        {
            txtBoxBrojZauzetihDana.IsEnabled = true;
            txtBoxPocetakGodisnjegOdmora.IsEnabled = true;
            imaGodisnjiOdmor = true;

            this.btnIzmeniLekara.IsEnabled = false;
        }
        private void checkBoxGodisnjiOdmor_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBoxBrojZauzetihDana.IsEnabled = false;
            txtBoxPocetakGodisnjegOdmora.IsEnabled = false;
            imaGodisnjiOdmor = false;

            //brojSlobodnihDanaZaVracanje += Int32.Parse(txtBoxBrojSlobodnihDana.Text);

            txtBoxBrojZauzetihDana.Text = "0";
            txtBoxPocetakGodisnjegOdmora.Clear();
        }

        private void dataGridLekari_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridLekari.SelectedIndex != -1)
            {
                flagIzmeni = true;
                btnDodajLekara.IsEnabled = false;
                btnIzbrisiLekara.IsEnabled = true;
                btnIzmeniLekara.IsEnabled = true;
                btnOdustaniLekar.IsEnabled = true;

                prikaziDodatnaPoljaLekara();
                ucitajSvaPoljaLekara();
            }
            else
            {
                flagIzmeni = false;
                btnDodajLekara.IsEnabled = false;
                btnIzbrisiLekara.IsEnabled = false;
                btnIzmeniLekara.IsEnabled = false;
                btnOdustaniLekar.IsEnabled = false;
            }
        }

        // PRIKAZI, UCITAVANJA I SAKRIVANJA
        private void prikaziDodatnaPoljaLekara()
        {
            gridDodatnaPoljaLekar.Visibility = Visibility.Visible;

        }

        private void sakrijDodatnaPoljaLekara()
        {
            gridDodatnaPoljaLekar.Visibility = Visibility.Hidden;
            checkBoxGodisnjiOdmor.IsChecked = false;
            txtBoxPocetakGodisnjegOdmora.Clear();

        }

        private void ucitajSvaPoljaLekara()
        {
            LekarSpecijalizacija lekarSpecijalizacija = (LekarSpecijalizacija)dataGridLekari.SelectedItem;
            Lekar lekar = LekarKontroler.nadjiLekaraPoId(lekarSpecijalizacija.idLekara);

            txtBoxImeLekara.Text = lekar.ime;
            txtBoxPrezimeLekara.Text = lekar.prezime;
            txtBoxJMBG.Text = lekar.jmbg;
            txtBoxDatumRodjenja.Text = lekar.datumRodjenja.ToString("MM/dd/yyyy");
            txtBoxMestoRodjenja.Text = lekar.mestoRodjenja;
            txtBoxDrzavaRodjenja.Text = lekar.drzavaRodjenja;
            postaviPolLekara(lekar);
            txtBoxAdresaS.Text = lekar.adresa;
            txtBoxEmail.Text = lekar.email;
            txtBoxTelefon.Text = lekar.brojTelefona;
            txtBoxBrojZdravKnjizice.Text = lekar.brojZdravstveneKnjizice;
            txtBoxKorisnickoIme.Text = lekar.korisnickoIme;
            txtBoxKorisnickoIme.IsEnabled = false;
            passwordBoxLozinkaLekar.Password = lekar.lozinka.ToString();
            passwordBoxLozinkaLekar.IsEnabled = false;
            specijalizacija.SelectedIndex = Int32.Parse(lekar.idSpecijalizacije);
            postaviBracnoStanjeLekara(lekar);
            txtBoxPocetakRadnogVremena.Text = lekar.pocetakRadnogVremena.ToString("hh:mm");
            txtBoxKrajRadnogVremena.Text = lekar.krajRadnogVremena.ToString("hh:mm");


            if (lekar.jeNaGodisnjemOdmoru)
            {
                checkBoxGodisnjiOdmor.IsEnabled = true;
                checkBoxGodisnjiOdmor.IsChecked = true;
                txtBoxPocetakGodisnjegOdmora.IsEnabled = false;
                txtBoxBrojZauzetihDana.IsEnabled = false;

                lblBrojDana.Content = lekar.brojSlobodnihDana.ToString();
                txtBoxBrojZauzetihDana.Text = lekar.brojZauzetihDana.ToString();
                txtBoxPocetakGodisnjegOdmora.Text = lekar.pocetakGodisnjegOdmora.ToString("MM/dd/yyyy");
            }
            else
            {

                checkBoxGodisnjiOdmor.IsChecked = false;
                lblBrojDana.Content = lekar.brojSlobodnihDana.ToString();
                txtBoxBrojZauzetihDana.Text = lekar.brojZauzetihDana.ToString();
                txtBoxPocetakGodisnjegOdmora.Text = "";
            }
        }

        private void postaviPolLekara(Lekar lekar)
        {
            if (lekar.pol.Equals("Musko"))
            {
                radioBtnMusko.IsChecked = true;
            }
            else if (lekar.pol.Equals("Zensko"))
            {
                radioBtnZensko.IsChecked = true;
            }
            else
            {
                radioBtnOstalo.IsChecked = true;
            }
        }

        private void postaviBracnoStanjeLekara(Lekar lekar)
        {
            if (lekar.bracniStatus.Equals("U braku"))
            {
                radioBtnUBraku.IsChecked = true;
            }
            else if (lekar.bracniStatus.Equals("Nije u braku"))
            {
                radioBtnNijeUBraku.IsChecked = true;
            }
            else
            {
                radioBtnOstalo.IsChecked = true;
            }
        }

        private void btnOdustaniLekar_Click(object sender, RoutedEventArgs e)
        {
            flagIzmeni = false;
            ocistiPoljaLekara();
            sakrijDodatnaPoljaLekara();
            dataGridLekari.SelectedIndex = -1;

            txtBoxKorisnickoIme.IsEnabled = true;
            passwordBoxLozinkaLekar.IsEnabled = true;
        }


        #endregion

        #region Validacija lekar

        private bool proveriValidnostPoljaLekara(String ime, String prezime, String jmbg,
                                                 String email, String telefon, String brojZdravstveneKnjizice, String pocetakRadnogVremena,
                                                 String krajRadnogVremena, String brojZauzetihDana, String pocetakGodisnjegOdmora)
        {
            if (!proverIme(ime) || !proveriPrezime(prezime) || !proveriJMBG(jmbg) || !proveriEmail(email) || !proveriTelefon(telefon) ||
               !proveriBrojZdravstveneKnjizice(brojZdravstveneKnjizice) || !proveriRadnoVremePocetak(pocetakRadnogVremena) || !proveriRadnoVremeKraj(krajRadnogVremena))

            {
                return false;
            }

            if (imaGodisnjiOdmor)
            {
                foreach (char c in brojZauzetihDana)
                {
                    if (!Char.IsDigit(c))
                    {
                        txtBoxBrojZauzetihDana.Clear();
                        return false;
                    }
                }

                int brZauzetihDana = Int32.Parse(txtBoxBrojZauzetihDana.Text);
                int brSlobodnihDana = Int32.Parse(lblBrojDana.Content.ToString());

                if (brZauzetihDana > brSlobodnihDana)
                {
                    txtBoxBrojZauzetihDana.Clear();
                    return false;
                }
            }

            return true;

        }

        private bool proveriJMBG(String jmbg)
        {
            if ((!Regex.IsMatch(jmbg, @"[0-9]{13}$")) || (jmbg.Length != 13))
            {
                txtBoxJMBG.Clear();
                return false;
            }

            return true;
        }
        private bool proverIme(String ime)
        {
            bool povratnaVrednost = true;
            foreach (char c in ime)
            {
                if (!Char.IsLetter(c))
                {
                    txtBoxImeLekara.Clear();
                    return false;
                }
            }

            return true;
        }

        private bool proveriPrezime(String prezime)
        {
            foreach (char c in prezime)
            {
                if (!Char.IsLetter(c))
                {
                    txtBoxPrezimeLekara.Clear();
                    return false;
                }
            }

            return true;
        }

        private bool proveriEmail(String email)
        {
            if (!Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                txtBoxEmail.Clear();
                return false;
            }

            return true;
        }

        private bool proveriTelefon(String telefon)
        {
            if (!telefon.Equals(""))
            {
                foreach (char c in telefon)
                {
                    if (!Char.IsDigit(c))
                    {
                        txtBoxTelefon.Clear();
                        return false;
                    }
                }
            }

            return true;
        }

        private bool proveriBrojZdravstveneKnjizice(String brojKnjizice)
        {
            foreach (char c in brojKnjizice)
            {
                if (Char.IsLetter(c))
                {
                    txtBoxBrojZdravKnjizice.Clear();
                    Console.Write("NE VALJA KNJIZICA");
                    return false;
                }
            }

            return true;
        }

        private bool proveriRadnoVremePocetak(String radnVreme)
        {
            if (!Regex.IsMatch(radnVreme, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
            {
                txtBoxPocetakRadnogVremena.Clear();
                return false;
            }

            return true;
        }

        private bool proveriRadnoVremeKraj(String radnVreme)
        {
            if (!Regex.IsMatch(radnVreme, @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$"))
            {
                txtBoxKrajRadnogVremena.Clear();
                return false;
            }

            return true;
        }

        #endregion

        #region Izvestaj (MVVM)

        private void btnIzvetaj_Click(object sender, RoutedEventArgs e)
        {
            frame.Visibility = Visibility.Visible;
            frame.Content = new SekretarIzvestaj(this);

            this.TerminiGrid.IsEnabled = false;

        }

        #endregion
    }


}

