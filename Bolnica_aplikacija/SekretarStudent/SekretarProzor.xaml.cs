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
            var pacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            TabelaPacijenti.ItemsSource = pacijenti;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.PocetniEkranGrid.Visibility = Visibility.Visible;
            this.PacijentGrid.Visibility = Visibility.Hidden;
         
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

        private void helpPacijentiButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaPacijenti.SelectedIndex != -1)
            {
                omoguciKoriscenjaPolja(false);

                pacijent = (Pacijent)TabelaPacijenti.SelectedItem;

                this.PacijentInfoGrid.Visibility = Visibility.Visible;
              

                textJMBG.Text = pacijent.jmbg;
                textIme.Text = pacijent.ime;
                textPrezime.Text = pacijent.prezime;
                textAdresa.Text = pacijent.adresa;
                textEmail.Text = pacijent.email;
                textTelefon.Text = pacijent.brojTelefona;
                textIdBolnice.Text = pacijent.idBolnice;
                
                //Nece da ucita
                textKorisnickoIme.Text = pacijent.korisnickoIme;
                lozinka.Password = pacijent.lozinka;
            }

        }

        public static Pacijent GetPacijent()
        {
            return pacijent;
        }

      /* EXCEPTION
       * private void obrisiPacijentaButton_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaPacijenti.SelectedIndex != -1)
            {
                Pacijent izabraniPacijent = (Pacijent)TabelaPacijenti.SelectedItem;
                var sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
                foreach (Pacijent pacijent in sviPacijenti)
                {
                    if (izabraniPacijent.id.Equals(pacijent.id))
                    {
                        sviPacijenti.Remove(pacijent);
                    }
                }
                string jsonString = JsonSerializer.Serialize(sviPacijenti);
                File.WriteAllText("Datoteke/probaPacijenti.txt", jsonString);
            }
            ucitajPodatke();
        }
      */

        private void ucitajPodatke()
        {
            sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            List<Pacijent> pacijenti = new List<Pacijent>();
            foreach (Pacijent pacijent in sviPacijenti)
            {
                if (pacijent.id.Equals(GetPacijent().id))
                {
                    pacijenti.Add(pacijent);
                }
            }
            TabelaPacijenti.ItemsSource = pacijenti;
        }

        private void dodajPacijentaButton_Click(object sender, RoutedEventArgs e)
        {

            omoguciKoriscenjaPolja(true);
            
            buttonOdustaniDodavanje.Visibility = Visibility.Visible;
            buttonDodajPacijenta.Visibility = Visibility.Visible;


        }

        private void textKorisnickoIme_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void omoguciKoriscenjaPolja(bool enabled)
        {

            textJMBG.Clear();
            textIme.Clear();
            textPrezime.Clear();
            textAdresa.Clear();
            textEmail.Clear();
            textTelefon.Clear();
            textIdBolnice.Clear();
            textKorisnickoIme.Clear();
            lozinka.Clear();

            textJMBG.IsEnabled = enabled;
            textIme.IsEnabled = enabled;
            textPrezime.IsEnabled = enabled;
            textAdresa.IsEnabled = enabled;
            textEmail.IsEnabled = enabled;
            textTelefon.IsEnabled = enabled;
            textIdBolnice.IsEnabled = enabled;
            textTelefon.IsEnabled = enabled;
            textKorisnickoIme.IsEnabled = enabled;
            lozinka.IsEnabled = enabled;
        }

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {
            omoguciKoriscenjaPolja(false);
            buttonDodajPacijenta.Visibility = Visibility.Hidden;
            buttonOdustaniDodavanje.Visibility = Visibility.Hidden;
      
        }

        private void buttonDodajPacijenta_Click(object sender, RoutedEventArgs e)
        {
            Pacijent pacijent = new Pacijent();
            
            var sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            List<Pacijent> pacijenti = new List<Pacijent>();

           /* bool gostujuci = false;
            String id = (sviPacijenti.Count() + 1).ToString();
            String korisnickoIme = textKorisnickoIme.Text;
            String loz = lozinka.ToString();
            String ime = textIme.Text;
            String prezime = textPrezime.Text;
            String adresa = textAdresa.Text;
            String eemail = textEmail.Text;
            String brojTelefona = textTelefon.Text;

            sviPacijenti.Add(pacijent);
            TabelaPacijenti.Items.Refresh();
           */
        }

        private void izmeniPacijentaButton_Click(object sender, RoutedEventArgs e)
        {

            pacijent = (Pacijent)TabelaPacijenti.SelectedItem;
            buttonOdustaniDodavanje.Visibility = Visibility.Visible;
            buttonDodajPacijenta.Visibility = Visibility.Visible;

            this.PacijentInfoGrid.Visibility = Visibility.Visible;

            omoguciKoriscenjaPolja(true);

            textJMBG.Text = pacijent.jmbg;
            textIme.Text = pacijent.ime;
            textPrezime.Text = pacijent.prezime;
            textAdresa.Text = pacijent.adresa;
            textEmail.Text = pacijent.email;
            textTelefon.Text = pacijent.brojTelefona;
            textKorisnickoIme.Text = pacijent.korisnickoIme;
            lozinka.Password = pacijent.lozinka;
            textIdBolnice.Text = pacijent.idBolnice;
            //fali datum rodjenja

            
  

        }
    }
}

