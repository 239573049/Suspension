using System.Drawing;

namespace Gotrays.Contract.Services;

public interface IWindowService
{
    /// <summary>
    /// 更新窗口高宽
    /// </summary>
    /// <param name="size"></param>
    void SetWindowSize(Size size);

    /// <summary>
    /// 更新窗口State
    /// </summary>
    /// <param name="state"></param>
    void SetWindowState(int state);

    /// <summary>
    /// 修改窗口显示位置
    /// </summary>
    /// <param name="location"></param>
    void SetWindowStartupLocation(int location);

    /// <summary>
    /// 修改窗口标题
    /// </summary>
    /// <param name="title"></param>
    void UpdateTitle(string title);
}