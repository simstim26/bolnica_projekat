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
    /// Interaction logic for PacijentInfo.xaml
    /// </summary>
    public partial class PacijentInfo : UserControl
    {
        public static TabControl tab;
        public PacijentInfo()
        {
            InitializeComponent();
            tab = this.tabInfo;
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            Content = new PrikazPacijenata();
        }

        private void btnZakazi_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new ZakaziTermin();
        }

        public static TabControl getPregledTab()
        {
            return tab;
        }
    }
}
