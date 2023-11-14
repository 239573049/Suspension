namespace Gotrays.Application.Services;

public class ChatService : ServiceBase, IChatService
{
    public ChatService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(
        "Chats", httpClientFactory, storageService)
    {
    }

    public async Task ChatAsync(ChatCompletionDto dto, Action<string> onMessage)
    {
        using HttpRequestMessage req = new(HttpMethod.Post, Prefix);

        req.Content = new StringContent(JsonSerializer.Serialize(dto, new JsonSerializerOptions
        {
            IgnoreNullValues = true
        }), Encoding.UTF8, "application/json");

        UpdateToken(_storageService.Get(Constant.HttpClientOptions.Token));
        using var response = await Client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStreamAsync();
            using var strings = new StreamReader(json);
            while (!strings.EndOfStream)
            {
                var str = await strings.ReadLineAsync();
                onMessage.Invoke(str ?? string.Empty);
            }
            
            return;
        }

        var errorException = await response.Content.ReadFromJsonAsync<ErrorDto>();

        throw new ErrorException(errorException.Message);
    }
}