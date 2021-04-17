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
        public PacijentInfo()
        {
            InitializeComponent();
            LekarProzor.getNazad().Visibility = Visibility.Visible;
            aktivanPacijentInfo = true;
            tab = this.tabInfo;
            lblJmbg.Content = PacijentKontroler.getPacijent().jmbg;
            lblImePrezime.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblDatumRodjenja.Content = PacijentKontroler.getPacijent().datumRodjenja.ToString("dd.MM.yyyy.");
            lblAdresa.Content = PacijentKontroler.getPacijent().adresa;
            lblKontakt.Content = PacijentKontroler.getPacijent().brojTelefona;
           
            dataTermini = this.dataGridTerminiPacijenta;
            ucitajPodatke();
        }

        public static TabControl getTab()
        {
            return tab;
        }
        public static DataGrid getDataTermini()
        {
            return dataTermini;
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new LekarTabovi();
          //  LekarTabovi.getTab().SelectedIndex = 1;
        }

        private void btnZakazi_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new ZakaziTermin(0);
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
                        LekarTabovi.getRaspored().ItemsSource = LekarKontroler.prikaziZauzeteTermineZaLekara(KorisnikKontroler.getLekar());
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
              dataGridTerminiPacijenta.ItemsSource = PacijentKontroler.prikazSvihTerminaPacijenta();
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
                        LekarProzor.getX().Content = new ZakaziTermin(1);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new Izvestaj();
        }
    }
}
