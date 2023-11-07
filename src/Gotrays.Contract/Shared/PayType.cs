namespace GotraysService.Contracts.Shared;

public enum PayType
{
    /// <summary>
    /// 支付宝
    /// </summary>
    alipay,

    /// <summary>
    /// 微信
    /// </summary>
    wxpay,

    /// <summary>
    /// QQ钱包
    /// </summary>
    qqpay,

    /// <summary>
    /// 网银支付
    /// </summary>
    bank,

    /// <summary>
    /// 京东支付
    /// </summary>
    jdpay,

    /// <summary>
    /// PayPal
    /// </summary>
    paypal
}