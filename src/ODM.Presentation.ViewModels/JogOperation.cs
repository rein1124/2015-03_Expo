using Hdc.Mercury;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using ODM.Domain.Configs;

namespace ODM.Presentation.ViewModels
{
    public class JogOperation
    {
        private readonly IDevice<bool> _device;

        // ReSharper disable ConvertToLambdaExpression
        public JogOperation(IDevice<bool> device)
        {
            _device = device;

            BeginCommand = new DelegateCommand(
                () =>
                    {
                        if (ServiceLocator.Current.GetInstance<IMachineConfigProvider>()
                                          .MachineConfig.PLC_SimulationAccessChannelEnabled)
                            return;
                        _device.WriteTrue();
                    });

            EndCommand = new DelegateCommand(
                () =>
                    {
                        if (ServiceLocator.Current.GetInstance<IMachineConfigProvider>()
                                          .MachineConfig.PLC_SimulationAccessChannelEnabled)
                            return;
                        _device.WriteFalse();
                    });
        }

        // ReSharper restore ConvertToLambdaExpression

        public DelegateCommand BeginCommand { get; set; }
        public DelegateCommand EndCommand { get; set; }

        //        public DelegateCommand BeginBackwardCommand { get; set; }
        //        public DelegateCommand EndBackwardCommand { get; set; }
    }
}