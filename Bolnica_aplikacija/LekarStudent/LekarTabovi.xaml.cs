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
        //private static TabItem tabRad;
        public LekarTabovi()
        {
            InitializeComponent();
            this.contentControl.Content = new PrikazPacijenata();
            x = this.contentControl;
            tab = this.lekarTab;
        }

        public static ContentControl getX()
        {
            return x;
        }

        public static TabControl getTab()
        {
            return tab;
        }

    }
}
