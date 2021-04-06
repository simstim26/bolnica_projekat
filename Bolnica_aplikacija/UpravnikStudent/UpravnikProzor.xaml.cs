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
using System.Windows.Shapes;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.Servis;
using Model;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UpravnikProzor.xaml
    /// </summary>
    public partial class UpravnikProzor : Window
    {
        private static UpravnikProzor instance = null;

        public static UpravnikProzor getInstance()
        {
            if(instance == null)
            {
                instance = new UpravnikProzor();
            }
            
            return instance;  
        }

        private static Prostorija prostorija;
        private String lblId1;
        private String lblBrojProstorije1;
        private String lblSprat1;
        private String lblDostupnost1;
        private List<Prostorija> prostorijeNeobrisane = new List<Prostorija>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private static Upravnik upravnik;

        private UpravnikProzor()
        {
            InitializeComponent();
            lblIme.Text = KorisnikKontroler.GetUpravnik().ime + " " + KorisnikKontroler.GetUpravnik().prezime;
            dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajSve();
        }

        private void tbProstorija_Click(object sender, RoutedEventArgs e)
        {
            if (tbProstorija.IsChecked == true)
            {
                tb_Copy2.IsChecked = false;
                tb_Copy1.IsChecked = false;
            }
        }

        private void tb_Copy_Click(object sender, RoutedEventArgs e)
        {
            tbProstorija.IsChecked = false;
            tb_Copy1.IsChecked = false;
        }

        private void tb_Copy1_Click(object sender, RoutedEventArgs e)
        {
            tb_Copy2.IsChecked = false;
            tbProstorija.IsChecked = false;
        }


        private void tbProstorija_Checked(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Visible;
            vodoravniPravougaonik.Visibility = Visibility.Visible;
            horizontalniPravougaonik.Height = 33;
            horizontalniPravougaonik.Margin = new System.Windows.Thickness(19, 697, 0, 0);


            donjiPravougaonik.Visibility = Visibility.Visible;
        }

        private void tbProstorija_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tb_Copy1.IsChecked == false && tb_Copy2.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                donjiPravougaonik.Visibility = Visibility.Hidden;
            }
            horizontalniPravougaonik.Visibility = Visibility.Visible;
            horizontalniPravougaonik.Height = 679;
            horizontalniPravougaonik.Margin = new System.Windows.Thickness(19, 60, 0, 0);
            gridProstorija.Visibility = Visibility.Hidden;
        }

        private void tb_Copy2_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tb_Copy1.IsChecked == false && tbProstorija.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                horizontalniPravougaonik.Visibility = Visibility.Visible;
            }
        }

        private void tb_Copy1_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tbProstorija.IsChecked == false && tb_Copy2.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                horizontalniPravougaonik.Visibility = Visibility.Visible;
            }
        }

        private void tb_Copy1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            var prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/Prostorije.txt"));
            ProstorijaKontroler.upisi(prostorije);

            Prijava prijava = new Prijava();
            this.Close();
            instance = null;
            prijava.ShowDialog();

        }

        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Hidden;
            gridDodajProstoriju.Visibility = Visibility.Visible;
            unosBrojaProstorije.Clear();
            unosSprata.Clear();
            cbTipProstorije.SelectedIndex = -1;
            lblBrojPostojiDodaj.Visibility = Visibility.Hidden;
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            gridDodajProstoriju.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
        }

        private void btnIzmeniProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorija.SelectedIndex != -1)
            {
                lblBrojPostoji.Visibility = Visibility.Hidden;
                prostorija = (Prostorija)dataGridProstorija.SelectedItem;

                lblId1 = lblId.Text;
                gridIzmeniProstoriju.Visibility = Visibility.Visible;


                lblId.Text += prostorija.id;
                txtBrojProstorije.Text = prostorija.broj;
                txtSpratProstorije.Text = prostorija.sprat.ToString();

                //Prikaz combo boxa za dostupnost--------------------
                if (prostorija.dostupnost == true)
                {
                    cbDostupnostProstorije.SelectedIndex = 0;
                }
                else if (prostorija.dostupnost == false)
                {
                    cbDostupnostProstorije.SelectedIndex = 1;
                }


                //Prikaz combo boxa za tip prostorije----------------
                if (prostorija.tipProstorije == TipProstorije.BOLNICKA_SOBA)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 0;
                }
                else if (prostorija.tipProstorije == TipProstorije.OPERACIONA_SALA)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 1;
                }
                else if (prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                {
                    cbTipProstorijeIzmena.SelectedIndex = 2;
                }
            }
        }

        private void btnPotvrdiIzmenu_Click(object sender, RoutedEventArgs e)
        {

            upravnik.AzurirajProstoriju(prostorija);
            lblId.Text = lblId1;

        }

        private void btnOtkaziIzmeni_Click(object sender, RoutedEventArgs e)
        {
            gridIzmeniProstoriju.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
            lblId.Text = lblId1;
        }

        private void btnIzbrisiProstoriju_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProstorija.SelectedIndex != -1)
            {
                Console.WriteLine(prostorijeNeobrisane.Count);
                prostorija = (Prostorija)dataGridProstorija.SelectedItem;
                upravnik.ObrisiProstoriju(prostorija.id);
                dataGridProstorija.ItemsSource = upravnik.ProcitajProstoriju();

            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = new Prostorija();
            ProstorijaKontroler.NapraviProstoriju(prostorija);
            dataGridProstorija.ItemsSource = ProstorijaKontroler.ucitajSve();


        }
    }
}
