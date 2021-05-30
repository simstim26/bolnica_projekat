using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.PomocneKlase
{
    class LekarDTO 
    {
        public String korisnickoIme { get; set; }
        public String lozinka { get; set; }
        public String ime { get; set; }
        public String prezime { get; set; }
        public DateTime datumRodjenja { get; set; }
        public String adresa { get; set; }
        public String email { get; set; }
        public String brojTelefona { get; set; }
        public String jmbg { get; set; }
        public String mestoRodjenja { get; set; }
        public String drzavaRodjenja { get; set; }
        public String pol { get; set; }
        public String brojZdravstveneKnjizice { get; set; }
        public String bracniStatus { get; set; }
        public String zanimanje { get; set; }
        public String id { get; set; }
        public int brojSlobodnihDana { get; set; }
        public int brojZauzetihDana { get; set; }
        public double prosecnaOcena { get; set; }
        public String idBolnice { get; set; }
        public String idSpecijalizacije { get; set; }
        public DateTime pocetakGodisnjegOdmora { get; set; }
        public DateTime krajGodisnjegOdmora { get; set; }
        public DateTime pocetakRadnogVremena { get; set; }
        public DateTime krajRadnogVremena { get; set; }
        public bool jeNaGodisnjemOdmoru { get; set; }
        public bool jeLogickiObrisan { get; set; }
        public List<Notifikacija> notifikacije { get; set; }


        public LekarDTO(String idBolnice, String ime, String prezime, String jmbg, DateTime datumRodjenja, String mestoRodjenja, String drzavaRodjenja, String pol,
                       String adresa, String email, String telefon, String korisnickoIme, String lozinka, String brojZdravKnjizice, String zanimanje,
                       String bracnoStanje, DateTime pocetakRadnogVremena, DateTime krajRadnogVremena, String idSpecijalizacije, double ocena, bool jeNaGodisnjemOdmoru,
                       List<Notifikacija> notifikacije, int brojSlobodnihDana, int brojZauzetihDana, DateTime pocetakGodisnjegOdmora, bool jeLogickiObrisan)
        {

            this.idBolnice = idBolnice;
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.mestoRodjenja = mestoRodjenja;
            this.drzavaRodjenja = drzavaRodjenja;
            this.pol = pol;
            this.adresa = adresa;
            this.email = email;
            this.brojTelefona = telefon;
            this.korisnickoIme = korisnickoIme;
            this.lozinka = lozinka;
            this.brojZdravstveneKnjizice = brojZdravKnjizice;
            this.zanimanje = zanimanje;
            this.bracniStatus = bracnoStanje;
            this.pocetakRadnogVremena = pocetakRadnogVremena;
            this.krajRadnogVremena = krajRadnogVremena;
            this.idSpecijalizacije = idSpecijalizacije;

            this.prosecnaOcena = ocena;
            this.jeNaGodisnjemOdmoru = jeNaGodisnjemOdmoru;
            this.notifikacije = notifikacije;
            this.brojSlobodnihDana = brojSlobodnihDana;
            this.brojZauzetihDana = brojZauzetihDana;
            this.pocetakGodisnjegOdmora = pocetakGodisnjegOdmora;
            this.jeLogickiObrisan = jeLogickiObrisan;

            this.krajGodisnjegOdmora = pocetakGodisnjegOdmora.AddDays(brojZauzetihDana);


        }

        public LekarDTO()
        {

        }
    }
}
