namespace GotraysService.Contracts.Dtos.Admin;

public class UserItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 账户
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 是否vip
    /// </summary>
    public bool IsVip { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool IsDisable { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 上一次登陆时间
    /// </summary>
    public DateTime LastLoginTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime ModificationTime { get; set; }
}