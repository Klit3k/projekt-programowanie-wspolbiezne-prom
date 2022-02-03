
using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PW_Projekt_v5
{
    public class Samochod
    {
        MainWindow window;
        Droga droga;
        Image car;
        public Samochod(MainWindow window, Droga droga)
        {
            this.droga = droga;
            this.window = window;
        }
        
        public void generujObraz()
        {
            car = new();
            BitmapImage obraz = new BitmapImage();
            obraz.BeginInit();
            obraz.UriSource = new Uri("/car_3.png", UriKind.Relative);
            obraz.EndInit();
            car.Width = 65;
            car.Height = 58;
            if (droga.Strona == true)
            {
                car.SetValue(Grid.RowProperty, 1);
                car.SetValue(Grid.ColumnProperty, 2);
            }
            else
            {
                car.SetValue(Grid.RowProperty, 2);
                car.SetValue(Grid.ColumnProperty, 0);
            }
            car.Source = obraz;
            car.Margin = new System.Windows.Thickness(300, 7, 2, 0);
        }
        public async Task wyjedz()
        {
            window.Dispatcher.Invoke(() =>
            {
                generujObraz();
                car.SetValue(Grid.ColumnSpanProperty, 2);
                if (droga.Strona == true)
                {
                    car.SetValue(Grid.RowProperty, 2);
                }
                else
                {
                    car.SetValue(Grid.RowProperty, 1);
                }

                window.kratownica.Children.Add(car);
            });
            if(this.droga.Strona == true ) //zjezdza z promu z prawej strony
            
                {
                    for (int i = -360; i < 320; i += 10)
                    {
                        window.Dispatcher.Invoke(() =>
                        {
                            car.Margin = new System.Windows.Thickness(i, -15, 0, -6);
                        });
                        await Task.Delay(2);
                    }
                    window.Dispatcher.Invoke(() =>
                    {
                        //window.l3.Visibility = System.Windows.Visibility.Visible;
                        window.kratownica.Children.Remove(car);
                    });
                }
            else
            {
                for (int i = 140; i > -580; i -= 10)
                {
                    window.Dispatcher.Invoke(() =>
                    {
                        car.Margin = new System.Windows.Thickness(i, 10, 0, 0);
                    });
                    await Task.Delay(2);
                }
                window.Dispatcher.Invoke(() =>
                {
                    //window.l3.Visibility = System.Windows.Visibility.Visible;
                    window.kratownica.Children.Remove(car);
                });
            }
        }
        public async Task wjedz()
        {
            window.Dispatcher.Invoke(() =>
            {
                generujObraz();
                window.kratownica.Children.Add(car);
            });
            if (droga.Strona == true) //wjazd na prawa droge
            {
                switch (droga.Licznik)
                {
                    case 0:
                        {
                            for (int i = 300; i > -212; i -= 10)
                            {
                                window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, 7, 2, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                               // window.p1.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    case 1:
                        {
                            for (int i = 300; i > 0; i -= 10)
                            {
                                window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, 7, 2, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                                //window.p2.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    case 2:
                        {
                            for (int i = 300; i > 212; i -= 10)
                            {
                                 window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, 7, 2, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                                //window.p3.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    default:
                        break;
                }

            }
            else if (droga.Strona == false) //wjazd na lewa droge
            {
                switch (droga.Licznik)
                {
                    case 0:
                        {
                            for (int i = -200; i < 212; i += 10)
                            {
                                window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, -15, 0, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                                //window.l3.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    case 1:
                        {
                            for (int i = -200; i < 0; i += 10)
                            {
                                window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, -15, 0, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                                //window.l3.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    case 2:
                        {
                            for (int i = -200; i < -212; i += 10)
                            {
                                window.Dispatcher.Invoke(() =>
                                {
                                    car.Margin = new System.Windows.Thickness(i, -15, 0, 0);
                                });
                                await Task.Delay(2);
                            }
                            window.Dispatcher.Invoke(() =>
                            {
                                //window.l3.Visibility = System.Windows.Visibility.Visible;
                                window.kratownica.Children.Remove(car);
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
        }
       
        public Droga getDroga { get => droga; }
    }
}