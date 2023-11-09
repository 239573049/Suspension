using GotraysService.Contracts.Dtos.Products;

namespace Gotrays.Application.Services;

public class ChatProductService : ServiceBase,IChatProductService
{
    public ChatProductService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base("ChatProducts", httpClientFactory, storageService)
    {
    }


    public async Task<List<GetProductListDto>> GetListAsync()
    {
        var result = await Client.GetFromJsonAsync<List<GetProductListDto>>(Prefix+"/List");

        return result;
    }
}