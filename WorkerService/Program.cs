using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using WorkerService;
using WorkerService.Data;
using WorkerService.Services.Implements;
using WorkerService.Services.Interfaces;
using WorkerService.Settings;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration config = hostContext.Configuration;
        var redisSetting = config.GetSection("Redis").Get<RedisSetting>();
        services.AddHostedService<Worker>();
        services.AddDbContext<CounterContext>(option =>
        {
            option.UseSqlServer(config.GetConnectionString("DefaultConnection"), providerOptions => providerOptions.CommandTimeout(120));
        });

        services.AddSingleton<IDistributedLockFactory, RedLockFactory>(sp =>
        RedLockFactory.Create(new List<RedLockMultiplexer> {
            ConnectionMultiplexer.Connect(redisSetting.RedisUrl)
        }));
        services.AddTransient<IDatabaseService, DatabaseService>();
        services.AddTransient<IDistributedLockService, DistributedLockService>();
    })
    .Build();

await host.RunAsync();
