using System.Text.Json.Serialization;

namespace Gotrays.Contract.Dtos.Chats;

public class ChatCompletionDto
{
    public string model { get; set; }

    public List<ChatCompletionRequestMessage> messages { get; set; } = new();

    public double? temperature { get; set; }

    /// <summary>
    /// 用温度采样的另一种方法称为核采样，其中模型考虑具有top_p概率质量的token的结果。因此0.1意味着只考虑包含前10%概率质量的标记。我们通常建议改变这个或“温度”，但不建议两者都改变。
    /// </summary>
    public double? top_p { get; set; }

    public int max_tokens { get; set; } = 500;

    /// <summary>
    /// 在-2.0到2.0之间的数字。正值会根据新标记在文本中存在的频率来惩罚它们，降低模型逐字重复同一行的可能性。[有关频率和存在惩罚的更多信息。](https://docs.api-reference/parameter -details)
    /// </summary>
    public double? frequency_penalty { get; set; }

    public Error error { get; set; }
}

public class ChatCompletionRequestMessage
{
    public string role { get; set; }

    public string content { get; set; }

    public string? name { get; set; }

    [JsonIgnore] public int token { get; set; }

    [JsonIgnore] public int Sort { get; set; }
}

public class Error
{
    public string message { get; set; }
    public string type { get; set; }
    public object param { get; set; }
    public string code { get; set; }
}