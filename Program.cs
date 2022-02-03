namespace PW_Projekt_v5
{
    public partial class LogikaV2
    {
        public static void Main()
        {
            var ListaZadan = new List<Task>();
            Droga drogaLewa = new("Zachod", false);
            Droga drogaPrawa = new("Wschod", true);
            Prom prom = new(true, drogaLewa, drogaPrawa);
            drogaLewa.dodajProm(prom);
            drogaPrawa.dodajProm(prom);
            int wybor = 0;
            int wybor2 = 0;
            int licznik = 0;
            MyTimer oTimer = new(prom, drogaPrawa, drogaLewa);
            oTimer.StartTimer();
            prom.plyn();
            Task.Run(async () =>
            {
            start:
                Console.WriteLine("\n\n\n\n\n1. Prom \n2. Droga Zachod \n3. Droga Wschod");
                wybor = Convert.ToInt32(Console.ReadLine());
                switch (wybor)
                {
                    case 1:
                        Console.WriteLine("========== PROM ==========\n" +
                            "1. getStatus\n" +
                            "2. Plyn");
                        Console.Write("Wybor: ");
                        wybor2 = Convert.ToInt32(Console.ReadLine());
                        switch (wybor2)
                        {
                            case 1:
                                prom.getStatus();
                                break;
                            case 2:
                                Task.Run(() => { ListaZadan.Add((prom.plyn())); });
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("========== ZACHOD ==========\n" +
                            "1. getStatus\n2. stworzSamochod()");
                        Console.Write("Wybor: ");
                        wybor2 = Convert.ToInt32(Console.ReadLine());
                        switch (wybor2)
                        {
                            case 1:
                                Task.Run(() => {
                                 drogaLewa.getStatus();
                                });
                                break;
                            case 2:
                                Task.Run(() => {
                                    drogaLewa.stworzSamochod();
                                });
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        Console.WriteLine("========== WSCHOD ==========\n" +
                            "1. getStatus\n2. stworzSamochod()");
                        Console.Write("Wybor: ");
                        wybor2 = Convert.ToInt32(Console.ReadLine());
                        switch (wybor2)
                        {
                            case 1:
                                Task.Run(() => { drogaPrawa.getStatus(); });

                                break;
                            case 2:
                                Task.Run(() => { drogaPrawa.stworzSamochod(); });
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                goto start;
            }).Wait();
        }
    }
}