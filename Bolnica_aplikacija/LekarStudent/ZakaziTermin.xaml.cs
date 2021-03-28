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

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ZakaziTermin : UserControl
    {

        public ZakaziTermin()
        {
            InitializeComponent();
        }

        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new LekarTabovi();
            LekarTabovi.getX().Content = new PacijentInfo();
            LekarTabovi.getTab().SelectedIndex = 1;
            PacijentInfo.getPregledTab().SelectedIndex = 1;
        }
    }
}
