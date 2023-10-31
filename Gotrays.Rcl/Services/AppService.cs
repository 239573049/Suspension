using System.Net.Http.Json;
using Gotrays.Contract.Modules;
using Gotrays.Contract.Services;

namespace Gotrays.Rcl.Services;

public class AppService : ApiService
{
    private const string ApiPrefix = "Apps";

    public AppService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(httpClientFactory, storageService)
    {
    }

    public async Task<AppDto> GetAsync()
    {
        return await httpClient.GetFromJsonAsync<AppDto>(ApiPrefix + "?os=" + Constant.OS);
    }
}