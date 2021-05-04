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
using System.Windows.Shapes;

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for PotvrdaProzor.xaml
    /// </summary>
    /// 

    public partial class PotvrdaProzor : Window
    {

        private int povratnaVrednost { get; set; }

        public PotvrdaProzor()
        {
            povratnaVrednost = 0;
            InitializeComponent();
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            this.povratnaVrednost = 1;
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.povratnaVrednost = 2;
            this.Close();
        }

        internal int GetPovratnaVrednost()
        {
            return povratnaVrednost;
        }
    }
}
