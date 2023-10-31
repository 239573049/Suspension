using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace Gotrays.Shread;

public class MainInterop : JSModule
{
    public MainInterop(IJSRuntime js) : base(js,"/_content/Gotrays.Rcl/js/main.js")
    {
    }

    public async ValueTask Init(string id)
    {
        await InvokeVoidAsync("init", id);
    }

    public async ValueTask QrCode(string id, string value)
    {
        await InvokeVoidAsync("qrCode", id, value);
    }
}