using System.ComponentModel.DataAnnotations;

namespace Gotrays.Contract.Dtos.Chats;

/// <summary>
/// 频道
/// </summary>
public class ChannelDto :ICloneable
{
    public Guid Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    [Required]
    public string Title { get; set; }

    /// <summary>
    /// 模型
    /// </summary>
    [Required]
    public string Model { get; set; }

    /// <summary>
    /// 角色定义
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 最大关联历史数量
    /// </summary>
    public int MaxHistory { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    public object Clone()
    {
        return new ChannelDto()
        {
            Title = this.Title,
            CreatedTime = this.CreatedTime,
            MaxHistory = this.MaxHistory,
            Description = Description,
            Icon = this.Icon,
            Sort = this.Sort,
            Model = this.Model,
            Role = this.Role,
            Id = this.Id,
        };
    }
}