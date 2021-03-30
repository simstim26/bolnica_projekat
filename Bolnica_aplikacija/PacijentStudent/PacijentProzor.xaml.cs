using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for PacijentProzor.xaml
    /// </summary>
    public partial class PacijentProzor : Window
    {

        private String idPacijenta;

        public PacijentProzor(String id)
        {
            InitializeComponent();
            this.idPacijenta = id;
            this.DataContext = this;

            SetScreenSize();
            CenterWindow();
            SetDataGridBounds();
            SetLabelPacijentContent();

            Pacijent.ProcitajTermin(dataGridTermin, this.idPacijenta);

        }

        private void SetLabelPacijentContent()
        {
            var sviPacijenti = JsonSerializer.Deserialize<List<Pacijent>>(File.ReadAllText("Datoteke/probaPacijenti.txt"));

            foreach (Pacijent pacijent in sviPacijenti)
            {
                if (pacijent.id.Equals(this.idPacijenta))
                {
                    lblPacijent.Content = lblPacijent.Content + " " + pacijent.ime + " " + pacijent.prezime;
                    break;
                }
            }
        }

        private void SetScreenSize()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.MinHeight = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.MinWidth = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            WindowState = WindowState.Maximized;
        }

        private void SetDataGridBounds()
        {
            dataGridTermin.Loaded += SetMinWidths;
            dataGridTermin.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 300;
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
            foreach (var column in dataGridTermin.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        private void btnZakaziPregled_Click_1(object sender, RoutedEventArgs e)
        {
            PacijentZakaziTermin zakaziTermin = new PacijentZakaziTermin(dataGridTermin, this.idPacijenta);
            zakaziTermin.Owner = this;
            zakaziTermin.ShowDialog();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newHeight = e.NewSize.Height;
            dataGridTermin.Height = newHeight - 300;
        }

        private void btnOtkaziPregled_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTermin.SelectedIndex != -1)
            {
                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                String izabraniDatum = izabraniTermin.datum;

                DateTime izabraniDatumDate = Convert.ToDateTime(izabraniDatum);

                DateTime danasnjiDatum = DateTime.Today.AddDays(1);

                int rezultat = DateTime.Compare(izabraniDatumDate, danasnjiDatum);

                if(rezultat < 0)
                {
                    MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if(rezultat == 0)
                {
                    MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if(izabraniTermin.napomena.Equals("Pregled"))
                    {
                        PotvrdaProzor pprozor = new PotvrdaProzor();
                        pprozor.Owner = this;
                        pprozor.ShowDialog();

                        if (pprozor.GetPovratnaVrednost() == 1)
                            if (dataGridTermin.SelectedIndex != -1)
                            {
                                Pacijent.ObrisiTermin(dataGridTermin, this.idPacijenta);
                            }
                    }
                    else
                    {
                        MessageBox.Show("Potrebno je da se konsultujete sa Vašim lekarom kako biste otkazali termin operacije.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da otkažete.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridTermin.SelectedIndex != -1)
            {

                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                String izabraniDatum = izabraniTermin.datum;

                DateTime izabraniDatumDate = Convert.ToDateTime(izabraniDatum);

                DateTime danasnjiDatum = DateTime.Today.AddDays(1);

                int rezultat = DateTime.Compare(izabraniDatumDate, danasnjiDatum);

                if (rezultat < 0)
                {
                    MessageBox.Show("Nije moguće izvršiti izmenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (rezultat == 0)
                {
                    MessageBox.Show("Nije moguće izvršiti izmenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {

                    if (izabraniTermin.napomena.Equals("Pregled"))
                    {
                        IzmenaTerminaPacijent izmenaTermina = new IzmenaTerminaPacijent(dataGridTermin, this.idPacijenta);
                        izmenaTermina.Owner = this;
                        izmenaTermina.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Za izmenu termina operacije je potrebno da se konsultujete sa Vašim lekarom.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da izmenite.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
