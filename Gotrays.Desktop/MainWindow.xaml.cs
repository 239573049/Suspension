using System.ComponentModel;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using Gotrays.Contract;
using Gotrays.Rcl;
using Microsoft.AspNetCore.Components.WebView.Wpf;
using Application = System.Windows.Application;

namespace Gotrays.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private NotifyIcon _notifyIcon = null;

    public MainWindow()
    {
        InitializeComponent();

        BlazorWeb.RootComponents.Add(new RootComponent()
        {
            Selector = "#app",
            ComponentType = typeof(Main)
        });

        Resources.Add("services", GotrayAppHelper.ServiceProvider);


        this.Loaded += MainWindow_Loaded;
        this.Closing += MainWindow_Closing;
    }


    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        _notifyIcon = new NotifyIcon();
        _notifyIcon.Icon = new Icon("logo.ico");
        _notifyIcon.Visible = true;
        _notifyIcon.DoubleClick += (o, args) => ShowMainWindow();

        _notifyIcon.ContextMenuStrip = new ContextMenuStrip();
        _notifyIcon.ContextMenuStrip.Items.Add("显示", null, (o, args) => ShowMainWindow());
        _notifyIcon.ContextMenuStrip.Items.Add("关闭", null, (o, args) => CloseApp());
    }

    private void ShowMainWindow()
    {
        this.Show();
        this.WindowState = WindowState.Normal;
        this.ShowInTaskbar = true;
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e)
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }
    }

    private void CloseApp()
    {
        if (_notifyIcon != null)
        {
            _notifyIcon.Dispose();
            _notifyIcon = null;
        }

        Application.Current.Shutdown();
    }
}