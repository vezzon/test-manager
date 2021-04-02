using System;
using Newtonsoft.Json;

namespace Testro.TestingManagement.WebApi.Models
{
    public class GoldPrice
    {
        [JsonProperty("data")]
        public DateTimeOffset Data { get; set; }

        [JsonProperty("cena")]
        public double Cena { get; set; }
    }
}