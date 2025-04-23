using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Generyki;

namespace WpfApp5
{
    public partial class MainWindow : Window
    {
        private Dictionary<string, Student> studenci;
        private Regał<string> regał = new Regał<string>();
        private Queue<Zadanie> kolejka = new Queue<Zadanie>();

        public MainWindow()
        {
            InitializeComponent();
            AktualizujPółki();
            studenci = new Dictionary<string, Student>{
                {"1",new Student("MESSI",5.0)},
                {"2",new Student("GAVI",5.0)},
                {"3",new Student("PEDRI",5.0)},
                {"4",new Student("BALDE",5.0)}
            };
        }

        private void AktualizujPółki()
        {
            txtPółka1.Text = $"Półka1: {regał.Półka1}";
            txtPółka2.Text = $"Półka2: {regał.Półka2}";
            txtPółka3.Text = $"Półka3: {regał.Półka3}";
        }

        private void OnWstawNaWolnąPółkęClick(object sender, RoutedEventArgs e)
        {
            regał.WstawNaWolnąPółkę(inputTextBox.Text);
            AktualizujPółki();
        }

        private void OnWstawNaPółkę1Click(object sender, RoutedEventArgs e)
        {
            regał.Półka1 = inputTextBox.Text;
            AktualizujPółki();
        }

        private void OnWstawNaPółkę2Click(object sender, RoutedEventArgs e)
        {
            regał.Półka2 = inputTextBox.Text;
            AktualizujPółki();
        }

        private void OnWstawNaPółkę3Click(object sender, RoutedEventArgs e)
        {
            regał.Półka3 = inputTextBox.Text;
            AktualizujPółki();
        }

        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            string nr = txtNumerAlbumu.Text;
            if (studenci.TryGetValue(nr, out Student student))
            {
                MessageBox.Show($"Student: {student.Nazwisko}, Ocena: {student.Ocena}");
            }
            else
            {
                MessageBox.Show("Nie znaleziono studenta");
            }
        }

        private void btnCreateStozek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Stozek stozek = new Stozek();
                stozek.OBSLUGA += Stozek_OBSLUGA;

