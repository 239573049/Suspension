using Gotrays.Contract.Dtos.Chats;

namespace Gotrays.Contract.Services;

public interface IChatService
{
    /// <summary>
    /// Chat请求
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task ChatAsync(ChatCompletionDto dto,Action<string> onMessage);
}