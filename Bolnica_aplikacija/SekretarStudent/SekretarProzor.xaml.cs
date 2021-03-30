using Model;
using System;
using System.Collections.Generic;
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
        private bool ispravnostPolja;

        public SekretarProzor()
        {
            InitializeComponent();
            CenterWindow();

            this.PacijentGrid.Visibility = Visibility.Hidden;
       
        }

        private void pacijenti_Click(object sender, RoutedEventArgs e)
        {
            
            this.PocetniEkranGrid.Visibility = Visibility.Hidden;
            this.PacijentGrid.Visibility = Visibility.Visible;


            //TODO: Razdvojiti komponente (Prikaz pacijenata zasebna)
            
            List<Pacijent> ucitaniPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            List<Pacijent> neobrisaniPacijenti = new List<Pacijent>();
           
            foreach (Pacijent p in ucitaniPacijenti)
            {
                if (!p.jeLogickiObrisan)
                {
                    neobrisaniPacijenti.Add(p);
                }
            }

            sviPacijenti = neobrisaniPacijenti;
            TabelaPacijenti.ItemsSource = sviPacijenti;
            TabelaPacijenti.Items.Refresh();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PocetniEkranGrid.Visibility = Visibility.Visible;
            this.PacijentGrid.Visibility = Visibility.Hidden;

            omoguciKoriscenjaPolja(false);
            ocistiPolja();
            buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;

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
                textDatumRodj.Text = pacijent.datumRodjenja.ToString();
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

        public static Pacijent GetPacijent()
        {
            return pacijent;
        }


        private void obrisiPacijentaButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaPacijenti.SelectedIndex != -1)
            {
                Pacijent izabraniPacijent = (Pacijent)TabelaPacijenti.SelectedItem;
                String idZaBrisanje = izabraniPacijent.id;
                Sekretar.ObrisiPacijenta(idZaBrisanje, sviPacijenti);

            }

            sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            Sekretar.ProcitajPacijenta(TabelaPacijenti);
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

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {
          
            omoguciKoriscenjaPolja(false);
            ocistiPolja();
            
            textAdresa.IsEnabled = false;
            textEmail.IsEnabled = false;
            
            buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
           
        }

        private void buttonPotvrdiDodavanje_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("KLIK RADI");
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

            
            if (proveriIspravnostPolja(pacGost,pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon))
            {
                
                if (!flagIzmeni)
                {
                    Console.WriteLine("Kreiram");
                    Console.WriteLine(pacGost);
                    if (!pacGost)
                    {
                        Console.WriteLine("Ne Unosim gosta");
                        String pacId = (sviPacijenti.Count() + 1).ToString();
                        Sekretar.NapraviPacijenta(pacId, pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon, TabelaPacijenti, sviPacijenti);
                    }
                    else
                    {
                        Console.WriteLine("Unosim gosta");
                        String pacId = (sviPacijenti.Count() + 1).ToString();
                        Sekretar.NapraviPacijenta(pacId, pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, "", "", pacTelefon, TabelaPacijenti, sviPacijenti);
                    }

                }
                else
                {
                    
                    Sekretar.AzurirajPacijenta(idPacijenta, pacIdBolnice, pacGost, pacKorisnickoIme, pacLozinka, pacJmbg, pacIme, pacPrezime, pacDatumRodjenja, pacAdresa, pacEmail, pacTelefon, TabelaPacijenti, sviPacijenti);
                }

                omoguciKoriscenjaPolja(false);
                ocistiPolja();

                buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
                buttonPotvrdiDodavanje.Visibility = Visibility.Hidden;

            }
            else
            {
                Console.WriteLine("OVDE JE PROBLEM");
                return;
            }

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

                idPacijenta = pacijent.id;
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

                } else
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

            } else
            {
                if (!(bool)checkboxGostujuciNalog.IsChecked)
                {
                    Console.WriteLine("NIJE CEKIRAN");
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
                    Console.WriteLine("CEKIRAN");
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

        private bool proveriIspravnostPolja(bool gost, String korisnickoIme, String loznika, String jmbg, String ime, String prezime, DateTime datumRodj, string adresa, string email, string telefon)
        {
            
            //PROVERA JMBG
            if ((!Regex.IsMatch(jmbg, @"[0-9]{13}$")) || (jmbg.Length != 13))
            {
                textJMBG.Clear();
                return false;
            }

            if (!flagIzmeni) // Ako kreiramo pacijenta i vec postoji JMBG
            {
              
                foreach (Pacijent pac in sviPacijenti)
                {
                    if (pac.jmbg.Equals(jmbg))
                    {
                        textJMBG.Clear();
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

           /*if (flagIzmeni) // Ako kreiramo pacijenta i vec postoji korisnicko ime
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
           */
           

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
        }
    }
}

