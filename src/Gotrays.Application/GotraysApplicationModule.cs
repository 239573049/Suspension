using CoreFlex.Module;
using Gotrays.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gotrays.Application;

public class GotraysApplicationModule : CoreFlexModule
{
    public override void ConfigureServices(CoreFlexServiceContext context)
    {
        
        context.Services.AddSingleton<IFreeSql>(_ => new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, "Data Source=Gotrays.db;")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库
            .Build());

        context.Services.AddHttpClient(Constant.HttpClientOptions.OpenService, (services, options) =>
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

        context.Services.AddScoped<IStorageService, StorageService>();
        context.Services.AddScoped<IUserService, UserService>();
        context.Services.AddScoped<IChannelService, ChannelService>();
        context.Services.AddScoped<IChatMessageService, ChatMessageService>();
        context.Services.AddScoped<IAppService, AppService>();
        context.Services.AddScoped<IChatService, ChatService>();
        context.Services.AddScoped<IChatProductService, ChatProductService>();
        context.Services.AddScoped<IPayService, PayService>();
    }
}