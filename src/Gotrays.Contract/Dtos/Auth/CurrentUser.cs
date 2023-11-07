namespace GotraysService.Contracts.Dtos.Auth;

public class CurrentUser
{
    /// <summary>
    /// 用户id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 是否会员
    /// </summary>
    public bool IsVip { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public string? Role { get; set; }
}