using System.Net.Http.Json;
using Gotrays.Contract.Modules;
using Gotrays.Contract.Services;

namespace Gotrays.Rcl.Services;

public class ChatProductsService : ApiService
{
    private const string ApiPrefix = "ChatProducts";
    
    public ChatProductsService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(httpClientFactory, storageService)
    {
    }

    public async Task<List<ChatProductDto>> GetListAsync()
    {
        var data =await httpClient.GetAsync(ApiPrefix + "/List");
        if (data.IsSuccessStatusCode)
        {
            return await data.Content.ReadFromJsonAsync<List<ChatProductDto>>();
        }

        return new List<ChatProductDto>();
    }
}