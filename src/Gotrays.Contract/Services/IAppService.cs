using Gotrays.Contract.Dtos.Systems;

namespace Gotrays.Contract.Services;

public interface IAppService
{
    Task<GiteeReleaseDto> GetApp();
}