using System;
using System.Collections.Generic;
using System.Linq;

namespace CargoManifestOptimizer
{
    public class Item
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Category { get; set; }

        public Item(string name, double weight, string category)
        {
            Name = name;
            Weight = weight;
            Category = category;
        }

        public override string ToString()
        {
            return
                $"Item:\n" +
                $"  Name     : {Name}\n" +
                $"  Category : {Category}\n" +
                $"  Weight   : {Weight} kg\n";
        }
    }

    public class Container
    {
        public string ContainerID { get; set; }
        public List<Item> Items { get; set; }

        public Container(string id, List<Item> items)
        {
            ContainerID = id;
            Items = items;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
           
            var cargoBay = new List<List<Container>>
            {
                
                new List<Container>
                {
                    new Container("C001", new List<Item>
                    {
                        new Item("Laptop", 2.5, "Tech"),
                        new Item("Monitor", 5.0, "Tech"),
                        new Item("Smartphone", 0.5, "Tech")
                    }),
                    new Container("C104", new List<Item>
                    {
                        new Item("Server Rack", 45.0, "Tech"),
                        new Item("Cables", 1.2, "Tech")
                    })
                },

                new List<Container>
                {
                    new Container("C002", new List<Item>
                    {
                        new Item("Apple", 0.2, "Food"),
                        new Item("Banana", 0.2, "Food"),
                        new Item("Milk", 1.0, "Food")
                    }),
                    new Container("C003", new List<Item>
                    {
                        new Item("Table", 15.0, "Furniture"),
                        new Item("Chair", 7.5, "Furniture")
                    })
                },

                
                new List<Container>
                {
                    new Container("C205", new List<Item>
                    {
                        new Item("vase" ,3.0, "Decor"),
                        new Item("Mirror", 12.0, "Decor")
                    }),
                    new Container("C206", new List<Item>()) // Empty container
                },

                
                new List<Container>()
            };

            Console.WriteLine("------Heavy Containers (>10kg)-----");
            var heavyContainers = FindHeavyContainers(cargoBay, 10);
            foreach (var id in heavyContainers)
                Console.WriteLine(id);

            Console.WriteLine("------Item Counts By Category -----");
            var categoryCounts = GetItemCountsByCategory(cargoBay);
            foreach (var kvp in categoryCounts)
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");

            Console.WriteLine("----- Flattened, Deduplicated & Sorted Shipment -----");
            var flattened = FlattenAndSortShipment(cargoBay);
            foreach (var item in flattened)
                Console.WriteLine(item);
        }

        // Task A
        public static List<string> FindHeavyContainers( List<List<Container>> cargoBay, double weightThreshold)
        {
            if (cargoBay == null)
                return new List<string>();

            return cargoBay
                .Where(row => row != null)
                .SelectMany(row => row)
               .Where(c => c.Items
               .Sum(i => i.Weight) > weightThreshold)
               .Select(container => container.ContainerID)
               .OrderBy(x => x)
               .ToList(); 
        }

        // Task B
        public static Dictionary<string, int> GetItemCountsByCategory(
            List<List<Container>> cargoBay)
        {
            if (cargoBay == null)
                return new Dictionary<string ,int>();
            return cargoBay
                .Where(row => row != null)
                .SelectMany(row => row)
                .Where(container => container?.Items != null)
                .SelectMany(container => container.Items)
                .Where(item => item != null)
                .GroupBy(item => item.Category)
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()
                );
        }

        // Task C
        public static List<Item> FlattenAndSortShipment(
            List<List<Container>> cargoBay)
        {
            if (cargoBay == null)
                return new List<Item>();
            return cargoBay
                .Where(row => row != null)
                .SelectMany(row => row)
                .Where(container => container?.Items != null)
                .SelectMany(container => container.Items)
                .Where(item => item != null)
                .GroupBy(item => item.Name)     
                .Select(group => group.First())
                .OrderBy(item => item.Category)
                .ThenByDescending(item => item.Weight)
                .ToList();
               
        }
    }
}