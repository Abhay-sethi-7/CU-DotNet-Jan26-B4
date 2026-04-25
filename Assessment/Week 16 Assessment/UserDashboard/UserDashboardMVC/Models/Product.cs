namespace UserDashboardMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public string ImageUrl1 { get; set; } = string.Empty;
        public string? ImageUrl2 { get; set; }
        public string? ImageUrl3 { get; set; }
    }

    public class HomeViewModel
    {
        public List<Product> HeroProducts { get; set; } = new();
        public List<Product> NewInTech { get; set; } = new();
        public List<Product> Trending { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }

    public class Category
    {
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
    }
}
