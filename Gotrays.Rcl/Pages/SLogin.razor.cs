using Gotrays.Contract.Modules;
using Microsoft.AspNetCore.Components;

namespace Gotrays.Rcl.Pages;

public partial class SLogin
{
    private LoginInput Input = new();

    private MForm _form;

    private static string Id = Guid.NewGuid().ToString("N");

    private bool _show;

    [Inject] public NavigationManager Navigation { get; set; } = default!;

    private async Task OnClick()
    {
        if (_form.Validate())
        {
            try
            {
                await UserService.Login(Input);

                await PopupService.EnqueueSnackbarAsync("登录成功，请稍后", AlertTypes.Success);

                var user = await UserService.GetAsync();
                StorageService.Add(Constant.UserInfo, user);

                EventBus.LoginSuccessEvent?.Invoke();
            }
            catch (Exception e)
            {
                await PopupService.EnqueueSnackbarAsync(e.Message, AlertTypes.Error);
            }
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await Task.Delay(200);
            await MainInterop.Init(Id);
        }
    }
}