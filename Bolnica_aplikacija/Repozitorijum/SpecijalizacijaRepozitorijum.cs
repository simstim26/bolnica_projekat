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
    class SpecijalizacijaRepozitorijum
    {
        public List<Specijalizacija> ucitajSve()
        {
            List<Specijalizacija> sveSpecijalizacije;
            try
            {
                sveSpecijalizacije = JsonSerializer.Deserialize<List<Specijalizacija>>(File.ReadAllText("Datoteke/Specijalizacije.txt"));
            }
            catch (Exception e)
            {
                sveSpecijalizacije = new List<Specijalizacija>();
            }

            return sveSpecijalizacije;
        }
    }
}
