// File:    Korisnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:21 PM
// Purpose: Definition of Class Korisnik

using System;
using System.IO;
using System.Text;

namespace Model
{
   public abstract class Korisnik
   {
      public String korisnickoIme { get; set; }
      public String lozinka { get; set; }
      public String ime { get; set; }
      public String prezime { get; set; }
      public DateTime datumRodjenja { get; set; }
      public String email { get; set; }
      public String brojTelefona { get; set; }
      public String jmbg { get; set; }

        public static Korisnik Prijava(String id, String putanja)
        {
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("Datoteke/" + putanja + ".txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String linija;
                    while ((linija = streamReader.ReadLine()) != null)
                    {
                        string[] sadrzaj = linija.Split('|');
                        if(sadrzaj[0] != id)
                        {
                            continue;
                        }
                        else
                        {
                            switch (putanja)
                            {
                                case "Pacijenti":
                                    break;
                                case "Lekari":
                                    Lekar korisnik = new Lekar();
                                    korisnik.jmbg = sadrzaj[1];
                                    korisnik.ime = sadrzaj[2];
                                    korisnik.prezime = sadrzaj[3];
                                    korisnik.datumRodjenja = Convert.ToDateTime(sadrzaj[4]);
                                    korisnik.email = sadrzaj[5];
                                    korisnik.brojTelefona = sadrzaj[6];
                                    korisnik.brojSlobodnihDana = Convert.ToInt32(sadrzaj[7]);
                                    korisnik.prosecnaOcena = Convert.ToDouble(sadrzaj[8]);
                                    korisnik.idBolnice = sadrzaj[9];
                                    return korisnik;
                                case "Sekretari":
                                    break;
                                case "Upravnici":
                                    break;
                            }
                        }

                    }
                }
            }

            return null;
        }
   }
}