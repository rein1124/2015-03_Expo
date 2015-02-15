using System;
using System.Threading.Tasks;
using Advosol.Paxi;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiServerProvider : IXiServerProvider
    {
        [Dependency]
        public IXiServerConfigProvider XiServerConfigProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            var xiServerConfig = XiServerConfigProvider.XiServerConfig;

            string serverUrl; // = System.Configuration.ConfigurationManager.AppSettings["XiServerAddress"];

//            address = @"net.pipe://localhost/XiTOCO";
            //address = "net.tcp://localhost/XiTOCO";

            serverUrl = xiServerConfig.ServerUrl;

            if (string.IsNullOrEmpty(serverUrl))
                throw new Exception("The XiServer Address is not configed correctly");

            var appName = Guid.NewGuid().ToString();
            XiServer = new XiServer(serverUrl, appName, this)
                            {
                                ContextTimeout = 5*1000,
                                UserName = xiServerConfig.UserName,
                                Password = xiServerConfig.Password
                            };
            XiServer.StartKeepAlive(3);
        }

        public XiServer XiServer { get; private set; }
    }
}