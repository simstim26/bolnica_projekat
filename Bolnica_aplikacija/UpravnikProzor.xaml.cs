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
    /// Interaction logic for UpravnikProzor.xaml
    /// </summary>
    public partial class UpravnikProzor : Window
    {
        public UpravnikProzor()
        {
            InitializeComponent();
        }

        private void tbProstorija_Click(object sender, RoutedEventArgs e)
        {
            if (tbProstorija.IsChecked == true)
            {
                tb_Copy2.IsChecked = false;
                tb_Copy1.IsChecked = false;
            }
        }

        private void tb_Copy_Click(object sender, RoutedEventArgs e)
        {
            tbProstorija.IsChecked = false;
            tb_Copy1.IsChecked = false;
        }

        private void tb_Copy1_Click(object sender, RoutedEventArgs e)
        {
            tb_Copy2.IsChecked = false;
            tbProstorija.IsChecked = false;
        }


        private void tbProstorija_Checked(object sender, RoutedEventArgs e)
        {
            gridProstorija.Visibility = Visibility.Visible;
            vodoravniPravougaonik.Visibility = Visibility.Visible;
            horizontalniPravougaonik.Height = 33;
            horizontalniPravougaonik.Margin = new System.Windows.Thickness(19, 697, 0, 0);
            

            donjiPravougaonik.Visibility = Visibility.Visible;
           // skrozDolePravougaonik.Visibility = Visibility.Hidden;
        }

        private void tbProstorija_Unchecked(object sender, RoutedEventArgs e)
        {
            if(tb_Copy1.IsChecked == false && tb_Copy2.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                donjiPravougaonik.Visibility = Visibility.Hidden;
            }
            horizontalniPravougaonik.Visibility = Visibility.Visible;
            horizontalniPravougaonik.Height = 679;
            horizontalniPravougaonik.Margin = new System.Windows.Thickness(19, 60, 0, 0);
            gridProstorija.Visibility = Visibility.Hidden;
        }

        private void tb_Copy2_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tb_Copy1.IsChecked == false && tbProstorija.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                horizontalniPravougaonik.Visibility = Visibility.Visible;
            }
        }

        private void tb_Copy1_Unchecked(object sender, RoutedEventArgs e)
        {
            if (tbProstorija.IsChecked == false && tb_Copy2.IsChecked == false)
            {
                vodoravniPravougaonik.Visibility = Visibility.Hidden;
                horizontalniPravougaonik.Visibility = Visibility.Visible;
            }
        }

        private void tb_Copy1_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
