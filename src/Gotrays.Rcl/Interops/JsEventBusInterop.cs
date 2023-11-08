using Gotrays.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Token.Events;

namespace Gotrays.Rcl;

public class JsEventBusInterop
{
    [JSInvokable]
    public static async Task Notifications(string message)
    {
        var eventBus = RclContext.ServiceProvider.GetService<IKeyLoadEventBus>();

        await eventBus.PushAsync(Constant.LoadEventBus.Notifications, message);
    }
    
    /// <summary>
    /// base64编码
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    [JSInvokable]
    public static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    /// <summary>
    /// base64解码
    /// </summary>
    /// <param name="base64EncodedText"></param>
    /// <returns></returns>
    [JSInvokable]
    public static string Base64Decode(string base64EncodedText)
    {
        byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}