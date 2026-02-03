namespace UtilityBillingSystem
{
    abstract class UtilityBill
    {
        public int ConsumerId { get; set; }
        public string ConsumerName { get; set; }
        public decimal UnitsConsumed { get; set; }
        public decimal RatePerUnit { get; set; }

        public abstract decimal CalculateBillAmount();
        public virtual decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.05m;
        }

        public void PrintBill()
        {
            decimal billAmount = CalculateBillAmount();
            decimal tax = CalculateTax(billAmount);
            decimal totalAmount = billAmount + tax;

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Consumer ID    : {ConsumerId}");
            Console.WriteLine($"Consumer Name  : {ConsumerName}");
            Console.WriteLine($"Units Consumed : {UnitsConsumed}");
            Console.WriteLine($"Bill Amount    : {billAmount:C2}");
            Console.WriteLine($"Tax            : {tax:C2}");
            Console.WriteLine($"Total Payable  : {totalAmount:C2}");
            Console.WriteLine("----------------------------------\n");
        }
    }

    class ElectricityBill : UtilityBill
    {
        public override decimal CalculateBillAmount()
        {
            decimal amount = UnitsConsumed * RatePerUnit;

            if (UnitsConsumed > 300)
            {
                amount += amount * 0.10m; 
            }

            return amount;
        }
    }

    class WaterBill : UtilityBill
    {
        public override decimal CalculateBillAmount()
        {
            return UnitsConsumed * RatePerUnit;
        }

        public override decimal CalculateTax(decimal billAmount)
        {
            return billAmount * 0.02m;
        }
    }
    class GasBill : UtilityBill
    {
        public override decimal CalculateBillAmount()
        {
            return (UnitsConsumed * RatePerUnit) + 150m;
        }

        public override decimal CalculateTax(decimal billAmount)
        {
            return 0m;
        }
    }

    class Program
    {
        static void Main()
        {
            List<UtilityBill> bills = new List<UtilityBill>
        {
            new ElectricityBill
            {
                ConsumerId = 101,
                ConsumerName = "Abhay",
                UnitsConsumed = 350,
                RatePerUnit = 6.5m
            },
            new WaterBill
            {
                ConsumerId = 102,
                ConsumerName = "Ravi",
                UnitsConsumed = 120,
                RatePerUnit = 2.5m
            },
            new GasBill
            {
                ConsumerId = 103,
                ConsumerName = "Neha",
                UnitsConsumed = 50,
                RatePerUnit = 4.0m
            }
        };

            foreach (UtilityBill bill in bills)
            {
                bill.PrintBill(); 
            }

            Console.ReadLine();
        }
    }

}
