namespace Gotrays.Contract.Services;

public interface IForbiddensService
{
    /// <summary>
    /// 获取所有黑名单
    /// </summary>
    /// <returns></returns>
    Task<string[]> GetListAsync();

    /// <summary>
    /// 增加黑名单
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task AddAsync(string key);

    /// <summary>
    /// 删除黑名单
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task DeleteAsync(string key, string value);
}
