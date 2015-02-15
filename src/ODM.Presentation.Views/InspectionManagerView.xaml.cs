using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Hdc.Controls;
using Hdc.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;
using ODM.Domain.Inspection;
using Shared;

namespace ODM.Presentation.Views.Inspection
{
    /// <summary>
    /// Interaction logic for InspectionManagerView.xaml
    /// </summary>
    public partial class InspectionManagerView : UserControl
    {
        [Dependency(ViewModelNames.Inspection_InspectionManager)]
        public IViewModel ViewModel
        {
            set { DataContext = value; }
        }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        public InspectionManagerView()
        {
            InitializeComponent();
        }

        [InjectionMethod]
        public void Init()
        {
            SurfaceMonitorsGrid.RowDefinitions.Add(new RowDefinition());
            SurfaceMonitorsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0) });
        }
    }
}