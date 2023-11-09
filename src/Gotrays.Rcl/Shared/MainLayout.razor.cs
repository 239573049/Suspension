using System.Drawing;
using System.Security.Claims;
using Gotrays.Contract.Dtos.Users;

namespace Gotrays.Rcl.Shared;

public partial class MainLayout
{
    private GetUserDto userDto;

    private string currentMenuId = Home;

    private const string Home = "/";

    private const string Setting = "/pages/Setting";

    private const string Shopping = "/pages/shopping";

    protected override async Task OnInitializedAsync()
    {
        WindowService.UpdateTitle("智能聊天首页");
        WindowService.SetWindowSize(new Size(1080, 720));
        var result = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        // 当登录才获取信息
        if (result.User.Claims.Any(x => x.Type == ClaimTypes.Name))
            userDto = await UserService.GetAsync();
    }

    private void SelectMenu(string id)
    {
        if (id == currentMenuId)
        {
            return;
        }

        currentMenuId = id;

        NavigationManager.NavigateTo(id);
    }

    private string GetClass(string id)
    {
        if (id == currentMenuId)
        {
            return "rcl-menu-select";
        }

        return "";
    }
}