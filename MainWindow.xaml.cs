using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Generyki;
namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Student> studenci;
        private Regał<string> regał = new Regał<string>();

        public MainWindow()
        {
            InitializeComponent();
            AktualizujPółki();
            studenci = new Dictionary<string, Student>{
                {"1",new Student("MESSI",5.0) },
                {"2",new Student("GAVI",5.0) },
                {"3",new Student("PEDRI",5.0) },
                {"4",new Student("BALDE",5.0) },
                {"5",new Student("VINI",5.0) }
            };
        }

        private void AktualizujPółki()
        {
            txtPółka1.Text = $"Półka1: {regał.Półka1}";
            txtPółka2.Text = $"Półka2: {regał.Półka2}";
            txtPółka3.Text = $"Półka3: {regał.Półka3}";
        }

        // Zdarzenie kliknięcia przycisku "Wstaw Na Wolną Półkę"
        private void OnWstawNaWolnąPółkęClick(object sender, RoutedEventArgs e)
        {
            string wartość = inputTextBox.Text;
            regał.WstawNaWolnąPółkę(wartość);
            AktualizujPółki();
        }

        // Zdarzenie kliknięcia przycisku "Wstaw Na Półkę 1"
        private void OnWstawNaPółkę1Click(object sender, RoutedEventArgs e)
        {
            string wartość = inputTextBox.Text;
            regał.Półka1 = wartość;
            AktualizujPółki();
        }

        // Zdarzenie kliknięcia przycisku "Wstaw Na Półkę 2"
        private void OnWstawNaPółkę2Click(object sender, RoutedEventArgs e)
        {
            string wartość = inputTextBox.Text;
            regał.Półka2 = wartość;
            AktualizujPółki();
        }

        // Zdarzenie kliknięcia przycisku "Wstaw Na Półkę 3"
        private void OnWstawNaPółkę3Click(object sender, RoutedEventArgs e)
        {
            string wartość = inputTextBox.Text;
            regał.Półka3 = wartość;
            AktualizujPółki();
        }
    

    private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            string numerAlbumu = txtNumerAlbumu.Text;

            if (studenci.ContainsKey(numerAlbumu))
            {
                Student znalezionyStudent = studenci[numerAlbumu];
                MessageBox.Show($"Student: {znalezionyStudent.Nazwisko}\nOcena: {znalezionyStudent.Ocena}",
                                "Znaleziony student", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Nie znaleziono studenta o podanym numerze albumu.",
                                "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCreateStozek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stozek stozek = new Stozek();

                stozek.OBSLUGA += Stozek_OBSLUGA;

                double wysokosc = Convert.ToDouble(txtWysokosc.Text);
                double promien = Convert.ToDouble(txtPromien.Text);

                stozek.H = wysokosc;
                stozek.Promien = promien;
            }
            catch (FormatException)
            {
                MessageBox.Show("Wprowadź poprawne liczby w polach wysokości i promienia.");
            }
        }


        private void Stozek_OBSLUGA(string opisBledu)
        {

            MessageBox.Show(opisBledu, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);


            lblError.Content = opisBledu;
        }
        private void TestZnajdzWiekszy_Click(object sender, RoutedEventArgs e)
        {
            string a1 = "ALL";
            string a2 = "BLL";
            string wynik1 = Test.ZnajdzWiekszy(a1, a2);
            MessageBox.Show("Większy string: " + wynik1);
            double b1 = 3;
            double b2 = 4;
            double wynik2 = Test.ZnajdzWiekszy(b1, b2);
            MessageBox.Show("Większy double " + wynik2);

            Student s1 = new Student("MESSI", 5.0);
           Student s2 = new Student("MESSI", 5.0);

            Student s3 = Test.ZnajdzWiekszy(s1, s2);
            MessageBox.Show("Lepszy student: " + s3);
        }
    }


    public class Stozek
    {
        public delegate void ObslugaBledu(string OPIS);

        public event ObslugaBledu? OBSLUGA;


        private double h;
        public double H
        {
            set
            {
                h = value;
                if (h < 0 && OBSLUGA != null)
                {
                    OBSLUGA("ZLE");
                }


            }
        }
        private double p;
        public double Promien
        {
            set
            {
                p = value;
                if (p < 0 && OBSLUGA != null)
                {
                    OBSLUGA("ZLE");
                }



            }
        }

    }
}