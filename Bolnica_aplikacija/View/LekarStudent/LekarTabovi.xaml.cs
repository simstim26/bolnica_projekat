using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
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

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarTabovi.xaml
    /// </summary>
    public partial class LekarTabovi : UserControl
    {
        private static ContentControl x;
        private static TabControl tab;
        //private static TabItem tabRad;
        private static DataGrid raspored;
        private static Grid gridRasporedPretraga;
        private static Grid gridPrikaz;
        private static int indikator; //0-raspored; 1-prikaz
        public LekarTabovi()
        {
            InitializeComponent();
            // this.contentControl.Content = new PrikazPacijenata();
            // x = this.contentControl;
            raspored = this.dataRaspored;
            tab = this.lekarTab;
            gridRasporedPretraga = this.gridPretragaRaspored;
            lstPacijenti.ItemsSource = PacijentKontroler.prikazPacijenata();
            gridPrikaz = this.gridPrikazPacijenata;
            ucitajSve();

        }

        public static  int getIndikator()
        {
            return indikator;
        }

        public static Grid getRasporedPretraga()
        {
            return gridRasporedPretraga;
        }
        public static Grid getPacijentiPretraga()
        {
            return gridPrikaz;
        }
        public static DataGrid getRaspored()
        {
            return raspored;
        }

        public static ContentControl getX()
        {
            return x;
        }

        public static TabControl getTab()
        {
            return tab;
        }
        private void ucitajSve()
        {
            dataRaspored.ItemsSource = LekarKontroler.prikaziZauzeteTermineZaLekara(KorisnikKontroler.getLekar());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.prvi.SelectedDate != null && this.drugi.SelectedDate != null)
            {
                DateTime prvi = (DateTime)this.prvi.SelectedDate;
                DateTime drugi = (DateTime)this.drugi.SelectedDate;
                DateTime pomocni = DateTime.Now;
                DateTime danasnjiDatum = pomocni.Date.Add(new TimeSpan(0, 0, 0));
                if(DateTime.Compare(prvi, danasnjiDatum) < 0 || DateTime.Compare(drugi, danasnjiDatum) < 0 || DateTime.Compare(prvi, drugi) > 0)
                {
                    lblGreska.Visibility = Visibility.Visible;
                }
                else
                {
                    lblGreska.Visibility = Visibility.Hidden;
                    dataRaspored.ItemsSource = LekarKontroler.pretraziZauzeteTermineZaLekara(KorisnikKontroler.getLekar(), prvi, drugi);
                }
            }
            else
            {
                ucitajSve();
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            if(dataRaspored.SelectedIndex != -1)
            {
                PacijentKontroler.otkaziTerminPacijenta(((PacijentTermin)dataRaspored.SelectedItem).id);
                if (this.prvi.SelectedDate != null && this.prvi.SelectedDate != null)
                {
                    dataRaspored.ItemsSource = LekarKontroler.pretraziZauzeteTermineZaLekara(KorisnikKontroler.getLekar(), (DateTime)this.prvi.SelectedDate, (DateTime)this.drugi.SelectedDate);

                }
                else
                {
                    this.prvi.SelectedDate = null;
                    this.drugi.SelectedDate = null;
                    ucitajSve();
                }
                //PacijentInfo.getDataTermini().ItemsSource = PacijentKontroler.prikazSvihTerminaPacijenta();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnInfoPrikaz_Click(object sender, RoutedEventArgs e)
        {
            if (lstPacijenti.SelectedIndex != -1)
            {
                PacijentKontroler.nadjiPacijenta(((Pacijent)lstPacijenti.SelectedItem).id);
                indikator = 1;
                this.Content = new PacijentInfo();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if(dataRaspored.SelectedIndex != -1)
            {
                TerminKontroler.nadjiPacijentaZaTermin(((PacijentTermin)dataRaspored.SelectedItem).id);
                TerminKontroler.sacuvajTermin(((PacijentTermin)dataRaspored.SelectedItem).id);
                indikator = 0;
                this.Content = new PacijentInfo();
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
