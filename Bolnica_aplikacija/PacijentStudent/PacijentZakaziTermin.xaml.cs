﻿using Bolnica_aplikacija.PacijentModel;
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
        private DataGrid dataGrid;

        public PacijentZakaziTermin(DataGrid dataGrid, string idPacijenta)
        {
            InitializeComponent();
            this.idPacijenta = idPacijenta;
            this.dataGrid = dataGrid;
            dataGridSlobodniTermini.Loaded += SetMinSirina;

            ucitajPodatke();
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
           
            List<Termin> sviTermini;
            List<Prostorija> sveProstorije;
            List<Lekar> sviLekari;

            try
            {
                sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
                sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt"));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                sviTermini = new List<Termin>();
                sveProstorije = new List<Prostorija>();
                sviLekari = new List<Lekar>();
            }
            

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
                        if (lekar.idSpecijalizacije.Equals("0"))
                        {
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                        }
                        else
                            pacijentTermin.imeLekara = "";
                }

                if (termin.idPacijenta.Equals("") && !pacijentTermin.imeLekara.Equals(""))
                {
                   
                    DateTime trenutanDatum = DateTime.Now.AddDays(1);

                    int rezultat = DateTime.Compare(termin.datum, trenutanDatum);

                    if (rezultat == 1)
                    {
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
            }

            dataGridSlobodniTermini.ItemsSource = terminiPacijentaIspravni;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSlobodniTermini.SelectedIndex != -1)
            {
                PotvrdaProzor pprozor = new PotvrdaProzor();
                pprozor.Owner = this;
                pprozor.ShowDialog();

                if (pprozor.GetPovratnaVrednost() == 1)
                {
                    if (dataGridSlobodniTermini.SelectedIndex != -1)
                    {

                        Pacijent.NapraviTermin(dataGridSlobodniTermini, this.dataGrid, this.idPacijenta);
                        this.Close();

                    }
                }
            }
            else
            {
                MessageBox.Show("Molimo izaberite novi termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
