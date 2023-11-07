using Gotrays.Shared;

namespace Gotrays.Contract.Dtos.Chats;

public class ChatMessageDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 频道id
    /// </summary>
    public Guid ChannelId { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 是否本人发送
    /// </summary>
    public bool Curren { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public ChatMessageType Type { get; set; }
    
    public Func<Task> OnCopy { get; set; }
    
    public Func<Task> OnDelete { get; set; }
    
    public Func<Task> OnUpdate { get; set; }
}