using System;
using System.Collections.Generic;
using System.IO;
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

namespace Bolnica_aplikacija
{
    /// <summary>
    /// Interaction logic for Proba.xaml
    /// </summary>
    public partial class PrikazPacijenata : UserControl
    {
        private static Model.Pacijent pacijent;
        public System.Collections.ObjectModel.ObservableCollection<Model.Pacijent> Pacijenti
        {
            get;
            set;
        }
        public PrikazPacijenata()
        {
            InitializeComponent();

            const Int32 BufferSize = 128;
            this.DataContext = this;
            Pacijenti = new System.Collections.ObjectModel.ObservableCollection<Model.Pacijent>();

            using (var fileStream = File.OpenRead("Datoteke/Pacijenti.txt"))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
                {
                    String linija;
                    while ((linija = streamReader.ReadLine()) != null)
                    {
                        string[] sadrzaj = linija.Split('|');
                        Model.Pacijent pacijent = new Model.Pacijent();
                        pacijent.jmbg = sadrzaj[1];
                        pacijent.ime = sadrzaj[2];
                        pacijent.prezime = sadrzaj[3];
                        Pacijenti.Add(pacijent);

                    }

                    lstPacijenti.ItemsSource = Pacijenti;
                }
            }
        }

        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            if(lstPacijenti.SelectedIndex != -1)
            {
                pacijent = Pacijenti.ElementAt(lstPacijenti.SelectedIndex);
                this.Content = new PacijentInfo();
            }

        }

        public static Model.Pacijent GetPacijent()
        {
            return pacijent;
        }

    }
}
