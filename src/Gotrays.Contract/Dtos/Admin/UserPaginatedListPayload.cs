using Masa.BuildingBlocks.Ddd.Domain.Repositories;

namespace GotraysService.Contracts.Dtos.Admin;

public class UserPaginatedListPayload : PaginatedOptions
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Keyword { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    public bool? IsDisable { get; set; }
}