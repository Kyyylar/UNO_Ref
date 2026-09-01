namespace UNO_Ref
{
    internal class Talia
    {
        private static string[] koloryKart = { "Czerwone", "Zielone", "Niebieskie", "Żółte", "Czarne" };
        public static string[] KoloryKart
        {
            get
            {
                return koloryKart;
            }
        }
        private static string[] rodzajeKart = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "Postój", "Zmiana kierunku", "+2", "+4", "Zmiana koloru" };
        public static string[] RodzajeKart
        {
            get
            {
                return rodzajeKart;
            }
        }

        // Wiersze odpowiadają kolorom kart, a kolumny odpowiadają rodzajom kart. Wartości w tablicy określają ilość kart danego rodzaju i koloru w talii.
        private int[,] iloscKartDanegoRodzaju =
        {
            { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0}, //czerwony
            { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0}, //zielony
            { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0}, //niebieski
            { 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 0}, //żółty
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4} //czarny
        };

        public List<string> KartyNaStole { get; set; } = new List<string>();
        public List<string> TaliaKart { get; set; } = new List<string>();

        public Talia()
        {
            int iloscKolorow = koloryKart.Length;
            int iloscRodzajówKart = rodzajeKart.Length;
            int iloscTakichKart;
            List<string> kartyPozostaleDoUzycia = new List<string>();

            //Wypisuje wszystkie karty do listy kartyPozostaleDoUzycia
            for (int i = 0; i < iloscKolorow; i++)
            {
                for (int j = 0; j < iloscRodzajówKart; j++)
                {
                    iloscTakichKart = iloscKartDanegoRodzaju[i, j];
                    if (iloscTakichKart > 0)
                    {
                        for (int k = iloscTakichKart; k > 0; k--)
                        {
                            kartyPozostaleDoUzycia.Add($"{i}{j}");
                        }
                    }
                }
            }

            //Tworzenie talii w losowej kolejności
            Random rnd = new Random();
            int losowyIndeksKarty;
            while (kartyPozostaleDoUzycia.Count > 0)
            {
                losowyIndeksKarty = rnd.Next(kartyPozostaleDoUzycia.Count);
                TaliaKart.Add(kartyPozostaleDoUzycia[losowyIndeksKarty]);
                kartyPozostaleDoUzycia.RemoveAt(losowyIndeksKarty);
            }
        }
        // Metoda tłumacząca kod karty na czytelny format (kolor i rodzaj karty)
        public static string TlumaczKarte(string karta)
        {
            string kolor = "";
            string rodzajKarty = "";

            if (karta == "")
            {
                return "";
            }
            switch (karta[0])
            {
                case '0':
                    kolor = "Czerwone";
                    break;
                case '1':
                    kolor = "Zielone";
                    break;
                case '2':
                    kolor = "Niebieskie";
                    break;
                case '3':
                    kolor = "Żółte";
                    break;
                case '4':
                    kolor = "Czarne";
                    break;
            }
            switch (karta.Substring(1))
            {
                case "0":
                    rodzajKarty = "0";
                    break;
                case "1":
                    rodzajKarty = "1";
                    break;
                case "2":
                    rodzajKarty = "2";
                    break;
                case "3":
                    rodzajKarty = "3";
                    break;
                case "4":
                    rodzajKarty = "4";
                    break;
                case "5":
                    rodzajKarty = "5";
                    break;
                case "6":
                    rodzajKarty = "6";
                    break;
                case "7":
                    rodzajKarty = "7";
                    break;
                case "8":
                    rodzajKarty = "8";
                    break;
                case "9":
                    rodzajKarty = "9";
                    break;
                case "10":
                    rodzajKarty = "Postój";
                    break;
                case "11":
                    rodzajKarty = "Zmiana kierunku";
                    break;
                case "12":
                    rodzajKarty = "+2";
                    break;
                case "13":
                    rodzajKarty = "+4";
                    break;
                case "14":
                    rodzajKarty = "Zmiana koloru";
                    break;
            }
            return String.Concat(kolor + " " + rodzajKarty + $" ({karta})");
        }
    }
}
