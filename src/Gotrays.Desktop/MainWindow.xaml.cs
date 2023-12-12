using System.ComponentModel;
using System.Windows;
using CoreFlex.Module.Extensions;
using Gotrays.Contract.Services;
using Gotrays.Desktop.Services;
using Gotrays.Rcl;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;

namespace Gotrays.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static MainWindow Windwo;

    private IServiceCollection _service;

    public MainWindow()
    {
        InitializeComponent();

        Windwo = this;

        _service = new ServiceCollection();

        _service.AddCoreFlexAutoInjectAsync<GotraysDesktopModule>().ConfigureAwait(false).GetAwaiter()
            .GetResult();
        
        _service.AddWpfBlazorWebView();

#if DEBUG
        _service.AddBlazorWebViewDeveloperTools();
#endif
        
        _service.AddScoped<IWindowService, WindowService>();

        var provider = _service.BuildServiceProvider();
        
        provider.UseCoreFlexAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        Resources.Add("services", provider);

        GotraysWebView.RootComponents.Add(new RootComponent()
        {
            ComponentType = typeof(Main),
            Selector = "#app"
        });

        Closing += MainWindow_Closing;

    }


    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}