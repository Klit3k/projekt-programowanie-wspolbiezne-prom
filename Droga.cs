using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PW_Projekt_v5
{
    public class Droga
    {
        public SemaphoreSlim animacja = new(1, 1);
        MainWindow window;
        Samochod[] samochody = new Samochod[4];
        Prom prom;
        bool strona;
        string nazwa;
        int pojemnosc = 3;
        int licznik = 0;
        int losuj;
        public async Task stworzSamochodAsync()
        {
            animacja.Wait();
            if (pojemnosc != licznik)
            {
                Samochod temp = new(window, this);
                await temp.wjedz();
                samochody[licznik] = new(window, this);
                //await samochody[licznik].wjedz();
                licznik++;
           }
            animacja.Release();

        }
        public async void dodajSamochodNaProm()
        {
            if (prom.Licznik != 3)
            {
                prom.samochody[prom.Licznik] = samochody[licznik];
                prom.Licznik++;
                usunSamochod();
                prom.zaladunek.Release();
                await Task.Delay(500);
            }

        }
        public void usunSamochod()
        {
            licznik--;
            samochody[licznik] = null;
        }
        public void dodajProm(Prom prom)
        {
            this.prom = prom;
        }
        public Droga(MainWindow window, string nazwa, bool strona)
        {
            this.window = window;
            this.nazwa = nazwa;
            this.strona = strona;
        }
        public int Licznik { get => licznik; }
        public bool Strona { get => strona; }
        public string Nazwa { get => nazwa; }

    }
}