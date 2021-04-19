﻿using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PacijentStudent;
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
        private String imePrezimePacijenta;
        private PacijentTermin izabraniTermin;

        public SekretarProzor()
        {
            InitializeComponent();
            CenterWindow();

            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Hidden;

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

        public static Pacijent GetPacijent()
        {
            return pacijent;
        }
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
            //Radi se o dodavanju
            flagIzmeni = false;

            omoguciKoriscenjaPolja(true);
            ocistiPolja();

            buttonOdustaniDodavanje.Visibility = Visibility.Visible;
            buttonPotvrdiDodavanje.Visibility = Visibility.Visible;
            buttonPotvrdiDodavanje.IsEnabled = false;


        }

        private void izmeniPacijentaButton_Click(object sender, RoutedEventArgs e)
        {

            if (TabelaPacijenti.SelectedIndex != -1)
            {
                //IZMENA
                flagIzmeni = true;

                pacijent = (Pacijent)TabelaPacijenti.SelectedItem;

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
                textDatumRodj.Text = pacijent.datumRodjenja.ToString();
                textEmail.Text = pacijent.email;
                textTelefon.Text = pacijent.brojTelefona;

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
                SekretarKontroler.ObrisiPacijenta(idZaBrisanje);

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
            bool pacGost;
            String pacKorisnickoIme = textKorisnickoIme.Text;
            String pacLozinka = lozinka.Password.ToString();
            String pacIdBolnice = "1";
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

                        SekretarKontroler.NapraviPacijenta(pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon);
                        ucitajPacijenteTabela();
                    }
                    else
                    {

                        SekretarKontroler.NapraviPacijenta(pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, "", "", pacTelefon);
                        ucitajPacijenteTabela();
                    }

                }
                else
                {

                    SekretarKontroler.AzurirajPacijenta(idPacijenta, pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon);
                    ucitajPacijenteTabela();
                }

                omoguciKoriscenjaPolja(false);
                ocistiPolja();
                textAdresa.IsEnabled = false;
                textEmail.IsEnabled = false;

                buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
                buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;

            }
            else
            {
                return;
            }

        }

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {

            omoguciKoriscenjaPolja(false);
            ocistiPolja();

            textAdresa.IsEnabled = false;
            textEmail.IsEnabled = false;

            buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;

        }

        private void ucitajPacijenteTabela()
        {
            sviPacijenti = SekretarKontroler.ProcitajPacijente();
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

        // TERMINI
        private void ucitajTerminePacijenta()
        {

            pacijent = (Pacijent)TabelaPacijentiTermini.SelectedItem;
            idPacijenta = pacijent.id;
            imePrezimePacijenta = pacijent.ime + " " + pacijent.prezime;
            PacijentKontroler.nadjiPacijenta(pacijent.id);
            TabelaTerminiPacijenta.ItemsSource = PacijentKontroler.prikazPacijentovihTermina();

            lblUpozorenje.Visibility = Visibility.Hidden;

        }

        private void terminiButton_Click(object sender, RoutedEventArgs e)
        {
            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Hidden;
            this.TerminiGrid.Visibility = Visibility.Visible;

            sviPacijenti = SekretarKontroler.ProcitajPacijente();
            TabelaPacijentiTermini.ItemsSource = sviPacijenti;
            TabelaPacijentiTermini.Items.Refresh();
        }

        private void TabelaPacijenti1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabelaPacijentiTermini.SelectedIndex != -1)
            {
                ucitajTerminePacijenta();
                
                TabelaSlobodnihTermina.Visibility = Visibility.Hidden;
                slobodniTerminiLabel.Visibility = Visibility.Hidden;
                lblPacijent.Visibility = Visibility.Hidden;
                potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Hidden;
                lblUpozorenje.Visibility = Visibility.Hidden;
            }

        }

        private void zakaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            tipAkcijeTermini = 0;
            TabelaSlobodnihTermina.Visibility = Visibility.Visible;
            slobodniTerminiLabel.Visibility = Visibility.Visible;
            lblPacijent.Visibility = Visibility.Visible;
            lblPacijent.Content = imePrezimePacijenta;


            TabelaSlobodnihTermina.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(true);

            if (TabelaSlobodnihTermina.SelectedIndex == -1)
            {
                potvrdiZakazivanjeTerminaButton.IsEnabled = false;
            }

            TabelaSlobodnihTermina.Items.Refresh();
            potvrdiZakazivanjeTerminaButton.Visibility = Visibility.Visible;
        }

        private void OtkaziTerminButton_Click(object sender, RoutedEventArgs e)
        {
            tipAkcijeTermini = 2;
            if (TabelaTerminiPacijenta.SelectedIndex != -1)
            {
                izabraniTermin = (PacijentTermin)TabelaTerminiPacijenta.SelectedItem;
                if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                {
                    //MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning)
                    lblUpozorenje.Visibility = Visibility.Visible;
                }
                else
                {
                    
                   PotvrdaGrid.Visibility = Visibility.Visible;

                }
                                      
            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da otkažete.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
           
        }

        private void IzmeniTerminButton_Click(object sender, RoutedEventArgs e)
        {
            tipAkcijeTermini = 1;
            if (TabelaTerminiPacijenta.SelectedIndex != -1)
            {

                PacijentTermin izabraniTermin = (PacijentTermin)TabelaTerminiPacijenta.SelectedItem;

                if (izabraniTermin.napomena.Equals("Pregled"))
                {
                    if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (izabraniTermin.idSpecijalizacije.Equals("0"))
                        {
                            TerminKontroler.sacuvajTermin(izabraniTermin.id);
                            IzmenaTerminaPacijent izmenaTermina = new IzmenaTerminaPacijent(TabelaTerminiPacijenta, idPacijenta);
                            izmenaTermina.Owner = this;
                            izmenaTermina.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Potrebno je da se konsultujete sa Vašim specijalistom oko izmene termina pregleda.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Za izmenu termina operacije je potrebno da se konsultujete sa Vašim lekarom.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da izmenite.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void potvrdiZakazivanjeTerminaButton_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaGrid.Visibility = Visibility.Visible;
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

        private void btnPotvrdiIzbor_Click(object sender, RoutedEventArgs e)
        {
            if (tipAkcijeTermini == 0)
            {
                izabraniTermin = (PacijentTermin)TabelaSlobodnihTermina.SelectedItem;
                PacijentKontroler.nadjiPacijenta(idPacijenta);
                String idSelektovanog = izabraniTermin.id;
                PacijentKontroler.zakaziTerminPacijentu(idSelektovanog);

                ucitajTerminePacijenta();
                TabelaPacijentiTermini.Items.Refresh();
                TabelaSlobodnihTermina.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(true);
                TabelaSlobodnihTermina.Items.Refresh();

                PotvrdaGrid.Visibility = Visibility.Hidden;
                potvrdiZakazivanjeTerminaButton.IsEnabled = false;
            }
            else if (tipAkcijeTermini == 1)
            {

            }
            else
            {
                           
                PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);
                
                ucitajTerminePacijenta();
                TabelaSlobodnihTermina.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(true);
                TabelaSlobodnihTermina.Items.Refresh();

                PotvrdaGrid.Visibility = Visibility.Hidden;
            }
            
        }

        private void btnOdustaniOdIzborra_Click(object sender, RoutedEventArgs e)
        {
            PotvrdaGrid.Visibility = Visibility.Hidden;
            potvrdiZakazivanjeTerminaButton.IsEnabled = false;
        }


       
    }  
}

