
namespace LINQLearning
{
    

    class Transaction
    {
        public int Acc;
        public double Amount;
        public string Type;   // Credit / Debit
    }

    internal class Querry7
    {
        static void Main()
        {
            var transactions = new List<Transaction>
        {
            new Transaction{Acc=101, Amount=5000, Type="Credit"},
            new Transaction{Acc=101, Amount=2000, Type="Debit"},
            new Transaction{Acc=102, Amount=10000, Type="Debit"}
        };

            //  Total Balance per Account
            var balance = transactions
                .GroupBy(t => t.Acc)
                .Select(g => new
                {
                    Account = g.Key,
                    Balance = g.Where(t => t.Type == "Credit").Sum(t => t.Amount)
                              - g.Where(t => t.Type == "Debit").Sum(t => t.Amount)
                });

            Console.WriteLine("Total Balance per Account:");
            foreach (var b in balance)
                Console.WriteLine($"Account {b.Account} -> Balance: {b.Balance}");



            // Suspicious Accounts (Debit > Credit)
            var suspicious = transactions
                .GroupBy(t => t.Acc)
                .Select(g => new
                {
                    Account = g.Key,
                    Credit = g.Where(t => t.Type == "Credit").Sum(t => t.Amount),
                    Debit = g.Where(t => t.Type == "Debit").Sum(t => t.Amount)
                })
                .Where(x => x.Debit > x.Credit);

            Console.WriteLine("\nSuspicious Accounts:");
            foreach (var s in suspicious)
                Console.WriteLine($"Account {s.Account}");



            // Group Transactions by Type (since Date not provided)
            var grouped = transactions
                .GroupBy(t => t.Type);

            Console.WriteLine("\nTransactions Grouped by Type:");
            foreach (var g in grouped)
            {
                Console.WriteLine(g.Key);
                foreach (var t in g)
                    Console.WriteLine($"  Acc {t.Acc} -> {t.Amount}");
            }



            // Highest Transaction Amount per Account
            var highest = transactions
                .GroupBy(t => t.Acc)
                .Select(g => new
                {
                    Account = g.Key,
                    MaxAmount = g.Max(t => t.Amount)
                });

            Console.WriteLine("\nHighest Transaction per Account:");
            foreach (var h in highest)
                Console.WriteLine($"Account {h.Account} -> {h.MaxAmount}");
        }
    }

}
