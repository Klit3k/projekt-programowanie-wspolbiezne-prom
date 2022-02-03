using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PW_Projekt_v5
{
    public class Prom
    {
        MyTimer oTimer2;
        MainWindow window;
        public SemaphoreSlim stronaLewa = new(0, 1);
        public SemaphoreSlim stronaPrawa = new(1, 1);
        SemaphoreSlim zajety = new(1, 1);
        public SemaphoreSlim rozladunek = new(1, 1);
        public SemaphoreSlim zaladunek = new(0, 1);
        SemaphoreSlim usuwanie = new(1, 1);

        public Samochod[] samochody = new Samochod[4];
        Droga drogaLewa;
        Droga drogaPrawa;
        int pojemnosc = 3;
        int licznik = 0;
        bool strona;
        public void usunSamochod()
        {
            samochody[licznik] = null;
            licznik--;

        }
        public async Task plyn()
        {
            oTimer2.setCzas();
            zajety.Wait();
            oTimer2.StopTimer();
            if (strona) stronaPrawa.Wait(); else stronaLewa.Wait();
            Console.WriteLine($"Odplywam ze strony {strona} do {!strona}\n" +
                $"Semafor stronaLewa: {stronaLewa.CurrentCount.ToString()}\n" +
                $"Semafor stronaPrawa: {stronaPrawa.CurrentCount.ToString()}");

            if (strona == drogaPrawa.Strona)
            {
                Console.Write($"Plyne");
                await Task.Run(async () =>
                {
                    for (int i = -150; i < 150; i += 2)
                    {
                        window.Dispatcher.Invoke((Action)(async () =>
                        {

                            window.Dispatcher.Invoke(() =>
                            {
                                window.promyk.Margin = new Thickness(0, 0, i, 0);
                            });
                        }));
                        await Task.Delay(1);

                    }
                });
                dobijLewo();
                strona = false;
            }
            else if (strona == drogaLewa.Strona)
            {
                Console.WriteLine($"Plyne...");
                await Task.Run(async () =>
                {
                    for (int i = -150; i < 150; i += 2)
                    {
                        window.Dispatcher.Invoke((Action)(async () =>
                        {

                            window.Dispatcher.Invoke(() =>
                            {
                                window.promyk.Margin = new Thickness(i, 0, 0, 0);
                            });
                        }));
                        await Task.Delay(1);

                    }
                });
                dobijPrawo();
                strona = true;
            }
            Console.WriteLine($"Doplynalem do: {strona}" +
                $"\nSemafor stronaLewa: {stronaLewa.CurrentCount.ToString()}\n" +
                $"Semafor stronaPrawa: {stronaPrawa.CurrentCount.ToString()}");

            Console.WriteLine("Rozpoczynam rozladunek");
            rozladunek.Wait();

                int temp = Licznik;
                for (int i = 0; i < temp; i++)
                {
                    usuwanie.Wait();

                    Console.WriteLine($"Usunieto samochod");
                      usunSamochod();
                await Task.Delay(1000);
                     usuwanie.Release();
                }

 
            rozladunek.Release();
            Console.WriteLine("\nZakonczono rozladunek");
            oTimer2.StartTimer();
            Console.WriteLine("Rozpoczynam zaladunek");
            zaladunek.Wait();
            zaladunek.Wait();
            zaladunek.Wait();

            //akcja zaladunku
            Console.WriteLine($"\nZakonczono zaladunek sem zaladunek: {zaladunek.CurrentCount}");
            zajety.Release();
            this.plyn();

        }
        public void dobijPrawo()
        {
            stronaPrawa.Release();
        }
        public void dobijLewo()
        {
            stronaLewa.Release();
        }
        public int Pojemnosc { get => pojemnosc; }
        public bool Strona { get => strona; }
        public int Licznik { get => licznik; set => licznik = value; }
        public Prom(MainWindow window, bool strona, Droga drogaLewa, Droga drogaPrawa)
        {
            this.window = window;
            this.strona = strona;
            this.drogaLewa = drogaLewa;
            this.drogaPrawa = drogaPrawa;
            oTimer2 = new(window, this, drogaPrawa, drogaLewa, 1);
            Task.Run(() => { oTimer2.StartTimer(); });
        }
    }
}