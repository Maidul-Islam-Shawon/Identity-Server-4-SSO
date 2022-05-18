using API.Models;
using ClientApp.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Components;


namespace ClientApp.Pages
{
    public partial class CoffeeShops
    {
        private List<CoffeeShopModels> Shops = new();
        [Inject]private HttpClient HttpClient { get; set; }
        [Inject]private IConfiguration Configuration { get; set; }
        [Inject]private ITokenService TokenService { get; set; }

     
        protected override async Task OnInitializedAsync()
        {
            var tokenResponse = await TokenService.GetToken("CoffeeApi.Read");
            HttpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await HttpClient.GetAsync(Configuration["apiUrl"] + "/api/coffeeshop");

            if (result == null)
                throw new NullReferenceException("No data found!");

            if (result.IsSuccessStatusCode)
            {
                Shops = await result.Content.ReadFromJsonAsync<List<CoffeeShopModels>>();  
            }
        }
    }
}
