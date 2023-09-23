using System.ComponentModel.DataAnnotations;

namespace Gotrays.Contract.Modules;

public class LoginInput
{
    [Required(ErrorMessage = "请填写账号！")]
    [MinLength(5, ErrorMessage = "账号最短5位！")]
    public string Account { get; set; }
    
    [Required(ErrorMessage = "请填写密码！")]
    [MinLength(5, ErrorMessage = "密码最短5位")]
    public string Password { get; set; }
}