using Bolnica_aplikacija.Kontroler;
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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for InventarPogled.xaml
    /// </summary>
    public partial class InventarPogled : UserControl
    {
        public static DataGrid dataGridInventari;
        public static Grid gridStavki;
        public InventarPogled()
        {
            InitializeComponent();
            dataGridInventari = dataGridInventar;
            gridStavki = gridInventar;
            dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
        }

        private void btnPonistiFiltriranje_Click(object sender, RoutedEventArgs e)
        {
            comboBoxFiltrirajStavku.SelectedIndex = -1;
            comboBoxKolicinaFiltriranje.SelectedIndex = -1;

            dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
        }

        private void btnPretraziStavku_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxFiltrirajStavku.SelectedIndex == -1)
            {
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }

                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 2)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 0)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.ucitajDinamickeStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            else if (comboBoxFiltrirajStavku.SelectedIndex == 1)
            {
                dataGridInventar.ItemsSource = StavkaKontroler.ucitajStatickeStavke();
                dataGridInventar.Items.Refresh();
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 1)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoKoliciniOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 2)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuRastuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (comboBoxKolicinaFiltriranje.SelectedIndex == 3)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.poredjajListuStavkiPoNazivuOpadajuce((List<Stavka>)dataGridInventar.ItemsSource);
                }
                if (textBoxPretraga.Text.Length != 0)
                {
                    dataGridInventar.ItemsSource = StavkaKontroler.pretraziStavku(textBoxPretraga.Text, (List<Stavka>)dataGridInventar.ItemsSource);
                }
            }
            dataGridInventar.Items.Refresh();
        }

        public static DataGrid dobaviDataGridInventar()
        {
            return dataGridInventari;
        }

        public static Grid dobaviGridInventar()
        {
            return gridStavki;
        }

        private void btnDodajStavku_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new DodajStavku());
        }

        private void btnIzmeniStavku_Click(object sender, RoutedEventArgs e)
        {
            izaberiteStavku.Visibility = Visibility.Hidden;
            if (dataGridInventar.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new IzmeniStavku());
            }
            else
            {
                izaberiteStavku.Visibility = Visibility.Visible;
            }
        }

        private void btnObrisiStavku_Click(object sender, RoutedEventArgs e)
        {
            izaberiteStavku.Visibility = Visibility.Hidden;
            if (dataGridInventar.SelectedIndex != -1)
            {
                Potvrda potvrda = new Potvrda("stavku");
                potvrda.Show();
            }
            else
            {
                izaberiteStavku.Visibility = Visibility.Visible;
            }
        }

        private void btnPremestiUProstoriju_Click(object sender, RoutedEventArgs e)
        {
            izaberiteStavku.Visibility = Visibility.Hidden;
            if (dataGridInventar.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PremestiUProstoriju());
            }
            else
            {
                izaberiteStavku.Visibility = Visibility.Visible;
            }
        }

        private void btnPogledajProstorije_Click(object sender, RoutedEventArgs e)
        {
            izaberiteStavku.Visibility = Visibility.Hidden;
            if (dataGridInventar.SelectedIndex != -1)
            {
                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new PrikaziProstorije());
            }
            else
            {
                izaberiteStavku.Visibility = Visibility.Visible;
            }
        }
    }
}
