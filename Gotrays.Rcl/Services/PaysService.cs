using System.Net.Http.Json;
using Gotrays.Contract.Modules;
using Gotrays.Contract.Services;

namespace Gotrays.Rcl.Services;

public class PaysService : ApiService
{
    private const string ApiPrefix = "Pays";

    public PaysService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(httpClientFactory,
        storageService)
    {
    }

    public async Task<StartPayDto?> StartPayAsync(StartPayInput input)
    {
        var message = await httpClient.PostAsJsonAsync(ApiPrefix + "/StartPay", input);

        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync<StartPayDto>();
        }

        var result = await message.Content.ReadFromJsonAsync<ErrorDto>();
        throw new Exception(result?.Message);
    }
}