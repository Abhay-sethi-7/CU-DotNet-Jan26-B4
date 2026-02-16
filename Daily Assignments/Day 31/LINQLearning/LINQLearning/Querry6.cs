
namespace LINQLearning
{

class Movie
    {
        public string Title;
        public string Genre;
        public double Rating;
        public int Year;
    }
 internal class Querry6
    {
        static void Main()
        {
            var movies = new List<Movie>
        {
            new Movie{Title="Inception", Genre="SciFi", Rating=9, Year=2010},
            new Movie{Title="Avatar", Genre="SciFi", Rating=8.5, Year=2009},
            new Movie{Title="Titanic", Genre="Drama", Rating=8, Year=1997}
        };

            // 1️ Filter movies with rating > 8
            var highRated = movies.Where(m => m.Rating > 8);

            Console.WriteLine("Movies with Rating > 8:");
            foreach (var m in highRated)
                Console.WriteLine(m.Title);



            // 2️ Group by Genre & get Average Rating
            var avgByGenre = movies
                .GroupBy(m => m.Genre)
                .Select(g => new
                {
                    Genre = g.Key,
                    AvgRating = g.Average(m => m.Rating)
                });

            Console.WriteLine("\nAverage Rating per Genre:");
            foreach (var g in avgByGenre)
                Console.WriteLine($"{g.Genre} -> {g.AvgRating}");



            // 3️ Latest Movie per Genre
            var latestPerGenre = movies
                .GroupBy(m => m.Genre)
                .Select(g => g.OrderByDescending(m => m.Year).First());

            Console.WriteLine("\nLatest Movie per Genre:");
            foreach (var m in latestPerGenre)
                Console.WriteLine($"{m.Genre} -> {m.Title} ({m.Year})");



            // 4️ Top 5 Highest Rated Movies
            var topMovies = movies
                .OrderByDescending(m => m.Rating)
                .Take(5);

            Console.WriteLine("\nTop Highest Rated Movies:");
            foreach (var m in topMovies)
                Console.WriteLine($"{m.Title} -> {m.Rating}");
        }
    }


}
