namespace Gotrays.Contract.Dtos.Chats;

public class CreateAIRoleSettingPayload
{
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
}