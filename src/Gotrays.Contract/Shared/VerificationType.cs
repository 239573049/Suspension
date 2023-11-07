namespace GotraysService.Contracts.Shared;

public enum VerificationType
{
    Login = 0,

    /// <summary>
    /// 注册
    /// </summary>
    Registry = 1,

    /// <summary>
    /// 绑定
    /// </summary>
    Binding = 2,

    /// <summary>
    /// 找回密码
    /// </summary>
    RetrievePassword = 3
}