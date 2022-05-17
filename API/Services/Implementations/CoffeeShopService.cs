using API.Models;
using API.Services.Interfaces;
using AutoMapper;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CoffeeShopService(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<List<CoffeeShopModels>> GetCoffeeShopList()
        {
            var coffeeShops = await _dbContext.CoffeeShops.ToListAsync();
            var coffeeShopDto = _mapper.Map<List<CoffeeShopModels>>(coffeeShops);
            return coffeeShopDto;
            //return await (from shop in _dbContext.CoffeeShops
            //              select new CoffeeShopModels()
            //              {
            //                  Id = shop.Id,
            //                  Name = shop.Name,
            //                  OpeningHours = shop.OpeningHours,
            //                  Address = shop.Address,
            //              }).ToListAsync();
        }
    }
}
