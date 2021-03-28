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
using Model;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UpravnikProzor.xaml
    /// </summary>
    public partial class UpravnikProzor : Window
    {
        private static Prostorija prostorija;
        private String lblId1;
        private String lblBrojProstorije1;
        private String lblSprat1;
        private String lblDostupnost1;
        private List<Prostorija> prostorijeNeobrisane = new List<Prostorija>();
        private List<Prostorija> prostorije = new List<Prostorija>();
        private Upravnik upravnik;
        public UpravnikProzor(String upravnikId)
        {
            InitializeComponent();

            var upravnici = JsonSerializer.Deserialize<List<Upravnik>>(File.ReadAllText("Datoteke/probaUpravnici.txt"));
            foreach(Upravnik u in upravnici)
            {
                if (u.id == upravnikId)
                {
                    lblIme.Text = u.ime + " " + u.prezime;
                    upravnik = u;
                }
            }

             

            const Int32 BufferSize = 128;
            this.DataContext = this;
            prostorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));

            foreach(Prostorija p in prostorije)
            {
                if(p.logickiObrisana == false)
                {
                    prostorijeNeobrisane.Add(p);
                }
            }
            dataGridProstorija.ItemsSource = prostorijeNeobrisane;

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
            if(tb_Copy1.IsChecked == false && tb_Copy2.IsChecked == false)
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
            Prijava prijava = new Prijava();
            this.Close();
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
                if(prostorija.dostupnost == true)
                {
                    cbDostupnostProstorije.SelectedIndex = 0;
                }
                else if(prostorija.dostupnost == false)
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

            bool pronadjenBroj = false;

            //provera da li broj prostorije vec postoji
            foreach(Prostorija p in prostorije)
            {
                if (p.broj == txtBrojProstorije.Text && p.broj != prostorija.broj)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            if (pronadjenBroj)
            {
                lblBrojPostoji.Visibility = Visibility.Visible;
            }
            else if(String.IsNullOrEmpty(txtBrojProstorije.Text) || String.IsNullOrEmpty(txtSpratProstorije.Text) || cbTipProstorijeIzmena.SelectedIndex == -1 || cbDostupnostProstorije.SelectedIndex   == -1)
            {
                lblNijePopunjenoIzmeni.Visibility = Visibility.Visible;
            }
            else
            {
                prostorija.broj = txtBrojProstorije.Text;
                prostorija.sprat = Int32.Parse(txtSpratProstorije.Text);

                if (cbTipProstorijeIzmena.SelectedIndex == 0)
                {
                    prostorija.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (cbTipProstorijeIzmena.SelectedIndex == 1)
                {
                    prostorija.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (cbTipProstorijeIzmena.SelectedIndex == 2)
                {
                    prostorija.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                if (cbDostupnostProstorije.SelectedIndex == 0)
                {
                    prostorija.dostupnost = true;
                }
                else if (cbDostupnostProstorije.SelectedIndex == 1)
                {
                    prostorija.dostupnost = false;
                }

                gridIzmeniProstoriju.Visibility = Visibility.Hidden;
                gridProstorija.Visibility = Visibility.Visible;
                dataGridProstorija.Items.Refresh();
            }

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
                prostorija.logickiObrisana = true;
                prostorijeNeobrisane.Remove(prostorija);
                Console.WriteLine(prostorijeNeobrisane.Count());
                dataGridProstorija.Items.Refresh();
            }
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            bool pronadjenBroj = false;

            //provera da li broj prostorije vec postoji
            foreach (Prostorija p in prostorije)
            {
                if (p.broj == unosBrojaProstorije.Text)
                {
                    pronadjenBroj = true;
                    break;
                }
            }

            if (pronadjenBroj)
            {
                lblBrojPostojiDodaj.Visibility = Visibility.Visible;
            }
            else if (String.IsNullOrEmpty(unosBrojaProstorije.Text) || String.IsNullOrEmpty(unosSprata.Text) || cbTipProstorije.SelectedIndex == -1)
            {
                lblNijePopunjenoDodaj.Visibility = Visibility.Visible;
            }
            else
            {
                Prostorija prostorija = new Prostorija();
                prostorija.id = (prostorije.Count + 1).ToString();
                prostorija.broj = unosBrojaProstorije.Text;
                prostorija.sprat = Int32.Parse(unosSprata.Text);
                prostorija.logickiObrisana = false;
                prostorija.dostupnost = true;
                prostorija.idBolnice = upravnik.id;
                if (cbTipProstorije.SelectedIndex == 0)
                {
                    prostorija.tipProstorije = TipProstorije.BOLNICKA_SOBA;
                }
                else if (cbTipProstorije.SelectedIndex == 1)
                {
                    prostorija.tipProstorije = TipProstorije.OPERACIONA_SALA;
                }
                else if (cbTipProstorije.SelectedIndex == 2)
                {
                    prostorija.tipProstorije = TipProstorije.SOBA_ZA_PREGLED;
                }

                prostorije.Add(prostorija);
                prostorijeNeobrisane.Add(prostorija);
                gridDodajProstoriju.Visibility = Visibility.Hidden;
                gridProstorija.Visibility = Visibility.Visible;
                dataGridProstorija.Items.Refresh();
            }
        }

        
    }
}
