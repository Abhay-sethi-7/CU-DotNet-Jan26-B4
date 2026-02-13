namespace ExpenseSharing
{
        class User
        {
            public string Name { get; set; }
            public double Amount { get; set; }
        }
        class Splitwise
        {
            List<User> users = new List<User>();
            public void AddNewUser(string name)
            {
                users.Add(new User { Name = name, Amount = 0 });
            }
            private User GetUser(string name)
            {
                foreach (User u in users)
                {
                    if (u.Name == name)
                    {
                        return u;
                    }

                }
                return null;
            }
            public void AddExpenses(string PaidBy, double amount, List<string> contributors)
            {
                User Payer = GetUser(PaidBy);
                double SplitAmount = amount / contributors.Count;
                foreach (string name in contributors)
                {
                    User u = GetUser(name);
                    if (u != null)
                    {
                        u.Amount -= SplitAmount;
                    }
                }
                Payer.Amount += amount;
            }
            public void ShowBalance()
            {
                Console.WriteLine("--------Final Balance ---------");
                foreach (User u in users)
                {
                    if (u.Amount > 0)
                        Console.WriteLine($"{u.Name} gets {u.Amount}");
                    else if (u.Amount < 0)
                        Console.WriteLine($"{u.Name} owes {-u.Amount}");
                    else
                        Console.WriteLine($"{u.Name} is settled");
                }
            }
            public void ShowWhoPaysWhom()
            {


                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Amount >= 0)
                        continue;

                    for (int j = 0; j < users.Count; j++)
                    {
                        if (users[j].Amount <= 0)
                            continue;

                        double pay = -users[i].Amount;

                        if (pay > users[j].Amount)
                            pay = users[j].Amount;

                        Console.WriteLine($"{users[i].Name} owes {pay} to {users[j].Name}");

                        users[i].Amount += pay;
                        users[j].Amount -= pay;

                        if (users[i].Amount == 0)
                            break;
                    }
                }
            }


            internal class Program
            {
                static void Main(string[] args)
                {
                    Splitwise app = new Splitwise();

                    app.AddNewUser("A");
                    app.AddNewUser("B");
                    app.AddNewUser("C");
                    app.AddExpenses("A", 900, new List<string> { "A", "B", "C" });
                    app.AddExpenses("B", 900, new List<string> { "C", "B", "A" });
                    app.AddExpenses("C", 30, new List<string> { "C", "B", "A" });
                    app.ShowBalance();
                    app.ShowWhoPaysWhom();
                    app.ShowWhoPaysWhom();
                    Console.ReadLine();
                }

            }

        }
    }
