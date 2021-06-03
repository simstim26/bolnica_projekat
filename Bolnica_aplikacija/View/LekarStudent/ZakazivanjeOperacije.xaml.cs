using Bolnica_aplikacija.Kontroler;
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
    /// Interaction logic for ZakazivanjeOperacije.xaml
    /// </summary>
    public partial class ZakazivanjeOperacije : UserControl
    {
        public static bool aktivan { get; set; }
        private static Grid odabirProstorije;
        private static Grid prikazInventara;
        private static FrameworkElement fm = new FrameworkElement();
        public ZakazivanjeOperacije(String idPacijenta)
        {
            InitializeComponent();
            lblPacijent.Content = PacijentKontroler.nadjiPacijenta(idPacijenta).ime + " " + PacijentKontroler.nadjiPacijenta(idPacijenta).prezime;
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + " " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
            aktivan = true;
            ZakaziTermin.aktivan = false;
            LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
            this.DataContext = idPacijenta;
            fm.DataContext = this.DataContext;

            odabirProstorije = gridOdabirProstorija;
            prikazInventara = gridPrikazInventara;
            btnPotvrdi.IsEnabled = false;
            btnDodajProstoriju.IsEnabled = false;
        }

        public static FrameworkElement getFM()
        {
            return fm;
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
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije(), ((String[])ZakaziTermin.getFM().DataContext)[0]
                    , ((String[])ZakaziTermin.getFM().DataContext)[1]); ;
                aktivan = false;
            }
        }
        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Visible;

            String[] satnica = txtVreme.Text.Split(':');

            dataGridProstorije.ItemsSource = TerminKontroler.nadjiSlobodneProstorijeZaTermin(KorisnikKontroler.getLekar(), new TerminDTO((DateTime)datum.SelectedDate,
             (DateTime)datum.SelectedDate + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0)),"", "", TipTermina.OPERACIJA));
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
            String[] satnica = txtVreme.Text.Split(':');

            TerminKontroler.napraviTermin(new TerminDTO(TipTermina.OPERACIJA, (DateTime)datum.SelectedDate, (DateTime)datum.SelectedDate
                + (new TimeSpan(Convert.ToInt32(satnica[0]), Convert.ToInt32(satnica[1]), 0)), false, "", ((Prostorija)dataGridProstorije.SelectedItem).id,
                 ((String)fm.DataContext), KorisnikKontroler.getLekar().id, null, null, null, null, null, null, TipTermina.PREGLED, (bool)cBoxHitna.IsChecked));
            Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0], ((String[])PacijentInfo.getFM().DataContext)[1]);
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
            if(!String.IsNullOrWhiteSpace(txtVreme.Text) && (datum.SelectedDate != null && DateTime.Compare((DateTime)datum.SelectedDate, DateTime.Now.Date) >= 0))
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
            Content = new ZakaziTermin(ZakaziTermin.getTipAkcije(), ((String[])ZakaziTermin.getFM().DataContext)[0]
                    , ((String[])ZakaziTermin.getFM().DataContext)[1]);
        }

        private void datum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            lblGreskaDatum.Visibility = DateTime.Compare((DateTime)datum.SelectedDate, DateTime.Now.Date) < 0 ? Visibility.Visible : Visibility.Hidden;
            omoguciDugme();
            omoguciDugmeZaProstoriju();
        }
    }
}
