using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.Model;
using Bolnica_aplikacija.Servis;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for BiranjeDatumaZaIzvestaj.xaml
    /// </summary>
    public partial class BiranjeDatumaZaIzvestaj : Window
    {
        public BiranjeDatumaZaIzvestaj()
        {
            InitializeComponent();
            (ProstorijePogled.dobaviGridProstorija()).Opacity = 0.50f;
        }

        private void napraviIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            if (datumOd.SelectedDate == null || datumOd_Copy.SelectedDate == null || datumOd.SelectedDate > datumOd_Copy.SelectedDate)
            {
                ispravanPeriod.Visibility = Visibility.Visible;
            }
            else
            {

                PdfWriter writer = new PdfWriter("demo.pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                FontProgram fontProgram = FontProgramFactory.CreateFont();
                PdfFont font = PdfFontFactory.CreateFont(fontProgram, "Cp1250");
                document.SetFont(font);

                iText.Layout.Element.Paragraph header = new iText.Layout.Element.Paragraph("HEADER")
                   .SetFontSize(20);
                DateTime? datumOdd = datumOd.SelectedDate;
                DateTime? datumDo = datumOd_Copy.SelectedDate;

                var prostorije = ProstorijaKontroler.pronadjiTermineZaSveProstorije(datumOdd, datumDo);

                foreach (var p in prostorije)
                {
                    iText.Layout.Element.Paragraph broj = new iText.Layout.Element.Paragraph("Zauzetost za prostoriju " + p.prostorija.broj).SetUnderline();
                    document.Add(broj);
                    if (p.prostorija.tipProstorije == TipProstorije.BOLNICKA_SOBA)
                    {
                        if (p.bolnickoLecenje.Count == 0)
                        {
                            String nesto = "Nema pacijenata koji su se lečili u ovoj prostoriji";
                            Paragraph nema = new Paragraph(nesto);
                            document.Add(nema);
                        }
                        else
                        {
                            Paragraph termini = new Paragraph("Zauzete sobe:");
                            Table table = new Table(3);
                            Cell cell1 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph("Pacijent"));

                            Cell cell2 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph("Datum početka"));

                            Cell cell3 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph("Datum kraja"));

                            table.AddCell(cell1);
                            table.AddCell(cell2);
                            table.AddCell(cell3);

                            foreach (BolnickoLecenje bl in p.bolnickoLecenje)
                            {

                                Cell cell11 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph((PacijentKontroler.nadjiPacijenta(bl.pacijent.id)).ime + (PacijentKontroler.nadjiPacijenta(bl.pacijent.id)).prezime));

                                Cell cell12 = new Cell(1, 1)
                                    .SetBackgroundColor(ColorConstants.CYAN)
                                    .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                      .Add(new Paragraph((bl.datumPocetka.Date).ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)));

                                Cell cell13 = new Cell(1, 1)
                                    .SetBackgroundColor(ColorConstants.CYAN)
                                    .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                      .Add(new Paragraph(bl.datumPocetka.Date.AddDays(bl.trajanje).ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)));

                                table.AddCell(cell11);
                                table.AddCell(cell12);
                                table.AddCell(cell13);
                            }
                        }


                    }
                    else
                    {
                        if (p.termini.Count == 0)
                        {
                            Paragraph nema = new Paragraph("Nema termina koji su se održali u ovoj prostoriji");
                            document.Add(nema);
                        }
                        else
                        {
                            Paragraph termini = new Paragraph("Zauzeti termini:");
                            Table table = new Table(2);
                            Cell cell1 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph("Pacijent"));

                            Cell cell2 = new Cell(1, 1)
                                .SetBackgroundColor(ColorConstants.CYAN)
                                .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph("Datum"));

                            table.AddCell(cell1);
                            table.AddCell(cell2);

                            foreach (Termin t in p.termini)
                            {
                                Cell cell11 = new Cell(1, 1)
                                  .SetBackgroundColor(ColorConstants.WHITE)
                                  .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph((TerminKontroler.nadjiPacijentaZaTermin(t.idTermina)).ime + (TerminKontroler.nadjiPacijentaZaTermin(t.idTermina)).prezime));

                                Cell cell12 = new Cell(1, 1)
                                  .SetBackgroundColor(ColorConstants.WHITE)
                                  .SetTextAlignment((iText.Layout.Properties.TextAlignment?)TextAlignment.Center)
                                  .Add(new Paragraph((t.datum.Date).ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture)));

                                table.AddCell(cell11);
                                table.AddCell(cell12);
                            }

                            document.Add(table);
                        }
                    }
                }

                document.Close();
                this.Close();
                (ProstorijePogled.dobaviGridProstorija()).Opacity = 1f;
            }
        }

        private void otkaziIzvestaj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            (ProstorijePogled.dobaviGridProstorija()).Opacity = 1f;
        }
    }
}
