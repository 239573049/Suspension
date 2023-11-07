using GotraysService.Contracts.Shared;

namespace GotraysService.Contracts.Dtos.Products;

public class StartPayPayload
{
    /// <summary>
    /// 支付平台
    /// </summary>
    public PayType PayType { get; set; } = PayType.alipay;

    /// <summary>
    /// 产品Id
    /// </summary>
    public Guid ProductId { get; set; }
}