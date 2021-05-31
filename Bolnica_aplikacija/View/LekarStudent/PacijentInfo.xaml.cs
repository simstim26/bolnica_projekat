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
using Model;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.LekarStudent;
using Bolnica_aplikacija.View.LekarStudent;
using Bolnica_aplikacija.ViewModel;

namespace Bolnica_aplikacija
{
    public partial class PacijentInfo : UserControl
    {
        public static TabControl tab;
        public static bool aktivanPacijentInfo { get; set; }

        private static FrameworkElement fm = new FrameworkElement();
        public PacijentInfo(String idPacijenta, String idTermina)
        {

            InitializeComponent();
            this.DataContext = new PacijentiInfoViewModel(idPacijenta, idTermina); 
            LekarProzor.getNazad().Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Rad sa pacijentima";
            aktivanPacijentInfo = true;
            tab = this.tabInfo;

            String[] id = { idPacijenta, idTermina};
            fm.DataContext = id;

            zavrsiBolnickoLecenje();
            prikaziBolnickoLecenje();
            
            /*if(LekarTabovi.getIndikator() == 1)
            {
                btnIzvestaj.IsEnabled = false;
            }
            else
            {
                DateTime danasnjiDatum = DateTime.Now;
                String satnica = TerminKontroler.getTermin().satnica.ToString("HH:mm:ss");
                String trenutnaSatnica = danasnjiDatum.ToString("HH:mm");
                String[] trenutnaSatnica1 = trenutnaSatnica.Split(':');
                String[] satnica1 = satnica.Split(':');
                DateTime terminDatum = TerminKontroler.getTermin().datum.Add(new TimeSpan(Convert.ToInt32(satnica1[0]), Convert.ToInt32(satnica1[1]), Convert.ToInt32(satnica1[2])));
                DateTime pomocni = danasnjiDatum.Date.Add(new TimeSpan(Convert.ToInt32(trenutnaSatnica1[0]), Convert.ToInt32(trenutnaSatnica1[1]), 0));
                if(DateTime.Compare(terminDatum,pomocni) == 0)
                {
                    btnIzvestaj.IsEnabled = true;
                }
                else
                {
                    btnIzvestaj.IsEnabled = false;
                }
            }*/
           
        }

        private void prikaziBolnickoLecenje()
        {
            gridNaBLecenju.Visibility = BolnickoLecenjeKontroler.proveriBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]) ? Visibility.Visible : Visibility.Hidden;
            if (BolnickoLecenjeKontroler.proveriBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]))
            {
                lblBLecenjeSoba.Content = BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]).bolnickaSoba.sprat + " " + BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]).bolnickaSoba.broj;
                lblBLecenjeDatum.Content = (BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]).datumPocetka.AddDays(BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(((String[])fm.DataContext)[0]).trajanje)).ToString("dd.MM.yyyy");
            }
        }

        private void zavrsiBolnickoLecenje()
        {
            if (BolnickoLecenjeKontroler.proveriKrajBolnickogLecenje(((String[])fm.DataContext)[0]))
            {
                BolnickoLecenjeKontroler.zavrsiBolnickoLecenje(((String[])fm.DataContext)[0]);
            }
        }
        public static FrameworkElement getFM()
        {
            return fm;
        }

        public static TabControl getTab()
        {
            return tab;
        }

        public static TabControl getPregledTab()
        {
            return tab;
        }
        private void tabInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.tabInfo.SelectedIndex == 0)
            {
                LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            }
            else if(this.tabInfo.SelectedIndex == 1)
            {
                LekarProzor.getPretraga().Visibility = Visibility.Visible;

            }
            else
            {
                LekarProzor.getPretraga().Visibility = Visibility.Visible;
            }
        }

        private void btnIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            Content = new Izvestaj(((String[])fm.DataContext)[0], ((String[])fm.DataContext)[1]);
        }

        private void btnBolesti_Click(object sender, RoutedEventArgs e)
        {
            Content = new IstorijaBolesti(((String[])fm.DataContext)[0]);
        }

        private void btnTerapije_Click(object sender, RoutedEventArgs e)
        {
            Content = new UvidUTerapije(((String[])fm.DataContext)[0]);
        }

        private void btnAlergije_Click(object sender, RoutedEventArgs e)
        {
            Content = new Alergije(((String[])fm.DataContext)[0]);
        }

        private void btnIzmeniBLecenje_Click(object sender, RoutedEventArgs e)
        {
            Content = new IzmenaBLecenja(((String[])fm.DataContext)[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridProsliTermini.SelectedIndex != -1)
            {
                Content = new ProsliTermini(((PacijentTermin)dataGridProsliTermini.SelectedItem).id);
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin!",  "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
