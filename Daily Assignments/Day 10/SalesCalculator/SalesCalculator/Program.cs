using System;

namespace SalesOrderProcessingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[] weeklySales = new decimal[7];
            string[] categories = new string[7];

            ReadWeeklySales(weeklySales);

            decimal total = CalculateTotal(weeklySales);
            decimal average = CalculateAverage(total, weeklySales.Length);

            int highestDay, lowestDay;
            decimal highestSale = FindHighestSale(weeklySales, out highestDay);
            decimal lowestSale = FindLowestSale(weeklySales, out lowestDay);

            bool isFestivalWeek = false;

            decimal discount = CalculateDiscount(total, isFestivalWeek);
            decimal tax = CalculateTax(total - discount);
            decimal finalAmount = CalculateFinalAmount(total, discount, tax);

            GenerateSalesCategory(weeklySales, categories);

            PrintReport(total, average, highestSale, highestDay,
                        lowestSale, lowestDay, discount, tax,
                        finalAmount, categories);
        }

        static void ReadWeeklySales(decimal[] sales)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                while (true)
                {
                    Console.Write($"Enter sales for Day {i + 1}: ");
                    decimal value = decimal.Parse(Console.ReadLine());
                    if (value >= 0)
                    {
                        sales[i] = value;
                        break;
                    }
                    Console.WriteLine("Sales cannot be negative. Try again.");
                }
            }
        }

        static decimal CalculateTotal(decimal[] sales)
        {
            decimal total = 0;
            for (int i = 0; i < sales.Length; i++)
                total += sales[i];
            return total;
        }

        static decimal CalculateAverage(decimal total, int days)
        {
            return total / days;
        }

        static decimal FindHighestSale(decimal[] sales, out int day)
        {
            decimal highest = sales[0];
            day = 1;
            for (int i = 1; i < sales.Length; i++)
            {
                if (sales[i] > highest)
                {
                    highest = sales[i];
                    day = i + 1;
                }
            }
            return highest;
        }

        static decimal FindLowestSale(decimal[] sales, out int day)
        {
            decimal lowest = sales[0];
            day = 1;
            for (int i = 1; i < sales.Length; i++)
            {
                if (sales[i] < lowest)
                {
                    lowest = sales[i];
                    day = i + 1;
                }
            }
            return lowest;
        }

        static decimal CalculateDiscount(decimal total)
        {
            if (total >= 50000)
                return total * 0.10m;
            return total * 0.05m;
        }

        static decimal CalculateDiscount(decimal total, bool isFestivalWeek)
        {
            decimal discount = CalculateDiscount(total);
            if (isFestivalWeek)
                discount += total * 0.05m;
            return discount;
        }

        static decimal CalculateTax(decimal amount)
        {
            return amount * 0.18m;
        }

        static decimal CalculateFinalAmount(decimal total, decimal discount, decimal tax)
        {
            return (total - discount) + tax;
        }

        static void GenerateSalesCategory(decimal[] sales, string[] categories)
        {
            for (int i = 0; i < sales.Length; i++)
            {
                if (sales[i] < 5000)
                    categories[i] = "Low";
                else if (sales[i] <= 15000)
                    categories[i] = "Medium";
                else
                    categories[i] = "High";
            }
        }

        static void PrintReport(decimal total, decimal average,
                                decimal highestSale, int highestDay,
                                decimal lowestSale, int lowestDay,
                                decimal discount, decimal tax,
                                decimal finalAmount, string[] categories)
        {
            Console.WriteLine("\nWeekly Sales Summary");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Total Sales : {total:F2}");
            Console.WriteLine($"Average Daily Sale : {average:F2}\n");
            Console.WriteLine($"Highest Sale : {highestSale:F2} (Day {highestDay})");
            Console.WriteLine($"Lowest Sale : {lowestSale:F2} (Day {lowestDay})\n");
            Console.WriteLine($"Discount Applied : {discount:F2}");
            Console.WriteLine($"Tax Amount : {tax:F2}");
            Console.WriteLine($"Final Payable : {finalAmount:F2}\n");
            Console.WriteLine("Day-wise Category:");
            for (int i = 0; i < categories.Length; i++)
                Console.WriteLine($"Day {i + 1} : {categories[i]}");
        }
    }
}
