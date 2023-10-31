using BlazorComponent.JSInterop;
using Microsoft.JSInterop;

namespace Gotrays.Shread;

public class DocumentInterop : JSModule
{
    public DocumentInterop(IJSRuntime js) : base(js, "/_content/Gotrays.Rcl/js/document-helper.js")
    {
    }

    public async ValueTask AddEventListener<T>(string key, DotNetObjectReference<T> objectReference, string method)
        where T : class
    {
        await InvokeVoidAsync("addEventListener", objectReference, method);
    }

    public async ValueTask OnScroll<T>(string id, DotNetObjectReference<T> objectReference, string method)
        where T : class
    {
        await InvokeVoidAsync("onScroll", id, objectReference, method);
    }

    /// <summary>
    /// 将滚动条慢慢移动
    /// </summary>
    /// <param name="id"></param>
    /// <param name="value"></param>
    /// <param name="delay"></param>
    public async ValueTask ScrollToBottom(string id, bool value = false, int delay = 0)
    {
        if (delay == 0)
        {
            await InvokeVoidAsync("scrollToBottom", id, value);
        }
        else
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(delay);
                await InvokeVoidAsync("scrollToBottom", id, value);
            });
        }
    }

    public async ValueTask ScrollBottom(string id)
    {
        await InvokeVoidAsync("scrollBottom", id);
    }
}