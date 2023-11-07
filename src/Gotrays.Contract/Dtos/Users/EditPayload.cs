using System.ComponentModel.DataAnnotations;

namespace Gotrays.Contract.Dtos.Users;

public class EditPayload
{
    public Guid UserId { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [Required(ErrorMessage = "昵称不能为空")]
    public string UserName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [Required(ErrorMessage = "头像不能为空")]
    public string Avatar { get; set; }
}