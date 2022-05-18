using IdentityModel.Client;

namespace ClientApp.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
