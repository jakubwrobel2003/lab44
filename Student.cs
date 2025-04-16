namespace Generyki
{
    public class Student : IComparable<Student>
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

        public override string ToString()
        {
            return $"STUDent {Nazwisko}, OCena {Ocena}";
        }
    }
    public static class Test
    {
        public static T ZnajdzWiekszy<T>(T a, T b) where T : IComparable<T>
        {
            
            return a.CompareTo(b) > 0 ? a : b;
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
            {
                Półka1 = wartość;
            }
            else if (EqualityComparer<T>.Default.Equals(Półka2, default(T)))
            {
                Półka2 = wartość;
            }
            else if (EqualityComparer<T>.Default.Equals(Półka3, default(T)))
            {
                Półka3 = wartość;
            }
           
        }

          public T WolnaPółka
        {
            set { WstawNaWolnąPółkę(value); }
        }
    }
}