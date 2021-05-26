using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
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
using System.Windows.Shapes;

namespace Bolnica_aplikacija.View.PacijentStudent
{
    /// <summary>
    /// Interaction logic for Obavestenje.xaml
    /// </summary>
    public partial class Obavestenje : Window
    {
        DataGrid dataGrid;
        int indikator;

        public Obavestenje(DataGrid dataGrid, int indikator)
        {
            InitializeComponent();
            this.dataGrid = dataGrid;
            this.indikator = indikator;

            popuniPonavljanje();
            popuniPodatke(indikator);

        }

        private void popuniPonavljanje()
        {
            comboBoxPonavljanje.Items.Add("Nikada");
            comboBoxPonavljanje.Items.Add("Jednom");
            comboBoxPonavljanje.Items.Add("Svaki dan");
            comboBoxPonavljanje.SelectedIndex = 0;
        }

        private void popuniPodatke(int indikator)
        {
            if(indikator == 1)
            {
                Notifikacija notifikacijaDTO = (Notifikacija)dataGrid.SelectedItem;

                txtNaziv.Text = notifikacijaDTO.nazivNotifikacije;
                txtVreme.Text = notifikacijaDTO.vremeNotifikovanja.ToString("HH:mm");
                txtPoruka.Text = notifikacijaDTO.porukaNotifikacije;

                switch(notifikacijaDTO.ponavljanje)
                {
                    case Ponavljanje.Jednom: comboBoxPonavljanje.SelectedIndex = 0; break;
                    case Ponavljanje.Svaki_dan: comboBoxPonavljanje.SelectedIndex = 2; break;
                    default: comboBoxPonavljanje.SelectedIndex = 1; break;
                }

            }
        }

        private void btnNapravi_Click(object sender, RoutedEventArgs e)
        {
            Ponavljanje ponavljanje;

            switch(comboBoxPonavljanje.SelectedIndex)
            {
                case 0: ponavljanje = Ponavljanje.Nikada; break;
                case 1: ponavljanje = Ponavljanje.Jednom; break;
                default: ponavljanje = Ponavljanje.Svaki_dan; break;
            }

            DateTime vreme = Convert.ToDateTime(txtVreme.Text);

            if(indikator == 1)
            {
                NotifikacijaKontroler.azurirajNotifikacijuDTO(new NotifikacijaDTO(Convert.ToString(NotifikacijaKontroler.ucitajSve().Count + 1), txtNaziv.Text, vreme, txtPoruka.Text, KorisnikKontroler.GetPacijent().id, DateTime.Now, false), ponavljanje);


            }
            else
            {
                NotifikacijaKontroler.pacijentNapraviNotifikaciju(new NotifikacijaDTO(Convert.ToString(NotifikacijaKontroler.ucitajSve().Count + 1), txtNaziv.Text, vreme, txtPoruka.Text, KorisnikKontroler.GetPacijent().id, DateTime.Now, false), ponavljanje);

            }
       
            MessageBox.Show("Podsetnik je uspešno kreiran.", "Kreiranje uspešno", MessageBoxButton.OK, MessageBoxImage.Information);

            dataGrid.ItemsSource = NotifikacijaKontroler.getNoveNotifikacijeKorisnika(KorisnikKontroler.GetPacijent().id);

            this.Close();
        }

        private void comboBoxPonavljanje_DropDownClosed(object sender, EventArgs e)
        {
            if (comboBoxPonavljanje.SelectedIndex == 0)
            {
                txtVreme.IsEnabled = false;
            }
            else
                txtVreme.IsEnabled = true;
        }
    }
}
