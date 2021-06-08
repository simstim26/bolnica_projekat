using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PomocneKlase;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bolnica_aplikacija.View.UpravnikStudent
{
    /// <summary>
    /// Interaction logic for ZakaziRenoviranje.xaml
    /// </summary>
    public partial class ZakaziRenoviranje : UserControl
    {
        public ZakaziRenoviranje()
        {
            InitializeComponent();
            dataGridProstorijeZaRenoviranje.ItemsSource = ProstorijaKontroler.ucitajNeobrisane();
        }

        private void btnZakaziRenoviranjeProstorije_Click(object sender, RoutedEventArgs e)
        {
            praznaPolja.Visibility = Visibility.Hidden;
            praznaPolja.Visibility = Visibility.Hidden;
            prostorijeSpratGreska.Visibility = Visibility.Hidden;
            prostorijeSpajanjeGreska.Visibility = Visibility.Hidden;
            izaberiteProstorije.Visibility = Visibility.Hidden;

            if (dataGridProstorijeZaRenoviranje.SelectedItems.Count != 0)
            {

                if (datumRenoviranjaDo.SelectedDate == null || datumRenoviranjaOd.SelectedDate == null || cbTipRenoviranja.SelectedIndex == -1 || textBoxRazlogRenoviranja.Text == "")
                {
                    praznaPolja.Visibility = Visibility.Visible;
                }
                else
                {
                    if (datumRenoviranjaDo.SelectedDate < datumRenoviranjaOd.SelectedDate || datumRenoviranjaOd.SelectedDate.Value <= System.DateTime.Today.AddDays(-1))
                    {
                        neispravanDatum.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        List<ProstorijaRenoviranje> prostorijeZaRenoviranje = new List<ProstorijaRenoviranje>();
                        foreach (var selektovan in dataGridProstorijeZaRenoviranje.SelectedItems)
                        {
                            Prostorija selektovanaProstorija = (Prostorija)selektovan;
                            ProstorijaRenoviranje prostorijaZaRenoviranje = new ProstorijaRenoviranje(selektovanaProstorija.id, (DateTime)datumRenoviranjaOd.SelectedDate,
                                                                            (DateTime)datumRenoviranjaDo.SelectedDate, textBoxRazlogRenoviranja.Text, cbTipRenoviranja.SelectedIndex);
                            prostorijeZaRenoviranje.Add(prostorijaZaRenoviranje);
                        }
                        if (cbTipRenoviranja.SelectedIndex == 2 && prostorijeZaRenoviranje.Count == 2)
                        {
                            if ((ProstorijaKontroler.nadjiProstorijuPoId(prostorijeZaRenoviranje[0].idProstorije)).sprat == (ProstorijaKontroler.nadjiProstorijuPoId(prostorijeZaRenoviranje[1].idProstorije)).sprat)
                            {
                                ProstorijaRenoviranje prostorijaZaRenoviranje = prostorijeZaRenoviranje[0];
                                prostorijaZaRenoviranje.idProstorijeKojaSeSpaja = prostorijeZaRenoviranje[1].idProstorije;
                                ProstorijaKontroler.zakaziRenoviranje(prostorijaZaRenoviranje);
                                ProstorijaKontroler.pregledajProstorijeZaRenoviranje();
                                GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                                GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
                            }
                            else
                            {
                                prostorijeSpratGreska.Visibility = Visibility.Visible;
                            }

                        }
                        else if (cbTipRenoviranja.SelectedIndex == 2 && prostorijeZaRenoviranje.Count != 2)
                        {
                            prostorijeSpajanjeGreska.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            foreach (var prostorijaZaRenoviranje in prostorijeZaRenoviranje)
                            {
                                ProstorijaKontroler.zakaziRenoviranje(prostorijaZaRenoviranje);
                            }
                            ProstorijaKontroler.pregledajProstorijeZaRenoviranje();
                            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
                            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
                        }
                        
                    }
                }
            }
                
            else
            {
                izaberiteProstorije.Visibility = Visibility.Visible;
            }

        }

        private void btnOtkaziRenoviranjeProstorije_Click(object sender, RoutedEventArgs e)
        {
            GlavniProzor.DobaviProzorZaIzmenu().Children.Clear();
            GlavniProzor.DobaviProzorZaIzmenu().Children.Add(new ProstorijePogled());
        }
    }
}
