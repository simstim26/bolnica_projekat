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

           // Baza.Lekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt"));
            Baza.Pacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));
            Baza.Upravnici = JsonSerializer.Deserialize<List<Upravnik>>(File.ReadAllText("Datoteke/probaUpravnici.txt"));
            Baza.Sekretari = JsonSerializer.Deserialize<List<Sekretar>>(File.ReadAllText("Datoteke/probaSekretari.txt"));
            Baza.Termini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            Baza.Korisnici = JsonSerializer.Deserialize<List<PomocnaKlasaKorisnici>>(File.ReadAllText("Datoteke/probaKorisnici.txt"));


        }

        private void btnPrijava_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = txtKorisnickoIme.Text;
            string lozinka = txtLozinka.Password.ToString();
            String[] pronadjen = Korisnik.Prijava(korisnickoIme, lozinka);

             switch (pronadjen[0])
             {
                 case "pacijent":
                    foreach(Pacijent pacijent in Baza.Pacijenti)
                    {
                        if (pacijent.id.Equals(pronadjen[1]))
                        {
                            PacijentProzor pacijentProzor = new PacijentProzor(pacijent.id);
                            this.Close();
                            pacijentProzor.ShowDialog();
                        }
                    }
                     break;
                 case "lekar":
                     foreach(Lekar lekar in JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt")))
                     {
                         if(lekar.id.Equals(pronadjen[1]))
                         {
                             LekarProzor lekarProzor = new LekarProzor(lekar);
                             this.Close();
                             lekarProzor.ShowDialog();
                         }
                     }
                     break;
                 case "sekretar":
                    foreach(Sekretar sekretar in Baza.Sekretari)
                    {
                        if (sekretar.id.Equals(pronadjen[1]))
                        {
                            SekretarProzor sekretarProzor = new SekretarProzor();
                            this.Close();
                            sekretarProzor.ShowDialog();
                        }
                    }
                     break;
                 case "upravnik":
                    foreach(Upravnik upravnik in Baza.Upravnici)
                    {
                        if (upravnik.id.Equals(pronadjen[1]))
                        {
                            UpravnikProzor upravnikProzor = new UpravnikProzor(upravnik.id);
                            this.Close();
                            upravnikProzor.ShowDialog();
                        }
                    }
                     break;
                 default:
                     lblPogresno.Visibility = Visibility.Visible;
                     break;
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
