using GotraysService.Contracts.Dtos.Products;

namespace Gotrays.Contract.Services;

public interface IChatProductService
{
    /// <summary>
    /// 获取产品列表
    /// </summary>
    /// <returns></returns>
    Task<List<GetProductListDto>> GetListAsync();


}