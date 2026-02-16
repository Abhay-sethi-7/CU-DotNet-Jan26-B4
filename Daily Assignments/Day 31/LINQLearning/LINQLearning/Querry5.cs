
namespace LINQLearning
{class Customer
    {
        public int Id;
        public string Name;
        public string City;
    }

    class Order
    {
        public int OrderId;
        public int CustomerId;
        public double Amount;
    }
    internal class Querry5
    {
        static void Main()
        {
            var customers = new List<Customer>
        {
            new Customer{Id=1, Name="Ajay", City="Delhi"},
            new Customer{Id=2, Name="Sunita", City="Mumbai"}
        };

            var orders = new List<Order>
        {
            new Order{OrderId=1, CustomerId=1, Amount=20000},
            new Order{OrderId=2, CustomerId=1, Amount=40000}
        };

            // 1️ Total Order Amount per Customer
            var totalSpent = customers
                .GroupJoin(
                    orders,
                    c => c.Id,
                    o => o.CustomerId,
                    (c, og) => new
                    {
                        c.Name,
                        TotalAmount = og.Sum(x => x.Amount)
                    });

            Console.WriteLine("Total Spending per Customer:");
            foreach (var item in totalSpent)
                Console.WriteLine($"{item.Name} -> {item.TotalAmount}");



            // 2️ Customers with NO orders
            var noOrders = customers
                .GroupJoin(orders,
                    c => c.Id,
                    o => o.CustomerId,
                    (c, og) => new { Customer = c, Orders = og })
                .Where(x => !x.Orders.Any())
                .Select(x => x.Customer);

            Console.WriteLine("\nCustomers with No Orders:");
            foreach (var c in noOrders)
                Console.WriteLine(c.Name);



            // 3️ Customers who spent above 50,000
            var bigSpenders = customers
                .GroupJoin(orders,
                    c => c.Id,
                    o => o.CustomerId,
                    (c, og) => new
                    {
                        c.Name,
                        Total = og.Sum(x => x.Amount)
                    })
                .Where(x => x.Total > 50000);

            Console.WriteLine("\nCustomers Spending Above 50,000:");
            foreach (var c in bigSpenders)
                Console.WriteLine($"{c.Name} -> {c.Total}");



            // 4️ Sort Customers by Total Spending
            var sortedCustomers = customers
                .GroupJoin(orders,
                    c => c.Id,
                    o => o.CustomerId,
                    (c, og) => new
                    {
                        c.Name,
                        Total = og.Sum(x => x.Amount)
                    })
                .OrderByDescending(x => x.Total);

            Console.WriteLine("\nCustomers Sorted by Spending:");
            foreach (var c in sortedCustomers)
                Console.WriteLine($"{c.Name} -> {c.Total}");
        }
    }

}
