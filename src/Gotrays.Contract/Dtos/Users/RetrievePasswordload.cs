using System.ComponentModel.DataAnnotations;

namespace Gotrays.Contract.Dtos.Users;

public class RetrievePasswordload
{
    /// <summary>
    /// 手机号码
    /// </summary>
    [Required(ErrorMessage = "手机号码不能为空")]
    public string Phone { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    [Required(ErrorMessage = "验证码不能为空")]
    public string Code { get; set; }

    /// <summary>
    /// 新的密码
    /// </summary>
    [Required(ErrorMessage = "新的密码不能为空")]
    public string NewPassword { get; set; }
}