using Microsoft.EntityFrameworkCore;
using RedLockNet.SERedis.Configuration;
using RedLockNet.SERedis;
using RedLockNet;
using StackExchange.Redis;
using WorkerService1;
using WorkerService1.Settings;
using WorkerService1.Data;
using WorkerService1.Services.Interfaces;
using WorkerService1.Services.Implements;

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
