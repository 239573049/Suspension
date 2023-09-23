using Microsoft.Extensions.DependencyInjection;

namespace Gotrays.Contract;

public static class GotrayAppHelper
{
    private static GotrayApp _gotrayApp;

    public static IServiceProvider Build(this GotrayApp app)
    {
        return app.Builder();
    }

    public static T? GetService<T>()
    {
        return _gotrayApp!.ServiceProvider.GetService<T>();
    }

    public static IEnumerable<T> GetServices<T>()
    {
        return _gotrayApp!.ServiceProvider.GetServices<T>();
    }

    public static T GetRequiredService<T>() where T : notnull
    {
        return _gotrayApp!.ServiceProvider.GetRequiredService<T>();
    }

    public static IServiceProvider ServiceProvider => _gotrayApp.ServiceProvider;

    public class GotrayApp
    {
        public static GotrayApp? CreateGotrayAppBuilder()
        {
            _gotrayApp = new GotrayApp();

            return _gotrayApp;
        }
        
        public IServiceCollection Service { get; } = new ServiceCollection();

        private IServiceProvider? _serviceProvider;

        public IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                {
                    throw new ArgumentNullException(nameof(_serviceProvider));
                }

                return _serviceProvider;
            }
        }

        internal IServiceProvider Builder()
        {
            return _serviceProvider = Service.BuildServiceProvider();
        }
    }
}