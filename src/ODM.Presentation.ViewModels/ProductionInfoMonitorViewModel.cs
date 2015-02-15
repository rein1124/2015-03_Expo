using System;
using Hdc.Mercury;
using Hdc.Mvvm;
using Hdc.Mvvm.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using ODM.Domain;

namespace ODM.Presentation.ViewModels
{
    public class ProductionInfoMonitorViewModel : ViewModel, IViewModel
    {
        [Dependency]
        public IMachineViewModelProvider MachineViewModelProvider { get; set; }

        [Dependency]
        public IMachineProvider MachineProvider { get; set; }

        [Dependency]
        public IAskDialogService AskDialogService { get; set; }

        // ReSharper disable ConvertToLambdaExpression
        [InjectionMethod]
        public void Init()
        {
            ResetJobCounterCommand = new DelegateCommand(
                () =>
                    {
                        AskDialogService
                            .Show("是否确定复位班产？")
                            .Subscribe(
                                args =>
                                    {
                                        if (args.IsCanceled)
                                            return;

                                        if (!args.Data)
                                            return;

                                        MachineProvider.Machine.Production_ResetJobCounterCommandDevice.WriteTrue();
                                    });
                    });
        }
        // ReSharper restore ConvertToLambdaExpression

        public IMachineViewModel Machine
        {
            get { return MachineViewModelProvider.Machine; }
        }

        public DelegateCommand ResetJobCounterCommand { get; set; }
    }
}