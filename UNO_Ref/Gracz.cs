namespace UNO_Ref
{
    internal class Gracz
    {
        private List<string> kartyNaRece;
        public List<string> KartyNaRece
        {
            get
            {
                return kartyNaRece;
            }
        }
        public Gracz()
        {
            kartyNaRece = new List<string>();
        }
        public void WylozKarte(string karta)
        {
            kartyNaRece.Remove(karta);
        }
        public void DobierzKarte(string karta)
        {
            kartyNaRece.Add(karta);
        }
        public void WyswietlKarty()
        {
            Console.WriteLine("Twoje Karty: \n");
            kartyNaRece.ForEach(x => Console.WriteLine($"{Talia.TlumaczKarte(x)}, "));
        }
    }
}

