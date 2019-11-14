using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;

namespace Aark.Netatmo.SDK.Test
{
    [TestClass]
    public class SecurityStationTest
    {
        private readonly NetatmoManager netatmoManager;

        public SecurityStationTest()
        {
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            var clientId = configuration.GetConnectionString("ClientId");
            var clientSecret = configuration.GetConnectionString("ClientSecret");
            var netatmoAccount = configuration.GetConnectionString("NetatmoAccount");
            var netatmoPassword = configuration.GetConnectionString("NetatmoPassword");
            netatmoManager = new NetatmoManager(clientId, clientSecret, netatmoAccount, netatmoPassword);
        }

        [TestMethod]
        public async Task TestLoadSecurityDataAsync()
        {
            var result = await netatmoManager.LoadSecurityDataAsync();
            Assert.IsTrue(result, netatmoManager.GetLastError());
        }
    }
}