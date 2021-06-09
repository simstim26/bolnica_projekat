﻿using Bolnica_aplikacija.ViewModel;
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

namespace Bolnica_aplikacija.View.SekretarStudent
{
    /// <summary>
    /// Interaction logic for SekretarFeedback.xaml
    /// </summary>
    public partial class SekretarFeedback : Page
    {
        public SekretarFeedback(SekretarProzor pocetni)
        {
            InitializeComponent();
            this.DataContext = new SekretarFeedbackViewModel(this, pocetni);
        }
    }
}
