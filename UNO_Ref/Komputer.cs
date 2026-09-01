namespace UNO_Ref
{
    internal class Komputer
    {
        private List<string> kartyNaRece;
        public List<string> KartyNaRece
        {
            get
            {
                return kartyNaRece;
            }
        }
        public Komputer()
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
    }
}
