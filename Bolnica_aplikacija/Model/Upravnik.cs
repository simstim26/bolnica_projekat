// File:    Upravnik.cs
// Author:  User
// Created: Monday, March 22, 2021 7:07:23 PM
// Purpose: Definition of Class Upravnik

using System;

namespace Model
{
   public class Upravnik : Korisnik
   {
      public String id { get; set; }
      public String idBolnice { get; set; }

        public void NapraviProstoriju(Prostorija prostorija)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajProstoriju(Prostorija prostorija)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajProstoriju(Prostorija prostorija)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiProstoriju(String idProstorija)
      {
         throw new NotImplementedException();
      }
      
      public void NapraviLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajLek(Lek lek)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiLek(String idLeka)
      {
         throw new NotImplementedException();
      }
      
      public void NapraviStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajStavku(Stavka stavka)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiStavku(String idStavke)
      {
         throw new NotImplementedException();
      }
      
      public void NapraviOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiOsoblje()
      {
         throw new NotImplementedException();
      }
      
      public void EvidencijaPraktikanata()
      {
         throw new NotImplementedException();
      }
      
      public void FormiranjeSpiskaLekova()
      {
         throw new NotImplementedException();
      }
      
      public void NapraviZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajZahtev()
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiZahtev()
      {
         throw new NotImplementedException();
      }
   
   }
}