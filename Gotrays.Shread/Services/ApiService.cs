using Gotrays.Contract.Services;

namespace Gotrays.Shread.Services;

public abstract class ApiService
{
    protected HttpClient httpClient;

    protected ApiService(IHttpClientFactory httpClientFactory,IStorageService storageService)
    {
        httpClient = httpClientFactory.CreateClient(Constant.HttpServiceName);
        httpClient.DefaultRequestHeaders.Remove("Authorization");
        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + storageService.GetString(Constant.ApiToken));
    }
}