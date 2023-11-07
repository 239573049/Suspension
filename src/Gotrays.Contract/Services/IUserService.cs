using Gotrays.Contract.Dtos.Auth;
using Gotrays.Contract.Dtos.Users;
using GotraysService.Contracts.Dtos.Auth;

namespace Gotrays.Contract.Services;

public interface IUserService
{
    Task<string> Login(LoginPayload payload);

    Task Registry(RegistryPayload payload);

    Task<GetUserDto> GetAsync();

    Task BindingPhone(string phone, string code);

    Task<GetDayDosageDto> GetDayDosageAsync();

    Task Edit(EditPayload payload);

    Task RetrievePassword(RetrievePasswordload payload);
}
