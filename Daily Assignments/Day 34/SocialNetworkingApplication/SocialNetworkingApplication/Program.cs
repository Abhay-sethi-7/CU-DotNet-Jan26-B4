namespace SocialNetworkingApplication
{

    class Person
    {
        public string Name { get; set; }

        public List<Person> Friends = new List<Person>();
        public void AddFriend(Person friend)
        {

            if (friend == this)
            {
                Console.WriteLine("Cannot add yourself as friend.");
                return;
            }


            if (!Friends.Contains(friend))
            {
                Friends.Add(friend);
                friend.Friends.Add(this);
                Console.WriteLine($"{Name} and {friend.Name} are now friends.");
            }
            else
            {
                Console.WriteLine($"{friend.Name} is already your friend.");
            }
        }
        public void ShowFriends()
        {
            Console.WriteLine($"Friends of {Name}:");

            if (Friends.Count == 0)
            {
                Console.WriteLine("No friends yet.");
                return;
            }

            foreach (var f in Friends)
            {
                Console.WriteLine("- " + f.Name);
            }
        }


    }



    class SocialNetwork
    {
        private List<Person> members = new List<Person>();

        public void AddMember(Person member)
        {
            members.Add(member);
        }

        public void ShowNetwork()
        {
            foreach (var member in members)
            {
                Console.WriteLine(member.Name);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            SocialNetwork network = new SocialNetwork();

            Person abhay = new Person { Name = "Abhay" };
            Person rahul = new Person { Name = "Rahul" };
            Person aman = new Person { Name = "Aman" };
            network.AddMember(abhay);
            network.AddMember(rahul);
            network.AddMember(aman);
            network.ShowNetwork();

            abhay.AddFriend(rahul);
            rahul.AddFriend(aman);

            abhay.ShowFriends();
            rahul.ShowFriends();
            aman.ShowFriends();

            Console.ReadLine();

        }
    }
}
