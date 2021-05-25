using Bolnica_aplikacija.Model;
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
    class LekKontroler
    {
        public static bool proveriLekoveZaOdobravanjeZaLogovanogLekara(String idLekara)
        {
            return LekServis.getInstance().proveriLekoveZaOdobravanjeZaLogovanogLekara(idLekara);
        }
        public static void azurirajOdobravanje(LekZaOdobravanje lekZaAzuriranje)
        {
            LekServis.getInstance().azurirajOdobravanje(lekZaAzuriranje);
        }

        public static List<LekZaOdobravanje> nadjiLekoveZaOdobravanjeZaLogovanogLekara(String idLekara)
        {
            return LekServis.getInstance().nadjiLekoveZaOdobravanjeZaLogovanogLekara(idLekara);
        }

        public static List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            return LekServis.getInstance().ucitajSveSemTrenutnogNaTerapiji(idLeka);
        }
        public static Lek nadjiLekPoId(String idLeka)
        {
            return LekServis.getInstance().nadjiLekPoId(idLeka);
        }
        public static List<Lek> ucitajSve()
        {
            return LekServis.getInstance().ucitajSve();
        }

        public static List<TipLeka> tipLeka()
        {
            return LekServis.getInstance().tipLeka();
        }

        public static List<NacinUpotrebe> nacinUpotrebeLeka()
        {
            return LekServis.getInstance().nacinUpotrebeLeka();
        }

        public static void napraviLek(LekZaOdobravanje lek)
        {
            LekServis.getInstance().napraviLek(lek);
        }

        public static void napraviCeoLek(LekZaOdobravanje lek)
        {
            LekServis.getInstance().napraviLek(lek);
        }

        public static void dodajLekuLekare(List<String> idLekari, LekZaOdobravanje lek)
        {
            LekServis.getInstance().dodajLekuLekare(idLekari, lek);
        }

        public static void dodajLek(LekZaOdobravanje lekZaDodavanje)
        {
            LekServis.getInstance().dodajLek(lekZaDodavanje);
        }

        public static void odbacivanjeLeka(LekZaOdobravanje lekZaOdbacivanje)
        {
            LekServis.getInstance().odbacivanjeLeka(lekZaOdbacivanje);
        }

        public static void azurirajLek(Lek lekZaAzuriranje)
        {
            LekServis.getInstance().azurirajLek(lekZaAzuriranje);
        }

        public static void izbrisiSastojak(String idLeka, String sastojak)
        {
            LekServis.getInstance().izbrisiSastojak(idLeka, sastojak);
        }

        public static void dodajSastojak(String idLeka, String sastojak)
        {
            LekServis.getInstance().dodajSastojak(idLeka, sastojak);
        }

        public static List<Lek> ucitajSveLekoveBezZamenskih(String idLeka)
        {
            return LekServis.getInstance().ucitajSveLekoveBezZamenskih(idLeka);
        }

        public static void dodajZamenskiLek(String idLek, Lek zamenskiLek)
        {
            LekServis.getInstance().dodajZamenskiLek(idLek, zamenskiLek);
        }

        public static void obrisiZamenskiLek(String idLek, String idZamenskogLeka)
        {
            LekServis.getInstance().obrisiZamenskiLek(idLek, idZamenskogLeka);
        }

        public static List<LekZaOdobravanje> ucitajOdbaceneLekove()
        {
            return LekServis.getInstance().ucitajOdbaceneLekove();
        }

        public static void upisiOdbaceneLekove(List<LekZaOdobravanje> lekoviZaOdbacivanje)
        {
            LekServis.getInstance().upisiOdbaceneLekove(lekoviZaOdbacivanje);
        }

        public static void fizickiObrisiLekZaOdbacivanje(LekZaOdobravanje lekZaOdbacivanje)
        {
            LekServis.getInstance().fizickiObrisiLekZaOdbacivanje(lekZaOdbacivanje);
        }

        public static List<LekZaOdobravanje> ucitajLekoveZaOdobravanje()
        {
            return LekServis.getInstance().ucitajLekoveZaOdobravanje();
        }

        public static void napraviLekara(LekarDTO lekarDTO)
        {
            Lekar lekar = new Lekar(lekarDTO.idBolnice, lekarDTO.ime, lekarDTO.prezime, lekarDTO.jmbg, lekarDTO.datumRodjenja,
                                    lekarDTO.mestoRodjenja, lekarDTO.drzavaRodjenja, lekarDTO.pol, lekarDTO.adresa, lekarDTO.email,
                                    lekarDTO.brojTelefona, lekarDTO.korisnickoIme, lekarDTO.lozinka, lekarDTO.brojZdravstveneKnjizice,
                                    lekarDTO.zanimanje, lekarDTO.bracniStatus, lekarDTO.pocetakRadnogVremena, lekarDTO.krajRadnogVremena, lekarDTO.idSpecijalizacije);
            
            LekarServis.getInstance().napraviLekara(lekar);
        }

        public static void obrisiLekara(String idLekara)
        {
            LekarServis.getInstance().obrisiLekara(idLekara);
        }
    }
}
