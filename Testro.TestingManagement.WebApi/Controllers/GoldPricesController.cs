using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Testro.TestingManagement.WebApi.Exceptions;
using Testro.TestingManagement.WebApi.Models;
using Testro.TestingManagement.WebApi.Services;

namespace Testro.TestingManagement.WebApi.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class GoldPricesController : ControllerBase
    {
        private readonly NBPGoldService _service;

        public GoldPricesController(NBPGoldService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<GoldPrice>> GetGoldPrice()
        {
            var goldPrices = await _service.GetGoldPriceAsync();
            if (goldPrices is null)
                throw new NotFoundException();

            return goldPrices;
        }
    }
}