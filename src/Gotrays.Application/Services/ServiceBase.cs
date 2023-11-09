namespace Gotrays.Application.Services;

public abstract class ServiceBase
{
    protected HttpClient Client;
    protected readonly IStorageService _storageService;
    protected readonly string Prefix;
    protected readonly IHttpClientFactory _httpClientFactory;
    protected ServiceBase(string prefix, IHttpClientFactory httpClientFactory, IStorageService storageService)
    {
        Prefix = prefix;
        _httpClientFactory = httpClientFactory;
        _storageService = storageService;
        Client = _httpClientFactory.CreateClient(Constant.HttpClientOptions.OpenService);
    }

    /// <summary>
    /// 更新HttpClient中的Token
    /// </summary>
    /// <param name="token"></param>
    protected void UpdateToken(string token)
    {
        Client.DefaultRequestHeaders.Remove("Authorization");
        Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        _storageService.Add(Constant.HttpClientOptions.Token, token);
    }
}