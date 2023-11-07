﻿namespace Gotrays.Application.Services;

public class ChatMessageService : IChatMessageService
{
    private readonly IFreeSql _freeSql;

    public ChatMessageService(IFreeSql freeSql)
    {
        _freeSql = freeSql;
    }

    public async Task<List<ChatMessageDto>> GetListAsync(Guid channelId, int page = 1, int pageSize = 5)
    {
        var result = await _freeSql.Select<ChatMessageDto>()
            .Where(x => x.ChannelId == channelId)
            .OrderBy(x=>x.CreatedTime)
            .Page(page, pageSize)
            .ToListAsync();

        return result;
    }

    public async Task UpdateAsync(ChatMessageDto messageDto)
    {
        await _freeSql.Update<ChatMessageDto>()
            .Where(x => x.Id == messageDto.Id)
            .Set(x => x.Message, messageDto.Message)
            .Set(x => x.Type, messageDto.Type)
            .ExecuteAffrowsAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _freeSql.Delete<ChatMessageDto>()
            .Where(x => x.Id == id)
            .ExecuteAffrowsAsync();
    }

    public async Task CreateAsync(ChatMessageDto dto)
    {
        await _freeSql.Insert(dto)
            .ExecuteAffrowsAsync();
    }
}