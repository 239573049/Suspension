namespace GotraysService.Contracts.Dtos.Admin;

public class ChatRecordTop10Summary
{
    public string UserName { get; set; }

    public string Account { get; set; }

    public string Avatar { get; set; }

    public DateTime VipExpires { get; set; }

    public int ChatRecordCount { get; set; }
}