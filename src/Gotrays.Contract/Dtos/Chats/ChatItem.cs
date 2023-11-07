using GotraysService.Contracts.Shared;

namespace GotraysService.Contracts.Dtos.Chats;


public class ChatItem
{
    public Guid Id { get; set; }

    public ChatModel Model { get; set; }

    /// <summary>
    /// 对话id
    /// </summary>
    public Guid DialogId { get;  set; }

    /// <summary>
    /// 提示
    /// </summary>
    public string Prompt { get;  set; }

    /// <summary>
    /// 回应
    /// </summary>
    public string Replay { get;  set; }

    /// <summary>
    /// 花费的tokens
    /// </summary>
    public double UsageTokens { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    public Guid UserId { get; set; }

    /// <summary>
    /// 访问IP
    /// </summary>
    public string? Ip { get;  set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
}