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
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using Shared;

namespace ODM.Layout.Screens
{
    /// <summary>
    /// Interaction logic for MonitorScreen.xaml
    /// </summary>
    public partial class MonitorScreen : UserControl
    {

        [Dependency]
        public IRegionManager RegionManager { get; set; }

        public MonitorScreen()
        {
            InitializeComponent();

            _InspectionManager.Content = null;
            _ProductionInfoMonitor.Content = null;
            _InfoBar.Content = null;
        }
    }
}
