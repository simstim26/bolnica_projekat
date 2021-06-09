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
        public static Grid gridPretraga { get; set; }
        
        public static Grid pretragaBuduci { get; set; }
        public PacijentInfo(String idPacijenta, String idTermina)
        {

            InitializeComponent();
            this.DataContext = new PacijentiInfoViewModel(idPacijenta, idTermina);
            LekarProzor.getNazad().Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Rad sa pacijentima";
            aktivanPacijentInfo = true;
            tab = this.tabInfo;

            String[] id = { idPacijenta, idTermina };
            fm.DataContext = id;

            gridPretraga = this.gridIstorijaTerminaPretraga;
            pretragaBuduci = lblPretragaBuduci;

            Alergije.aktivan = false;
            IzmenaBLecenja.aktivan = false;
            IzmenaBolesti.aktivan = false;
            IstorijaBolesti.aktivan = false;
            UvidUTerapije.aktivan = false;
            TerapijeIzdavanjeRecpeta.aktivan = false;
            Izvestaj.aktivan = false;
            ProsliTermini.aktivan = false;
            ZakaziTermin.aktivan = false;
            ZakazivanjeOperacije.aktivan = false;
            PrikazProstorija.aktivan = false;

            if(LekarTabovi.getTab().SelectedIndex == 1)
            {
                btnIzvestaj.IsEnabled = false;
            }
            else
            {
                DateTime danasnjiDatum = DateTime.Now;
                String satnica = TerminKontroler.nadjiTerminPoId(idTermina).satnica.ToString("HH:mm:ss");
                String trenutnaSatnica = danasnjiDatum.ToString("HH:mm");
                String[] trenutnaSatnica1 = trenutnaSatnica.Split(':');
                String[] satnica1 = satnica.Split(':');
                DateTime terminDatum = TerminKontroler.nadjiTerminPoId(idTermina).datum.Add(new TimeSpan(Convert.ToInt32(satnica1[0]), Convert.ToInt32(satnica1[1]), Convert.ToInt32(satnica1[2])));
                DateTime pomocni = danasnjiDatum.Date.Add(new TimeSpan(Convert.ToInt32(trenutnaSatnica1[0]), Convert.ToInt32(trenutnaSatnica1[1]), 0));
                if(DateTime.Compare(terminDatum.Date,pomocni.Date) == 0)
                {
                    btnIzvestaj.IsEnabled = true;
                }
                else
                {
                    btnIzvestaj.IsEnabled = false;
                }
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
            if (this.tabInfo.SelectedIndex == 0)
            {
                LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            }
            else if (this.tabInfo.SelectedIndex == 1)
            {
                LekarProzor.getPretraga().Visibility = Visibility.Visible;

            }
            else
            {
                LekarProzor.getPretraga().Visibility = Visibility.Visible;
            }
        }
    }
}