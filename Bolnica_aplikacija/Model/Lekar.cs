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
      public DateTime pocetakGodisnjegOdmora { get; set; }
      public DateTime pocetakRadnogVremena { get; set; }
      public DateTime krajRadnogVremena { get; set; }
      public bool jeNaGodisnjemOdmoru { get; set; }
      public List<Notifikacija> notifikacije { get; set; }
      public void NapraviTermin(String idTermina)
      {
          /*  var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));

            foreach (Termin termin in sviTermini)
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = PrikazPacijenata.GetPacijent().id;
                    break;
                }
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviTermini, options);
            File.WriteAllText("Datoteke/Termini.txt", jsonString);*/
      }

        public List<PacijentTermin> ProcitajTermin() //read termina odredjenog pacijenta
        {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));
            List<PacijentTermin> terminiPacijenta = new List<PacijentTermin>();
           /* foreach (Termin termin in sviTermini)
            {
                if (termin.idPacijenta.Equals(PrikazPacijenata.GetPacijent().id))
                {
                    PacijentTermin pacijentTermin = new PacijentTermin();
                    pacijentTermin.id = termin.idTermina;
                    pacijentTermin.napomena = termin.getTipString();
                    pacijentTermin.datum = termin.datum.Date.ToString("dd.MM.yyyy.");
                    String satnica = termin.satnica.ToString("HH:mm");
                    pacijentTermin.satnica = satnica;

                    foreach (Lekar lekar in JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/Lekari.txt")))
                    {
                        if (lekar.id.Equals(termin.idLekara))
                        {
                            pacijentTermin.imeLekara = lekar.ime + " " + lekar.prezime;
                            break;
                        }
                    }

                    foreach (Prostorija prostorija in JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/Prostorije.txt")))
                    {
                        if (termin.idProstorije.Equals(prostorija.id))
                        {
                            pacijentTermin.lokacija = prostorija.sprat + " " + prostorija.broj;
                            break;
                        }
                    }

                    terminiPacijenta.Add(pacijentTermin);
                }
            }*/

            return terminiPacijenta;
        }
      
      public void AzurirajTermin(String idStarogTermina, String idNovogTermina)
      {
          /*  var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));
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
        File.WriteAllText("Datoteke/Termini.txt", jsonString);*/
      }
      
      public void ObrisiTermin(String idTermina)
      {/*
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));
            foreach (Termin termin in sviTermini)
            {
                if (idTermina.Equals(termin.idTermina))
                {
                    termin.idPacijenta = "";
                }
            }
            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/Termini.txt", jsonString);*/
        }
      
      public void PrikazRasporeda()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajRaspored()
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
          var sveProstorije = JsonSerializer.Deserialize<List<Prostorija>>(File.ReadAllText("Datoteke/Prostorije.txt"));
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

          List<String> nePrikazati = ProveriProstorijeZaPrikaz(termin, prostorijeZaPrikaz);
          List<Prostorija> povratnaVrednost = new List<Prostorija>();
          bool zastavica;
          foreach(Prostorija prostorija in prostorijeZaPrikaz)
          {
                zastavica = false;
                foreach(String idProstorije in nePrikazati)
                {
                    if(idProstorije.Equals(prostorija.id))
                    {
                        zastavica = true;
                        break;
                    }
                }

                if (!zastavica)
                {
                    povratnaVrednost.Add(prostorija);
                }

          }

          return povratnaVrednost;
      }
      
      private List<String> ProveriProstorijeZaPrikaz(Termin termin, List<Prostorija> prostorijeZaPrikaz)
        {
            List<String> povratnaVrednost = new List<String>();
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));

            povratnaVrednost.Add(termin.idProstorije);
            foreach (Termin temp in sviTermini)
            {
                if (DateTime.Compare(termin.datum, temp.datum) == 0)
                {
                    if (TimeSpan.Compare(termin.satnica.TimeOfDay, temp.satnica.TimeOfDay) == 0)
                    {
                        povratnaVrednost.Add(temp.idProstorije);
                    }
                    
                }
        
            }
            return povratnaVrednost;
        }
      public void AzurirajProstorijuTermina(String idTermina, String idProstorije)
      {
            var sviTermini = JsonSerializer.Deserialize<List<Termin>>(File.ReadAllText("Datoteke/Termini.txt"));
            
            foreach(Termin termin in sviTermini)
            {
                if (termin.idTermina.Equals(idTermina))
                {
                    termin.idProstorije = idProstorije;
                    break;
                }
            }

            string jsonString = JsonSerializer.Serialize(sviTermini);
            File.WriteAllText("Datoteke/Termini.txt", jsonString);  
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