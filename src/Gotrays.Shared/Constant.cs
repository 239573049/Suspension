namespace Gotrays.Shared;

public static class Constant
{
    private const string Default = "Gotrays:";

    public class HttpClientOptions
    {
        private const string Default = Constant.Default + "HttpClientOptions:";

        public const string OpenService = Default + "OpenService";

        public const string Token = Default + "Token";
    }

    public class LoadEventBus
    {
        private const string Default = Constant.Default + "LoadEventBus:";

        public const string Notifications = Default + "Notifications";
    }
}