using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for PacijentZakaziTermin.xaml
    /// </summary>
    public partial class PacijentZakaziTermin : Window
    {
        private DataGrid dataGrid;
        private String idPacijenta;

        public PacijentZakaziTermin(DataGrid dataGrid, String idPacijenta)
        {
            InitializeComponent();

            this.idPacijenta = idPacijenta;
            this.dataGrid = dataGrid;
            dataGridSlobodniTermini.Loaded += SetMinSirina;

            ucitajPodatke();
        }

        public void SetMinSirina(object source, EventArgs e)
        {
            foreach(var column in dataGridSlobodniTermini.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void ucitajPodatke()
        {
            dataGridSlobodniTermini.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(0, false);
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSlobodniTermini.SelectedIndex != -1)
            {
                PotvrdaProzor pprozor = new PotvrdaProzor();
                pprozor.Owner = this;
                pprozor.ShowDialog();

                if (pprozor.GetPovratnaVrednost() == 1)
                {
                    if (dataGridSlobodniTermini.SelectedIndex != -1)
                    {
                        PacijentTermin selektovanTermin = (PacijentTermin)dataGridSlobodniTermini.SelectedItem;
                        String idSelektovanog = selektovanTermin.id;
                        PacijentKontroler.zakaziTerminPacijentu(idSelektovanog);
                        dataGrid.ItemsSource = PacijentKontroler.prikazPacijentovihTermina();

                        //ANTI TROL SISTEM

                        PomocnaKlasaProvere.antiTrolMetoda(idPacijenta);

                        this.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo izaberite novi termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnPretrazi_Click(object sender, RoutedEventArgs e)
        {
            int indikator = -1;
            if (rbtnLekar.IsChecked == true)
            {
                PomocnaKlasaProvere.rbtnLekarCekiran(txtPretraga.Text, indikator);
            }
            if (rbtnTermin.IsChecked == true)
            {
                PomocnaKlasaProvere.rbtnTerminCekiran(txtPretraga.Text, indikator);
            }

            if(!PomocnaKlasaProvere.proveraPretrage(indikator))
            {
                ucitajPodatke();
            }
            else
            {

                dataGridSlobodniTermini.ItemsSource = PacijentKontroler.filtrirajTermine(indikator, txtPretraga.Text);

                if (txtPretraga.Text == "")
                {
                    ucitajPodatke();
                }
            }
        }

        private void txtPretraga_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtPretraga.Text.Length == 0)
            {
                ucitajPodatke();
            }
        }
    }
}
