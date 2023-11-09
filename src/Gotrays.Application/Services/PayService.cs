using Gotrays.Contract.Dtos.Pays;
using Gotrays.Contract.Dtos.Products;
using Masa.Utils.Models;

namespace Gotrays.Application.Services;

public class PayService : ServiceBase, IPayService
{
    public PayService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base("Pays", httpClientFactory, storageService)
    {
    }

    public async Task<StartPayPayloadDto> StartPayAsync(StartPayPayload payload)
    {
        var response = await Client.PostAsJsonAsync(Prefix + "/StartPay", payload);

        return await response.Content.ReadFromJsonAsync<StartPayPayloadDto>();
    }

    public async Task<PaginatedListBase<ProductListDto>> PayHistoryAsync(int page, int pageSize)
    {
        return await Client.GetFromJsonAsync<PaginatedListBase<ProductListDto>>(Prefix + "/PayHistory?page="+ page+"&pageSize="+pageSize);

    }
}