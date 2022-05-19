using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ClientApp.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> _IdentityServerSettings;
        public readonly DiscoveryDocumentResponse _discoveryDocument;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<IdentityServerSettings> IdentityServerSettings)
        {
            this._IdentityServerSettings = IdentityServerSettings;
            this._httpClient = new HttpClient();
            this._discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(
                this._IdentityServerSettings.Value.DiscoveryUrl).Result;

            if (_discoveryDocument.IsError)
                throw new Exception("Unable to get discovery document", _discoveryDocument.Exception);
        }


        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = _discoveryDocument.TokenEndpoint,
                    ClientId = _IdentityServerSettings.Value.ClientName,
                    ClientSecret = _IdentityServerSettings.Value.ClientPassword,
                    Scope = scope
                });

            if (tokenResponse.IsError)
                throw new Exception("Unable to get Token", tokenResponse.Exception);

            return tokenResponse;
        }
    }
}
