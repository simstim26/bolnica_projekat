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

namespace Bolnica_aplikacija
{
    public partial class PacijentInfo : UserControl
    {
        public static TabControl tab;
        public PacijentInfo()
        {
            InitializeComponent();
            tab = this.tabInfo;
            lblIme.Content += PrikazPacijenata.GetPacijent().ime;
            ucitajPodatke();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Content = new PrikazPacijenata();
        }

        private void btnZakazi_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new ZakaziTermin(0, null);
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
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));

                foreach (Termin termin in sviTermini)
                {
                    if (izabraniTermin.id.Equals(termin.idTermina))
                    {
                        DateTime trenutanDatum = DateTime.Now.AddDays(1);
                        int rezultat = DateTime.Compare(termin.datum, trenutanDatum);

                        if (rezultat <= 0)
                        {
                            MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            LekarProzor.getLekar().ObrisiTermin(izabraniTermin.id);
                        }
                        break;
                    }
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
            dataGridTerminiPacijenta.ItemsSource = LekarProzor.getLekar().ProcitajTermin();
        }

        private void btnPromeni_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridTerminiPacijenta.SelectedIndex != -1)
            {
                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTerminiPacijenta.SelectedItem;
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));

                foreach (Termin termin in sviTermini)
                {
                    if (izabraniTermin.id.Equals(termin.idTermina))
                    {
                        DateTime trenutanDatum = DateTime.Now.AddDays(1);
                        int rezultat = DateTime.Compare(termin.datum, trenutanDatum);

                        if(rezultat <= 0)
                        {
                            MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            LekarProzor.getX().Content = new ZakaziTermin(1, izabraniTermin);

                        }
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
    }
}
