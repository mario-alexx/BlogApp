﻿using Blazored.LocalStorage;
using BlogClienteWasm.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace BlogClienteWasm.Servicios
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _cliente;
        private readonly ILocalStorageService _localStorageService;

        public AuthStateProvider(HttpClient cliente, ILocalStorageService localStorageService)
        {
            _cliente = cliente;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>(Inicializar.Token_Local);
            if(token == null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            _cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }
    }
}