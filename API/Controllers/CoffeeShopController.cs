using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeShopController : ControllerBase
    {
        private readonly ICoffeeShopService _coffeeShopService;

        public CoffeeShopController(ICoffeeShopService coffeeShopService)
        {
            this._coffeeShopService = coffeeShopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoffeeShops()
        {
            var coffeeShops = await _coffeeShopService.GetCoffeeShopList();
            return Ok(coffeeShops);
        }
    }
}
