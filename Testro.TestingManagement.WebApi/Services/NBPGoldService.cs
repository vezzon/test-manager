using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Testro.TestingManagement.WebApi.Models;

namespace Testro.TestingManagement.WebApi.Services
{
    public class NBPGoldService
    {
        private readonly HttpClient _httpClient;

        public NBPGoldService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GoldPrice>> GetGoldPriceAsync()
        {
            var response = await _httpClient.GetAsync("http://api.nbp.pl/api/cenyzlota/?format=json");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<GoldPrice>>(content);
        }
    }
}