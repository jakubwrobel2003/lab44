using System;
using System.Collections.Generic;

namespace Generyki
{
    public interface IPoprawialny<T>
    {
        T PobierzLepsząWersję();
    }

    public class Student : IComparable<Student>, IPoprawialny<Student>
    {
        public string Nazwisko { get; set; }
        public double Ocena { get; set; }

        public Student(string nazwisko, double ocena)
        {
            Nazwisko = nazwisko;
            Ocena = ocena;
        }

        public int CompareTo(Student? other)
        {
            if (other == null) return 1;
            return this.Ocena.CompareTo(other.Ocena);
        }

        public Student PobierzLepsząWersję()
        {
            return new Student(Nazwisko, Math.Min(Ocena + 0.5, 5.0));
        }

        public override string ToString()
        {
            return $"Student: {Nazwisko}, Ocena: {Ocena}";
        }
    }

    public static class Test
    {
        public static T ZnajdzWiekszy<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) >= 0 ? a : b;
        }
    }

    public class Regał<T>
    {
        public T Półka1 { get; set; } = default(T);
        public T Półka2 { get; set; } = default(T);
        public T Półka3 { get; set; } = default(T);

        public override string ToString()
        {
            return $"Półka1: {Półka1}, Półka2: {Półka2}, Półka3: {Półka3}";
        }

        public void WstawNaWolnąPółkę(T wartość)
        {
            if (EqualityComparer<T>.Default.Equals(Półka1, default(T)))
                Półka1 = wartość;
            else if (EqualityComparer<T>.Default.Equals(Półka2, default(T)))
                Półka2 = wartość;
            else if (EqualityComparer<T>.Default.Equals(Półka3, default(T)))
                Półka3 = wartość;
        }

        public T WolnaPółka
        {
            set { WstawNaWolnąPółkę(value); }
        }
    }

    public class Zadanie
    {
        public string Opis { get; set; }
        public DateTime DataWprowadzenia { get; set; }

        public Zadanie(string opis)
        {
            Opis = opis;
            DataWprowadzenia = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{Opis} (Dodano: {DataWprowadzenia})";
        }
    }

    public interface IZwiększany
    {
        void Zmień();
    }

    public interface IZmniejszany
    {
        void Zmień();
    }

    public class Samochód : IZwiększany, IZmniejszany, IPoprawialny<Samochód>
    {
        public int Prędkość { get; private set; } = 0;

        void IZwiększany.Zmień() => Prędkość += 10;
        void IZmniejszany.Zmień() => Prędkość -= 5;

        public Samochód PobierzLepsząWersję()
        {
            var kopia = new Samochód();
            ((IZwiększany)kopia).Zmień();
            ((IZwiększany)kopia).Zmień();
            return kopia;
        }

        public override string ToString() => $"Prędkość: {Prędkość} km/h";
    }
}
