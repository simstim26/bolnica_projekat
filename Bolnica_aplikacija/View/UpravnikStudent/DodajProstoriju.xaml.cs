using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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
using Model;

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for DodajProstoriju.xaml
    /// </summary>
    public partial class DodajProstoriju : UserControl
    {
        public DodajProstoriju()
        {
            InitializeComponent();
            unosBrojaProstorije.Clear();
            unosSprata.Clear();
            cbTipProstorije.SelectedIndex = -1;
            lblBrojPostojiDodaj.Visibility = Visibility.Hidden;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            Prostorija prostorija = new Prostorija();
            ProstorijaKontroler.NapraviProstoriju(prostorija);
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }
    }
}
