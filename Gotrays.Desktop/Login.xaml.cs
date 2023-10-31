using System.ComponentModel;
using System.Windows;
using Gotrays.Contract;
using Gotrays.Rcl.Pages;
using Microsoft.AspNetCore.Components.WebView.Wpf;

namespace Gotrays.Desktop;
/// <summary>
/// Login.xaml 的交互逻辑
/// </summary>
public partial class Login : Window
{
	public Login()
	{
		InitializeComponent();

		BlazorWeb.RootComponents.Add(new RootComponent()
		{
			Selector = "#app",
			ComponentType = typeof(SLogin)
		});

		Resources.Add("services", GotrayAppHelper.ServiceProvider);

		EventBus.LoginSuccessEvent += OnSuccess;
	}

	private void OnSuccess()
	{
		var main = new MainWindow();
		main.Show();
		Close();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		EventBus.LoginSuccessEvent -= OnSuccess;
	}
}
