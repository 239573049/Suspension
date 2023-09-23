namespace Gotrays.Contract.Modules;

public class MessageDto
{
    public Guid Id { get; set; }

    public bool Chat { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string? Message { get; set; }

    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 频道Id
    /// </summary>
    public Guid ChannelId { get; set; }

    protected MessageDto()
    {
    }

    public MessageDto(Guid id, bool chat, string value, Guid channelId)
    {
        Id = id;
        Chat = chat;
        Message = value;
        ChannelId = channelId;
        CreatedTime = DateTime.Now;
    }
}