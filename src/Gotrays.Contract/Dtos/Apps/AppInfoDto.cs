namespace GotraysService.Contracts.Dtos.Apps;

public class AppInfoDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string OS { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 版本号
    /// </summary>
    public string Versions { get; set; }

    /// <summary>
    /// 下载地址
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// 更新内容
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 上架日期
    /// </summary>
    public DateTime UploadDate { get; set; }
}
