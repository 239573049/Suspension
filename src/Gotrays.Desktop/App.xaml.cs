using Hardcodet.Wpf.TaskbarNotification;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Gotrays.Desktop;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    private TaskbarIcon notifyIcon;

    public App()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        // 获取资源文件的流
        using var stream = assembly.GetManifestResourceStream("Gotrays.Desktop.favicon.ico");

        // 创建NotifyIcon实例
        notifyIcon = new TaskbarIcon();
        notifyIcon.Icon = new System.Drawing.Icon(stream); // 设置托盘图标
        notifyIcon.ContextMenu = new ContextMenu();

        // 添加托盘菜单项
        MenuItem openWindow = new MenuItem();
        openWindow.Header = "打开首页";
        openWindow.Click += OnOpenWindow;
        notifyIcon.ContextMenu.Items.Add(openWindow);

        // 添加托盘菜单项
        MenuItem exitItem = new MenuItem();
        exitItem.Header = "退出";
        exitItem.Click += OnExit;
        notifyIcon.ContextMenu.Items.Add(exitItem);

    }

    private void OnOpenWindow(object sender, RoutedEventArgs e)
    {
        this.MainWindow.Show();
    }

    private void OnExit(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }


}

