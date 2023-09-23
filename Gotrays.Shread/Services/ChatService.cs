using System.Text;
using System.Text.Json;
using Gotrays.Contract.Services;

namespace Gotrays.Shread.Services;

public class ChatService : ApiService
{
    public ChatService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(httpClientFactory,
        storageService)
    {
    }

    public async Task CreateChatGptClient(object value, Func<string, bool> result)
    {
        var response = await HttpRequestRaw("Chats", value);
        var json = await response.Content.ReadAsStreamAsync();
        using var strings = new StreamReader(json);
        try
        {
            while (!strings.EndOfStream)
            {
                var str = await strings.ReadLineAsync();
                var stop = result.Invoke(str);
                if (stop)
                {
                    return;
                }
            }
        }
        catch
        {
        }
    }

    public async Task<HttpResponseMessage> HttpRequestRaw(string url, object postData = null,
        bool streaming = true)
    {
        HttpRequestMessage req = new(HttpMethod.Post, url);

        if (postData != null)
        {
            if (postData is HttpContent)
            {
                req.Content = postData as HttpContent;
            }
            else
            {
                var jsonContent = JsonSerializer.Serialize(postData, new JsonSerializerOptions
                {
                    IgnoreNullValues = true
                });
                var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                req.Content = stringContent;
            }
        }

        HttpResponseMessage response = await httpClient.SendAsync(req,
            streaming ? HttpCompletionOption.ResponseHeadersRead : HttpCompletionOption.ResponseContentRead);

        if (response.IsSuccessStatusCode)
        {
            return response;
        }

        string resultAsString;
        try
        {
            resultAsString = await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            resultAsString =
                "Additionally, the following error was thrown when attemping to read the response content: " + e;
        }

        throw new HttpRequestException(resultAsString);
    }
}