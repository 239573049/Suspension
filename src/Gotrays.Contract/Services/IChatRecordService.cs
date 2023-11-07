using GotraysService.Contracts.Dtos.Chats;
using Masa.Utils.Models;

namespace Gotrays.Contract.Services;

public interface IChatRecordService
{
    /// <summary>
    /// 获取聊天记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PaginatedListBase<ChatItem>> ChatHistoryAsync(GetChatHistoryInput input);
}
