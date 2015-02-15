using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Controls.Charts
{
    public class RelativeYLine : System.Windows.Controls.Control
    {
        static RelativeYLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RelativeYLine),
                                                           new System.Windows.FrameworkPropertyMetadata(
                                                               typeof (RelativeYLine)));
        }
    }
}
