using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;

namespace Aark.Netatmo.SDK.Test
{
    [TestClass]
    public class WeatherStationTest
    {
        private readonly NetatmoManager netatmoManager;

        public WeatherStationTest()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var clientId = configuration.GetConnectionString("ClientId");
            var clientSecret = configuration.GetConnectionString("ClientSecret");
            var netatmoAccount = configuration.GetConnectionString("NetatmoAccount");
            var netatmoPassword = configuration.GetConnectionString("NetatmoPassword");
            netatmoManager = new NetatmoManager(clientId, clientSecret, netatmoAccount, netatmoPassword);
        }

        [TestMethod]
        public async Task TestLoadWeatherDataAsync()
        {
           var result = await netatmoManager.LoadWeatherDataAsync();
           Assert.IsTrue(result, netatmoManager.GetLastError());
        }
    }
}
