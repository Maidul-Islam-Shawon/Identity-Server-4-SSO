using API.Models;

namespace API.Services.Interfaces
{
    public interface ICoffeeShopService
    {
        Task<List<CoffeeShopModels>> GetCoffeeShopList();
    }
}
