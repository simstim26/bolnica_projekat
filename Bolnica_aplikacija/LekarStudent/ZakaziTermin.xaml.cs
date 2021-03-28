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
        private int tipAkcije; //0-zakazivanje; 1-promena (zahteva zakazivanje i otkazivanje)
        private PacijentTermin izabraniTermin;
        public ZakaziTermin(int tipAkcije, PacijentTermin izabraniTermin)
        {
            InitializeComponent();
            this.tipAkcije = tipAkcije;
            this.izabraniTermin = izabraniTermin;
            ucitajPodatke();
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
                    else if(pacijentTermin.napomena.Equals("Pregled"))
                    {
                        terminiPacijenta.Add(pacijentTermin);
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
                PacijentTermin pacijentTermin = (PacijentTermin)dataGridZakazivanjeTermina.SelectedItem;

                if (tipAkcije == 0)
                {
                    foreach (Termin termin in sviTermini)
                    {
                        if (pacijentTermin.id.Equals(termin.idTermina))
                        {
                            termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                            break;
                        }
                    }
                }
                else
                {
                    foreach(Termin termin in sviTermini)
                    {
                        if (izabraniTermin.id.Equals(termin.idTermina)) //otkazivanje starog termina
                        {
                            termin.idPacijenta = "";
                        }

                        if(pacijentTermin.id.Equals(termin.idTermina))
                        {
                            termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                        }
                    }
                }
                string jsonString = JsonSerializer.Serialize(sviTermini);
                File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
            }

            LekarProzor.getX().Content = new LekarTabovi();
            LekarTabovi.getX().Content = new PacijentInfo();
            LekarTabovi.getTab().SelectedIndex = 1;
            PacijentInfo.getPregledTab().SelectedIndex = 1;
        }
    }
}
