using System.Diagnostics;
using Gotrays.Contract.Modules;

namespace Gotrays.Rcl.Components;

public partial class About
{
    private bool Update;

    private AppDto _app;

    private double progressPercentage;

    private async Task CheckUpdate()
    {
        _app = await AppService.GetAsync();

        if (_app.versions != Constant.Versions)
        {
            Update = true;
        }
        else
        {
            await PopupService.EnqueueSnackbarAsync("已经是最新版本！", AlertTypes.Success);
        }
    }

    private async Task OnSave()
    {
        var httpClient = HttpClientFactory.CreateClient(nameof(About));

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GotraysAI");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var response = await httpClient.GetAsync(_app.url, HttpCompletionOption.ResponseHeadersRead);
        if (!response.IsSuccessStatusCode)
        {
            await PopupService.EnqueueSnackbarAsync("下载出现异常", AlertTypes.Error);

            return;
        }


        await PopupService.EnqueueSnackbarAsync("正在更新", AlertTypes.Success);

        var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();

        await using var stream = await response.Content.ReadAsStreamAsync();

        // 保证下载的是可执行程序
        await using var fileStream = new FileStream(Path.Combine(path, "gotrays-update.exe"), FileMode.Create,
            FileAccess.Write, FileShare.None, 8192, true);

        var totalReadBytes = 0L;
        var buffer = new byte[8192];
        var isMoreToRead = true;
        do
        {
            var read = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (read == 0)
            {
                isMoreToRead = false;
            }
            else
            {
                await fileStream.WriteAsync(buffer, 0, read);

                totalReadBytes += read;
                progressPercentage = (double)totalReadBytes / totalBytes * 100;

                _ = InvokeAsync(StateHasChanged);
            }
        } while (isMoreToRead);

        fileStream.Close();

        Process.Start(Path.Combine(path, "gotrays-update.exe"));

        await PopupService.EnqueueSnackbarAsync("更新完成，等待安装！", AlertTypes.Success);

        await Task.Delay(2000);

        // 直接退出程序
        Environment.Exit(0);
    }

    private void OnCancel()
    {
        Update = false;
    }
}