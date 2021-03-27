using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Interaction logic for PacijentProzor.xaml
    /// </summary>
    public partial class PacijentProzor : Window
    {

        private String idPacijenta;

        public ObservableCollection<PacijentTermin> Termini
        {
            get;
            set;
        }

        public PacijentProzor(String id)
        {
            InitializeComponent();
            this.idPacijenta = id;

            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.MinHeight = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.MinWidth = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            WindowState = WindowState.Maximized;

            this.DataContext = this;

            CenterWindow();

            dataGridTermin.Loaded += SetMinWidths;

            //to do: implementirati metodu za citanje iz fajla
            //dataGridTermin.ItemsSource = UcitajTermine();
            //dataGridTermin.AutoGenerateColumns = false;
            UcitajTermine();


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

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach(var column in dataGridTermin.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        //ZA MODIFIKOVATI JOS

        public void UcitajTermine()
        {
            //List<PacijentTermin> listaPacijentovihTermina = new List<PacijentTermin>();

            Termini = new ObservableCollection<PacijentTermin>();

            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("Datoteke/Termini.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String linija;
                    while ((linija = streamReader.ReadLine()) != null)
                    {
                        string[] sadrzaj = linija.Split('|');

                        if (String.Equals(this.idPacijenta, sadrzaj[5]))
                        {

                            Termin termin = new Termin();
                            if(String.Equals(sadrzaj[4],"Operacija"))
                            {
                                termin.setTipTermina(TipTermina.OPERACIJA);
                            }
                            else
                            {
                                termin.setTipTermina(TipTermina.PREGLED);
                            }

                            DateTime datum = DateTime.ParseExact(sadrzaj[1], "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                            termin.setDatum(datum);
                            DateTime satnica = DateTime.ParseExact(sadrzaj[2], "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                            termin.setSatnica(satnica);

                            termin.idProstorije = sadrzaj[3];
                            termin.idLekara = sadrzaj[6];

                            PacijentTermin pacijentTermin = new PacijentTermin();
                            pacijentTermin.setDatum(datum);
                            pacijentTermin.setSatnica(satnica);
                            pacijentTermin.setNazivProstorije("TODO");
                            pacijentTermin.setImeLekara("TODO");
                            pacijentTermin.setNapomena(sadrzaj[4]);

                            Termini.Add(pacijentTermin);
                            Console.WriteLine(pacijentTermin.getImeLekara().ToString());

                        }
                    }
                }
            }

            

        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        private void btnZakaziPregled_Click(object sender, RoutedEventArgs e)
        {
            PacijentZakaziTermin zakaziTermin = new PacijentZakaziTermin();
            zakaziTermin.Owner = Application.Current.MainWindow;
            zakaziTermin.ShowDialog();
        }

        private void btnZakaziPregled_Click_1(object sender, RoutedEventArgs e)
        {
            PacijentZakaziTermin zakazivanjeTermina = new PacijentZakaziTermin();
            zakazivanjeTermina.Owner = Application.Current.MainWindow;
            zakazivanjeTermina.ShowDialog();
        }
    }
}
