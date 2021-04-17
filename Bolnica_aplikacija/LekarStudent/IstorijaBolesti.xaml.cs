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
    /// Interaction logic for IstorijaBolesti.xaml
    /// </summary>
    public partial class IstorijaBolesti : UserControl
    {
        public static bool aktivan;
        public IstorijaBolesti()
        {
            InitializeComponent();
            aktivan = true;
            PacijentInfo.aktivanPacijentInfo = false;
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            Content = new IzmenaBolesti();
            aktivan = false;
        }
    }
}
