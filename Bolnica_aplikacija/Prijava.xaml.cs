using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

            const Int32 BufferSize = 128;
            using(var fileStream = File.OpenRead("Datoteke/Korisnici.txt"))
            {
                using(var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String linija;
                    while ((linija = streamReader.ReadLine()) != null)
                    {
                        string[] sadrzaj = linija.Split('|');

                        if (korisnickoIme == sadrzaj[1] && lozinka == sadrzaj[2])
                        {

                            fileStream.Close(); 

                            if(sadrzaj[3].Equals("pacijent"))
                            {
                                Console.WriteLine("pacijent");
                                PacijentProzor pacijent = new PacijentProzor();
                                this.Close();
                                pacijent.ShowDialog();

                            }
                            else if(sadrzaj[3].Equals("lekar"))
                            {
                                Console.WriteLine("lekar");
                                LekarProzor lekar = new LekarProzor();
                                this.Close();
                                lekar.ShowDialog();
                            }
                            else if(sadrzaj[3].Equals("sekretar"))
                            {

                            }
                            else if(sadrzaj[3].Equals("upravnik"))
                            {

                            }
                            
                            break;
                        }
                    }
                }
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
