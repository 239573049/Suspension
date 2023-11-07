using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace Gotrays.Rcl.Interops;

public class GotraysInterop : JSModule
{
    public GotraysInterop(IJSRuntime js) : base(js, "/_content/Gotrays.Rcl/js/gotrays.js")
    {
    }

    public async ValueTask CopyText(string text)
    {
        await InvokeVoidAsync("copyText", text);
    }
}