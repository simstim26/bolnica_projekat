using System;

namespace Model
{
   public class Lekar : Korisnik
   {
      public String id { get; set; }
      public int brojSlobodnihDana { get; set; }
      public double prosecnaOcena { get; set; }
      public String idBolnice { get; set; }
      public String idSpecijalizacije { get; set; }
      public void NapraviTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ProcitajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajTermin(Termin termin)
      {
         throw new NotImplementedException();
      }
      
      public void ObrisiTermin(String idTermina)
      {
         throw new NotImplementedException();
      }
      
      public void PrikazRasporeda()
      {
         throw new NotImplementedException();
      }
      
      public void UpdateRasporeda()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazPacijenta()
      {
         throw new NotImplementedException();
      }
      
      public void UvidUZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public void AzurirajZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public void IzdavanjeUputa()
      {
         throw new NotImplementedException();
      }
      
      public void IzdavanjeTerapije()
      {
         throw new NotImplementedException();
      }
      
      public void PrikazStavki()
      {
         throw new NotImplementedException();
      }
      
      public void PretragaStavki()
      {
         throw new NotImplementedException();
      }
      
      public int AzuriranjeInventara()
      {
         throw new NotImplementedException();
      }
      
      public Termin[] termin;
   
   }
}