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
    /// Interaction logic for IzmeniProstoriju.xaml
    /// </summary>
    public partial class IzmeniProstoriju : UserControl
    {
        public IzmeniProstoriju()
        {
            InitializeComponent();
            lblBrojPostoji.Visibility = Visibility.Hidden;
            Prostorija prostorija = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
            ProstorijaKontroler.pregledajProstorijeZaRenoviranje();

            gridIzmeniProstoriju.Visibility = Visibility.Visible;

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

        private void btnPotvrdiIzmenu_Click(object sender, RoutedEventArgs e)
        {
            ProstorijaKontroler.AzurirajProstoriju((Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem);
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }

        private void btnOtkaziIzmeni_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }
    }
}
