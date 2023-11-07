namespace GotraysService.Service.Services;


public class GenerationsModel
{
    public string Prompt { get; set; }

    public int N { get; set; } = 1;

    public string Size { get; set; } = "1024x1024";

    public string response_format { get; set; } = "b64_json";
}

