using System.ComponentModel.DataAnnotations;

namespace GotraysService.Contracts.Dtos.Auth;

public class RegistryPayload
{
    /// <summary>
    /// 昵称
    /// </summary>
    [Required(ErrorMessage = "昵称不能为空")]
    public string UserName { get; set; }

    /// <summary>
    /// 账户
    /// </summary>
    [Required(ErrorMessage = "账号不能为空")]
    [MinLength(6)]
    public string Account { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [Required(ErrorMessage = "手机号不能为空")]
    [MinLength(6)]
    public string Phone { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6)]
    public string Password { get; set; }

    /// <summary>
    /// 邀请码
    /// </summary>
    public string? InvitationCode { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string Verification { get; set; }
}