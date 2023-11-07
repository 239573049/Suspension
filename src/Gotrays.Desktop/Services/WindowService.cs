using System.Windows;
using Gotrays.Contract.Services;
using Size = System.Drawing.Size;

namespace Gotrays.Desktop.Services;

public class WindowService : IWindowService
{
    public void SetWindowSize(Size size)
    {
        MainWindow.Windwo.Width = size.Width;
        MainWindow.Windwo.Height = size.Height;
    }

    public void SetWindowState(int state)
    {
        MainWindow.Windwo.WindowState = (WindowState)state;
    }

    public void SetWindowStartupLocation(int location)
    {
        MainWindow.Windwo.WindowStartupLocation = (WindowStartupLocation)location;
    }

    public void UpdateTitle(string title)
    {
        MainWindow.Windwo.Title = title;
    }
}