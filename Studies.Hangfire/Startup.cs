using Hangfire;
using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;
using System;

[assembly: OwinStartup(typeof(Studies.Hangfire.Startup))]

namespace Studies.Hangfire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Config config = JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "bin\\config.personal.json"));
            GlobalConfiguration.Configuration.UseSqlServerStorage(config.ConnectionString);
            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}