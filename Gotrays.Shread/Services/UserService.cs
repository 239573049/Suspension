using System.Net.Http.Json;
using Gotrays.Contract.Modules;
using Gotrays.Contract.Services;

namespace Gotrays.Shread.Services;

public class UserService : ApiService
{
    private readonly IStorageService _storageService;

    private const string ApiPrefix = "Users/";

    public UserService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base(httpClientFactory,
        storageService)
    {
        _storageService = storageService;
    }

    public async Task Login(LoginInput input)
    {
        var message = await httpClient.PostAsJsonAsync(ApiPrefix + "Login", input);

        if (message.IsSuccessStatusCode)
        {
            var result = await message.Content.ReadAsStringAsync();

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + result);
            _storageService.Add(Constant.ApiToken, result);
        }
        else
        {
            var result = await message.Content.ReadFromJsonAsync<ErrorDto>();
            throw new Exception(result?.Message);
        }
    }

    public async Task<UserInfoDto?> GetAsync()
    {
        var message = await httpClient.GetAsync(ApiPrefix);

        if (message.IsSuccessStatusCode)
        {
            return await message.Content.ReadFromJsonAsync<UserInfoDto>();
        }

        return default;
    }
}