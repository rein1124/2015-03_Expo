using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Controls.Charts
{
    public class ChartValue : System.Windows.Controls.Control
    {
        static ChartValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ChartValue),
                                                     new System.Windows.FrameworkPropertyMetadata(typeof (ChartValue)));
        }
    }
}
