using Bolnica_aplikacija.PacijentModel;
using Bolnica_aplikacija.PomocneKlase;
using Bolnica_aplikacija.Servis;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class LekarKontroler
    {
        public static List<PacijentTermin> prikaziSlobodneTermineZaLekara(Lekar ulogovaniLekar, int tipAkcije)
        {
            return LekarServis.getInstance().prikaziSlobodneTermineZaLekara(ulogovaniLekar, tipAkcije);
        }

        public static List<PacijentTermin> pretraziSlobodneTermineZaLekara(DateTime datum, int tipAkcije)
        {
            return LekarServis.getInstance().pretraziSlobodneTermineZaLekara(datum, tipAkcije);
        }
        public static List<PacijentTermin> prikaziZauzeteTermineZaLekara(Lekar ulogovaniLekar)
        {
            return LekarServis.getInstance().prikaziZauzeteTermineZaLekara(ulogovaniLekar);
        }
        public static List<PacijentTermin> pretraziZauzeteTermineZaLekara(Lekar lekar, DateTime prvi, DateTime drugi)
        {
            return LekarServis.getInstance().pretraziZauzeteTermineZaLekara(lekar, prvi, drugi);
        }
        public static List<Lekar> ucitajSve()
        {
            return LekarServis.getInstance().ucitajSve();
        }
        public static List<LekarSpecijalizacija> ucitajLekareSaSpecijalizacijom()
        {
            return LekarServis.getInstance().ucitajLekareSaSpecijalizacijom();
        }

        public static Lekar nadjiLekaraPoId(String idLekara)
        {
            return LekarServis.getInstance().nadjiLekaraPoId(idLekara);
        }

        public static Dictionary<String, String> popuniLekarComboBox(String idPacijenta)
        {
            return LekarServis.getInstance().popuniLekarComboBox(idPacijenta);
        }

        public static void napraviLekara(LekarDTO lekarDTO)
        {
            Lekar lekar = new Lekar(lekarDTO.idBolnice, lekarDTO.ime, lekarDTO.prezime, lekarDTO.jmbg, lekarDTO.datumRodjenja, lekarDTO.mestoRodjenja, lekarDTO.drzavaRodjenja,
                                             lekarDTO.pol, lekarDTO.adresa, lekarDTO.email, lekarDTO.brojTelefona, lekarDTO.korisnickoIme, lekarDTO.lozinka, lekarDTO.brojZdravstveneKnjizice,
                                             lekarDTO.zanimanje, lekarDTO.bracniStatus, lekarDTO.pocetakRadnogVremena, lekarDTO.krajRadnogVremena, lekarDTO.idSpecijalizacije, lekarDTO.prosecnaOcena,
                                             lekarDTO.jeNaGodisnjemOdmoru, lekarDTO.notifikacije, lekarDTO.brojSlobodnihDana, lekarDTO.brojZauzetihDana, lekarDTO.pocetakGodisnjegOdmora, lekarDTO.jeLogickiObrisan);
            
            LekarServis.getInstance().napraviLekara(lekar);
        }

        public static void izmeniLekara(LekarDTO lekarDTO)
        {
            Lekar lekar = new Lekar(lekarDTO.idBolnice, lekarDTO.ime, lekarDTO.prezime, lekarDTO.jmbg, lekarDTO.datumRodjenja, lekarDTO.mestoRodjenja, lekarDTO.drzavaRodjenja,
                                            lekarDTO.pol, lekarDTO.adresa, lekarDTO.email, lekarDTO.brojTelefona, lekarDTO.korisnickoIme, lekarDTO.lozinka, lekarDTO.brojZdravstveneKnjizice,
                                            lekarDTO.zanimanje, lekarDTO.bracniStatus, lekarDTO.pocetakRadnogVremena, lekarDTO.krajRadnogVremena, lekarDTO.idSpecijalizacije, lekarDTO.prosecnaOcena,
                                            lekarDTO.jeNaGodisnjemOdmoru, lekarDTO.notifikacije, lekarDTO.brojSlobodnihDana, lekarDTO.brojZauzetihDana, lekarDTO.pocetakGodisnjegOdmora, lekarDTO.jeLogickiObrisan);
            lekar.id = lekarDTO.id;
            LekarServis.getInstance().izmeniLekara(lekar);
        }

        public static void obrisiLekara(String idLekara)
        {
            LekarServis.getInstance().obrisiLekara(idLekara);
        }
    }
}
