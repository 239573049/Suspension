namespace GotraysService.Contracts.Dtos.Admin;

public class ChatRecordItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// 发起人
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 提示
    /// </summary>
    public string Prompt { get; set; }

    /// <summary>
    /// 回应
    /// </summary>
    public string Replay { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}