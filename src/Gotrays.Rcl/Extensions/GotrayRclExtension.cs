using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Gotrays.Rcl.Extensions;

public static class GotrayRclExtension
{
    public static void AddGotraysRcl(this IServiceCollection services)
    {
        services.AddMasaBlazor();

        services.AddAuthorizationCore();
        services.TryAddSingleton<AuthenticationStateProvider, GotraysAuthenticationStateProvider>();

    }
}