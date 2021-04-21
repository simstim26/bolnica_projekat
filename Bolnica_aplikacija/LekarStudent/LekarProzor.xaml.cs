﻿using System;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Model;
using Bolnica_aplikacija.Kontroler;
using Bolnica_aplikacija.LekarStudent;

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for LekarProzor.xaml
    /// </summary>
    public partial class LekarProzor : Window
    {
        private static ContentControl x;
        private static Button nazad;
        private static Button pretraga;
        private static Label glavnaLabela;
        public LekarProzor()
        {
            InitializeComponent();
            this.contentControl.Content = new LekarTabovi();
            x = this.contentControl;
            lblGlavna.Content = "Zdravo korporacija";
            glavnaLabela = this.lblGlavna;
            nazad = this.btnNazad;
            pretraga = this.btnPretraga;
            btnNazad.Visibility = Visibility.Hidden;
        }

        public static Label getGlavnaLabela()
        {
            return glavnaLabela;
        }
        public static Button getPretraga()
        {
            return pretraga;
        }
        public static Button getNazad()
        {
            return nazad;
        }
        public static ContentControl getX()
        {
            return x;
        }

        private void meniOdjava_Click(object sender, RoutedEventArgs e)
        {
            Prijava prijava = new Prijava();
            this.Close();
            prijava.ShowDialog();
        }

        private void btnNazad_Click(object sender, RoutedEventArgs e)
        {
            if (PacijentInfo.aktivanPacijentInfo)
            {
                lblGlavna.Content = "Zdravo korporacija";
                LekarProzor.getX().Content = new LekarTabovi();
                LekarTabovi.getTab().SelectedIndex = 1;
                btnPretraga.Visibility = Visibility.Visible;
                btnNazad.Visibility = Visibility.Hidden;
                PacijentInfo.aktivanPacijentInfo = false;
            }
            else if (ZakaziTermin.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                PacijentInfo.getTab().SelectedIndex = 2;
                PacijentInfo.aktivanPacijentInfo = true;
                ZakaziTermin.aktivan = false;
            }
            else if (PrikazProstorija.aktivan)
            {
                LekarProzor.getX().Content = new ZakaziTermin(ZakaziTermin.getTipAkcije());
                PrikazProstorija.aktivan = false;
            }
            else if (Izvestaj.aktivan)
            {
                if (Izvestaj.getGridUput().Visibility == Visibility.Visible)
                {
                    lblGlavna.Content = "Pisanje izveštaja";
                    Izvestaj.getGridUput().Visibility = Visibility.Hidden;

                }
                else if(Izvestaj.getGridRecept().Visibility == Visibility.Visible)
                {
                    lblGlavna.Content = "Pisanje izveštaja";
                    Izvestaj.getGridRecept().Visibility = Visibility.Hidden;
                }
                else if(Izvestaj.getGridAzuriranje().Visibility == Visibility.Visible)
                {
                    lblGlavna.Content = "Pisanje izveštaja";
                    Izvestaj.getGridAzuriranje().Visibility = Visibility.Hidden;
                }
                else if(Izvestaj.getGridOdabirLeka().Visibility == Visibility.Visible)
                {
                    lblGlavna.Content = "Izdavanje recepta";
                    Izvestaj.getGridOdabirLeka().Visibility = Visibility.Hidden;
                    Izvestaj.getGridRecept().Visibility = Visibility.Visible;
                }
                else
                {
                    LekarProzor.getX().Content = new PacijentInfo();
                    Izvestaj.aktivan = false;
                }
            }
            else if (IstorijaBolesti.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                IstorijaBolesti.aktivan = false;
            }
            else if (IzmenaBolesti.aktivan)
            {
                if (IzmenaBolesti.getGridLekovi().Visibility == Visibility.Visible)
                {
                    IzmenaBolesti.getGridLekovi().Visibility = Visibility.Hidden;
                    IzmenaBolesti.getGridRecept().Visibility = Visibility.Visible;
                    glavnaLabela.Content = "Izdavanje recepta";
                }
                else if (IzmenaBolesti.getGridRecept().Visibility == Visibility.Visible)
                {
                    IzmenaBolesti.getGridRecept().Visibility = Visibility.Hidden;
                    LekarProzor.getGlavnaLabela().Content = "Ažuriranje bolesti";
                }
                else
                {
                    LekarProzor.getX().Content = new IstorijaBolesti();
                    IzmenaBolesti.aktivan = false;
                }
            }
            else if (Alergije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                Alergije.aktivan = false;

            }
            else if (UvidUTerapije.aktivan)
            {
                LekarProzor.getX().Content = new PacijentInfo();
                UvidUTerapije.aktivan = false;
            }
            else if (TerapijeIzdavanjeRecpeta.aktivan)
            {
                if(TerapijeIzdavanjeRecpeta.getGridLekovi().Visibility == Visibility.Visible)
                {
                    TerapijeIzdavanjeRecpeta.getGridLekovi().Visibility = Visibility.Hidden;
                    glavnaLabela.Content = "Izdavanje recepta";
                }
                else
                {
                    LekarProzor.getX().Content = new UvidUTerapije();
                    TerapijeIzdavanjeRecpeta.aktivan = false;
                }
            }
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            if (LekarTabovi.getTab().SelectedIndex == 0)
            {
                if (LekarTabovi.getRasporedPretraga().Visibility == Visibility.Visible)
                {
                    LekarTabovi.getRasporedPretraga().Visibility = Visibility.Hidden;
                }
                else
                {
                    LekarTabovi.getRasporedPretraga().Visibility = Visibility.Visible;
                }


            }
            else if (LekarTabovi.getTab().SelectedIndex == 1)
            {
                if (LekarTabovi.getPacijentiPretraga().Visibility == Visibility.Visible)
                {
                    LekarTabovi.getPacijentiPretraga().Visibility = Visibility.Hidden;
                }
                else
                {
                    LekarTabovi.getPacijentiPretraga().Visibility = Visibility.Visible;
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {

        }
    }
}