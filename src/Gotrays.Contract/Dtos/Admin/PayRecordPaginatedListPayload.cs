using GotraysService.Contracts.Shared;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;

namespace GotraysService.Contracts.Dtos.Admin;

public class PayRecordPaginatedListPayload : PaginatedOptions
{
    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 支付平台
    /// </summary>
    public PayType? PayType { get; set; }

    /// <summary>
    /// 产品名称
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// 支付状态
    /// </summary>
    public PayState? PayState { get; set; }

    /// <summary>
    /// 创建起始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 创建截至时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}