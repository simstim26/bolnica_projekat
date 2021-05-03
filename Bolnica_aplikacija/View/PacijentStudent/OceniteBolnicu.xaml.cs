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

namespace Bolnica_aplikacija.View.PacijentStudent
{
    /// <summary>
    /// Interaction logic for OceniteBolnicu.xaml
    /// </summary>
    public partial class OceniteBolnicu : Window
    {
        private String idPacijenta;

        public OceniteBolnicu(String idPacijenca)
        {
            InitializeComponent();
            this.idPacijenta = idPacijenta;
            popuniPolja();
        }

        private void popuniPolja()
        {
            comboBoxOcena.Items.Add("1 (nedovoljan)");
            comboBoxOcena.Items.Add("2 (dovoljan)");
            comboBoxOcena.Items.Add("3 (dobar)");
            comboBoxOcena.Items.Add("4 (vrlo dobar)");
            comboBoxOcena.Items.Add("5 (odlican)");
            comboBoxOcena.SelectedIndex = 0;
        }
    }
}
