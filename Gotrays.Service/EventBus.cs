namespace Gotrays.Contract;

public class EventBus
{
    /// <summary>
    /// 登录成功事件
    /// </summary>
    public static Action? LoginSuccessEvent;

    /// <summary>
    /// 退出登录
    /// </summary>
    public static Action? LogOutEvent;
}