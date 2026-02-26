namespace kItchenApp
{
    abstract class KitchenAppliance
    {
        public string ModelName { get; set; }
        public int PowerWatts { get; set; }

        public KitchenAppliance(string name, int power)
        {
            ModelName = name;
            PowerWatts = power;
        }
        public abstract void Cook();
        public virtual void Preheat()
        {
            Console.WriteLine("No preheating needed.");
        }
    }
    interface ITimer
    {
        void SetTimer(int minutes);
    }

    interface IWifiEnabled
    {
        void ConnectWifi();
    }

    class Microwave : KitchenAppliance, ITimer
    {
        public Microwave(string name, int power) : base(name, power) { }

        public void SetTimer(int minutes)
        {
            Console.WriteLine($"Microwave timer set for {minutes} minutes.");
        }

        public override void Cook()
        {
            Console.WriteLine("Microwave is heating food using radiation.");
        }
    }
    class ElectricOven : KitchenAppliance, ITimer, IWifiEnabled
    {
        public ElectricOven(string name, int power) : base(name, power) { }

        public override void Preheat()
        {
            Console.WriteLine("Oven is preheating...");
        }

        public void SetTimer(int minutes)
        {
            Console.WriteLine($"Oven timer set for {minutes} minutes.");
        }

        public void ConnectWifi()
        {
            Console.WriteLine("Oven connected to WiFi.");
        }

        public override void Cook()
        {
            Console.WriteLine("Oven is baking food evenly.");
        }
    }
    class AirFryer : KitchenAppliance
    {
        public AirFryer(string name, int power) : base(name, power) { }

        public override void Cook()
        {
            Console.WriteLine("Air Fryer is cooking food using hot air.");
        }
    }
  internal  class Program
    {
        static void Main()
        {
            List<KitchenAppliance> appliances = new List<KitchenAppliance>()
        {
            new Microwave("LG Micro", 1200),
            new ElectricOven("Samsung Oven", 2500),
            new AirFryer("Philips Fryer", 1500)
        };

            foreach (var app in appliances)
            {
                Console.WriteLine($"\nDevice: {app.ModelName}");

                app.Preheat();
                app.Cook();

                if (app is ITimer timer)
                    timer.SetTimer(10);

                if (app is IWifiEnabled wifi)
                    wifi.ConnectWifi();
            }
        }
    }
}
