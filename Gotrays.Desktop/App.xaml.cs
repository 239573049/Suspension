using System.Net.Http;
using System.Windows;
using Gotrays.Contract;
using Gotrays.Contract.Services;
using Gotrays.Shread;
using Gotrays.Shread.Services;
using Microsoft.Extensions.DependencyInjection;

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

    protected override void OnStartup(StartupEventArgs e)
    {
        var storageService = GotrayAppHelper.GetRequiredService<IStorageService>();

        var token = storageService.GetString(Constant.ApiToken);


        if (token == null)
        {
            StartupUri = new Uri("Login.xaml", UriKind.Relative);
        }
        else
        {
            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}