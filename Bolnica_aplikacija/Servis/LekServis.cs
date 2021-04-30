using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Repozitorijum;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Servis
{
    class LekServis
    {
        private static LekServis instance;
        public static LekServis getInstance()
        {
            if (instance == null)
            {
                instance = new LekServis();
            }

            return instance;
        }
        private LekRepozitorijum lekRepozitorijum = new LekRepozitorijum();

        public bool proveriLekoveZaOdobravanjeZaLogovanogLekara(String idLekara)
        {
            return nadjiLekoveZaOdobravanjeZaLogovanogLekara(idLekara).Count == 0;
        }

        public void azurirajOdobravanje(LekZaOdobravanje lekZaAzuriranje)
        {
            ++lekZaAzuriranje.brLekaraKojiSuodobriliLek;
            lekRepozitorijum.azurirajLekZaOdobravanje(ukloniLekaraKojiJeOdobrioLek(lekZaAzuriranje));
        }

        public LekZaOdobravanje ukloniLekaraKojiJeOdobrioLek(LekZaOdobravanje lekZaAzuriranje)
        {
            foreach(String idLekara in lekZaAzuriranje.lekariKojimaJePoslatLek)
            {
                if (idLekara.Equals(KorisnikServis.getInstance().getLekar().id))
                {
                    lekZaAzuriranje.lekariKojimaJePoslatLek.Remove(idLekara);
                    break;
                }
            }

            return lekZaAzuriranje;
        }

        public List<LekZaOdobravanje> nadjiLekoveZaOdobravanjeZaLogovanogLekara(String idLogovanogLekara)
        {
            List<LekZaOdobravanje> povratnaVrednost = new List<LekZaOdobravanje>();

            foreach(LekZaOdobravanje lek in lekRepozitorijum.ucitajLekoveZaOdobravanje())
            {
                if (proveriLogovanogLekaraIIzabranog(lek.lekariKojimaJePoslatLek))
                {
                    povratnaVrednost.Add(lek);
                }
            }

            return povratnaVrednost;
        } 

        private bool proveriLogovanogLekaraIIzabranog(List<String> lekariKojimaJePoslatLek)
        {
            bool povratnaVrednost = false;
            if(lekariKojimaJePoslatLek != null)
            {
                foreach(String idLekara in lekariKojimaJePoslatLek)
                {
                    if (idLekara.Equals(KorisnikServis.getInstance().getLekar().id))
                    {
                        povratnaVrednost = true;
                        break;
                    }
                }
            }
            return povratnaVrednost;
        }

        public List<Lek> ucitajSveSemTrenutnogNaTerapiji(String idLeka)
        {
            List<Lek> povratnaVrednost = new List<Lek>();

            if (idLeka == null)
            {
                return lekRepozitorijum.ucitajSve();
            }

            foreach (Lek lek in ucitajSve())
            {
                if (!idLeka.Equals(lek.id))
                {
                    povratnaVrednost.Add(lek);
                }
            }

            return povratnaVrednost;
        }
        public Lek nadjiLekPoId(String idLeka)
        {
            Lek povratnaVrednost = new Lek();

            foreach (Lek lek in ucitajSve())
            {
                if (idLeka != null && idLeka.Equals(lek.id))
                {
                    povratnaVrednost.id = lek.id;
                    povratnaVrednost.kolicina = lek.kolicina;
                    povratnaVrednost.nacinUpotrebe = lek.nacinUpotrebe;
                    povratnaVrednost.naziv = lek.naziv;
                    povratnaVrednost.proizvodjac = lek.proizvodjac;
                    povratnaVrednost.tip = lek.tip;

                    break;
                }
            }

            return povratnaVrednost;
        }

        public List<Lek> ucitajSve()
        {
            return lekRepozitorijum.ucitajSve();
        }

        public List<NacinUpotrebe> nacinUpotrebeLeka()
        {
            List<NacinUpotrebe> nacinUpotrebe = Enum.GetValues(typeof(NacinUpotrebe)).Cast<NacinUpotrebe>().ToList();
            return nacinUpotrebe;
        }

        public List<TipLeka> tipLeka()
        {
            List<TipLeka> tipLeka = Enum.GetValues(typeof(TipLeka)).Cast<TipLeka>().ToList();
            return tipLeka;
        }

        public void napraviLek(LekZaOdobravanje lek)
        {
            var lekoviZaOdobravanje = lekRepozitorijum.ucitajLekoveZaOdobravanje();
            lekoviZaOdobravanje.Add(lek);
            lekRepozitorijum.upisiLekoveZaObradu(lekoviZaOdobravanje);
        }

        public void dodajLekuLekare(List<String> idLekari, LekZaOdobravanje lek)
        {
            lek.lekariKojimaJePoslatLek = idLekari;
        }

        public void dodajLek(LekZaOdobravanje lekZaDodavanje)
        {
            lekRepozitorijum.fizickiObrisiLekZaDodavanje(lekZaDodavanje);
            lekRepozitorijum.dodajLek(new Lek(lekZaDodavanje, (ucitajSve().Count + 1).ToString()));
        }
        public void odbacivanjeLeka(LekZaOdobravanje lekZaOdbacivanje)
        {
            lekRepozitorijum.fizickiObrisiLekZaDodavanje(lekZaOdbacivanje);
            lekRepozitorijum.dodajLekZaOdbacivanje(lekZaOdbacivanje);
        }

    }
}
