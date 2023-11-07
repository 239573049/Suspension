using System.Drawing;
using Gotrays.Contract.Dtos.Auth;

namespace Gotrays.Rcl.Pages.Authentication;

public partial class Login
{
    private bool _show;

    private LoginPayload _payload = new ();

    protected override void OnInitialized()
    {
        WindowService.SetWindowSize(new Size(500,600));
        WindowService.UpdateTitle("登录页面");
    }

    private async Task OnLogin()
    {
        var result = await UserService.Login(_payload);

        if (!result.IsNullOrWhiteSpace())
        {
            ((GotraysAuthenticationStateProvider)AuthenticationStateProvider).AuthenticateUser(result);
            NavigationManager.NavigateTo("/");
        }
    }
}