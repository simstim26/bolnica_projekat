using Bolnica_aplikacija.Kontroler;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Bolnica_aplikacija.View.LekarStudent
{
    /// <summary>
    /// Interaction logic for IzmenaBLecenja.xaml
    /// </summary>
    public partial class IzmenaBLecenja : UserControl
    {
        public static bool aktivan;
        private static FrameworkElement fm = new FrameworkElement();
        public IzmenaBLecenja(String idPacijenta)
        {
            InitializeComponent();
            aktivan = true ;
            PacijentInfo.aktivanPacijentInfo = false;
            LekarProzor.getGlavnaLabela().Content = "Bolničko lečenje";

            lblDatumPocekta.Content = BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(idPacijenta).datumPocetka.ToString("dd.MM.yyyy.");
            lblTrenutnaProstorija.Content = BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(idPacijenta).bolnickaSoba.sprat + " " + BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(idPacijenta).bolnickaSoba.broj;
            txtTrajanje.Text = BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta(idPacijenta).trajanje.ToString();
            dataGridBolnickeSobe.ItemsSource = ProstorijaKontroler.pronadjiSlobodneBolnickeSobe();
            fm.DataContext = idPacijenta;
        }

        public static FrameworkElement getFM()
        {
            return fm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BolnickoLecenjeKontroler.zavrsiBolnickoLecenje((String)fm.DataContext);
            aktivan = false;
            Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                   ((String[])PacijentInfo.getFM().DataContext)[1]);
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridBolnickeSobe.SelectedIndex != -1)
                BolnickoLecenjeKontroler.azurirajProstoriju((String)fm.DataContext, ((Prostorija)dataGridBolnickeSobe.SelectedItem).id);

            BolnickoLecenjeKontroler.azurirajTrajanje((String)fm.DataContext, Convert.ToInt32(txtTrajanje.Text));

            Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                 ((String[])PacijentInfo.getFM().DataContext)[1]);
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            aktivan = false;
            Content = new PacijentInfo(((String[])PacijentInfo.getFM().DataContext)[0],
                   ((String[])PacijentInfo.getFM().DataContext)[1]);
        }

        private void txtTrajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            Regex rx = new Regex("[^0-9]+");
            if (rx.IsMatch(txtTrajanje.Text) || String.IsNullOrWhiteSpace(txtTrajanje.Text))
            {
                btnPotvrdi.IsEnabled = false;
                lblGreska.Content = "*U unosu moraju biti samo brojevi!";
                lblGreska.Visibility = Visibility.Visible;
            }
            else
            {
                if (Convert.ToInt32(txtTrajanje.Text) < BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta((String)fm.DataContext).trajanje)
                {
                    btnPotvrdi.IsEnabled = false;
                    lblGreska.Content = "*Broj mora biti veći od " + BolnickoLecenjeKontroler.nadjiBolnickoLecenjeZaPacijenta((String)fm.DataContext).trajanje + " !";
                    lblGreska.Visibility = Visibility.Visible;

                }
                else
                {
                    btnPotvrdi.IsEnabled = true;
                    lblGreska.Visibility = Visibility.Hidden;

                }
            }
        }
    }
}
