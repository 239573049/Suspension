namespace GotraysService.Contracts.Dtos.Activitys;

public class InvitationItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// 被邀请人
    /// </summary>
    public string InviteTo { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    /// 邀请码
    /// </summary>
    public string InvitationCode { get; set; }

    /// <summary>
    /// 邀请时间
    /// </summary>
    public DateTime InvitationTime { get; set; }

    /// <summary>
    /// 可领取vip天数
    /// </summary>
    public int Days { get; set; }
}