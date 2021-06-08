using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.ViewModel;
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
    /// Interaction logic for Alergije.xaml
    /// </summary>
    public partial class Alergije : UserControl
    {
        public static bool aktivan;
        public Alergije(String idPacijenta)
        {
            InitializeComponent();
            this.DataContext = new AlergijaViewModel(idPacijenta);
            LekarProzor.getGlavnaLabela().Content = "Alergije";
            PacijentInfo.aktivanPacijentInfo = false;
            aktivan = true;
        }
    }
}
