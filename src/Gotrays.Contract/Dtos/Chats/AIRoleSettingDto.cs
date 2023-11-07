namespace GotraysService.Contracts.Dtos.Chats;

public class AIRoleSettingDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 角色定义内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }
    
    /// <summary>
    /// 是否用户自定义的
    /// </summary>
    public bool IsCustom { get; set; }
}
