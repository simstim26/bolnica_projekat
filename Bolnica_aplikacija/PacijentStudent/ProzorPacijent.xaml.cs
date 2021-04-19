﻿using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.PacijentModel;
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
using System.Windows.Threading;

namespace Bolnica_aplikacija.PacijentStudent
{
    /// <summary>
    /// Interaction logic for ProzorPacijent.xaml
    /// </summary>
    public partial class ProzorPacijent : Window
    {

        private String idPacijenta;
        private DispatcherTimer tajmer;

        public ProzorPacijent()
        {
            InitializeComponent();
            this.idPacijenta = KorisnikKontroler.GetPacijent().id;
            this.DataContext = this;

            SetScreenSize();
            CenterWindow();
            SetDataGridBounds();
            SetLabelPacijentContent();

            PopuniTermine();
            setujTajmer();
        }

        private void PopuniTermine()
        {
            PacijentKontroler.nadjiPacijenta(this.idPacijenta);
            dataGridTermin.ItemsSource = PacijentKontroler.prikazPacijentovihTermina();
        }

        private void SetLabelPacijentContent()
        {

            lblPacijent.Content = lblPacijent.Content + " " +
                KorisnikKontroler.GetPacijent().ime + " " +
                KorisnikKontroler.GetPacijent().prezime;

        }

        private void SetScreenSize()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            this.MinHeight = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.92);
            this.MinWidth = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.75);
            WindowState = WindowState.Maximized;
        }

        private void SetDataGridBounds()
        {
            dataGridTermin.Loaded += SetMinWidths;
            dataGridTermin.Height = System.Windows.SystemParameters.PrimaryScreenHeight - 370;
        }

        private void CenterWindow()
        {

            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

        }

        public void SetMinWidths(object source, EventArgs e)
        {
            foreach (var column in dataGridTermin.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double newHeight = e.NewSize.Height;
            dataGridTermin.Height = newHeight - 370;
            double newWidth = e.NewSize.Width;
        }

        private void btnOtkaziPregled_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTermin.SelectedIndex != -1)
            {
                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                if (izabraniTermin.napomena.Equals("Pregled"))
                {
                    if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti otkazivanje termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (izabraniTermin.idSpecijalizacije.Equals("0"))
                        {
                            PacijentKontroler.otkaziTerminPacijenta(izabraniTermin.id);
                        }
                        else
                        {
                            MessageBox.Show("Potrebno je da se konsultujete sa Vašim specijalistom kako biste otkazali ovaj termin pregleda.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Potrebno je da se konsultujete sa Vašim lekarom kako biste otkazali termin operacije.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da otkažete.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            PopuniTermine();
        }

        private void btnIzmeniTermin_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTermin.SelectedIndex != -1)
            {

                PacijentTermin izabraniTermin = (PacijentTermin)dataGridTermin.SelectedItem;

                if (izabraniTermin.napomena.Equals("Pregled"))
                {
                    if (TerminKontroler.proveriDatumTermina(izabraniTermin.id) <= 0)
                    {
                        MessageBox.Show("Nije moguće izvršiti promenu termina 24h pred termin.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        if (izabraniTermin.idSpecijalizacije.Equals("0"))
                        {
                            TerminKontroler.sacuvajTermin(izabraniTermin.id);
                            IzmenaTerminaPacijent izmenaTermina = new IzmenaTerminaPacijent(dataGridTermin, this.idPacijenta);
                            izmenaTermina.Owner = this;
                            izmenaTermina.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Potrebno je da se konsultujete sa Vašim specijalistom oko izmene termina pregleda.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Za izmenu termina operacije je potrebno da se konsultujete sa Vašim lekarom.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

            }
            else
            {
                MessageBox.Show("Molimo odaberite termin koji želite da izmenite.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnZakaziPregled_Click_1(object sender, RoutedEventArgs e)
        {
            PacijentZakaziTermin zakaziTermin = new PacijentZakaziTermin(dataGridTermin);
            zakaziTermin.Owner = this;
            zakaziTermin.ShowDialog();
        }

        private void menuItemOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.Show();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            menuItemOdjava.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        }

        private void setujTajmer()
        {
            tajmer = new DispatcherTimer();
            tajmer.Interval = TimeSpan.FromSeconds(1);
            tajmer.Tick += timer_Tick;
            tajmer.Start();
        }

        void timer_Tick(Object sender, EventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss"));
        }

    }
}
