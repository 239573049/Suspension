namespace GotraysService.Contracts.Dtos.Chats;


public class TranslateModel
{
    /// <summary>
    /// 翻译文本
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 源
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    /// 翻译语言
    /// </summary>
    public string? to { get; set; }

    /// <summary>
    /// 是否使用ChatGpt翻译
    /// </summary>
    public bool ChatGpt { get; set; }
}



public class TranslateResultModel
{
    public Translation[] translations { get; set; }
}

public class Translation
{
    public string text { get; set; }
    public string to { get; set; }
}
