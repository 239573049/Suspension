namespace Gotrays.Contract.Dtos.Systems;

public class AnnouncementDto
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 内容
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 高亮内容
    /// </summary>
    public string Highlighting { get; set; }
}