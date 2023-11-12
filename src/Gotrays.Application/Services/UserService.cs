
namespace Gotrays.Application.Services;

public class UserService : ServiceBase, IUserService
{

    public UserService(IHttpClientFactory httpClientFactory, IStorageService storageService) : base("Users", httpClientFactory, storageService)
    {
    }

    public async Task<string> Login(LoginPayload payload)
    {
        var response = await Client.PostAsJsonAsync(Prefix + "/Login", payload);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();

            UpdateToken(result);
            return result;
        }

        var errorException = await response.Content.ReadFromJsonAsync<ErrorDto>();

        throw new ErrorException(errorException.Message);
    }

    public async Task Registry(RegistryPayload payload)
    {
        var response = await Client.PostAsJsonAsync(Prefix + "/", payload);

    }

    public async Task<GetUserDto> GetAsync()
    {
        var response = await Client.GetAsync(Prefix);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GetUserDto>();
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }

        throw new UnauthorizedAccessException();
    }

    public Task BindingPhone(string phone, string code)
    {
        throw new NotImplementedException();
    }

    public async Task<GetDayDosageDto> GetDayDosageAsync()
    {
        return await Client.GetFromJsonAsync<GetDayDosageDto>(Prefix+"/DayDosage");
    }

    public Task Edit(EditPayload payload)
    {
        throw new NotImplementedException();
    }

    public Task RetrievePassword(RetrievePasswordload payload)
    {
        throw new NotImplementedException();
    }

}