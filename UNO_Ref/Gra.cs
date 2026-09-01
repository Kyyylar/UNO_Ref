namespace UNO_Ref
{
    internal class Gra
    {
        private bool turaGracza;
        private int iloscKartDoDobrania;
        private string wybranyKolor;
        public Gra()
        {
            turaGracza = true;
            iloscKartDoDobrania = 1;
            wybranyKolor = "0";

            Talia talia = new Talia();
            Gracz gracz = new Gracz();
            Komputer komputer = new Komputer();

            //Rozpoczęcie gry
            RozdajKarty(gracz, komputer, talia);
            WylozPierwszaKarte(talia);
            while (gracz.KartyNaRece.Count > 0 && komputer.KartyNaRece.Count > 0)
            {
                if (turaGracza)
                {
                    RuchGracza(gracz, komputer, talia);
                }
                else RuchKomputera(komputer, talia);
            }
            Console.Clear();

            //Rozstrzygnięcie gry
            if (gracz.KartyNaRece.Count == 0)
            {
                Console.WriteLine("Gratulację, wygrałeś!");
            }
            else Console.WriteLine("Niestety przegrałeś :(");
            Console.ReadKey();
        }
        //Metoda sprawdzająca czy karta może zostać wyłożona na kartę znajdującą się na wierzchu stosu
        private bool CzyKartaPasuje(string karta, List<string> kartyNaStole)
        {
            string kartaZWierzchu = kartyNaStole.Last();

            if (karta[0] == wybranyKolor[0] && iloscKartDoDobrania == 1)
            {
                return true;
            }
            else if (karta.Substring(1) == "13")
            {
                return true;
            }
            else if (karta.Substring(1) == "14" && iloscKartDoDobrania == 1)
            {
                return true;
            }
            else if (karta.Substring(1) == kartaZWierzchu.Substring(1))
            {
                return true;
            }
            return false;
        }
        private void RuchGracza(Gracz gracz, Komputer komputer, Talia talia)
        {
            turaGracza = false;
            string wybor = "";
            string kodKarty = "";
            bool powtorzRuch = false;

            do
            {
                powtorzRuch = false;
                Console.Clear();
                gracz.WyswietlKarty();
                Console.WriteLine();
                Console.WriteLine("Liczba kart przeciwnika: " + komputer.KartyNaRece.Count);
                Console.WriteLine();
                Console.WriteLine("Karta na wierzchu: " + Talia.TlumaczKarte(talia.KartyNaStole.Last()));
                Console.WriteLine();
                Console.WriteLine("Wybierz jedną z opcji: ");
                Console.WriteLine("1 - Wyłóż kartę");
                Console.WriteLine("2 - Dobierz kartę");
                wybor = Console.ReadLine();
                if ((wybor != "1") && (wybor != "2"))
                {
                    powtorzRuch = true;
                    Console.WriteLine("Podano nieprawidłową wartość! Podaj 1 lub 2");
                    Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                    Console.ReadKey();
                }
                else
                {
                    //Wykładanie karty
                    if (wybor == "1")
                    {
                        Console.WriteLine("Jaką kartę chcesz wyłożyć? Podaj jej kod podany w nawiasie");
                        kodKarty = Console.ReadLine();
                        if (gracz.KartyNaRece.Contains(kodKarty) == false)
                        {
                            powtorzRuch = true;
                            Console.WriteLine("Nie posiadasz karty: " + Talia.TlumaczKarte(kodKarty));
                            Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                            Console.ReadKey();
                        }
                        else
                        {
                            if (CzyKartaPasuje(kodKarty, talia.KartyNaStole) == false)
                            {
                                powtorzRuch = true;
                                Console.WriteLine("Nie możesz położyć karty: " + Talia.TlumaczKarte(kodKarty) + " na kartę: " + Talia.TlumaczKarte(talia.KartyNaStole.Last()));
                                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                                Console.ReadKey();
                            }
                            else
                            {
                                gracz.WylozKarte(kodKarty);
                                talia.KartyNaStole.Add(kodKarty);
                                wybranyKolor = $"{kodKarty[0]}";
                                EfektKarty(kodKarty, komputer);
                                Console.WriteLine("Wyłożyłeś kartę: " + Talia.TlumaczKarte(kodKarty));
                                Console.WriteLine();
                                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                                Console.ReadKey();
                            }
                        }
                    }

                    //Dobieranie karty
                    else
                    {
                        for (int i = 0; i < iloscKartDoDobrania; i++)
                        {
                            gracz.DobierzKarte(talia.TaliaKart.Last());
                            talia.TaliaKart.RemoveAt(talia.TaliaKart.Count - 1);
                            Console.WriteLine("Dobrałeś kartę: " + Talia.TlumaczKarte(gracz.KartyNaRece.Last()));
                        }
                        iloscKartDoDobrania = 1;
                        Console.WriteLine();
                        Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                        Console.ReadKey();
                    }
                }
            } while (powtorzRuch);
        }
        private void RuchKomputera(Komputer komputer, Talia talia)
        {
            turaGracza = true;

            //Symulacja myślenia komputera
            Console.Clear();
            Console.WriteLine("Ruch przeciwnika... ");
            System.Threading.Thread.Sleep(600);
            Console.WriteLine(".");
            System.Threading.Thread.Sleep(600);
            Console.WriteLine(".");
            System.Threading.Thread.Sleep(600);
            Console.WriteLine(".");
            Console.WriteLine();

            //Sprawdzenie czy komputer posiada pasującą kartę
            bool czyJestPasujacaKarta = false;
            int liczbaKartDoSprawdzenia = komputer.KartyNaRece.Count;
            string kartaDoSprawdzenia = komputer.KartyNaRece[liczbaKartDoSprawdzenia - 1];
            while (czyJestPasujacaKarta == false && liczbaKartDoSprawdzenia > 0)
            {
                kartaDoSprawdzenia = komputer.KartyNaRece[liczbaKartDoSprawdzenia - 1];
                if (CzyKartaPasuje(kartaDoSprawdzenia, talia.KartyNaStole) == true)
                {
                    czyJestPasujacaKarta = true;
                }
                else
                {
                    liczbaKartDoSprawdzenia--;
                }
            }
            //Jeżeli komputer posiada pasującą kartę, wyłoży ją, w przeciwnym wypadku dobierze kartę
            if (czyJestPasujacaKarta == true)
            {
                komputer.WylozKarte(kartaDoSprawdzenia);
                talia.KartyNaStole.Add(kartaDoSprawdzenia);
                wybranyKolor = $"{kartaDoSprawdzenia[0]}";
                EfektKarty(kartaDoSprawdzenia, komputer);
                Console.WriteLine("Komputer wyłożył kartę: " + Talia.TlumaczKarte(kartaDoSprawdzenia));
                Console.WriteLine();
                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                Console.ReadKey();
            }
            else
            {
                for (int i = 0; i < iloscKartDoDobrania; i++)
                {
                    komputer.DobierzKarte(talia.TaliaKart.Last());
                    talia.TaliaKart.RemoveAt(talia.TaliaKart.Count - 1);
                    Console.WriteLine("Komputer dobrał kartę");
                }
                iloscKartDoDobrania = 1;
                Console.WriteLine();
                Console.WriteLine("Wciśnij dowolny przycisk aby kontynuować...");
                Console.ReadKey();
            }
        }
        private void EfektKarty(string karta, Komputer komputer)
        {
            int rodzajKarty = Convert.ToInt32(karta.Substring(1));
            bool czyPoprawnyKolor = true;
            string kolor = "";

            if (rodzajKarty == 10 || rodzajKarty == 11)
            {
                //Postój albo Zmiana Kierunku (w przypadku gdy grają tylko dwie osoby, zmiana kierunku jest równoznaczna z postojem)
                turaGracza = !turaGracza;
            }
            else if (rodzajKarty == 12)
            {
                //+2
                if (iloscKartDoDobrania == 1) iloscKartDoDobrania += 1;
                else iloscKartDoDobrania += 2;
            }
            else if (rodzajKarty == 13)
            {
                //+4
                if (iloscKartDoDobrania == 1) iloscKartDoDobrania += 3;
                else iloscKartDoDobrania += 4;
                //Zmiana koloru
                if (turaGracza == false)
                {
                    Console.WriteLine("Na jaki kolor zmienić? Podaj kod koloru z nawiasu");
                    Console.WriteLine("Czerwony (0)");
                    Console.WriteLine("Zielony (1)");
                    Console.WriteLine("Niebieski (2)");
                    Console.WriteLine("Żółty (3)");
                    kolor = Console.ReadLine();
                    if (kolor != "0" && kolor != "1" && kolor != "2" && kolor != "3")
                    {
                        czyPoprawnyKolor = false;
                    }
                    while (czyPoprawnyKolor == false)
                    {
                        Console.WriteLine("Podaj poprawny kod koloru (0/1/2/3) !");
                        kolor = Console.ReadLine();
                        if (kolor != "0" && kolor != "1" && kolor != "2" && kolor != "3")
                        {
                            czyPoprawnyKolor = false;
                        }
                        else czyPoprawnyKolor = true;
                    }
                    wybranyKolor = kolor;
                }
                else
                {
                    int iloscKartDanegoKoloru = 0;
                    int iloscKartDanegoKoloruMax = 0;
                    int indeksKoloruMax = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        iloscKartDanegoKoloru = 0;
                        for (int j = 0; j < komputer.KartyNaRece.Count; j++)
                        {
                            if (komputer.KartyNaRece[j][0] == i)
                            {
                                iloscKartDanegoKoloru++;
                            }
                        }
                        if (iloscKartDanegoKoloru > iloscKartDanegoKoloruMax)
                        {
                            iloscKartDanegoKoloruMax = iloscKartDanegoKoloru;
                            indeksKoloruMax = i;
                        }
                    }
                    wybranyKolor = $"{indeksKoloruMax}";
                    Console.WriteLine("Kolor został zmieniony na: " + Talia.KoloryKart[indeksKoloruMax]);
                }
            }
            else if (rodzajKarty == 14)
            {
                //Zmiana koloru
                if (turaGracza == false)
                {
                    Console.WriteLine("Na jaki kolor zmienić? Podaj kod koloru z nawiasu");
                    Console.WriteLine("Czerwony (0)");
                    Console.WriteLine("Zielony (1)");
                    Console.WriteLine("Niebieski (2)");
                    Console.WriteLine("Żółty (3)");
                    kolor = Console.ReadLine();
                    if (kolor != "0" && kolor != "1" && kolor != "2" && kolor != "3")
                    {
                        czyPoprawnyKolor = false;
                    }
                    while (czyPoprawnyKolor == false)
                    {
                        Console.WriteLine("Podaj poprawny kod koloru (0/1/2/3) !");
                        kolor = Console.ReadLine();
                        if (kolor != "0" && kolor != "1" && kolor != "2" && kolor != "3")
                        {
                            czyPoprawnyKolor = false;
                        }
                        else czyPoprawnyKolor = true;
                    }
                    wybranyKolor = kolor;
                }
                else
                {
                    int iloscKartDanegoKoloru = 0;
                    int iloscKartDanegoKoloruMax = 0;
                    int indeksKoloruMax = 0;

                    for (int i = 0; i < 4; i++)
                    {
                        iloscKartDanegoKoloru = 0;
                        for (int j = 0; j < komputer.KartyNaRece.Count; j++)
                        {
                            if (komputer.KartyNaRece[j][0] == i)
                            {
                                iloscKartDanegoKoloru++;
                            }
                        }
                        if (iloscKartDanegoKoloru > iloscKartDanegoKoloruMax)
                        {
                            iloscKartDanegoKoloruMax = iloscKartDanegoKoloru;
                            indeksKoloruMax = i;
                        }
                    }
                    wybranyKolor = $"{indeksKoloruMax}";
                    Console.WriteLine("Kolor został zmieniony na: " + Talia.KoloryKart[indeksKoloruMax]);
                }
            }
        }
        private void RozdajKarty(Gracz gracz, Komputer komputer, Talia talia)
        {
            for (int i = 0; i < 7; i++)
            {
                gracz.KartyNaRece.Add(talia.TaliaKart.Last());
                talia.TaliaKart.RemoveAt(talia.TaliaKart.Count - 1);
                komputer.KartyNaRece.Add(talia.TaliaKart.Last());
                talia.TaliaKart.RemoveAt(talia.TaliaKart.Count - 1);
            }
        }
        private void WylozPierwszaKarte(Talia talia)
        {
            bool czyPasuje = false;
            int indeksKarty = talia.TaliaKart.Count - 1;
            string karta = talia.TaliaKart[indeksKarty];

            //Sprawdzenie czy karta nie jest kartą specjalną (działa tylko na zwykłe karty)
            while (czyPasuje == false)
            {
                if (Convert.ToInt32(karta.Substring(1)) < 10)
                {
                    czyPasuje = true;
                }
                else
                {
                    indeksKarty--;
                    karta = talia.TaliaKart[indeksKarty];
                }
            }
            wybranyKolor = $"{karta[0]}";
            talia.KartyNaStole.Add(karta);
            talia.TaliaKart.RemoveAt(indeksKarty);
        }

    }
}
