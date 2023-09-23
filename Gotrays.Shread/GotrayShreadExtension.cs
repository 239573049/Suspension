using FreeSql;
using Gotrays.Contract.Services;
using Gotrays.Shread.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gotrays.Shread;

public static class GotrayShreadExtension
{
    public static IServiceCollection AddShread(this IServiceCollection services)
    {
        // TODO: 注意在使用js互操作的时候使用作用域生命周期会遇到IJSRuntime被释放。
        services.AddTransient<MainInterop>();
        services.AddTransient<DocumentInterop>();

        
        services.AddTransient((_) =>
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GotraysAI");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return new FreeSqlBuilder()
                .UseConnectionString(DataType.Sqlite, @"Data Source=" + Path.Combine(path, "gotrays.db"))
                .UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}")) //监听SQL语句
                .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                .Build();
        });

        services.AddSingleton<IStorageService, StorageService>();

        services.AddHttpClient()
            .AddHttpClient(string.Empty, (options) =>
            {
                options.DefaultRequestHeaders.Add("X-Token", "token");
            });

        services.AddHttpClient(Constant.HttpServiceName, (options) =>
        {
            options.BaseAddress = new Uri("https://open666.cn/api/v1/");
        });

        services.AddSingleton<UserService>();
        services.AddSingleton<ChatService>();
        services.AddSingleton<ChatProductsService>();
        services.AddSingleton<PaysService>();
        
        return services;
    }
}