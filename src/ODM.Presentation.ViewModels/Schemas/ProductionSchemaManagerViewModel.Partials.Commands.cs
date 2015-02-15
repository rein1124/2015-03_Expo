using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Hdc.IO;
using Hdc.Reflection;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using Microsoft.Win32;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using ODM.Domain.Schemas;
using Hdc.Mercury;
using Hdc;

namespace ODM.Presentation.ViewModels.Schemas
{
    // ReSharper disable InconsistentNaming
    public partial class ProductionSchemaManagerViewModel
    {
//        [Dependency]
//        public IInspectorProvider InspectorProvider { get; set; }

        // ReSharper disable ConvertToLambdaExpression
        public void Init_Commands()
        {
            ChangeParameterValueInPLCCommand = new DelegateCommand<object>(
                x =>
                {
                    var parameterName = (string)x;

                    var pm = MachineConfigProvider.MachineConfig.GetParameterMetadata(parameterName);
                    var dvc = Machine.Context.Context.GetDevice<int>(parameterName);

                    CalculateDialogService
                        .Show(pm.Minimum, pm.Maximum, 1, dvc.Value, pm.Description)
                        .Subscribe(args =>
                                   {
                                       if (args.IsCanceled)
                                           return;

                                       var value = (int) args.Data;
                                       dvc.Write(value);
                                   });
                });

        }

        // ReSharper restore ConvertToLambdaExpression

        public DelegateCommand<object> ChangeParameterValueInPLCCommand { get; set; }

        private void UpdateCommandStates()
        {
            ChangeParameterValueInPLCCommand.RaiseCanExecuteChanged();
        }
    }

    // ReSharper restore InconsistentNaming
}