using System.ComponentModel.DataAnnotations;

namespace Gotrays.Contract.Dtos.Auth;

public class LoginPayload
{
    /// <summary>
    /// 账户
    /// </summary>
    [Required(ErrorMessage = "账户不能为空")]
    [MinLength(6, ErrorMessage = "账号至少6位")]
    public string Account { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码至少6位")]
    public string Password { get; set; } = string.Empty;
}