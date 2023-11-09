using System.Buffers;
using System.Diagnostics;
using BlazorComponent;
using Gotrays.Contract.Dtos.Systems;
using Masa.Blazor;
using Microsoft.AspNetCore.Components;

namespace Gotrays.Rcl.Components;

public partial class UpdateVersion
{
    [Parameter] public bool Value { get; set; }

    [Parameter] public EventCallback<bool> ValueChanged { get; set; }

    /// <summary>
    /// 新版本信息
    /// </summary>
    [Parameter]
    public GiteeReleaseDto GiteeReleaseDto { get; set; }

    public double skill { get; set; }

    /// <summary>
    /// 是否更新
    /// </summary>
    public bool Update { get; set; }

    private async Task UpdateAsync()
    {
        Update = true;
        var client = HttpClientFactory.CreateClient(nameof(UpdateVersion));

        var download = GiteeReleaseDto.assets.FirstOrDefault();
        var response = await client.GetAsync(download.browser_download_url,
            HttpCompletionOption.ResponseHeadersRead);

        var path = Path.Combine(AppContext.BaseDirectory, "Update_" + download.name);

        try
        {
            await PopupService.EnqueueSnackbarAsync("正在更新", AlertTypes.Success);

            var totalBytes = response.Content.Headers.ContentLength.GetValueOrDefault();

            await using var stream = await response.Content.ReadAsStreamAsync();

            // 保证下载的是可执行程序
            await using var fileStream = new FileStream(path, FileMode.Create,
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
                    skill = (int)((double)totalReadBytes / totalBytes * 100);

                    _ = InvokeAsync(StateHasChanged);
                }
            } while (isMoreToRead);

            fileStream.Close();

            // 创建一个新的进程对象
            Process process = new Process();

            try
            {
                // 设置进程的启动信息
                process.StartInfo.FileName = path;

                // 启动进程
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("启动进程时出错：" + ex.Message);
            }
            finally
            {
                // 释放进程资源
                process.Dispose();
            }

            await Task.Delay(3000);
            Environment.Exit(0);
        }
        finally
        {
        }
    }
}