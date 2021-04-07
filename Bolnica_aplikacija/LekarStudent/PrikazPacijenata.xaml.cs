using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Bolnica_aplikacija.Kontroler;
using Model;

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

            lstPacijenti.ItemsSource = PacijentKontroler.prikazPacijenata();
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if(lstPacijenti.SelectedIndex != -1)
            {
                PacijentKontroler.nadjiPacijenta(((Pacijent)lstPacijenti.SelectedItem).id);
                this.Content = new PacijentInfo();
            }

        }

    }
}
