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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Model;
using Bolnica_aplikacija.Kontroler;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarProzor.xaml
    /// </summary>
    public partial class LekarProzor : Window
    {
        private static ContentControl x;

        public LekarProzor()
        {
            InitializeComponent();
            this.contentControl.Content = new LekarTabovi();
            x = this.contentControl;
            lblImePrezime.Content = KorisnikKontroler.getLekar().ime + " " + KorisnikKontroler.getLekar().prezime;
            lblprosecnaOcena.Content += " " + KorisnikKontroler.getLekar().prosecnaOcena;
        }

        public static ContentControl getX()
        {
            return x;
        }

        private void meniOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }
    }
}
