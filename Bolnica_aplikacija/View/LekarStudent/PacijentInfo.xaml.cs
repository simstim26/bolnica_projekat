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

namespace Bolnica_aplikacija
{
    public partial class PacijentInfo : UserControl
    {
        public static TabControl tab;
        private static DataGrid dataTermini;
        public static bool aktivanPacijentInfo { get; set; }

        private static FrameworkElement fm = new FrameworkElement();
        public PacijentInfo(String idPacijenta, String idTermina)
        {
            InitializeComponent();
            LekarProzor.getNazad().Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Rad sa pacijentima";
            aktivanPacijentInfo = true;
            tab = this.tabInfo;
            Pacijent pacijent = PacijentKontroler.nadjiPacijenta(idPacijenta);
            lblJmbg.Content = pacijent.jmbg;
            lblImePrezime.Content = pacijent.ime + " " + pacijent.prezime;
            lblDatumRodjenja.Content = pacijent.datumRodjenja.ToString("dd.MM.yyyy.");
            lblAdresa.Content = pacijent.adresa;
            lblKontakt.Content = pacijent.brojTelefona;
            String[] id = { idPacijenta, idTermina};
            this.DataContext = id;
            fm.DataContext = this.DataContext;

            
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
           
            dataTermini = this.dataGridTerminiPacijenta;
            ucitajPodatke();
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }

        public static TabControl getTab()
        {
            return tab;
        }
        public static DataGrid getDataTermini()
        {
            return dataTermini;
        }

        private void btnZakazi_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new ZakaziTermin(0, ((String[])DataContext)[0]);
        }

        public static TabControl getPregledTab()
        {
            return tab;
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTerminiPacijenta.SelectedIndex != -1)
            {
                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTerminiPacijenta.SelectedItem;
                if (TerminKontroler.proveriTipTermina(KorisnikKontroler.getLekar(), izabraniTermin.id))
                {
                    if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);
                    }
                }
                else
                {
                    MessageBox.Show("Ne mozete otkazati operaciju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

             ucitajPodatke();
        }

        private void ucitajPodatke()
        {
            dataGridTerminiPacijenta.ItemsSource = PacijentKontroler.prikazBuducihTerminaPacijenta();
            dataGridProsliTermini.ItemsSource = PacijentKontroler.prikazProslihTerminaPacijenta();
        }

        private void btnPromeni_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridTerminiPacijenta.SelectedIndex != -1)
            {
                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTerminiPacijenta.SelectedItem;

                if (TerminKontroler.proveriTipTermina(KorisnikKontroler.getLekar(), izabraniTermin.id))
                {
                    if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        TerminKontroler.sacuvajTermin(izabraniTermin.id);
                        LekarProzor.getX().Content = new ZakaziTermin(1, ((String[])DataContext)[0]);

                    }
                }
                else 
                {
                    MessageBox.Show("Nije moguće promeniti operaciju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                            
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
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
            Content = new Alergije();
        }
    }
}
