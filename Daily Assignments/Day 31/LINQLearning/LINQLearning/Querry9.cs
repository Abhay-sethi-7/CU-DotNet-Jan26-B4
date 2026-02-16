
namespace LINQLearning
{
    class User
    {
        public int Id;
        public string Name;
        public string Country;
    }

    class Post
    {
        public int UserId;
        public int Likes;
    }

    class Querry9
    {
        static void Main()
        {
            var users = new List<User>
        {
            new User{Id=1, Name="A", Country="India"},
            new User{Id=2, Name="B", Country="USA"}
        };

            var posts = new List<Post>
        {
            new Post{UserId=1, Likes=100},
            new Post{UserId=1, Likes=50}
        };

            //  Top Users by Total Likes
            var topUsers = users
                .GroupJoin(posts,
                    u => u.Id,
                    p => p.UserId,
                    (u, pg) => new
                    {
                        u.Name,
                        TotalLikes = pg.Sum(p => p.Likes)
                    })
                .OrderByDescending(x => x.TotalLikes);

            Console.WriteLine("Top Users by Likes:");
            foreach (var u in topUsers)
                Console.WriteLine($"{u.Name} -> {u.TotalLikes}");



            // Group Users by Country
            var usersByCountry = users
                .GroupBy(u => u.Country);

            Console.WriteLine("\nUsers Grouped by Country:");
            foreach (var g in usersByCountry)
            {
                Console.WriteLine(g.Key);
                foreach (var u in g)
                    Console.WriteLine("  " + u.Name);
            }



            //  Inactive Users (No Posts)
            var inactive = users
                .GroupJoin(posts,
                    u => u.Id,
                    p => p.UserId,
                    (u, pg) => new { User = u, Posts = pg })
                .Where(x => !x.Posts.Any())
                .Select(x => x.User);

            Console.WriteLine("\nInactive Users:");
            foreach (var u in inactive)
                Console.WriteLine(u.Name);



            //  Average Likes Per Post
            var avgLikes = posts.Average(p => p.Likes);

            Console.WriteLine("\nAverage Likes per Post: " + avgLikes);
        }
    }

}
