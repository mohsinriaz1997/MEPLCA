using Blazored.LocalStorage;
using CA.API.Models;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;
using System.Security.Claims;

namespace CA.UI.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly RestClient _restClient;

        //private readonly HttpClient oClient;
        private readonly ILocalStorageService oService;

        private readonly AuthenticationState oAnonymus;

        //public AuthStateProvider(HttpClient restClient, ILocalStorageService localStorageService)
        //{
        //    oClient = restClient;
        //    oService = localStorageService;
        //    oAnonymus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        //}

        public AuthStateProvider(ILocalStorageService localStorageService)
        {
            _restClient = new RestClient(Settings.APIBaseURL);
            oService = localStorageService;
            oAnonymus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                MstUserProfile user = await oService.GetItemAsync<MstUserProfile>("User");
                if (user is not null)
                {
                    string jwtTokenString = user.TokenValue;
                    if (string.IsNullOrEmpty(jwtTokenString))
                    {
                        return oAnonymus;
                    }
                    //Logs.oUser = user;
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(jwtTokenString), "jwtAuthType")));
                }
                else
                {
                    return oAnonymus;
                }
            }
            catch (Exception ex)
            {
                return oAnonymus;
            }
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(oAnonymus);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}