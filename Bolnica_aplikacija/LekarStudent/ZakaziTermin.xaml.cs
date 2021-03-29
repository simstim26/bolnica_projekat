﻿using Bolnica_aplikacija.LekarStudent;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ZakaziTermin : UserControl
    {
        private static int tipAkcije; //0-zakazivanje; 1-promena (zahteva zakazivanje i otkazivanje)
        private static PacijentTermin izabraniTermin;
        public ZakaziTermin(int tip, PacijentTermin izabrani)
        {
            InitializeComponent();
            tipAkcije = tip;
            izabraniTermin = izabrani;
            if(tipAkcije == 1)
            {
                lblIzaberi.Content = "Izaberite novi termin: ";
                btnOdabirProstorije.Content = "Promena prostorije";
            }
            ucitajPodatke();
        }
        public static int getTipAkcije()
        {
            return tipAkcije;
        }
        public static PacijentTermin getIzabraniTermin()
        {
            return izabraniTermin;
        }
        private void btnPonisti_Click(object sender, RoutedEventArgs e)
        {
            LekarProzor.getX().Content = new LekarTabovi();
            LekarTabovi.getX().Content = new PacijentInfo();
            LekarTabovi.getTab().SelectedIndex = 1;
            PacijentInfo.getPregledTab().SelectedIndex = 1;
        }

        private void ucitajPodatke()
        {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
            foreach (Termin termin in sviTermini)
            {
                if (termin.idPacijenta.Equals(""))
                {
                    DateTime trenutanDatum = DateTime.Now.AddDays(1);

                    int rezultat = DateTime.Compare(termin.datum, trenutanDatum);
                    if (rezultat == 1)
                    {
                        PacijentTermin pacijentTermin = new PacijentTermin();
                        pacijentTermin.id = termin.idTermina;
                        pacijentTermin.napomena = termin.getTipString();
                        String[] datumBezVremena = termin.datum.Date.ToString().Split(' ');
                        pacijentTermin.datum = datumBezVremena[0];
                        String[] satnicaString = termin.satnica.ToString().Split(' ');
                        String[] sat = satnicaString[1].Split(':');
                        pacijentTermin.satnica = sat[0] + ':' + sat[1];

                        foreach (Lekar lekar in JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/probaLekari.txt")))
                        {
                            if (lekar.id.Equals(termin.idLekara))
                            {
                                pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                                break;
                            }
                        }

                        foreach (Prostorija prostorija in JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt")))
                        {
                            if (termin.idProstorije.Equals(prostorija.id))
                            {
                                pacijentTermin.lokacija = prostorija.sprat + " " + prostorija.broj;
                                break;
                            }
                        }
                        if (!LekarProzor.getLekar().idSpecijalizacije.Equals("0") && pacijentTermin.napomena.Equals("Operacija"))
                        {
                            terminiPacijenta.Add(pacijentTermin);
                        }
                        else if (pacijentTermin.napomena.Equals("Pregled"))
                        {
                            terminiPacijenta.Add(pacijentTermin);
                        }
                    }
                }
            }

            dataGridZakazivanjeTermina.ItemsSource = terminiPacijenta;
        }

        private void btnPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridZakazivanjeTermina.SelectedIndex != -1)
            {
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                PacijentTermin pacijentTermin = (PacijentTermin)dataGridZakazivanjeTermina.SelectedItem; //novi termin

                if (tipAkcije == 0)
                {
                    LekarProzor.getLekar().NapraviTermin(pacijentTermin.id);
                }
                else
                {
                    LekarProzor.getLekar().AzurirajTermin(izabraniTermin.id, pacijentTermin.id);
                }

                LekarProzor.getX().Content = new LekarTabovi();
                LekarTabovi.getX().Content = new PacijentInfo();
                LekarTabovi.getTab().SelectedIndex = 1;
                PacijentInfo.getPregledTab().SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

            }

        }

        private void btnOdabirProstorije_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridZakazivanjeTermina.SelectedIndex != -1)
            {
                PacijentTermin termin = (PacijentTermin)dataGridZakazivanjeTermina.SelectedItem;
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                Termin pronadjenTermin = new Termin();
                foreach(Termin temp in sviTermini)
                {
                    if (temp.idTermina.Equals(termin.id))
                    {
                        pronadjenTermin.datum = temp.datum;
                        pronadjenTermin.idLekara = temp.idLekara;
                        pronadjenTermin.idPacijenta = temp.idPacijenta;
                        pronadjenTermin.idProstorije = temp.idProstorije;
                        pronadjenTermin.idTermina = temp.idTermina;
                        pronadjenTermin.jeZavrsen = temp.jeZavrsen;
                        pronadjenTermin.tip = temp.tip;
                        pronadjenTermin.satnica = temp.satnica;
                        break;
                    }
                }
                Content = new PrikazProstorija(pronadjenTermin);
            }
            else if (dataGridZakazivanjeTermina.SelectedIndex == -1 && tipAkcije == 1)
            {
                var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
                Termin pronadjenTermin = new Termin();
                foreach (Termin temp in sviTermini)
                {
                    if (temp.idTermina.Equals(izabraniTermin.id))
                    {
                        pronadjenTermin.datum = temp.datum;
                        pronadjenTermin.idLekara = temp.idLekara;
                        pronadjenTermin.idPacijenta = temp.idPacijenta;
                        pronadjenTermin.idProstorije = temp.idProstorije;
                        pronadjenTermin.idTermina = temp.idTermina;
                        pronadjenTermin.jeZavrsen = temp.jeZavrsen;
                        pronadjenTermin.tip = temp.tip;
                        pronadjenTermin.satnica = temp.satnica;
                        break;
                    }
                }
                Content = new PrikazProstorija(pronadjenTermin);   
            }
            else
            {
                MessageBox.Show("Potrebno je izabrati termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
