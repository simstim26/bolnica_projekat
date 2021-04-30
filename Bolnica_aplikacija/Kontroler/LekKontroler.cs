﻿using Bolnica_aplikacija.Model;
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

        public static void napraviLek(String naziv, TipLeka tipLeka, int kolicina, String proizvodjac, NacinUpotrebe nacinUpotrebe, 
            LekZaOdobravanje lek)
        {
            lek = new LekZaOdobravanje(naziv, tipLeka, kolicina, proizvodjac, nacinUpotrebe);
            LekServis.getInstance().napraviLek(lek);
        }

        public static void dodajLekuLekare(List<String> idLekari, LekZaOdobravanje lek)
        {
            LekServis.getInstance().dodajLekuLekare(idLekari, lek);
        }
    }
}
