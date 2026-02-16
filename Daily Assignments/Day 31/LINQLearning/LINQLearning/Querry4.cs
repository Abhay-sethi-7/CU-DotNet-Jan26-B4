
namespace LINQLearning
{
    
    class Book
    {
        public string Title;
        public string Author;
        public string Genre;
        public int Year;
        public double Price;
    }

    class Querry4
    {
        static void Main()
        {
            var books = new List<Book>
        {
            new Book{Title="C# Basics", Author="John", Genre="Tech", Year=2018, Price=500},
            new Book{Title="Java Advanced", Author="Mike", Genre="Tech", Year=2016, Price=700},
            new Book{Title="History India", Author="Raj", Genre="History", Year=2019, Price=400}
        };

            //  Books published after 2015
            var recentBooks = books.Where(b => b.Year > 2015);

            Console.WriteLine("Books Published After 2015:");
            foreach (var b in recentBooks)
                Console.WriteLine(b.Title);


            //  Group by Genre and count books
            var countByGenre = books
                .GroupBy(b => b.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    Count = g.Count()
                });

            Console.WriteLine("\nBook Count by Genre:");
            foreach (var g in countByGenre)
                Console.WriteLine($"{g.Genre} -> {g.Count}");


            //  Most Expensive Book per Genre
            var expensiveByGenre = books
                .GroupBy(b => b.Genre)
                .Select(g => g.OrderByDescending(b => b.Price).FirstOrDefault());

            Console.WriteLine("\nMost Expensive Book per Genre:");
            foreach (var b in expensiveByGenre)
                Console.WriteLine($"{b.Genre} -> {b.Title} ({b.Price})");


            //  Distinct Authors List
            var authors = books
                .Select(b => b.Author)
                .Distinct();

            Console.WriteLine("\nDistinct Authors:");
            foreach (var a in authors)
                Console.WriteLine(a);
        }
    }

}
