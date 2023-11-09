using System.Drawing;
using System.Reflection;
using System.Security.Claims;
using Gotrays.Contract.Dtos.Chats;
using Gotrays.Contract.Dtos.Systems;
using Gotrays.Contract.Dtos.Users;
using Gotrays.Rcl.Components;

namespace Gotrays.Rcl.Shared;

public partial class MainLayout
{
    private GetUserDto userDto;

    private string currentMenuId = Home;

    private const string Home = "/";

    private const string Setting = "/pages/Setting";

    private const string Shopping = "/pages/shopping";

    public GiteeReleaseDto GiteeReleaseDto { get; set; }
    private bool updateVersion;

    protected override async Task OnInitializedAsync()
    {
        WindowService.UpdateTitle("智能聊天首页");
        await UpdateAsync();
        WindowService.SetWindowSize(new Size(1080, 720));
        var result = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        // 当登录才获取信息
        if (result.User.Claims.Any(x => x.Type == ClaimTypes.Name))
            userDto = await UserService.GetAsync();
    }

    private async Task UpdateAsync()
    {
        try
        {
            GiteeReleaseDto = await AppService.GetApp();

            if (GiteeReleaseDto.tag_name != typeof(About).Assembly.GetName().Version.ToString())
            {
                updateVersion = true;
                await InvokeAsync(StateHasChanged);
            }
        }
        catch (Exception e)
        {
            // TODO: Gitee的Api可能存在限流不一定每次都获取成功
        }
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