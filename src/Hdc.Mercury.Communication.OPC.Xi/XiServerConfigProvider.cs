using Hdc.Mercury.Communication.OPC.Xi;
using Microsoft.Practices.Unity;
//using Ppg.Domain.Configs;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public class XiServerConfigProvider : IXiServerConfigProvider
    {
        public XiServerConfig XiServerConfig { get; set; }

        public XiServerConfigProvider()
        {
            XiServerConfig = new XiServerConfig();
        }

/*        [Dependency]
        public IPressConfigProvider PressConfigProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            var opcXiServerConfig = PressConfigProvider.PressConfig.OpcXiServerConfig;

            XiServerConfig = new XiServerConfig
                                 {
                                     ServerUrl = opcXiServerConfig.ServerUrl,
                                     UserName = opcXiServerConfig.UserName,
                                     Password = opcXiServerConfig.Password
                                 };
        }*/
    }
}