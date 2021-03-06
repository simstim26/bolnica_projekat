using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Kontroler
{
    class AlergijaKontroler
    {
        public static void obrisiAlergiju(Alergija alergija)
        {
            AlergijaServis.getInstance().obrisiAlergiju(alergija);
        }

        public static void dodajAlergijePacijentu(String idPacijenta, List<Alergija> alergije)
        {
            AlergijaServis.getInstance().dodajAlergijePacijentu(idPacijenta, alergije);
        }

        public static void azurirajAlergije(List<Alergija> alergije, String id)
        {
            AlergijaServis.getInstance().azurirajAlergije(alergije, id);
        }
        public static bool proveriPostojanjeAlergije(String idPacijenta, String nazivAlergije)
        {
            return AlergijaServis.getInstance().proveriPostojanjeAlergije(idPacijenta, nazivAlergije);
        }
    }
}
