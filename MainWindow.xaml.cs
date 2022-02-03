using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PW_Projekt_v5
{

    public partial class MainWindow : Window
    {
        Droga drogaNE;
        Droga drogaSW;
        Prom prom;

        public MainWindow()
        {

            InitializeComponent();
            
            drogaSW = new(this, "Zachod", false);
            drogaNE = new(this, "Wschod", true);
            prom = new(this, true, drogaSW, drogaNE);
            drogaSW.dodajProm(prom);
            drogaNE.dodajProm(prom);
            MyTimer oTimer = new(this, prom, drogaNE, drogaSW, 0);
            MyTimer oTimer2 = new(this, prom, drogaNE, drogaSW, 2);
            oTimer.StartTimer();
            Task.Run(() => { oTimer2.StartTimer(); });
            Task.Run(() => { prom.plyn(); });
           
        }

        private void lewyPrzycisk_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.lewyPrzycisk.Background = Brushes.Red;

        }

        private void lewyPrzycisk_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.lewyPrzycisk.Background = Brushes.Transparent;
            Task.Run(async () =>
            {
                drogaSW.stworzSamochodAsync();
            });

        }

        private void prawyPrzycisk_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prawyPrzycisk.Background = Brushes.Red;
        }

        private void prawyPrzycisk_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.prawyPrzycisk.Background = Brushes.Transparent;
            Task.Run(async () =>
            {
                drogaNE.stworzSamochodAsync();
            });
        }
    }
}
