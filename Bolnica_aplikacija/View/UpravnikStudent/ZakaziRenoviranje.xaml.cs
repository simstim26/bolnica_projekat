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

        private void image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Demo();
        }

        private async Task Demo()
        {
            await Task.Delay(1000);
            image1.Visibility = Visibility.Visible;
            textBlock1.Visibility = Visibility.Visible;
            await Task.Delay(4000);

            image1.Visibility = Visibility.Hidden;
            textBlock1.Visibility = Visibility.Hidden;
            await Task.Delay(1000);

            image2.Visibility = Visibility.Visible;
            black1.Visibility = Visibility.Visible;
            textBlock2.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            tap.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            image2.Visibility = Visibility.Hidden;
            textBlock2.Visibility = Visibility.Hidden;
            black1.Visibility = Visibility.Hidden;
            tap.Visibility = Visibility.Hidden;

            await Task.Delay(1000);
            image3.Visibility = Visibility.Visible;
            black2.Visibility = Visibility.Visible;
            textBlock3.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            tap2.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            tap2.Visibility = Visibility.Hidden;
            datumRenoviranjaOd.IsDropDownOpen = true;
            await Task.Delay(700);
            datumRenoviranjaOd.IsDropDownOpen = false;
            datumRenoviranjaOd.SelectedDate = System.DateTime.Now;
            await Task.Delay(1000);
            tap3.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            tap3.Visibility = Visibility.Hidden;
            datumRenoviranjaDo.IsDropDownOpen = true;
            await Task.Delay(1000);
            datumRenoviranjaDo.IsDropDownOpen = false;
            datumRenoviranjaDo.SelectedDate = System.DateTime.Now.AddDays(2);
            await Task.Delay(1000);
            image3.Visibility = Visibility.Hidden;
            black2.Visibility = Visibility.Hidden;
            await Task.Delay(1000);
            image4.Visibility = Visibility.Visible;
            black3.Visibility = Visibility.Visible;
            textBlock44.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            image4.Visibility = Visibility.Hidden;
            black3.Visibility = Visibility.Hidden;
            textBlock44.Visibility = Visibility.Hidden;
            textBoxRazlogRenoviranja.Text = "Razlog renoviranja";
            await Task.Delay(700);
            image5.Visibility = Visibility.Visible;
            black4.Visibility = Visibility.Visible;
            textBlock55.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            cbTipRenoviranja.IsDropDownOpen = true;
            await Task.Delay(1000);
            cbTipRenoviranja.IsDropDownOpen = false;
            cbTipRenoviranja.SelectedIndex = 2;
            await Task.Delay(500);
            image5.Visibility = Visibility.Hidden;
            black4.Visibility = Visibility.Hidden;
            textBlock55.Visibility = Visibility.Hidden;
            await Task.Delay(700);
            image6.Visibility = Visibility.Visible;
            black5.Visibility = Visibility.Visible;
            textBlock66.Visibility = Visibility.Visible;
            await Task.Delay(3000);
            image6.Visibility = Visibility.Hidden;
            black5.Visibility = Visibility.Hidden;
            textBlock66.Visibility = Visibility.Hidden;
            cbTipRenoviranja.SelectedIndex = -1;
            textBoxRazlogRenoviranja.Text = "";
            datumRenoviranjaOd.SelectedDate = null;
            datumRenoviranjaDo.SelectedDate = null;




            /*//cursor1.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            //dataGridPacijenti.SelectedIndex = 1;
            await Task.Delay(2000);
            image.Visibility = Visibility.Hidden;
            //cursor1.Visibility = Visibility.Hidden;

            //lblDatum.Visibility = Visibility.Visible;
            image2.Visibility = Visibility.Visible;
            //await Task.Delay(1000);
            //datumPocetak.SelectedDate = DateTime.Now;
            await Task.Delay(1000);
            image2.Visibility = Visibility.Hidden;

            image3.Visibility = Visibility.Visible;
            *//*await Task.Delay(1000);
            datumKraj.SelectedDate = DateTime.Now.AddDays(10);*//*
            await Task.Delay(1000);
            //lblDatum.Visibility = Visibility.Hidden;
            image4.Visibility = Visibility.Hidden;

            *//*lblLekar.Visibility = Visibility.Visible;*/
            /*image.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            dataGridLekari.SelectedIndex = 1;
            await Task.Delay(2000);
            lblLekar.Visibility = Visibility.Hidden;
            cursor4.Visibility = Visibility.Hidden;

            lblPrioritet.Visibility = Visibility.Visible;
            cursor5.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            prioritet.SelectedIndex = 1;
            await Task.Delay(2000);
            lblPrioritet.Visibility = Visibility.Hidden;
            cursor5.Visibility = Visibility.Hidden;

            lblPotvrdi.Visibility = Visibility.Visible;
            cursor6.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            lblPotvrdi.Visibility = Visibility.Hidden;
            cursor6.Visibility = Visibility.Hidden;

            await Task.Delay(1000);
            dataGridPacijenti.SelectedIndex = -1;
            datumPocetak.SelectedDate = null;
            datumKraj.SelectedDate = null;
            dataGridLekari.SelectedIndex = -1;
            prioritet.SelectedIndex = -1;

            lblDemo.Visibility = Visibility.Hidden;*/
        }
    }
}
