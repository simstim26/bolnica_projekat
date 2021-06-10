using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
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

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for Potvrda.xaml
    /// </summary>
    public partial class Potvrda : Window
    {
        String ime;
        public Potvrda(String ime)
        {
            InitializeComponent();
            this.ime = ime;
            textBlockBrisanje.Text = ime + "?";
            if (ime == "prostoriju")
            {
                (ProstorijePogled.dobaviGridProstorija()).Opacity = 0.50;
            }
            if (ime == "stavku")
            {
                (InventarPogled.dobaviGridInventar()).Opacity = 0.50;
            }
            if (ime == "odbačen lek")
            {
                (OdbijeniLekovi.dobaviGridOdbijeniLekovi()).Opacity = 0.50;
            }
            if (ime == "lek")
            {
                (LekoviProzor.dobaviGridLekova()).Opacity = 0.50;
            }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (ime == "prostoriju")
            {
                var prostorija = (Prostorija)ProstorijePogled.dobaviDataGridProstorija().SelectedItem;
                ProstorijaKontroler.ObrisiProstoriju(prostorija.id);
                this.Close();
                (ProstorijePogled.dobaviGridProstorija()).Opacity = 1;
                ProstorijePogled.dobaviDataGridProstorija().ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
                
            }
            if (ime == "stavku")
            {
                StavkaKontroler.IzbrisiStavku((Stavka)InventarPogled.dobaviDataGridInventar().SelectedItem);
                InventarPogled.dobaviDataGridInventar().ItemsSource = StavkaKontroler.UcitajNeobrisaneStavke();
                this.Close();
                (InventarPogled.dobaviGridInventar()).Opacity = 1;
            }
            if (ime == "odbačen lek")
            {
                LekKontroler.fizickiObrisiLekZaOdbacivanje((LekZaOdobravanje)OdbijeniLekovi.dobaviDataGridOdbijenihLekova().SelectedItem);
                OdbijeniLekovi.dobaviDataGridOdbijenihLekova().ItemsSource = LekKontroler.ucitajOdbaceneLekove();
                OdbijeniLekovi.dobaviDataGridOdbijenihLekova().Items.Refresh();
                this.Close();
                (OdbijeniLekovi.dobaviGridOdbijeniLekovi()).Opacity = 1;
            }
            if (ime == "lek")
            {
                Lek lek = (Lek)LekoviProzor.dobaviDataGridLekova().SelectedItem;
                List<Lek> lekovi = (List<Lek>)LekoviProzor.dobaviDataGridLekova().ItemsSource;
                lekovi.Remove(lek);
                LekoviProzor.dobaviDataGridLekova().ItemsSource = lekovi;
                LekoviProzor.dobaviDataGridLekova().Items.Refresh();
                this.Close();
                (LekoviProzor.dobaviGridLekova()).Opacity = 1;
            }
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (ime == "prostoriju")
            {
                (ProstorijePogled.dobaviGridProstorija()).Opacity = 1;
                this.Close();
            }
            if (ime == "stavku")
            {
                this.Close();
                (InventarPogled.dobaviGridInventar()).Opacity = 1;
            }
            if (ime == "odbačen lek")
            {
                this.Close();
                (OdbijeniLekovi.dobaviGridOdbijeniLekovi()).Opacity = 1;
            }
            if (ime == "lek")
            {
                this.Close();
                (LekoviProzor.dobaviGridLekova()).Opacity = 1;
            }
        }
    }
}
