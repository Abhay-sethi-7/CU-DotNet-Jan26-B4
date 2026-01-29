
namespace EcoDriveSimulation
{
    
    abstract class Vehicle
    {
        public string ModelName { get; set; }

        public Vehicle(string modelName)
        {
            ModelName = modelName;
        }

        public abstract void Move();

        
        public virtual string GetFuelStatus()
        {
            return "Fuel level is stable.";
        }
    }

 
    class ElectricCar : Vehicle
    {
        public ElectricCar(string modelName) : base(modelName) { }

        public override void Move()
        {
            Console.WriteLine($"{ModelName} is gliding silently on battery power.");
        }

        public override string GetFuelStatus()
        {
            return $"{ModelName} battery is at 80%.";
        }
    }

    class HeavyTruck : Vehicle
    {
        public HeavyTruck(string modelName) : base(modelName) { }

        public override void Move()
        {
            Console.WriteLine($"{ModelName} is hauling cargo with high-torque diesel power.");
        }
       
    }

    
    class CargoPlane : Vehicle
    {
        public CargoPlane(string modelName) : base(modelName) { }

        public override void Move()
        {
            Console.WriteLine($"{ModelName} is ascending to 30,000 feet.");
        }

        public override string GetFuelStatus()
        {
            return base.GetFuelStatus() + " Checking jet fuel reserves...";
        }
    }

   
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle[] fleet =
            {
                new ElectricCar("Tesla Model X"),
                new HeavyTruck("Volvo FH16"),
                new CargoPlane("Boeing 747")
            };

            foreach (Vehicle v in fleet)
            {
                v.Move();
                Console.WriteLine(v.GetFuelStatus());
                Console.WriteLine();
            }
        }
    }
}
