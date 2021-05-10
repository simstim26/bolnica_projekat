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
    /// Interaction logic for ZakazivanjeOperacije.xaml
    /// </summary>
    public partial class ZakazivanjeOperacije : UserControl
    {
        public static bool aktivan { get; set; }
        private static Grid odabirProstorije;
        private static Grid prikazInventara;
        public ZakazivanjeOperacije()
        {
            InitializeComponent();
            lblPacijent.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + " " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
            aktivan = true;
            ZakaziTermin.aktivan = false;
            LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
            odabirProstorije = gridOdabirProstorija;
            prikazInventara = gridPrikazInventara;
            btnPotvrdi.IsEnabled = false;
            btnDodajProstoriju.IsEnabled = false;
        }

        public static void podesiKretanjeZaNazad()
        {
            if(odabirProstorije.Visibility == Visibility.Visible)
            {
                odabirProstorije.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";

            }
            else if(prikazInventara.Visibility == Visibility.Visible)
            {
                prikazInventara.Visibility = Visibility.Hidden;
                odabirProstorije.Visibility = Visibility.Visible;
                LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";

            }
            else
            {
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije());
                aktivan = false;
            }
        }
        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Visible;
            Termin termin = new Termin();
            termin.datum = (DateTime)datum.SelectedDate;
            String[] satnica = txtVreme.Text.Split(':');
            termin.satnica = termin.datum + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));
            termin.idProstorije = "";
            dataGridProstorije.ItemsSource = TerminKontroler.nadjiSlobodneProstorijeZaTermin(KorisnikKontroler.getLekar(), termin);
            LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";

        }

        private void btnPrikazInventara_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorije.SelectedIndex != -1)
            {
                gridOdabirProstorija.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Prikaz inventara";
                dataGridStavke.ItemsSource = ((Prostorija)dataGridProstorije.SelectedItem).Stavka;
                gridPrikazInventara.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Termin termin = new Termin();
            termin.datum = (DateTime)datum.SelectedDate;
            String[] satnica = txtVreme.Text.Split(':');
            termin.satnica = termin.datum + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0));
            termin.idTermina = "";
            termin.idLekara = KorisnikKontroler.getLekar().id;
            termin.idProstorije = ((Prostorija)dataGridProstorije.SelectedItem).id;
            termin.idPacijenta = PacijentKontroler.getPacijent().id;
            termin.tip = TipTermina.OPERACIJA;
            termin.jeHitan = (bool)cBoxHitna.IsChecked;
            TerminKontroler.napraviTermin(termin);
        }

        private void txtVreme_TextChanged(object sender, TextChangedEventArgs e)
        {
            omoguciDugme();
            omoguciDugmeZaProstoriju();
        }

        private void txtProstorija_TextChanged(object sender, TextChangedEventArgs e)
        {
            omoguciDugme();
            omoguciDugmeZaProstoriju();
        }

        private void omoguciDugme()
        {
            if (!String.IsNullOrWhiteSpace(txtVreme.Text) && datum.SelectedDate != null && !txtProstorija.Text.Equals("Prostorija..."))
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
            if(!String.IsNullOrWhiteSpace(txtVreme.Text) && datum.SelectedDate != null)
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

        private void btnPotvrdiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProstorije.SelectedIndex != -1)
            {
                txtProstorija.Text = ((Prostorija)dataGridProstorije.SelectedItem).sprat + " " +
                    ((Prostorija)dataGridProstorije.SelectedItem).broj;
                gridOdabirProstorija.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati prostoriju.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUkloniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            txtProstorija.Text = "Prostorija...";
            dataGridProstorije.SelectedIndex = -1;
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            Content = new ZakaziTermin(ZakaziTermin.getTipAkcije());
        }
    }
}
