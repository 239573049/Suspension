namespace GotraysService.Contracts.Dtos.Admin;

public class EditUserDto
{
    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public string Avatar { get; set; }

    public string Password { get; set; }

    public bool IsDisable { get; set; }
}