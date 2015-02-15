using System;
using Hdc.Mercury;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using ODM.Domain.Configs;

namespace ODM.Presentation.ViewModels
{
    public class CommandExecutorVM : CommandExecutor
    {
        public CommandExecutorVM()
        {
            Init();
        }

        public CommandExecutorVM(IDevice<bool> commandDevice, Func<bool> filter = null, Action alternativeAction = null)
            : base(commandDevice, filter, alternativeAction)
        {
            Init();
        }

        private void Init()
        {
            CommitCommand = new DelegateCommand(() =>
                {
                    if (ServiceLocator.Current.GetInstance<IMachineConfigProvider>()
                                      .MachineConfig.PLC_SimulationAccessChannelEnabled)
                        return;
                    Commit();
                });
        }

        public DelegateCommand CommitCommand { get; set; }
    }
}