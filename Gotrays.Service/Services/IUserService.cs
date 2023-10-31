using Gotrays.Contract.Modules;

namespace Gotrays.Contract.Services;

public interface IUserService
{
    Task Login(LoginInput input);

    Task<UserInfoDto?> GetAsync();
}