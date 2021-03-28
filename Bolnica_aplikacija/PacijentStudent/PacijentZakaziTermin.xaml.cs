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
    /// Interaction logic for PacijentZakaziTermin.xaml
    /// </summary>
    public partial class PacijentZakaziTermin : Window
    {
        private string idPacijenta;

        public PacijentZakaziTermin(string idPacijenta)
        {
            InitializeComponent();
            this.idPacijenta = idPacijenta;
            dataGridSlobodniTermini.Loaded += SetMinSirina;

        }

        public void SetMinSirina(object source, EventArgs e)
        {
            foreach(var column in dataGridSlobodniTermini.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        public void ucitajPodatke()
        {
            //ISPRAVITI DA UCITA NORMALNO


            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            var sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
            var sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt"));

            List<Termin> terminiPacijenta = new List<Termin>();
            List<PacijentTermin> terminiPacijentaIspravni = new List<PacijentTermin>();

            foreach (Termin termin in sviTermini)
            {
                PacijentTermin pacijentTermin = new PacijentTermin();
                foreach (Prostorija prostorija in sveProstorije)
                {
                    if (prostorija.id.Equals(termin.idProstorije))
                        pacijentTermin.lokacija = "Sprat " + prostorija.sprat + ", broj " + prostorija.broj;

                }

                foreach (Lekar lekar in sviLekari)
                {
                    if (lekar.id.Equals(termin.idLekara))
                    {
                        pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                    }
                }

                if (termin.idPacijenta.Equals(this.idPacijenta))
                {
                    //terminiPacijenta.Add(termin);
                    String[] datumBezVremena = termin.datum.Date.ToString().Split(' ');
                    pacijentTermin.datum = datumBezVremena[0];
                    switch (termin.tip)
                    {
                        case TipTermina.OPERACIJA: pacijentTermin.napomena = "Operacija"; break;
                        case TipTermina.PREGLED: pacijentTermin.napomena = "Pregled"; break;
                        default: break;
                    }
                    String[] satnicaString = termin.satnica.ToString().Split(' ');
                    String[] sat = satnicaString[1].Split(':');
                    pacijentTermin.satnica = sat[0] + ':' + sat[1];
                    pacijentTermin.id = termin.idTermina;

                    terminiPacijentaIspravni.Add(pacijentTermin);

                }
            }

            dataGridSlobodniTermini.ItemsSource = terminiPacijentaIspravni;
        }

    }
}
