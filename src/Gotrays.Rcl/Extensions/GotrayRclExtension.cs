﻿using BlazorComponent;
using Gotrays.Rcl.Interops;
using Masa.Blazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Token.Extensions;

namespace Gotrays.Rcl.Extensions;

public static class GotrayRclExtension
{
    public static void AddGotraysRcl(this IServiceCollection services)
    {
        services.AddMasaBlazor(options => {
            
            options.Locale = new Locale("zh-CN", "en-US");
        });
        services.AddAuthorizationCore();
        services.TryAddSingleton<AuthenticationStateProvider, GotraysAuthenticationStateProvider>();
        services.AddScoped<GotraysInterop>();
        services.AddEventBus();
    }

    public static IServiceProvider BuilderRcl(this IServiceProvider serviceProvider)
    {
        RclContext.SetServiceProvider(serviceProvider);

        return serviceProvider;
    }
}