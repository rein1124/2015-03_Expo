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
using ODM.Domain.Inspection;

namespace ODM.Presentation.Views
{
    /// <summary>
    /// Interaction logic for InspectStateIndicator.xaml
    /// </summary>
    public partial class InspectStateIndicator : UserControl
    {
        public InspectStateIndicator()
        {
            InitializeComponent();
        }

        #region InspectState

        public InspectState InspectState
        {
            get { return (InspectState) GetValue(InspectStateProperty); }
            set { SetValue(InspectStateProperty, value); }
        }

        public static readonly DependencyProperty InspectStateProperty = DependencyProperty.Register(
            "InspectState", typeof (InspectState), typeof (InspectStateIndicator));

        #endregion
    }
}
