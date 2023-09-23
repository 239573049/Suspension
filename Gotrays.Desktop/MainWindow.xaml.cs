using System.Windows;
using Gotrays.Contract;
using Gotrays.Shread;
using Microsoft.AspNetCore.Components.WebView.Wpf;

namespace Gotrays.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        BlazorWeb.RootComponents.Add(new RootComponent()
        {
            Selector = "#app",
            ComponentType = typeof(Main)
        });

        Resources.Add("services", GotrayAppHelper.ServiceProvider);
    }
}