using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppSzamla
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //1) eset listával
        List<Tetel> tetelek = new List<Tetel>();

        //2) ObservableCollection<Tetel> tetelek = new ObservableCollection<Tetel>(); //automatán frissül a hozzákapcsolódó vezérlő (ItemSource)
        List<String> egysegek = new List<string> { "db", "l", "kg", "m" };

        double kosarErtek = 0;

        public MainWindow()
        {
            InitializeComponent();
            dgTetelek.ItemsSource = tetelek;
            cbEgyseg.ItemsSource = egysegek;
            cbEgyseg.SelectedIndex = 0;
        }

        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            var sorok = tetelek.Select(ob => $"{ob.TermekNev};{ob.EgysegAr};{ob.Egyseg};{ob.Mennyiseg}").ToList();
            File.WriteAllLines("lista.csv", sorok);
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            kosarErtek += int.Parse(txtAr.Text) * sliMennyiseg.Value;
            labSzum.Content = kosarErtek+" Ft";

            Tetel ujtetel = new Tetel(txtNev.Text, int.Parse(txtAr.Text), cbEgyseg.SelectedItem.ToString(), sliMennyiseg.Value);
            tetelek.Add(ujtetel);

            //Módosítás esetén (új elem, törlés, tartalom módosítás), szeretném a változást a DataGrid-ben látni akkor két út van:
            //1) List<Tetel> tetelek = new List<Tetel>(); esetben
            dgTetelek.ItemsSource = null;
            dgTetelek.ItemsSource = tetelek;

            //2) Ha a "listám" ilyen :  ObservableCollection<Tetel> tetelek = new ObservableCollection<Tetel>();
            // akkor nem kell semmi, mivel magától frissíül
        }

        private void cbEgyseg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEgyseg.SelectedItem.ToString() != "db")
            {
                sliMennyiseg.IsSnapToTickEnabled = false;
            }
            else
            {
                sliMennyiseg.IsSnapToTickEnabled = true;
            }
        }

        private void btnBetolt_Click(object sender, RoutedEventArgs e)
        {
            tetelek.Clear();
            kosarErtek = 0;
            var sorok = File.ReadAllLines("lista.csv");
            foreach (var sor in sorok)
            {
                var mezoErtekek = sor.Split(';');
                Tetel ujTetel = new Tetel(mezoErtekek[0], int.Parse(mezoErtekek[1]), mezoErtekek[2], double.Parse(mezoErtekek[3]));
                tetelek.Add(ujTetel);
                kosarErtek += int.Parse(mezoErtekek[1]) * double.Parse(mezoErtekek[3]);
            }
            labSzum.Content = kosarErtek + " Ft";
            //Ha List<Tetel> tetelek = new List<Tetel>(); használom akkor kell:
            dgTetelek.ItemsSource = null;
            dgTetelek.ItemsSource = tetelek;

        }
    }
}