

namespace WeeklySalesAnalysis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] sales = new decimal[7];
            string[] category = new string[7];

            for (int i = 0; i < sales.Length; i++)
            {
                while (true)
                {
                    Console.Write($"Enter sales for Day {i + 1}: ");
                    bool isValid = decimal.TryParse(Console.ReadLine(), out sales[i]);

                    if (isValid && sales[i] >= 0)
                        break;

                    Console.WriteLine("Invalid input! Sales must be zero or positive.");
                }
            }

            decimal totalSales = 0;
            for (int i = 0; i < sales.Length; i++)
            {
                totalSales += sales[i];
            }

            decimal averageSales = totalSales / sales.Length;

            decimal highestSale = sales[0];
            decimal lowestSale = sales[0];
            int highestDay = 1;
            int lowestDay = 1;

            for (int i = 1; i < sales.Length; i++)
            {
                if (sales[i] > highestSale)
                {
                    highestSale = sales[i];
                    highestDay = i + 1;
                }

                if (sales[i] < lowestSale)
                {
                    lowestSale = sales[i];
                    lowestDay = i + 1;
                }
            }

            int daysAboveAverage = 0;
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] > averageSales)
                {
                    daysAboveAverage++;
                }
            }

            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] < 5000)
                    category[i] = "Low";
                else if (sales[i] <= 15000)
                    category[i] = "Medium";
                else
                    category[i] = "High";
            }

            Console.WriteLine("\nWeekly Sales Report");
            Console.WriteLine("-------------------");
            Console.WriteLine($"Total Sales           : {totalSales:F2}");
            Console.WriteLine($"Average Daily Sale    : {averageSales:F2}\n");

            Console.WriteLine($"Highest Sale          : {highestSale:F2} (Day {highestDay})");
            Console.WriteLine($"Lowest Sale           : {lowestSale:F2} (Day {lowestDay})\n");

            Console.WriteLine($"Days Above Average    : {daysAboveAverage}\n");

            Console.WriteLine("Day-wise Sales Category:");
            for (int i = 0; i < category.Length; i++)
            {
                Console.WriteLine($"Day {i + 1} : {category[i]}");
            }

            Console.ReadKey();
        }
    }
}
