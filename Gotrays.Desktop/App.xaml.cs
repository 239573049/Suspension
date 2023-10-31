using System.ComponentModel.Design;
using System.Drawing;
using Gotrays.Contract;
using Gotrays.Contract.Services;
using Gotrays.Shread;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Application = System.Windows.Application;
using System.Windows.Forms;
using Gotrays.Rcl;

namespace Gotrays.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var gotryas = GotrayAppHelper.GotrayApp.CreateGotrayAppBuilder();

        gotryas.Service.AddBlazorWebViewDeveloperTools();
        gotryas.Service.AddMasaBlazor();
        gotryas.Service.AddWpfBlazorWebView();
        gotryas.Service.AddSingleton<IWindowService, WindowService>();
        gotryas.Service.AddShread();

        var app = gotryas.Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var storageService = GotrayAppHelper.GetRequiredService<IStorageService>();

        var token = storageService.GetString(Constant.ApiToken);

        if (token == null)
        {
            StartupUri = new Uri("Login.xaml", UriKind.Relative);
        }
        else
        {
            var userService = GotrayAppHelper.GetService<IUserService>();

            var info = await userService?.GetAsync();
            StartupUri = info == null ? new Uri("Login.xaml", UriKind.Relative) : new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}