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
    /// Interaction logic for IzmenaBolesti.xaml
    /// </summary>
    public partial class IzmenaBolesti : UserControl
    {
        public static bool aktivan;
        public IzmenaBolesti()
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
            aktivan = true;
            this.txtNaziv.Text = PacijentKontroler.getBolestTerapija().nazivBolesti;
            this.txtTerapija.Text = PacijentKontroler.getBolestTerapija().nazivTerapije;
            this.txtIzvestaj.Text = PacijentKontroler.getBolestTerapija().izvestaj;
        }
    }
}
