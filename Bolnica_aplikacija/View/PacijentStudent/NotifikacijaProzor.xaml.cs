using Bolnica_aplikacija.Kontroler;
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

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for NotifikacijaProzor.xaml
    /// </summary>
    public partial class NotifikacijaProzor : Window
    {
        
        private String idNotifikacije;
        //private String idKorisnika;

        public NotifikacijaProzor(String idNotifikacije, String idKorisnika)
        {
            InitializeComponent();

            this.idNotifikacije = idNotifikacije;
            CenterWindow();
            popuniNotifikaciju();
            
        }

        private void CenterWindow()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        void popuniNotifikaciju()
        {
            lblNazivObavestenja.Content = NotifikacijaKontroler.getNotifikacija(idNotifikacije, KorisnikKontroler.GetPacijent().id).nazivNotifikacije;
            lblVremeObavestenja.Content = NotifikacijaKontroler.getNotifikacija(idNotifikacije, KorisnikKontroler.GetPacijent().id).vremeNotifikovanja.ToString("HH:mm");
            txtPoruka.Text = NotifikacijaKontroler.getNotifikacija(idNotifikacije, KorisnikKontroler.GetPacijent().id).porukaNotifikacije;
        }

        private void btnPotvrda_Click(object sender, RoutedEventArgs e)
        {
            if(checkBoxProcitano.IsChecked == true)
            {
                NotifikacijaKontroler.procitajNotifikaciju(idNotifikacije, KorisnikKontroler.GetPacijent().id);
            }
        }
    }
}
