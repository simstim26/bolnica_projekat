using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Bolnica_aplikacija.Model;
using Model;
using Bolnica_aplikacija.PacijentStudent;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Repozitorijum;
using Bolnica_aplikacija.View.UpravnikStudent;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for Prijava.xaml
    /// </summary>
    
    public partial class Prijava : Window
    {

        public Prijava()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = txtKorisnickoIme.Text;
            string lozinka = txtLozinka.Password.ToString();
            String[] ulogovaniKorisnik = KorisnikKontroler.prijava(korisnickoIme, lozinka); // [0] - tip; // [1] - id

            switch (ulogovaniKorisnik[0])
            {
                case "pacijent":

                    KorisnikKontroler.NadjiPacijenta(ulogovaniKorisnik[1]); //cuvanje ulogovanog korisnika
                    break;

                case "lekar":
                    
                    KorisnikKontroler.nadjiLekara(ulogovaniKorisnik[1]); 
                    break;

                case "sekretar":
                    
                    KorisnikKontroler.nadjiSekretara(ulogovaniKorisnik[1]);
                    break;

                case "upravnik":

                    KorisnikKontroler.NadjiUpravnika(ulogovaniKorisnik[1]);
                    ProstorijaZauzetoKontroler.zauzmiProstorije();
                    ProstorijaKontroler.pregledajProstorijeZaRenoviranje();
                    break;

                default:
                    
                    lblPogresno.Visibility = Visibility.Visible;
                    break;

            }
           
           
            Window prozor = ProzorFactoryKontroler.inicijalizujProzor(ulogovaniKorisnik[0]);

            if(prozor != null)
            {
                this.Close();
                prozor.ShowDialog();

            } else
            {
                lblPogresno.Visibility = Visibility.Visible;
            }
            
        }

        private void txtKorisnickoIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.btnPrijava.IsEnabled = !string.IsNullOrWhiteSpace(this.txtKorisnickoIme.Text) && !string.IsNullOrWhiteSpace(this.txtLozinka.Password.ToString());
            
        }

        private void txtLozinka_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.btnPrijava.IsEnabled = !string.IsNullOrWhiteSpace(this.txtKorisnickoIme.Text) && !string.IsNullOrWhiteSpace(this.txtLozinka.Password.ToString());

        }
    }
}
