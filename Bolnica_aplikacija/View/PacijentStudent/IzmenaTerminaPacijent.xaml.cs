using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
using Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for IzmenaTerminaPacijent.xaml
    /// </summary>
    public partial class IzmenaTerminaPacijent : Window
    {
        private string idPacijenta;
        private DataGrid dataGrid;

        public IzmenaTerminaPacijent(DataGrid dataGrid, string idPacijenta)
        {
            InitializeComponent();

            this.idPacijenta = idPacijenta;
            this.dataGrid = dataGrid;
            dataGridSlobodniTermini.Loaded += SetMinSirina;

            ucitajSlobodneTermine();

        }

        public void SetMinSirina(object source, EventArgs e)
        {
            foreach (var column in dataGridSlobodniTermini.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void ucitajSlobodneTermine()
        {
            dataGridSlobodniTermini.ItemsSource = PacijentKontroler.ucitajSlobodneTermine(1, false);
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridSlobodniTermini.SelectedIndex != -1)
            {
                PotvrdaProzor pprozor = new PotvrdaProzor();
                pprozor.Owner = this;
                pprozor.ShowDialog();

                if (pprozor.GetPovratnaVrednost() == 1)
                    if (dataGridSlobodniTermini.SelectedIndex != -1)
                    {
                        PacijentTermin noviTermin = (PacijentTermin)dataGridSlobodniTermini.SelectedItem;

                        PacijentKontroler.azurirajTerminPacijentu(TerminKontroler.getTermin().idTermina,noviTermin.id);
                        dataGrid.ItemsSource = PacijentKontroler.prikazPacijentovihTermina();

                        //ANTI TROL

                        if(LogovanjeKontroler.proveriPostojanjeLogovanja(idPacijenta))
                        {
                            if (LogovanjeKontroler.proveriVremePostojecegLogovanja(idPacijenta))
                            {
                                LogovanjeKontroler.resetujLogovanje(idPacijenta);
                                //Console.WriteLine("USAO JE OVDE");
                            }
                            else
                                LogovanjeKontroler.uvecajBrojIzmena(idPacijenta);
                        }
                        else
                        {
                            //ako ne postoji kreira se
                            DateTime vremeIzmene = DateTime.Now;
                            //Termin termin = new Termin();
                            //termin.idTermina = idSelektovanog;
                            int brojUzastopnihIzmena = 1;
                            LogovanjeKontroler.dodajLogovanje(new PomocneKlase.Logovanje(idPacijenta, vremeIzmene, brojUzastopnihIzmena));

                        }


                        this.Close();
                    }
            }
            else
            {
                MessageBox.Show("Molimo izaberite novi termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
    }
}
