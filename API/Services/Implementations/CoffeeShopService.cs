using API.Models;
using API.Services.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _dbContext;

        public CoffeeShopService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<CoffeeShopModels>> GetCoffeeShopList()
        {
            return await (from shop in _dbContext.CoffeeShops
                          select new CoffeeShopModels()
                          {
                              Id = shop.Id,
                              Name = shop.Name,
                              OpeningHours = shop.OpeningHours,
                              Address = shop.Address,
                          }).ToListAsync();

        }
    }
}
