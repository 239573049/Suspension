using Gotrays.Rcl;
using Gotrays.Rcl.Extensions;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Gotrays.Contract.Services;
using Gotrays.Desktop.Services;
using System.ComponentModel;

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
        _service.AddWpfBlazorWebView();
        _service.AddMasaBlazor();
        _service.AddBlazorWebViewDeveloperTools();
        _service.AddGotraysRcl();
        _service.AddGotraysApplication();

        _service.AddScoped<IWindowService, WindowService>();

        Resources.Add("services", _service.BuildServiceProvider().BuilderRcl());

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