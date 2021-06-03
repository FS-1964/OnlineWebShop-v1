using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebShop.DAL.Datacontext
{
    public class AppConfiguration
    {

        public string _connectionString { get; set; }
        public AppConfiguration()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSetting = root.GetSection("ConnectionStrings:DefaultConnectionPloto");
            _connectionString = appSetting.Value;
        }
    }
}
