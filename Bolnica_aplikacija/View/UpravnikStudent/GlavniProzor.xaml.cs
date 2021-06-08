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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        private static Grid PocetniPogled;
        
        public GlavniProzor()
        {
            InitializeComponent();
            
            PocetniPogled = this.GlavniProzorIzmena;
            //PromenaPogleda(new ProstorijePogled());
        }

       public void PromenaPogleda(UserControl userControl)
        {
            GlavniProzorIzmena.Children.Clear();
            GlavniProzorIzmena.Children.Add(userControl);
        }

        public static Grid DobaviProzorZaIzmenu()
        {
            return PocetniPogled;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            PromenaPogleda(new ProstorijePogled());  
        }

        private void listInventar_Selected(object sender, RoutedEventArgs e)
        {
            PromenaPogleda(new InventarPogled());
        }

        private void listLekovi_Selected(object sender, RoutedEventArgs e)
        {
            PromenaPogleda(new LekoviProzor());
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }
    }
}
