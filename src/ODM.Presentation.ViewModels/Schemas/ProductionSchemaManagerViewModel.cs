using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Hdc.Collections.ObjectModel;
using Hdc.ComponentModel;
using Hdc.Mvvm;
using Hdc.Mvvm.Dialogs;
using Hdc.Patterns;
using Hdc.Mercury;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;
using ODM.Domain.Schemas;
using Omu.ValueInjecter;

namespace ODM.Presentation.ViewModels.Schemas
{
    // ReSharper disable InconsistentNaming
    public partial class ProductionSchemaManagerViewModel : ViewModel, IViewModel
    {
        private readonly BindableCollection<IProductionSchemaViewModel> _productionSchemas =
            new BindableCollection<IProductionSchemaViewModel>();

        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        [Dependency]
        public IAskDialogService AskDialogService { get; set; }

        [Dependency]
        public IMessageDialogService MessageDialogService { get; set; }

        [Dependency]
        public IMachineViewModelProvider MachineViewModelProvider { get; set; }

        [Dependency]
        public ICalculateDialogService CalculateDialogService { get; set; }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Dependency]
        public IStringInputDialogService StringInputDialogService { get; set; }

        // ReSharper disable ConvertToLambdaExpression
        [InjectionMethod]
        public void Init()
        {
            Init_Commands();

            Init_Schema();

            UpdateSchema(Schema);

            SelectedSchema = _productionSchemas.FirstOrDefault();

            UpdateCommandStates();
        }

        private void UpdateSchema(IProductionSchemaViewModel schema)
        {
            if (schema == null)
            {
                //TODO
                return;
            }

            Schema.Id = schema.Id;
            Schema.CopyValuesFrom(schema);

            foreach (var pe in Schema.ParameterEntries)
            {
                var pm = MachineConfigProvider.MachineConfig.GetParameterMetadata(pe.Name);
                pe.Description = pm.Description;
                pe.GroupName = pm.GroupName;
                pe.GroupDescription = pm.GroupDescription;
                pe.CatalogName = pm.CatalogName;
                pe.CatalogDescription = pm.CatalogDescription;
            }

            //
            var parameterEntryViewModels = Schema.ParameterEntries.Where(x => x.CatalogName == "Common").ToList();
            CommonParameterEntriesCollectionView = parameterEntryViewModels.GetDefaultCollectionView();
            CommonParameterEntriesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("GroupDescription"));

            //
            var entryViewModels = Schema.ParameterEntries.Where(x => x.CatalogName == "MV").ToList();
            MVParameterEntriesCollectionView = entryViewModels.GetDefaultCollectionView();
            MVParameterEntriesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("GroupDescription"));

            //
            var viewModels = Schema.ParameterEntries.Where(x => x.CatalogName == "ME").ToList();
            PLCParameterEntriesCollectionView = viewModels.GetDefaultCollectionView();
            PLCParameterEntriesCollectionView.GroupDescriptions.Add(new PropertyGroupDescription("GroupDescription"));
        }

        private void Init_Schema()
        {
            var ps = ProductionSchemaFactory.CreateDefaultProductionSchema();
            Schema = ps.ToViewModel();

            foreach (var entry in Schema.ParameterEntries)
            {
                var dvc = Machine.Context.Context.GetDevice<int>(entry.Name);
                if (dvc != null)
                    entry.InitMonitor(dvc);
            }
        }

        // ReSharper restore ConvertToLambdaExpression

        public BindableCollection<IProductionSchemaViewModel> ProductionSchemas
        {
            get { return _productionSchemas; }
        }

        private IProductionSchemaViewModel _selectedSchema;

        public IProductionSchemaViewModel SelectedSchema
        {
            get { return _selectedSchema; }
            set
            {
                if (Equals(_selectedSchema, value)) return;
                _selectedSchema = value;
                RaisePropertyChanged(() => SelectedSchema);

                UpdateCommandStates();
                UpdateSchema(value);
            }
        }

        /// <summary>
        /// Current Display Schema
        /// </summary>
        public IProductionSchemaViewModel Schema { get; set; }

        public IMachineViewModel Machine
        {
            get { return MachineViewModelProvider.Machine; }
        }

        private ICollectionView _mVParameterEntriesCollectionView;

        public ICollectionView MVParameterEntriesCollectionView
        {
            get { return _mVParameterEntriesCollectionView; }
            set
            {
                if (Equals(_mVParameterEntriesCollectionView, value)) return;
                _mVParameterEntriesCollectionView = value;
                RaisePropertyChanged(() => MVParameterEntriesCollectionView);
            }
        }

        private ICollectionView _pLCParameterEntriesCollectionView;

        public ICollectionView PLCParameterEntriesCollectionView
        {
            get { return _pLCParameterEntriesCollectionView; }
            set
            {
                if (Equals(_pLCParameterEntriesCollectionView, value)) return;
                _pLCParameterEntriesCollectionView = value;
                RaisePropertyChanged(() => PLCParameterEntriesCollectionView);
            }
        }

        private ICollectionView _commonParameterEntriesCollectionView;

        public ICollectionView CommonParameterEntriesCollectionView
        {
            get { return _commonParameterEntriesCollectionView; }
            set
            {
                if (Equals(_commonParameterEntriesCollectionView, value)) return;
                _commonParameterEntriesCollectionView = value;
                RaisePropertyChanged(() => CommonParameterEntriesCollectionView);
            }
        }
    }

    // ReSharper restore InconsistentNaming
}