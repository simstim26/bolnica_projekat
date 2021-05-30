using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.ViewModel;
using Model;
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
    /// Interaction logic for LekarTabovi.xaml
    /// </summary>
    public partial class LekarTabovi : UserControl
    {
        private static ContentControl x;
        private static TabControl tab;
        private static DataGrid raspored;
        private static Grid gridRasporedPretraga;
        private static Grid gridPrikaz;
        public LekarTabovi()
        {
            InitializeComponent();
            this.DataContext = new LekarTaboviViewModel();

            raspored = this.dataRaspored;
            tab = this.lekarTab;
            gridRasporedPretraga = this.gridPretragaRaspored;
            gridPrikaz = this.gridPrikazPacijenata;
        }
        public static Grid getRasporedPretraga()
        {
            return gridRasporedPretraga;
        }
        public static Grid getPacijentiPretraga()
        {
            return gridPrikaz;
        }
        public static TabControl getTab()
        {
            return tab;
        }
    
    }
}
