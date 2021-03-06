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
        public static Grid pretraga { get; set; }
        private static FrameworkElement fm = new FrameworkElement();
        public ZakaziTermin(int tip, String idPacijenta, String idTermina)
        {
            InitializeComponent();
            tipAkcije = tip;
            PacijentInfo.aktivanPacijentInfo = false;
            PrikazProstorija.aktivan = false;
            aktivan = true;
            LekarProzor.getPretraga().Visibility = Visibility.Visible;
            pretraga = this.gridPretraga;
            String[] id = { idPacijenta, idTermina };
            this.DataContext = id;
            fm.DataContext = this.DataContext;
            datum.SelectedDate = DateTime.Now.Date;
            lblGreska.Visibility = Visibility.Hidden;

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

        public static FrameworkElement getFM()
        {
            return fm;
        }

        public static int getTipAkcije()
        {
            return tipAkcije;
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);
            PacijentInfo.getPregledTab().SelectedIndex = 2;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridZakazivanjeTermina.SelectedIndex != -1)
            {
                PacijentTermin pacijentTermin = (PacijentTermin)dataGridZakazivanjeTermina.SelectedItem; //novi termin

                if (tipAkcije == 0)
                {
                    PacijentKontroler.zakaziTerminPacijentu(((String[])fm.DataContext)[0], pacijentTermin.id);
                }
                else
                {
                    PacijentKontroler.azurirajTerminPacijentu(((String[])fm.DataContext)[1], pacijentTermin.id);
                }

                MessageBox.Show("Uspešno ste zakazali pregled!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

                LekarProzor.getX().Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                    ((String[])PacijentInfo.getFM().DataContext)[1]);
                PacijentInfo.getPregledTab().SelectedIndex = 2;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnOdabirProstorije_Click(object sender, RoutedEventArgs e)
        {
            if (TerminKontroler.nadjiTerminPoId(((String[])fm.DataContext)[1]).idLekara.Equals(KorisnikKontroler.getLekar().id))
            {
                Content = new PrikazProstorija(((String[])fm.DataContext)[1]);
            }
            else
            {
                MessageBox.Show("Ne može se promeniti lokacija termina drugog lekara.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnZakaziOperaciju_Click(object sender, RoutedEventArgs e)
        {
            Content = new ZakazivanjeOperacije(((String[])fm.DataContext)[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(datum.SelectedDate != null)
            {
                if(DateTime.Compare((DateTime) datum.SelectedDate,DateTime.Now.Date) >= 0)
                {
                    lblGreska.Visibility = Visibility.Hidden;
                    dataGridZakazivanjeTermina.ItemsSource = LekarKontroler.pretraziSlobodneTermineZaLekara((DateTime)datum.SelectedDate, tipAkcije);

                }
                else
                {
                    lblGreska.Visibility = Visibility.Visible;
                }
            }
            else
            {
                dataGridZakazivanjeTermina.ItemsSource = LekarKontroler.prikaziSlobodneTermineZaLekara(KorisnikKontroler.getLekar(), tipAkcije);
            }
        }
    }
}
