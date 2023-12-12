using BlazorComponent;
using Gotrays.Contract;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Token.Extensions;

namespace Gotrays.Rcl;

[DependOns(typeof(GotraysContractModule))]
public class GotraysRclModule : CoreFlexModule
{
    public override void ConfigureServices(CoreFlexServiceContext context)
    {
        context.Services.AddMasaBlazor(options => { options.Locale = new Locale("zh-CN", "en-US"); });
        context.Services.AddAuthorizationCore();
        context.Services.TryAddSingleton<AuthenticationStateProvider, GotraysAuthenticationStateProvider>();

        context.Services.AddEventBus();
    }

    public override void OnApplicationShutdown(CoreFlexBuilder app)
    {
        RclContext.SetServiceProvider(app.Services);
    }
}