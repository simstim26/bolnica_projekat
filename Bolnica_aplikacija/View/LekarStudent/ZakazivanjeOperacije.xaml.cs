using Bolnica_aplikacija.Kontroler;
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

namespace Bolnica_aplikacija.LekarStudent
{
    /// <summary>
    /// Interaction logic for ZakazivanjeOperacije.xaml
    /// </summary>
    public partial class ZakazivanjeOperacije : UserControl
    {
        public static bool aktivan { get; set; }
        private static Grid odabirProstorije;
        private static Grid prikazInventara;
        public ZakazivanjeOperacije()
        {
            InitializeComponent();
            lblPacijent.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + " " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
            aktivan = true;
            ZakaziTermin.aktivan = false;
            LekarProzor.getPretraga().Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";
            odabirProstorije = gridOdabirProstorija;
            prikazInventara = gridPrikazInventara;
        }

        public static void podesiKretanjeZaNazad()
        {
            if(odabirProstorije.Visibility == Visibility.Visible)
            {
                odabirProstorije.Visibility = Visibility.Hidden;
                LekarProzor.getGlavnaLabela().Content = "Zakazivanje operacije";

            }
            else if(prikazInventara.Visibility == Visibility.Visible)
            {
                prikazInventara.Visibility = Visibility.Hidden;
                odabirProstorije.Visibility = Visibility.Visible;
                LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";

            }
            else
            {
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije());
                aktivan = false;
            }
        }
        private void btnDodajProstoriju_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Visible;
            LekarProzor.getGlavnaLabela().Content = "Odabir prostorije";

        }

        private void btnPrikazInventara_Click(object sender, RoutedEventArgs e)
        {
            gridOdabirProstorija.Visibility = Visibility.Hidden;
            LekarProzor.getGlavnaLabela().Content = "Prikaz inventara";

            gridPrikazInventara.Visibility = Visibility.Visible;
        }
    }
}
