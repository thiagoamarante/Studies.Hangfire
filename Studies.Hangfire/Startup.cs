using Hangfire;
using Hangfire.Redis;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using StackExchange.Redis;
using System;

[assembly: OwinStartup(typeof(Studies.Hangfire.Startup))]

namespace Studies.Hangfire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConnectionMultiplexer connectionRedis = ConnectionMultiplexer.Connect(connectionString);
            //var cache = connectionRedis.GetDatabase();
            //cache.StringSet("key1", "value");
            //var value = cache.StringGet("key1");

            Config config = JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "bin\\config.personal.json"));
            //GlobalConfiguration.Configuration.UseSqlServerStorage(config.ConnectionString);

            RedisStorage redis = new RedisStorage(config.ConnectionStringRedis, new RedisStorageOptions()
            {
                Db = 0,
                Prefix = "hangfire:"
            });

            GlobalConfiguration.Configuration.UseStorage<RedisStorage>(redis);
            var connection = redis.GetConnection();
            var t = connection.GetJobData("teste");
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}