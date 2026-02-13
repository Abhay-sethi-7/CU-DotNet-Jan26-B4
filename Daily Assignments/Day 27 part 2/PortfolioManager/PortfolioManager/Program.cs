namespace PortfolioManager
{
    public class Loan
    {
        public string ClientName { get; set; }
        public double Principal { get; set; }
        public double InterestRate { get; set; }
    }

    class Program
    {
        static string filePath = "../../../loans.csv";

        static void Main()
        {
            Console.WriteLine("=== Loan Portfolio Manager ===");
            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("ClientName,Principal,InterestRate");
                }
            }

            AddLoan();

            ReadLoans();
        }

        static void AddLoan()
        {
            Console.Write("\nEnter Client Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Principal Amount: ");
            double principal = double.Parse(Console.ReadLine());

            Console.Write("Enter Interest Rate (%): ");
            double rate = double.Parse(Console.ReadLine());

            // Append mode = true
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{name},{principal},{rate}");
            }

            Console.WriteLine("Loan added successfully!");
        }

        static void ReadLoans()
        {
            Console.WriteLine("\n-----------Loan Risk Report ----------\n");

            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine(); 

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');

                    string name = parts[0];

                    double principal, rate;

                    if (!double.TryParse(parts[1], out principal) ||
                        !double.TryParse(parts[2], out rate))
                    {
                        Console.WriteLine($"Skipping invalid record: {line}");
                        continue;
                    }
                    double interestAmount = principal * rate / 100;
                    string risk = GetRiskLevel(rate);

                    Console.WriteLine($"Client: {name}");
                    Console.WriteLine($"Principal: {principal:C}");
                    Console.WriteLine($"Rate: {rate}%");
                    Console.WriteLine($"Interest Amount: {interestAmount:C}");
                    Console.WriteLine($"Risk Level: {risk}");
                    Console.WriteLine("---------------------------------");
                }
            }
        }

        static string GetRiskLevel(double rate)
        {
            if (rate > 10) return "HIGH RISK";
            else if (rate >= 5) return "MEDIUM RISK";
            else return "LOW RISK";
        }
    }

  
}
