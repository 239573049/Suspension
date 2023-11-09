using Gotrays.Contract.Dtos.Systems;

namespace Gotrays.Application.Services;

public class AppService : IAppService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AppService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<GiteeReleaseDto> GetApp()
    {
        var client = _httpClientFactory.CreateClient(nameof(AppService));

        var result =
            await client.GetFromJsonAsync<GiteeReleaseDto>(
                "https://gitee.com/api/v5/repos/gotrays/gotrays-suspension/releases/latest");

        return result;
    }
}