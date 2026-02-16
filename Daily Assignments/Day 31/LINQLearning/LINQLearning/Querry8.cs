
namespace LINQLearning
{
    class CartItem
    {
        public string Name;
        public string Category;
        public double Price;
        public int Qty;
    }

    internal class Querry8
    {
        static void Main()
        {
            var cart = new List<CartItem>
        {
            new CartItem{Name="TV", Category="Electronics", Price=30000, Qty=1},
            new CartItem{Name="Sofa", Category="Furniture", Price=15000, Qty=1}
        };

            // Total Cart Value
            var totalValue = cart.Sum(c => c.Price * c.Qty);

            Console.WriteLine("Total Cart Value: " + totalValue);



            // Group by Category & Total Cost
            var categoryTotals = cart
                .GroupBy(c => c.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalCost = g.Sum(c => c.Price * c.Qty)
                });

            Console.WriteLine("\nCategory Wise Cost:");
            foreach (var c in categoryTotals)
                Console.WriteLine($"{c.Category} -> {c.TotalCost}");



            //Apply 10% Discount for Electronics
            var discountedCart = cart.Select(c =>
            {
                double total = c.Price * c.Qty;

                if (c.Category == "Electronics")// subtract 10%
                    total = total - (total * 0.10);   

                return new
                {
                    c.Name,
                    c.Category,
                    FinalPrice = total
                };
            });


            Console.WriteLine("\nDiscount Applied:");
            foreach (var item in discountedCart)
                Console.WriteLine($"{item.Name} -> {item.FinalPrice}");



            // Cart Summary DTO Objects
            var summary = cart.Select(c =>
            {
                double total = c.Price * c.Qty;
                double discounted = total;

                if (c.Category == "Electronics")// apply 10% discount
                    discounted = total * 0.9;   

                return new
                {
                    Product = c.Name,
                    Category = c.Category,
                    Qty = c.Qty,
                    Total = total,
                    DiscountedTotal = discounted
                };
            });


            Console.WriteLine("\nCart Summary:");
            foreach (var s in summary)
                Console.WriteLine($"{s.Product} | Qty:{s.Qty} | Final:{s.DiscountedTotal}");
        }
    }

}
