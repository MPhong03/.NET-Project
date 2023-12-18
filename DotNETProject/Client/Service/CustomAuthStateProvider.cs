using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace DotNETProject.Client.Service
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        public CustomAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                await _localStorage.SetItemAsync("authToken", token);
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(anonymousUser);
            }

            var tokenClaims = _tokenHandler.ReadJwtToken(token);
            var user = new ClaimsPrincipal(new ClaimsIdentity(tokenClaims.Claims, "jwt"));

            return new AuthenticationState(user);
        }

        public async Task CheckTokenExpirationAndRedirect()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                var tokenClaims = _tokenHandler.ReadJwtToken(token);

                var expClaim = tokenClaims.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Exp);

                if (expClaim != null && long.TryParse(expClaim.Value, out long exp))
                {
                    var utcNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    if (exp < utcNow)
                    {
                        await MarkUserAsLoggedOut();
                    }
                }
            }
        }

        public string GetUserRole(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Role)?.Value;
        }

        public string GetUserEmail(ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

        public async Task<string> GetTokenFromLocalStorage()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            return token;
        }

    }
}
