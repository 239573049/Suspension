namespace Gotrays.Contract.Modules;

public class UserInfoDto
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; }
    
    public string Account { get; set; }
    
    public string Avatar { get; set; }
    
    public bool IsVip { get; set; }
    
    public bool IsDisable { get; set; }
    
    public string LastLoginTime { get; set; }
}