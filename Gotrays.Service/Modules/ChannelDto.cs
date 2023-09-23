namespace Gotrays.Contract.Modules;

public class ChannelDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// 历史
    /// </summary>
    public int History { get; set; } = 0;

    /// <summary>
    /// 最大Token数量
    /// </summary>
    public int MaxTokens { get; set; } = 500;

    /// <summary>
    /// chatGPT的Key
    /// </summary>
    public string Key { get; set; }
    
    public ChannelDto()
    {
    }
    
    public ChannelDto(string title, string avatar)
    {
        Id = Guid.NewGuid();
        Title = title;
        Avatar = avatar;
        CreatedTime = DateTime.Now;
    }
}