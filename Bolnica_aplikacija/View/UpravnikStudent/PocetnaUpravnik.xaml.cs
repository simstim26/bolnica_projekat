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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for PocetnaUpravnik.xaml
    /// </summary>
    public partial class PocetnaUpravnik : UserControl
    {
        public PocetnaUpravnik()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            String zalba = textBox.Text;
            PrijavaGreskeKontroler.sacuvaj(zalba);
            textBox.Text = "";
        }
    }
}
