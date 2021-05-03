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
                    povratnaVrednost.kopiraj(lek);

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

        public void azurirajLek(Lek lekZaAzuriranje)
        {
            lekRepozitorijum.azurirajLek(lekZaAzuriranje);
        }

        public void izbrisiSastojak(String idLeka, String sastojak)
        {
            Lek lek = nadjiLekPoId(idLeka);
            lek.sastojci.Remove(sastojak);
            lekRepozitorijum.azurirajLek(lek);        
        }

        public void dodajSastojak(String idLeka, String sastojak)
        {
            Lek lek = nadjiLekPoId(idLeka);
            if (lek.sastojci == null)
                lek.sastojci = new List<String>();
            
            lek.sastojci.Add(sastojak);
            lekRepozitorijum.azurirajLek(lek);
        }
        public List<Lek> ucitajSveLekoveBezZamenskih(String idLeka)
        {
            Lek izabraniLek = nadjiLekPoId(idLeka);

            if (izabraniLek.zamenskiLekovi == null)
                return ucitajSveSemTrenutnogNaTerapiji(idLeka);

            List<Lek> povratnaVrednost = new List<Lek>();

            foreach (Lek lek in ucitajSveSemTrenutnogNaTerapiji(idLeka))
            {
                if(!nalaziSeUZamenskim(izabraniLek, lek))
                {
                    povratnaVrednost.Add(lek);
                }
            }

            return povratnaVrednost;
        }

        private bool nalaziSeUZamenskim(Lek izabraniLek, Lek lek)
        {
            bool povratnaVrednost = false;
            foreach(Lek zamenskiLek in izabraniLek.zamenskiLekovi)
            {
                if (lek.id.Equals(zamenskiLek.id))
                {
                    povratnaVrednost = true;
                    break;
                }
            }


            return povratnaVrednost;
        }

        public void dodajZamenskiLek(String idLek, Lek zamenskiLek)
        {
            Lek lek = nadjiLekPoId(idLek);
            if (lek.zamenskiLekovi == null)
                lek.zamenskiLekovi = new List<Lek>();

            lek.zamenskiLekovi.Add(zamenskiLek);
            lekRepozitorijum.azurirajLek(lek);
        }

        public void obrisiZamenskiLek(String idLek, String idZamenskogLeka)
        {
            Lek lek = nadjiLekPoId(idLek);
            foreach(Lek zamenskiLek in lek.zamenskiLekovi)
            {
                if (idZamenskogLeka.Equals(zamenskiLek.id))
                {
                    lek.zamenskiLekovi.Remove(zamenskiLek);
                    lekRepozitorijum.azurirajLek(lek);
                    break;
                }
            }
        }

        public void fizickiObrisiLekZaOdbacivanje(LekZaOdobravanje lekZaOdbacivanje)
        {
            lekRepozitorijum.fizickiObrisiLekZaOdbacivanje(lekZaOdbacivanje);
        }

        public List<LekZaOdobravanje> ucitajOdbaceneLekove()
        {
            return lekRepozitorijum.ucitajOdbaceneLekove();
        }

        public void upisiOdbaceneLekove(List<LekZaOdobravanje> lekoviZaOdbacivanje)
        {
            lekRepozitorijum.upisiOdbaceneLekove(lekoviZaOdbacivanje);
        }

        public List<LekZaOdobravanje> ucitajLekoveZaOdobravanje()
        {
            return lekRepozitorijum.ucitajLekoveZaOdobravanje();
        }


    }
}
