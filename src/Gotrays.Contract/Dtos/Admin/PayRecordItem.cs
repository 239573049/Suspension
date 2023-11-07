using GotraysService.Contracts.Shared;

namespace GotraysService.Contracts.Dtos.Admin;

public class PayRecordItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 支付平台
    /// </summary>
    public PayType PayType { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// 原价
    /// </summary>
    public decimal OriginalPrice { get; set; }

    /// <summary>
    /// 实际支付金额
    /// </summary>
    public decimal ActualPayment { get; set; }

    /// <summary>
    /// 支付状态
    /// </summary>
    public PayState PayState { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { get; set; }
}