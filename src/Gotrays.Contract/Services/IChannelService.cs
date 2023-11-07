using Gotrays.Contract.Dtos.Chats;

namespace Gotrays.Contract.Services;

public interface IChannelService
{
    /// <summary>
    /// 获取频道列表
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    Task<List<ChannelDto>> GetListAsync(string? search);

    /// <summary>
    /// 修改频道
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    Task UpdateAsync(ChannelDto channel);

    /// <summary>
    /// 删除指定频道
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 增加频道
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    Task AddAsync(ChannelDto channel);
}