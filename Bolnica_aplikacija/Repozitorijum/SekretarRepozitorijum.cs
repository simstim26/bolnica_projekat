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
    class SekretarRepozitorijum
    {
        public List<Sekretar> ucitajSve()
        {
            List<Sekretar> sviSekretari;
            try
            {
                sviSekretari = JsonSerializer.Deserialize<List<Sekretar>>(File.ReadAllText("Datoteke/Sekretari.txt"));
            }
            catch (Exception e)
            {
                sviSekretari = new List<Sekretar>();
            }

            return sviSekretari;
        }
    }
}
