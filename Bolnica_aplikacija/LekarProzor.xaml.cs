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

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarProzor.xaml
    /// </summary>
    public partial class LekarProzor : Window
    {
        public LekarProzor()
        {
            InitializeComponent();
            this.contentControl.Content = new PrikazPacijenata(); 
            
        }

        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            tabInfo.Visibility = Visibility.Visible;
            lstPacijenti.Visibility = Visibility.Hidden;
            btnInfo.Visibility = Visibility.Hidden;
            btnNazadInfo.Visibility = Visibility.Visible;
            
        }

        private void btnNazadInfo_Click(object sender, RoutedEventArgs e)
        {
            tabInfo.Visibility = Visibility.Hidden;
            lstPacijenti.Visibility = Visibility.Visible;
            btnInfo.Visibility = Visibility.Visible;
            btnNazadInfo.Visibility = Visibility.Hidden;
            *
        }*/
    }
}