                stozek.H = double.Parse(txtWysokosc.Text);
                stozek.Promien = double.Parse(txtPromien.Text);
            }
            catch
            {
                MessageBox.Show("Podaj liczby!");
            }
        }

        private void Stozek_OBSLUGA(string opisBledu)
        {
            MessageBox.Show(opisBledu, "Błąd");
            lblError.Content = opisBledu;
        }

        private void TestZnajdzWiekszy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("String: " + Test.ZnajdzWiekszy("AAA", "BBB"));
            MessageBox.Show("Double: " + Test.ZnajdzWiekszy(3.5, 7.2));

            Student s1 = new Student("Lewy", 4.5);
            Student s2 = new Student("Messi", 5.0);
            MessageBox.Show("Student: " + Test.ZnajdzWiekszy(s1, s2));
        }

        private void btnTestSamochod_Click(object sender, RoutedEventArgs e)
        {
            var auto = new Samochód();
            ((IZwiększany)auto).Zmień();
            listBox.Items.Add(auto.ToString());
            ((IZmniejszany)auto).Zmień();
            listBox.Items.Add(auto.ToString());
        }

        private void btnPopraw_Click(object sender, RoutedEventArgs e)
        {
            var student = new Student("Pedri", 4.0);
            listBox.Items.Add("Przed: " + student);
            var lepszy = student.PobierzLepsząWersję();
            listBox.Items.Add("Po: " + lepszy);
        }

        private void DodajZadanie_Click(object sender, RoutedEventArgs e)
        {
            string opis = Microsoft.VisualBasic.Interaction.InputBox("Wpisz opis zadania:", "Dodaj zadanie");
            if (!string.IsNullOrWhiteSpace(opis))
            {
                kolejka.Enqueue(new Zadanie(opis));
            }
        }

        private void PobierzZadanie_Click(object sender, RoutedEventArgs e)
        {
            if (kolejka.Count > 0)
            {
                var z = kolejka.Dequeue();
                MessageBox.Show(z.ToString());
            }
            else
            {
                MessageBox.Show("Brak zadań w kolejce.");
            }
        }
        private void btnWyswietlStos_Click(object sender, RoutedEventArgs e)
        {
            Stack<IWyświetlana> stos = new Stack<IWyświetlana>();
            stos.Push(new Towar("Komputer", "Elektronika"));
            stos.Push(new Towar("Chleb", "Spożywcze"));
            stos.Push(new Towar("Koszulka", "Odzież"));
            stos.Push(new Mieszkanie("Ul. Długa 1", 3));
            stos.Push(new Mieszkanie("Ul. Krótka 5", 2));
            stos.Push(new Mieszkanie("Rynek 10", 4));

            listBox.Items.Clear();
            foreach (var item in stos)
            {
                item.WyświetlDane(listBox);
            }
        }
        private void btnStudentPolitechniki_Click(object sender, RoutedEventArgs e)
        {
            StudentPolitechniki student = new StudentPolitechniki("99010112345", "123456");

            string pesel = ((IObywatel)student).Identyfikator;
            string album = ((IStudent)student).Identyfikator;

            lblPesel.Content = "PESEL: " + pesel;
            lblAlbum.Content = "Nr albumu: " + album;
        }
        private void btnPudełkoTest_Click(object sender, RoutedEventArgs e)
        {
            Pudełko<string> pudelko = new Pudełko<string>(10);

            pudelko.Włóż("Jabłko", 0);
            pudelko.Włóż("Gruszka", 1);
            pudelko.Włóż("Banan", 2);

            listBox.Items.Clear();
            for (int i = 0; i < 3; i++)
            {
                listBox.Items.Add(pudelko.Wyciągnij(i));
            }
        }
        private void btnJedenZTrzechString_Click(object sender, RoutedEventArgs e)
        {
            string wynik = Narzędzia.JedenZTrzech("Ala", "Ola", "Ela");
            MessageBox.Show("Wylosowano: " + wynik);
        }

        private void btnJedenZTrzechArmata_Click(object sender, RoutedEventArgs e)
        {
            Armata a1 = new Armata(120, 1000);
            Armata a2 = new Armata(90, 800);
            Armata a3 = new Armata(150, 1500);

            Armata wynik = Narzędzia.JedenZTrzech(a1, a2, a3);
            MessageBox.Show("Wylosowano: " + wynik);
        }

    }

    public class Stozek
    {
        public delegate void ObslugaBledu(string opis);
        public event ObslugaBledu? OBSLUGA;

        private double h;
        public double H
        {
            set
            {
                if (value < 0)
                    OBSLUGA?.Invoke("Wysokość nie może być ujemna.");
                else
                    h = value;
            }
        }

        private double p;
        public double Promien
        {
            set
            {
                if (value < 0)
                    OBSLUGA?.Invoke("Promień nie może być ujemny.");
                else
                    p = value;
            }
        }
    }
    public interface IWyświetlana
    {
        void WyświetlDane(ListBox listBox);
    }

    public class Towar : IWyświetlana
    {
        public string Nazwa { get; set; }
        public string Typ { get; set; }

        public Towar(string nazwa, string typ)
        {
            Nazwa = nazwa;
            Typ = typ;
        }

        public void WyświetlDane(ListBox listBox)
        {
            listBox.Items.Add($"Towar: {Nazwa}, Typ: {Typ}");
        }
    }

    public class Mieszkanie : IWyświetlana
    {
        public string Adres { get; set; }
        public int LiczbaPokoi { get; set; }

        public Mieszkanie(string adres, int liczbaPokoi)
        {
            Adres = adres;
            LiczbaPokoi = liczbaPokoi;
        }

        public void WyświetlDane(ListBox listBox)
        {
            listBox.Items.Add($"Mieszkanie: {Adres}, Pokoje: {LiczbaPokoi}");
        }
    }
    public interface IObywatel
    {
        string Identyfikator { get; }
    }

    public interface IStudent
    {
        string Identyfikator { get; }
    }

    public class StudentPolitechniki : IObywatel, IStudent
    {
        private string pesel;
        private string nrAlbumu;

        public StudentPolitechniki(string pesel, string nrAlbumu)
        {
            this.pesel = pesel;
            this.nrAlbumu = nrAlbumu;
        }

        string IObywatel.Identyfikator => pesel;
        string IStudent.Identyfikator => nrAlbumu;
    }
    public class Pudełko<T>
    {
        private T[] przegrody;

        public Pudełko(int rozmiar)
        {
            przegrody = new T[rozmiar];
        }

        public void Włóż(T element, int nrPrzegrody)
        {
            if (nrPrzegrody < 0 || nrPrzegrody >= przegrody.Length)
                throw new ArgumentException("Numer przegrody poza zakresem.");

            przegrody[nrPrzegrody] = element;
        }

        public T Wyciągnij(int nrPrzegrody)
        {
            if (nrPrzegrody < 0 || nrPrzegrody >= przegrody.Length)
                throw new ArgumentException("Numer przegrody poza zakresem.");

            return przegrody[nrPrzegrody];
        }
    }
    public static class Narzędzia
    {
        public static T JedenZTrzech<T>(T a, T b, T c)
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 3);
            return i switch
            {
                0 => a,
                1 => b,
                _ => c,
            };
        }
    }
    public class Armata
    {
        public double Kaliber { get; set; }
        public double Masa { get; set; }

        public Armata(double kaliber, double masa)
        {
            Kaliber = kaliber;
            Masa = masa;
        }

        public override string ToString()
        {
            return $"Kaliber: {Kaliber}, Masa: {Masa}kg";
        }
    }

}
