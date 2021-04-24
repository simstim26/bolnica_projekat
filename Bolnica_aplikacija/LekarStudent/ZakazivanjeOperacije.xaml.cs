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
        public ZakazivanjeOperacije()
        {
            InitializeComponent();
            lblPacijent.Content = PacijentKontroler.getPacijent().ime + " " + PacijentKontroler.getPacijent().prezime;
            lblLekar.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime + " " 
                + SpecijalizacijaKontroler.nadjiSpecijalizacijuPoId(KorisnikKontroler.getLekar().idSpecijalizacije);
        }
    }
}
