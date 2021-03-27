using System;
using System.Collections.Generic;
using System.IO;
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
using Model;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UpravnikProzor.xaml
    /// </summary>
    public partial class UpravnikProzor : Window
    {
        private static Prostorija prostorija;
        String lblId1;
        String lblBrojProstorije1;
        String lblSprat1;
        String lblDostupnost1;
        public System.Collections.ObjectModel.ObservableCollection<Model.Prostorija> Prostorije
        {
            get;
            set;
        }

        public UpravnikProzor()
        {
            InitializeComponent();

            const Int32 BufferSize = 128;
            this.DataContext = this;
            Prostorije = new System.Collections.ObjectModel.ObservableCollection<Model.Prostorija>();

            using (var fileStream = File.OpenRead("Datoteke/Prostorije.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String linija;
                    while ((linija = streamReader.ReadLine()) != null)
                    {
                        string[] sadrzaj = linija.Split('|');
                        Model.Prostorija prostorija = new Model.Prostorija();
                        prostorija.id = sadrzaj[0];
                        prostorija.tipProstorije = Prostorija.ConvertTip(sadrzaj[2]);
                        //Console.WriteLine(Prostorija.ConvertTip(sadrzaj[2]));
                        prostorija.broj = sadrzaj[3];
                        prostorija.sprat = Int32.Parse(sadrzaj[4]);
                        prostorija.dostupnost = Boolean.Parse(sadrzaj[5]);
                        prostorija.logickiObrisana = Boolean.Parse(sadrzaj[6]);
                        if(prostorija.logickiObrisana == false)
                        {
                            Prostorije.Add(prostorija);
                        }

                    }

                    dataGridProstorija.ItemsSource = Prostorije;
                }
            }
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
           // skrozDolePravougaonik.Visibility = Visibility.Hidden;
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
            Console.WriteLine(dataGridProstorija.SelectedIndex);
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
                prostorija = (Prostorija)dataGridProstorija.SelectedItem;

                gridIzmeniProstoriju.Visibility = Visibility.Visible;
                lblId1 = lblId.Text;
                lblBrojProstorije1 = lblBrojProstorije.Text;
                lblSprat1 = lblSprat.Text;
                lblDostupnost1 = lblDostupnost.Text;

                lblId.Text += prostorija.id;
                lblBrojProstorije.Text += prostorija.broj;
                lblSprat.Text += prostorija.sprat;
                lblDostupnost.Text += prostorija.dostupnost;
            }
        }

        private void btnOtkaziIzmeni_Click(object sender, RoutedEventArgs e)
        {
            gridIzmeniProstoriju.Visibility = Visibility.Hidden;
            gridProstorija.Visibility = Visibility.Visible;
            lblId.Text = lblId1;
            lblBrojProstorije.Text = lblBrojProstorije1;
            lblSprat.Text = lblSprat1;
            lblDostupnost.Text = lblDostupnost1;
        }
    }
}
