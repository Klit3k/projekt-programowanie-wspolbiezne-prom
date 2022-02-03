using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PW_Projekt_v5
{
    public class MyTimer
    {
        private int start = 0;
        Timer oTimer;
        Prom prom;
        Droga drogaLewa, drogaPrawa;
        MainWindow window;
        static int czas = 20;
        int temp;
        int czas_remember = czas;
        int tryb;
        public MyTimer(MainWindow window,Prom prom, Droga drogaPrawa, Droga drogaLewa, int tryb)
        {
            this.tryb = tryb;
            this.window = window;
            this.prom = prom;
            this.drogaPrawa = drogaPrawa;
            this.drogaLewa = drogaLewa;
        }
        public void StartTimer()
        {
            start = Environment.TickCount;
            if (tryb == 0)
                oTimer = new Timer(fn, null, 0, 1000);
            else if(tryb == 1)
                oTimer = new Timer(fn2, null, 0, 1000);
            else if(tryb == 2)
                oTimer = new Timer(fn3, null, 0, 100);

        }
        public void StopTimer()
        {
            czas = czas_remember;
            oTimer.Dispose();
        }
        public void setCzas()
        {
            czas = czas_remember;
        }
        private async void fn(object state)
        {
            //Jezeli statek stoi po prawej stronie i na drodze sa samochody oraz nie trwa rozladunek
            if (prom.rozladunek.CurrentCount!=0 && drogaPrawa.Licznik != 0 && prom.Strona == true 
                && (prom.stronaLewa.CurrentCount == 0 && prom.stronaPrawa.CurrentCount == 1))
            {
                drogaPrawa.dodajSamochodNaProm();
            }
            //Zapelniona jest droga po lewej stronie i statek jest na prawej stronie ORAZ NA DRODZE PRAWEJ NIE MA 3 SAMOCHODOW
            else if (drogaPrawa.Licznik != 3 && drogaLewa.Licznik == 3 && (prom.stronaLewa.CurrentCount == 0 && prom.stronaPrawa.CurrentCount == 1)
                && !(prom.stronaLewa.CurrentCount == 0 && prom.stronaPrawa.CurrentCount == 0))
            {
                Console.WriteLine("Prom jest po prawej stronie a po lewej jest max");
                temp = prom.Licznik;

                for (int i = 0; i < Math.Abs(temp - prom.Pojemnosc); i++)
                {
                    Console.WriteLine($"Zwalniam i uciekam");
                    while (prom.zaladunek.CurrentCount == 1) await Task.Delay(200);

                    prom.zaladunek.Release();
                }

            }

            //Jezeli statek stoi po lewej stronie i na drodze sa samochody
            if (prom.rozladunek.CurrentCount != 0 && drogaLewa.Licznik != 0 && prom.Strona == false 
                && (prom.stronaLewa.CurrentCount == 1 && prom.stronaPrawa.CurrentCount == 0))
            {

                drogaLewa.dodajSamochodNaProm();
            }
            //Zapelniona jest droga po prawej stronie i statek jest na lewej stronie ORAZ NA DRODZE LEWEJ NIE MA 3 SAMOCHODOW
            else if (drogaLewa.Licznik != 3 && drogaPrawa.Licznik==3 && (prom.stronaLewa.CurrentCount == 1 && prom.stronaPrawa.CurrentCount == 0) 
                && !(prom.stronaLewa.CurrentCount == 0 && prom.stronaPrawa.CurrentCount == 0))
            {
                Console.WriteLine("Prom jest po lewej stronie a po prawej jest max");
                temp = prom.Licznik;

                for (int i = 0; i < Math.Abs(temp - prom.Pojemnosc); i++)
                {
                    Console.WriteLine($"Zwalniam i uciekam");
                    while (prom.zaladunek.CurrentCount == 1) await Task.Delay(200);
                    prom.zaladunek.Release();
                }

            }
            
        }
        private async void fn2(object state)
        {
            //Drogi nie sa zapelnione i statek nie jest w ruchu oraz przekroczony zostal czas
            if (czas == 0 && drogaLewa.Licznik != 3 && drogaPrawa.Licznik != 3 &&
                ((prom.stronaLewa.CurrentCount == 1 && prom.stronaPrawa.CurrentCount == 0) ||
                (prom.stronaLewa.CurrentCount == 0 && prom.stronaPrawa.CurrentCount == 1)))
            {
                Console.WriteLine("Przekroczono czas");
                temp = prom.Licznik;

                for (int i = 0; i < Math.Abs(temp - prom.Pojemnosc); i++)
                {
                    Console.WriteLine($"Zwalniam i uciekam");
                    while (prom.zaladunek.CurrentCount == 1) await Task.Delay(200);
                    prom.zaladunek.Release();
                }
                czas = this.czas_remember;

            }
            window.Dispatcher.BeginInvoke(new Action(() => {
                window.zegar.Text = $"Czas: {czas}";
            }));
            czas--;

        }
        private async void fn3(object state)
        {
            window.Dispatcher.BeginInvoke(new Action(() => {
                window.prom_licznik.Text = $"{prom.Licznik.ToString()}";
                if (prom.Licznik == 3) window.prom_licznik.Foreground = Brushes.Blue; else window.prom_licznik.Foreground = Brushes.White;
                //window.licznik_lewo.Text = drogaLewa.Licznik.ToString();
                //window.licznik_prawo.Text = drogaPrawa.Licznik.ToString();
            }));
            switch (drogaPrawa.Licznik)
            {
                case 3:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.p1.Visibility = System.Windows.Visibility.Visible;
                        window.p2.Visibility = System.Windows.Visibility.Visible;
                        window.p3.Visibility = System.Windows.Visibility.Visible;
                    });
                    break;

                case 2:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.p1.Visibility = System.Windows.Visibility.Visible;
                        window.p2.Visibility = System.Windows.Visibility.Visible;
                        window.p3.Visibility = System.Windows.Visibility.Hidden;
                    });

                    break;
                case 1:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.p1.Visibility = System.Windows.Visibility.Visible;
                        window.p2.Visibility = System.Windows.Visibility.Hidden;
                        window.p3.Visibility = System.Windows.Visibility.Hidden;
                    });

                    break;
                default:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.p1.Visibility = System.Windows.Visibility.Hidden;
                        window.p2.Visibility = System.Windows.Visibility.Hidden;
                        window.p3.Visibility = System.Windows.Visibility.Hidden;
                    });

                    break;
            }
            switch (drogaLewa.Licznik)
            {
                case 3:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.l1.Visibility = System.Windows.Visibility.Visible;
                        window.l2.Visibility = System.Windows.Visibility.Visible;
                        window.l3.Visibility = System.Windows.Visibility.Visible;
                    });

                    break;
                case 2:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.l1.Visibility = System.Windows.Visibility.Hidden;
                        window.l2.Visibility = System.Windows.Visibility.Visible;
                        window.l3.Visibility = System.Windows.Visibility.Visible;
                    });

                    break;
                case 1:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.l1.Visibility = System.Windows.Visibility.Hidden;
                        window.l2.Visibility = System.Windows.Visibility.Hidden;
                        window.l3.Visibility = System.Windows.Visibility.Visible;
                    });

                    break;
                default:
                    window.Dispatcher.BeginInvoke(() =>
                    {
                        window.l1.Visibility = System.Windows.Visibility.Hidden;
                        window.l2.Visibility = System.Windows.Visibility.Hidden;
                        window.l3.Visibility = System.Windows.Visibility.Hidden;
                    });

                    break;
            }
        }
    }
}
