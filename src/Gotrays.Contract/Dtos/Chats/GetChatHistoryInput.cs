using GotraysService.Contracts.Shared;
using Masa.BuildingBlocks.Ddd.Domain.Repositories;

namespace GotraysService.Contracts.Dtos.Chats;

public class GetChatHistoryInput : PaginatedOptions
{
    public Guid? dialogId { get; set; }

    public string? keyword { get; set; }

    public ChatModel? model { get; set; }
}
