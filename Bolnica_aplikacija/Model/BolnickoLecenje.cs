﻿using Bolnica_aplikacija.PomocneKlase;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica_aplikacija.Model
{
    class BolnickoLecenje
    {
        public String id { get; set; }
        public DateTime datumPocetka { get; set; }
        public int trajanje { get; set; }

        public bool jeZavrsen { get; set; }
        public Pacijent pacijent { get; set; }
        public Prostorija bolnickaSoba { get; set; }

        public BolnickoLecenje() { }

        public BolnickoLecenje(string id, DateTime datumPocetka, int trajanje, bool jeZavrsen, Pacijent pacijent, Prostorija bolnickaSoba)
        {
            this.id = id;
            this.datumPocetka = datumPocetka;
            this.trajanje = trajanje;
            this.jeZavrsen = jeZavrsen;
            this.pacijent = pacijent;
            this.bolnickaSoba = bolnickaSoba;
        }

        public BolnickoLecenje(BolnickoLecenjeDTO dto)
        {
            id = dto.id;
            datumPocetka = dto.datumPocetka;
            trajanje = dto.trajanje;
            jeZavrsen = dto.jeZavrsen;
            pacijent.id = dto.idPacijenta;
            bolnickaSoba.id = dto.idProstorije;
        }
    }
}
