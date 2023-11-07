namespace GotraysService.Contracts.Dtos.Products;

public class GetProductListDto
{
    public Guid Id { get; set; }

    /// <summary>
    /// 产品名称 唯一
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 产品描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 实际需要支付金额
    /// </summary>
    public decimal ActualPrice { get; set; }

    /// <summary>
    /// 原价
    /// </summary>
    public decimal OriginalPrice { get; set; }

    /// <summary>
    /// 天
    /// </summary>
    public int Days { get; set; }
}