 using MongoDB.Driver;
    using VoltGearSystems.Models;
namespace VoltGearSystems.Services
{
   
    public class LaptopService
    {
        private readonly IMongoCollection<Laptop> _laptops;

        public LaptopService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(config["MongoDbSettings:DatabaseName"]);
            _laptops = database.GetCollection<Laptop>(config["MongoDbSettings:CollectionName"]);
        }

        public async Task CreateAsync(Laptop newLaptop)
        {
            await _laptops.InsertOneAsync(newLaptop);
        }

        public async Task<List<Laptop>> GetAsync()
        {
            return await _laptops.Find(_ => true).ToListAsync();
        }
    }
}
