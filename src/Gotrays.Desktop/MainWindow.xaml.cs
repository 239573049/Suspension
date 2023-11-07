using Gotrays.Rcl;
using Gotrays.Rcl.Extensions;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Gotrays.Contract.Services;
using Gotrays.Desktop.Services;

namespace Gotrays.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static MainWindow Windwo;

    public MainWindow()
    {
        InitializeComponent();

        Windwo = this;

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddMasaBlazor();
        serviceCollection.AddBlazorWebViewDeveloperTools();
        serviceCollection.AddGotraysRcl();
        serviceCollection.AddGotraysApplication();

        serviceCollection.AddScoped<IWindowService, WindowService>();

        Resources.Add("services", serviceCollection.BuildServiceProvider());

        GotraysWebView.RootComponents.Add(new RootComponent()
        {
            ComponentType = typeof(Main),
            Selector = "#app"
        });

    }
}