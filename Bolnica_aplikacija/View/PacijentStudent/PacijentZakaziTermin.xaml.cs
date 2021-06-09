using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PacijentTemplate;
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
        private MyCalendar.Calendar.Calendar calendar;

        public PacijentZakaziTermin(DataGrid dataGrid, MyCalendar.Calendar.Calendar calendar)
        {
            InitializeComponent();

            this.dataGrid = dataGrid;
            this.calendar = calendar;
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
                        PacijentKontroler.zakaziTerminPacijentu(KorisnikKontroler.GetPacijent().id, idSelektovanog);
                        dataGrid.ItemsSource = PacijentKontroler.prikazPacijentovihTermina(KorisnikKontroler.GetPacijent().id);

                        //ANTI TROL SISTEM

                        PomocnaKlasaProvere.antiTrolMetoda(KorisnikKontroler.GetPacijent().id);

                        PomocnaKlasaKalendar.azurirajKalendar(calendar);

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
            if (rbtnLekar.IsChecked == true)
            {
                if (!Regex.IsMatch(txtPretraga.Text, @"^[\p{L}\p{M}' \.\-]+$"))
                {
                    MessageBox.Show("Molimo unesite ime u odgovarajućem formatu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ucitajPodatke();
                }
                else
                {
                    dataGridSlobodniTermini.ItemsSource = Pretrazi.izvrsiFiltriranje(new PretragaPoLekarima(), txtPretraga.Text);
                }
            }
            else if (rbtnTermin.IsChecked == true)
            {
                var formati = new[] { "dd/MM/yyyy", "d/M/yyyy", "dd.MM.yyyy", "dd.MM.yyyy.", "d.M.yyyy.", "d.M.yyyy" };
                DateTime dt;
                if (DateTime.TryParseExact(txtPretraga.Text, formati, null, System.Globalization.DateTimeStyles.None, out dt))
                {
                    dataGridSlobodniTermini.ItemsSource = Pretrazi.izvrsiFiltriranje(new PretragaPoDatumima(), txtPretraga.Text);
                }
                else
                {
                    MessageBox.Show("Molimo unesite datum u odgovarajućem formatu. Neki od podržanih formata su: dd/MM/yyyy, d/m/yyyy, dd.MM.yyyy.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ucitajPodatke();
                }
            }
            else
            {
                MessageBox.Show("Molimo izaberite prioritet pretrage.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                ucitajPodatke();
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
