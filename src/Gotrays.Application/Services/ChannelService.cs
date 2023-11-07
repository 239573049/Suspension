namespace Gotrays.Application.Services;

public class ChannelService : IChannelService
{
    private readonly IFreeSql _freeSql;

    public ChannelService(IFreeSql freeSql)
    {
        _freeSql = freeSql;
    }

    public async Task<List<ChannelDto>> GetListAsync(string? search)
    {
        return await _freeSql.Select<ChannelDto>()
            .WhereIf(!search.IsNullOrWhiteSpace(), x => x.Description.Contains(search) || x.Title.Contains(search))
            .OrderBy(x => x.Sort).ToListAsync();
    }

    public async Task UpdateAsync(ChannelDto channel)
    {
        await _freeSql.Update<ChannelDto>()
            .Where(x => x.Id == channel.Id)
            .Set(x => x.Sort, channel.Sort)
            .Set(x => x.Description, channel.Description)
            .Set(x => x.Title, channel.Title)
            .Set(x => x.Icon, channel.Icon)
            .Set(x => x.Model, channel.Model)
            .Set(x => x.Role, channel.Role)
            .Set(x => x.MaxHistory, channel.MaxHistory)
            .ExecuteAffrowsAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _freeSql.Delete<ChannelDto>()
            .Where(x => x.Id == id)
            .ExecuteAffrowsAsync();
    }

    public async Task AddAsync(ChannelDto channel)
    {
        await _freeSql.Insert(channel)
            .ExecuteAffrowsAsync();
    }
}