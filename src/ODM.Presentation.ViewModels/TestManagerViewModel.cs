using System.Collections.ObjectModel;
using System.Windows.Input;
using Hdc.Collections.ObjectModel;
using Hdc.Mercury;
using Hdc.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using ODM.Domain;

namespace ODM.Presentation.ViewModels
{
    public class TestManagerViewModel : ViewModel, IViewModel
    {
        [Dependency]
        public IMachineProvider MachineProvider { get; set; }

        [Dependency]
        public IMachineViewModelProvider MachineViewModelProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            AlarmEntries = new BindableCollection<AlarmEntryViewModel>();
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_AirPressureErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_BackwardClampingCylinderBackwardErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_BackwardClampingCylinderDownErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_BackwardClampingCylinderForwardErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_BackwardClampingCylinderUpErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_ForwardClampingCylinderBackwardErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_ForwardClampingCylinderDownErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_ForwardClampingCylinderForwardErrorDevice));
            AlarmEntries.Add( new AlarmEntryViewModel(Machine.Context.Alarms_ForwardClampingCylinderUpErrorDevice));

            ResetAlarmsCommand = new DelegateCommand(
                () => Machine.Context.Alarms_ResetAlarmsCommandDevice.WriteTrue());
        }

        public IMachineViewModel Machine
        {
            get { return MachineViewModelProvider.Machine; }
        }

        public ObservableCollection<AlarmEntryViewModel> AlarmEntries { get; set; }

        public DelegateCommand ResetAlarmsCommand { get; set; }  
    }
}