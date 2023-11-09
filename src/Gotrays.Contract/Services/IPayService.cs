using Gotrays.Contract.Dtos.Pays;
using Gotrays.Contract.Dtos.Products;
using GotraysService.Contracts.Dtos.Products;
using Masa.Utils.Models;

namespace Gotrays.Contract.Services;

public interface IPayService
{
    Task<StartPayPayloadDto> StartPayAsync(StartPayPayload payload);

    Task<PaginatedListBase<ProductListDto>> PayHistoryAsync(int page,int pageSize);
}