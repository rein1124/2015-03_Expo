using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;
using Hdc.Localization;
using Hdc.Localization.Xml;
using Hdc.Mercury;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Communication.OPC.Xi;
using Hdc.Mvvm;
using Hdc.Patterns;
using Hdc.Reactive.Linq;
using Microsoft.Practices.Prism.PubSubEvents;
using ODM.Domain;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using ODM.Domain.Schemas;
using ODM.Infrastructure;
using ODM.Presentation.ViewModels;
using IEventAggregator = Microsoft.Practices.Prism.PubSubEvents.IEventAggregator;

namespace ODM.Boot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Unity;

    public class BootModule : IModule
    {
        private const string ConnectionStringForLocalDb = @"Server=(localdb)\LocalDbInstanceName;
Integrated Security=true;
AttachDbFileName=|DataDirectory|Vins.Phone.LocalDb.mdf;
initial catalog=Vins.Phone.DatasContext";

        [Dependency]
        public IUnityContainer Container { get; set; }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        [Dependency]
        public IAccessChannelController AccessChannelController { get; set; }

        [Dependency]
        public IEventBus EventBus { get; set; }

        [Dependency]
        public IXiServerConfigProvider XiServerConfigProvider { get; set; }

        public void Initialize()
        {
            XiServerConfigProvider.XiServerConfig.ServerUrl =
                MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_ServerUrl;
            XiServerConfigProvider.XiServerConfig.UserName =
                MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_UserName;
            XiServerConfigProvider.XiServerConfig.Password =
                MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_Password;

            var localDbInstanceName = MachineConfigProvider.MachineConfig.General_LocalDbInstanceName;

            string connectionStringForLocalDb = ConnectionStringForLocalDb.Replace("LocalDbInstanceName", localDbInstanceName);

            var dc = new DatasContext(connectionStringForLocalDb);
            Container.RegisterInstance<IDatasContext>(dc, new ContainerControlledLifetimeManager());


            TestDb();


            //return;
            string channelName = MachineConfigProvider.MachineConfig.PLC_SimulationAccessChannelEnabled
                ? AccessChannelNames.SimAccessChannelFactory
                : AccessChannelNames.AccessChannelFactory;

            EventAggregator
                .GetEvent<SplashFinishedEvent>()
                .Subscribe(async e =>
                           {
                               Debug.WriteLine(DateTime.Now + ": AccessChannelController.Start begin.");
                               await AccessChannelController.StartAsync(channelName);

                               EventAggregator
                                   .GetEvent<CommunicationInitializedEvent>()
                                   .Publish(new CommunicationInitializedEvent());

                               IMachine machine = Container.Resolve<IMachineProvider>().Machine;
                               int speed = 25000;
                               machine.TestME_Slider_JogSpeedDevice.Write(speed);
                               machine.TestME_Slider_ScanSpeedDevice.Write(speed);
                               machine.TestME_Slider_StartSpeedDevice.Write(speed);
                               Debug.WriteLine(DateTime.Now + ": Speeds have been set. ");


                               Observable
                                   .Interval(TimeSpan.FromSeconds(5))
                                   .ObserveOnTaskPool()
                                   .Subscribe(x =>
                                   {
                                       var mp = Container.Resolve<IMachineProvider>();
                                       mp.Machine.General_PlcStartedPlcEventDevice.Subscribe(
                                           p =>
                                           {
                                               mp.Machine.General_PlcStartedPlcEventDevice.WriteTrue();
//                                               mp.Machine.TestME_Slider_JogSpeedDevice.Write(speed);
                                               mp.Machine.TestME_Slider_ScanSpeedDevice.Write(speed);
//                                               mp.Machine.TestME_Slider_StartSpeedDevice.Write(speed);
//                                               Debug.WriteLine(DateTime.Now + ": Speeds have been set. ");
                                           });

                                       if (!_isConnected)
                                       {
                                           mp.Machine.General_AppStartedPcEventDevice.WriteTrue();
                                           _isConnected = true;
                                       }

                                       if (_isStoped)
                                           return;

                                       mp.Machine.General_UpdateWatchdogCommandDevice.WriteTrue();
//                                       mp.Machine.TestME_Slider_JogSpeedDevice.Write(speed);
                                       mp.Machine.TestME_Slider_ScanSpeedDevice.Write(speed);
//                                       mp.Machine.TestME_Slider_StartSpeedDevice.Write(speed);
                                       //Debug.WriteLine(DateTime.Now + ": Speeds have been set. ");
                                   });

                               Debug.WriteLine(DateTime.Now + ": AccessChannelController.Start end.");
                           }, ThreadOption.BackgroundThread, true);

            var mvmp = Container.Resolve<IMachineViewModelProvider>();
            //            var inspectorProvider = Container.Resolve<IInspectorControllerProvider>();
            //            var productionService = Container.Resolve<IInspectionDomainService>();


            AccessChannelController
                .Config(cfg =>
                        {
                            cfg.ServerUrl = MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_ServerUrl;
                            cfg.UserName = MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_UserName;
                            cfg.Password = MachineConfigProvider.MachineConfig.PLC_OpcXiServerConfig_Password;
                        });

            var xmlLocalizationService = new XmlLocalizationService();
            //            string lcidString = MachineConfigProvider.MachineConfig.Language;
            string lcidString = "zh-CN";
            xmlLocalizationService.Update(lcidString);
            LocalizationServiceLocator.Service = xmlLocalizationService;

            Container.RegisterInstance<ILocalizationService>(
                xmlLocalizationService,
                new ContainerControlledLifetimeManager());

            Application.Current.Exit += (sender, args) =>
                                        {
                                            _isStoped = true;
                                            AccessChannelController.Stop();
                                        };


            // Start watchdog.
            // Interval = 10s, PLC Timeout = 60s

            _isConnected = false;

        }

        private bool _isStoped;
        private bool _isConnected;

        private void TestDb()
        {
            bool firstTimeException = false;
            while (true)
            {
                try
                {
                    var alldatas = Container.Resolve<IWorkpieceInfoRepository>().GetAll();

                    break;
                }
                catch (Exception e)
                {
                    if (!firstTimeException)
                    {
                        firstTimeException = true;
                        DetachMdf(@"(localdb)\" + MachineConfigProvider.MachineConfig.General_LocalDbInstanceName, "Vins.Phone.DatasContext");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private static void DetachMdf(string dbServer, string dbName)
        {
            SqlConnection.ClearAllPools();
            using (
                SqlConnection conn =
                    new SqlConnection(string.Format("Server={0};Database=master;Integrated Security=SSPI", dbServer)))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_detach_db", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dbname", dbName);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}