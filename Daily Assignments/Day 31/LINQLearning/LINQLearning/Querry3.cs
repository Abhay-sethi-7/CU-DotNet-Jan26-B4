
namespace LINQLearning
{
 
    class Product
    {
        public int Id;
        public string Name;
        public string Category;
        public double Price;
    }

    class Sale
    {
        public int ProductId;
        public int Qty;
    }

    class Querry3
    {
        static void Main()
        {
            var products = new List<Product>
        {
            new Product{Id=1, Name="Laptop", Category="Electronics", Price=50000},
            new Product{Id=2, Name="Phone", Category="Electronics", Price=20000},
            new Product{Id=3, Name="Table", Category="Furniture", Price=5000}
        };

            var sales = new List<Sale>
        {
            new Sale{ProductId=1, Qty=10},
            new Sale{ProductId=2, Qty=20}
        };

            // 1️ Join Products with Sales
            var joined = products.Join(
                sales,
                p => p.Id,
                s => s.ProductId,
                (p, s) => new
                {
                    p.Name,
                    s.Qty,
                    p.Price
                });

            Console.WriteLine("Joined Products with Sales:");
            foreach (var item in joined)
                Console.WriteLine($"{item.Name} Sold: {item.Qty}");


            //  Total Revenue per Product
            var revenue = products
                .GroupJoin(
                    sales,
                    p => p.Id,
                    s => s.ProductId,
                    (p, sg) => new
                    {
                        p.Name,
                        Revenue = sg.Sum(x => x.Qty * p.Price)
                    });

            Console.WriteLine("\nRevenue per Product:");
            foreach (var item in revenue)
                Console.WriteLine($"{item.Name} -> {item.Revenue}");


            //  Best Selling Product (highest quantity sold)
            var bestSelling = products
                .Join(sales,
                    p => p.Id,
                    s => s.ProductId,
                    (p, s) => new
                    {
                        p.Name,
                        s.Qty
                    })
                .OrderByDescending(x => x.Qty)
                .First();

            Console.WriteLine($"\nBest Selling Product: {bestSelling.Name} ({bestSelling.Qty})");


            //  Products with Zero Sales
            var zeroSales = products
                .GroupJoin(
                    sales,
                    p => p.Id,
                    s => s.ProductId,
                    (p, sg) => new { Product = p, Sales = sg })
                .SelectMany(
                    x => x.Sales.DefaultIfEmpty(),
                    (x, s) => new { x.Product, Sale = s })
                .Where(x => x.Sale == null)
                .Select(x => x.Product);

            Console.WriteLine("\nProducts with Zero Sales:");
            foreach (var p in zeroSales)
                Console.WriteLine(p.Name);
        }
    }

}
