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
    /// Interaction logic for UvidUTerapije.xaml
    /// </summary>
    public partial class UvidUTerapije : UserControl
    {
        public static bool aktivan;
        public UvidUTerapije()
        {
            InitializeComponent();
            LekarProzor.getGlavnaLabela().Content = "Uvid u terapije";
            PacijentInfo.aktivanPacijentInfo = false;
            aktivan = true;
        }
    }
}
