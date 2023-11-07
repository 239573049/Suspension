using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Gotrays.Contract.Services;
using Gotrays.Shared;

namespace Gotrays.Rcl;

public class GotraysAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IStorageService _storageService;

    public GotraysAuthenticationStateProvider(IStorageService storageService)
    {
        _storageService = storageService;
    }

    private ClaimsPrincipal User { get; set; }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (User != null)
        {
            return Task.FromResult(new AuthenticationState(User));
        }

        var token = _storageService.Get(Constant.HttpClientOptions.Token);
        
        var identity = new ClaimsIdentity();
        User = new ClaimsPrincipal(identity);
        if (!token.IsNullOrWhiteSpace())
        {
            identity.AddClaim(new Claim(ClaimTypes.Name, "token"));
            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(User)));
            return Task.FromResult(new AuthenticationState(User));
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));

    }

    public void AuthenticateUser(string token)
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "token"),
        }, "GotraysAuthentication");

        User = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(User)));
    }
}