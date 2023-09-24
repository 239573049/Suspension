namespace Gotrays.Contract.Modules;

public class AppDto
{
    public Guid id { get; set; }

    public string os { get; set; }

    public string description { get; set; }

    public string versions { get; set; }

    public string url { get; set; }

    public string message { get; set; }

    public DateTime uploadDate { get; set; }
}