using Gotrays.Application.Services;
using Gotrays.Contract.Services;
using Gotrays.Shared;

namespace Microsoft.Extensions.DependencyInjection;

public static class GotrayApplicationExtension
{
    public static void AddGotraysApplication(this IServiceCollection services)
    {
        services.AddSingleton<IFreeSql>(_ => new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=Gotrays.db;")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库
            .Build());

        services.AddHttpClient(Constant.HttpClientOptions.OpenService, (services, options) =>
        {
            options.BaseAddress = new Uri("https://open666.cn/api/v1/");
            var storage = services.GetService<IStorageService>();
            var token = storage.Get(Constant.HttpClientOptions.Token);

            if (!token.IsNullOrWhiteSpace())
            {
                options.DefaultRequestHeaders.Remove("Authorization");
                options.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        });

        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChannelService, ChannelService>();
        services.AddScoped<IChatMessageService, ChatMessageService>();
        services.AddScoped<IAppService, AppService>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChatProductService, ChatProductService>();
        services.AddScoped<IPayService, PayService>();
    }
    
}