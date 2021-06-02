using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Repozitorijum
{
    class LekarRepozitorijum
    {

        public List<Lekar> ucitajSve()
        {
            List<Lekar> sviLekari;
            try
            {
                sviLekari = JsonSerializer.Deserialize<List<Lekar>>(File.ReadAllText("Datoteke/Lekari.txt"));
            }
            catch (Exception e)
            {
                sviLekari = new List<Lekar>();
            }

            return sviLekari;
        }

        public void upisi(List<Lekar> sviLekari)
        {
            var formatiranje = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string jsonString = JsonSerializer.Serialize(sviLekari, formatiranje);
            File.WriteAllText("Datoteke/Lekari.txt", jsonString);
        }

        public void dodajLekara(Lekar lekar)
        {
            List<Lekar> sviLekari = ucitajSve();
            sviLekari.Add(lekar);
            upisi(sviLekari);
        }

        public void azurirajLekara(Lekar izmeniLekar)
        {
            Console.WriteLine("USAO SAM U REPO I ID JE " + izmeniLekar.id);
            List<Lekar> sviLekari = ucitajSve();
            foreach (Lekar lekar in sviLekari)
            {
                if (lekar.id.Equals(izmeniLekar.id))
                {
                    Console.WriteLine("NASAO LEKARA");
                    lekar.idBolnice = izmeniLekar.idBolnice;
                    lekar.ime = izmeniLekar.ime;
                    lekar.prezime = izmeniLekar.prezime;
                    lekar.jmbg = izmeniLekar.jmbg;
                    lekar.datumRodjenja = izmeniLekar.datumRodjenja;
                    lekar.mestoRodjenja = izmeniLekar.mestoRodjenja;
                    lekar.drzavaRodjenja = izmeniLekar.drzavaRodjenja;
                    lekar.pol = izmeniLekar.pol;
                    lekar.adresa = izmeniLekar.adresa;
                    lekar.email = izmeniLekar.email;
                    lekar.brojTelefona = izmeniLekar.brojTelefona;
                    lekar.korisnickoIme = izmeniLekar.korisnickoIme;
                    lekar.lozinka = izmeniLekar.lozinka;
                    lekar.brojZdravstveneKnjizice = izmeniLekar.brojZdravstveneKnjizice;
                    lekar.zanimanje = izmeniLekar.zanimanje;
                    lekar.bracniStatus = izmeniLekar.bracniStatus;
                    lekar.pocetakRadnogVremena = izmeniLekar.pocetakRadnogVremena;
                    lekar.krajRadnogVremena = izmeniLekar.krajRadnogVremena;
                    lekar.idSpecijalizacije = izmeniLekar.idSpecijalizacije;
                    lekar.prosecnaOcena = izmeniLekar.prosecnaOcena;
                    lekar.jeNaGodisnjemOdmoru = izmeniLekar.jeNaGodisnjemOdmoru;
                    lekar.notifikacije = izmeniLekar.notifikacije;
                    lekar.brojSlobodnihDana = izmeniLekar.brojSlobodnihDana;
                    lekar.brojZauzetihDana = izmeniLekar.brojZauzetihDana;
                    lekar.pocetakGodisnjegOdmora = izmeniLekar.pocetakGodisnjegOdmora;
                    lekar.krajGodisnjegOdmora = izmeniLekar.krajGodisnjegOdmora;
                    lekar.jeLogickiObrisan = izmeniLekar.jeLogickiObrisan;
                    break;
                }              
            }

            upisi(sviLekari);
        }
     
    }
}
