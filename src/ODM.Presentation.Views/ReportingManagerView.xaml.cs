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
using Hdc.Mvvm;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;
using Shared;

namespace ODM.Presentation.Views.Inspection
{
    /// <summary>
    /// Interaction logic for ReportingManagerView.xaml
    /// </summary>
    public partial class ReportingManagerView : UserControl
    {
        public ReportingManagerView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Dependency(ViewModelNames.Inspection_ReportingManager)]
        public IViewModel ViewModel
        {
            set { DataContext = value; }
        }

        [InjectionMethod]
        public void Init()
        {
            SurfaceMonitorsGrid.RowDefinitions.Add(new RowDefinition());
            SurfaceMonitorsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0) });
        }
    }
}
