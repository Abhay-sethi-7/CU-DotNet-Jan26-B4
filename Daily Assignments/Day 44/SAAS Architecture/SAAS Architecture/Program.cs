using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAAS_Architecture
{
    
    abstract class Subscriber : IComparable<Subscriber>
    {
        public Guid GuidID { get; set; }
        public string Name { get; set; }
        public DateTime JoinDate { get; set; }

        public abstract decimal CalculateMonthlyBill();

        
        public override bool Equals(object obj)
        {
            if (obj is Subscriber other)
                return this.GuidID == other.GuidID;

            return false;
        }

        public override int GetHashCode()
        {
            return GuidID.GetHashCode();
        }

       
        public int CompareTo(Subscriber other)
        {
            int dateComparison = this.JoinDate.CompareTo(other.JoinDate);

            if (dateComparison == 0)
                return this.Name.CompareTo(other.Name);

            return dateComparison;
        }
    }

    class BusinessSubscriber : Subscriber
    {
        public decimal FixedRate { get; set; }
        public decimal TaxRate { get; set; }

        public override decimal CalculateMonthlyBill()
        {
            return FixedRate * (1 + TaxRate);
        }
    }

    class ConsumerSubscriber : Subscriber
    {
        public decimal DataUsageGB { get; set; }
        public decimal PricePerGB { get; set; }

        public override decimal CalculateMonthlyBill()
        {
            return DataUsageGB * PricePerGB;
        }
    }

   
    class ReportGenerator
    {
        public static void PrintRevenueReport(IEnumerable<Subscriber> subscribers)
        {
            StringBuilder sb = new StringBuilder();
            Console.WriteLine("--------Monthly Revenue Report------------");

            Console.WriteLine("Type\t\tName\t\tJoin Date\tMonthly Bill");
            Console.WriteLine("-----------------------------------------------------------");

            foreach (var sub in subscribers)
            {
                sb.AppendLine($"{sub.GetType().Name}\t{sub.Name}\t{sub.JoinDate.ToShortDateString()}\t${sub.CalculateMonthlyBill():.00}");
            }

            Console.WriteLine(sb.ToString());
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Subscriber> subscribers = new Dictionary<string, Subscriber>();

            subscribers.Add("biz1@corp.com", new BusinessSubscriber
            {
                GuidID = Guid.NewGuid(),
                Name = "Alpha",
                JoinDate = new DateTime(2023, 1, 10),
                FixedRate = 1000m,
                TaxRate = 0.15m
            });

            subscribers.Add("biz2@corp.com", new BusinessSubscriber
            {
                GuidID = Guid.NewGuid(),
                Name = "Beta",
                JoinDate = new DateTime(2022, 5, 2),
                FixedRate = 1500m,
                TaxRate = 0.10m
            });

            subscribers.Add("user1@gmail.com", new ConsumerSubscriber
            {
                GuidID = Guid.NewGuid(),
                Name = "x",
                JoinDate = new DateTime(2023, 3, 5),
                DataUsageGB = 50,
                PricePerGB = 2
            });

            subscribers.Add("user2@gmail.com", new ConsumerSubscriber
            {
                GuidID = Guid.NewGuid(),
                Name = "A",
                JoinDate = new DateTime(2022, 8, 15),
                DataUsageGB = 120,
                PricePerGB = 1.5m
            });

            subscribers.Add("user3@gmail.com", new ConsumerSubscriber
            {
                GuidID = Guid.NewGuid(),
                Name = "M",
                JoinDate = new DateTime(2024, 1, 1),
                DataUsageGB = 80,
                PricePerGB = 2.5m
            });

       
            var sortedSubscribers = subscribers
                .OrderByDescending(k => k.Value.CalculateMonthlyBill())
                .Select(k => k.Value)
                .ToList();

            
            ReportGenerator.PrintRevenueReport(sortedSubscribers);

            Console.ReadLine();
        }
    }
}