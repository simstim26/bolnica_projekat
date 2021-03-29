using Bolnica_aplikacija;
using Bolnica_aplikacija.PacijentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Model
{
   public class Lekar : Korisnik
   {
      public String id { get; set; }
      public int brojSlobodnihDana { get; set; }
      public double prosecnaOcena { get; set; }
      public String idBolnice { get; set; }
      public String idSpecijalizacije { get; set; }
      public void NapraviTermin(String idTermina)
      {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));

            foreach (Termin termin in sviTermini)
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                    break;
                }
            }

            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
      }

        public List<PacijentTermin> ProcitajTermin() //read termina odredjenog pacijenta
        {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
            foreach (Termin termin in sviTermini)
            {
                if (termin.idPacijenta.Equals(PrikazPacijenata.GetPacijent().id))
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

                    terminiPacijenta.Add(pacijentTermin);
                }
            }

            return terminiPacijenta;
        }
      
      public void AzurirajTermin(String idStarogTermina, String idNovogTermina)
      {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            foreach (Termin termin in sviTermini)
            {
                if (idStarogTermina.Equals(termin.idTermina)) //otkazivanje starog termina
                {
                    termin.idPacijenta = "";
                }

                if (idNovogTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                }
            }
        string jsonString = JsonSerializer.Serialize(sviTermini);
        File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
      }
      
      public void ObrisiTermin(String idTermina)
      {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            foreach (Termin termin in sviTermini)
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                }
            }
            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/probaTermini.txt", jsonString);
        }
      
      public void PrikazRasporeda()
      {
         throw new NotImplementedException();
      }
      
      public void UpdateRasporeda()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazPacijenta()
      {
         throw new NotImplementedException();
      }
      
      public void UvidUZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public void IzdavanjeUputa()
      {
         throw new NotImplementedException();
      }
      
      public void IzdavanjeTerapije()
      {
         throw new NotImplementedException();
      }
      
      public List<Prostorija> PrikazProstorija(Termin termin)
      {
          var sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/probaProstorije.txt"));
          List<Prostorija> prostorijeZaPrikaz = new List<Prostorija>();
          foreach(Prostorija prostorija in sveProstorije)
          {
                if (!prostorija.logickiObrisana && prostorija.dostupnost)
                {
                    if (idSpecijalizacije != "0" && prostorija.tipProstorije != TipProstorije.BOLNICKA_SOBA && prostorija.tipProstorije != TipProstorije.GRESKA)
                    {
                        if (termin.tip == TipTermina.OPERACIJA && prostorija.tipProstorije == TipProstorije.OPERACIONA_SALA)
                        {
                            prostorijeZaPrikaz.Add(prostorija);
                        }
                        else if (termin.tip == TipTermina.PREGLED && prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                        {
                            prostorijeZaPrikaz.Add(prostorija);
                        }
                    }
                    else if (idSpecijalizacije == "0" && prostorija.tipProstorije == TipProstorije.SOBA_ZA_PREGLED)
                    {
                        prostorijeZaPrikaz.Add(prostorija);
                    }
                }
          }

          return prostorijeZaPrikaz;
      }
      public void AzurirajProstorijuTermina(String idTermina, String idProstorije)
      {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/probaTermini.txt"));
            
            foreach(Termin termin in sviTermini)
            {
                if (termin.idTermina.Equals(idTermina))
                {
                    termin.idProstorije = idProstorije;
                    break;
                }
            }

            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/probaTermini.txt", jsonString);  
      }
      public void PrikazStavki()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaStavki()
      {
         throw new NotImplementedException();
      }
      
      public int AzuriranjeInventara()
      {
         throw new NotImplementedException();
      }
      
      public Termin[] termin;
   
   }
}