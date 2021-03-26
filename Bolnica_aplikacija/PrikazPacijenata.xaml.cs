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
    /// Interaction logic for Proba.xaml
    /// </summary>
    public partial class PrikazPacijenata : UserControl
    {
        public PrikazPacijenata()
        {
            InitializeComponent();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new PacijentInfo();
        }


    }
}
