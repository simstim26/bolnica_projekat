using Bolnica_aplikacija.Komande;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.View.SekretarStudent;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Bolnica_aplikacija.ViewModel
{
    class SekretarIzvestajViewModel : BindableBase
    { 
        public SekretarIzvestajViewModel(SekretarIzvestaj parent, SekretarProzor pocetni)
        {

            this.parent = parent;
            this.pocetni = pocetni;
           
            NapisiIzvestaj = new RelayCommand(sacuvaj);
            Odustani = new RelayCommand(odustani);

            DatumPocetak = DateTime.Now;
            DatumKraj = DateTime.Now;

        }

        #region RelayCommand property
        public RelayCommand NapisiIzvestaj { get; set; }
        public RelayCommand Odustani { get; set; }
        
        #endregion

        #region Polja i property
        SekretarIzvestaj parent;
        SekretarProzor pocetni;

        List<string> data = new List<string>();
        PdfPTable table = new PdfPTable(6);

        private DateTime datumPocetak;
        public DateTime DatumPocetak
        {
            get { return datumPocetak; }
            set
            {
                if (datumPocetak != value)
                {
                    datumPocetak = value;
                    OnPropertyChanged("DatumPocetak");
                }
            }
        }

        private DateTime datumKraj;
        public DateTime DatumKraj
        {
            get { return datumKraj; }
            set
            {
                if (datumKraj != value)
                {
                    datumKraj = value;
                    OnPropertyChanged("DatumKraj");
                }
            }
        }

        #endregion

        #region Komanda -> Sacuvaj
        private void sacuvaj(object obj)
        {
            if (datumPocetak == null || datumKraj == null)
            {
                parent.lblUpozorenje.Visibility = Visibility.Visible;
                return;
            }


            FileStream fs = new FileStream("Izvestaj Sekretara.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            Document doc = new Document(rec);
            PdfWriter pdfwriter = PdfWriter.GetInstance(doc, fs);
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font titleFont = new Font(bfTimes, 16, iTextSharp.text.Font.NORMAL);
            iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("IZVEŠTAJ SEKRETARA O ZAKAZANIM TERMINIMA", titleFont);
            title.Alignment = Element.ALIGN_CENTER;

            iTextSharp.text.Paragraph blanc = new iTextSharp.text.Paragraph("    ", titleFont);
            iTextSharp.text.Paragraph signature = new iTextSharp.text.Paragraph("Potpis:\n _______________", titleFont);
            signature.Alignment = Element.ALIGN_RIGHT;
            iTextSharp.text.Paragraph stamp = new iTextSharp.text.Paragraph("Pecat", titleFont);
            signature.Alignment = Element.ALIGN_LEFT;

            doc.Open();
            doc.Add(title);
            doc.Add(blanc);
            sakupiPodatke();
            popuniTabelu();
            doc.Add(table);
            doc.Add(blanc);
            doc.Add(signature);
            doc.Add(stamp);
            doc.Close();
            MessageBox.Show("Izvestaj kreiran.");

            odustani(null);

        }
        #endregion

        #region Komanda -> odustani
        private void odustani(object obj) 
        {
            parent.Content = null;
            parent.Visibility = Visibility.Hidden;
            pocetni.TerminiGrid.IsEnabled = true;

            parent.lblUpozorenjeNemaTermina.Visibility = Visibility.Hidden;
            parent.lblUpozorenje.Visibility = Visibility.Hidden;

        }
        #endregion

        #region Pomocne funkcije
        private void sakupiPodatke()
        {
            List<Termin> termini = TerminKontroler.pronadjiTermineZaIzvestajSekretara(datumPocetak, datumKraj);
            
            if(termini.Count == 0)
            {
                parent.lblUpozorenjeNemaTermina.Visibility = Visibility.Visible;
            }

            foreach(Termin termin in termini)
            {
                String pacijentImePrezime = PacijentKontroler.nadjiPacijenta(termin.idPacijenta).ime + " " +
                                            PacijentKontroler.nadjiPacijenta(termin.idPacijenta).prezime;
                String tipTermina = termin.tip.ToString();
                String datum = termin.datum.ToString("dd.MM.yyyy.");
                String satnica = termin.satnica.ToString("HH:mm");
                String prostorija = ProstorijaKontroler.nadjiProstorijuPoId(termin.idProstorije).broj;
                String lekar = LekarKontroler.nadjiLekaraPoId(termin.idLekara).ime + " " +
                               LekarKontroler.nadjiLekaraPoId(termin.idLekara).prezime;

                data.Add(pacijentImePrezime);
                data.Add(tipTermina);
                data.Add(datum);
                data.Add(satnica);
                data.Add(prostorija);
                data.Add(lekar);

            }
            
        }

        public void popuniTabelu()
        {
            PdfPHeaderCell pacijent = new PdfPHeaderCell();
            PdfPHeaderCell tipTermina = new PdfPHeaderCell();
            PdfPHeaderCell Datum = new PdfPHeaderCell();
            PdfPHeaderCell Satnica = new PdfPHeaderCell();
            PdfPHeaderCell Prostorija = new PdfPHeaderCell();
            PdfPHeaderCell Lekar = new PdfPHeaderCell();

            pacijent.Phrase = new Phrase("Ime i prezime pacijenta");
            tipTermina.Phrase = new Phrase("Tip termina");
            Datum.Phrase = new Phrase("Datum");
            Satnica.Phrase = new Phrase("Satnica");
            Prostorija.Phrase = new Phrase("Prostorija");
            Lekar.Phrase = new Phrase("Lekar");



            table.AddCell(pacijent);
            table.AddCell(tipTermina);
            table.AddCell(Datum);
            table.AddCell(Satnica);
            table.AddCell(Prostorija);
            table.AddCell(Lekar);

            foreach (string s in data)
            {
                PdfPCell cell = new PdfPCell(new Phrase(s));
                table.AddCell(cell);
            }
        }
        #endregion


    }
}
