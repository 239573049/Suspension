using Gotrays.Contract.Dtos.Chats;

namespace Gotrays.Contract.Services;

public interface IChatMessageService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="channelId">频道id</param>
    /// <param name="page">默认1 最小1页</param>
    /// <param name="pageSize">默认5</param>
    /// <returns></returns>
    Task<List<ChatMessageDto>> GetListAsync(Guid channelId, int page = 1, int pageSize = 5);

    /// <summary>
    /// 更新消息
    /// </summary>
    /// <param name="messageDto"></param>
    /// <returns></returns>
    Task UpdateAsync(ChatMessageDto messageDto);

    /// <summary>
    /// 删除id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 创建消息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task CreateAsync(ChatMessageDto dto);
}