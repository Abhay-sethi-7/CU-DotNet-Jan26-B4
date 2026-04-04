using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TravelDestination.FrontEnd.Models;
namespace TravelDestination.FrontEnd.Services
{


    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _httpClient;

        public DestinationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/destinations");

            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<Destination>();
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = await response.Content.ReadFromJsonAsync<IEnumerable<Destination>>(options);

            return data ?? Enumerable.Empty<Destination>();
        }

        // GET BY ID
        public async Task<Destination?> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync("api/destinations/" + id);

            if (!response.IsSuccessStatusCode)
                return null;

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = await response.Content.ReadFromJsonAsync<Destination>(options);

            return data;
        }

        // CREATE
        public async Task CreateAsync(Destination destination)
        {
            var json = JsonSerializer.Serialize(destination);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("api/destinations", content);
        }

        // UPDATE
        public async Task UpdateAsync(int id, Destination destination)
        {
            var json = JsonSerializer.Serialize(destination);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/destinations/" + id, content);
        }

        // DELETE
        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync("api/destinations/" + id);
        }
    }

}
