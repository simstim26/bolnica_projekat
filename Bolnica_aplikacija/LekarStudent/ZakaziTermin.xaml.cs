using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.LekarStudent;
using Bolnica_aplikacija.PacijentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ZakaziTermin : UserControl
    {
        private static int tipAkcije; //0-zakazivanje; 1-promena (zahteva zakazivanje i otkazivanje)
        public static bool aktivan { get; set; }
        public ZakaziTermin(int tip)
        {
            InitializeComponent();
            tipAkcije = tip;
            PacijentInfo.aktivanPacijentInfo = false;
            PrikazProstorija.aktivan = false;
            aktivan = true;

            if (tipAkcije == 1)
            {
               // lblIzaberi.Content = "Izaberite novi termin: ";
                btnOdabirProstorije.Content = "Promena prostorije";
                btnOdabirProstorije.Visibility = Visibility.Visible;
                btnZakaziOperaciju.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Promena termina";
            }
            else
            {
                btnOdabirProstorije.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Zakazivanje termina";
                if (KorisnikKontroler.getLekar().idSpecijalizacije != "0")
                {
                    btnZakaziOperaciju.Visibility = Visibility.Visible;
                }
                else
                {
                    btnZakaziOperaciju.Visibility = Visibility.Hidden;
                }
            }
            dataGridZakazivanjeTermina.ItemsSource = LekarKontroler.prikaziSlobodneTermineZaLekara(KorisnikKontroler.getLekar(), tipAkcije);

        }

        public static int getTipAkcije()
        {
            return tipAkcije;
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new PacijentInfo();
            PacijentInfo.getPregledTab().SelectedIndex = 2;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridZakazivanjeTermina.SelectedIndex != -1)
            {
                PacijentTermin pacijentTermin = (PacijentTermin)dataGridZakazivanjeTermina.SelectedItem; //novi termin

                if (tipAkcije == 0)
                {
                    PacijentKontroler.zakaziTerminPacijentu(pacijentTermin.id);
                }
                else
                {
                    PacijentKontroler.azurirajTerminPacijentu(TerminKontroler.getTermin().idTermina, pacijentTermin.id);
                }

                LekarProzor.getX().Content = new PacijentInfo();
                PacijentInfo.getPregledTab().SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnOdabirProstorije_Click(object sender, RoutedEventArgs e)
        {
            if (TerminKontroler.getTermin().idLekara.Equals(KorisnikKontroler.getLekar().id))
            {
                Content = new PrikazProstorija();
            }
            else
            {
                MessageBox.Show("Ne može se promeniti lokacija termina drugog lekara.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
