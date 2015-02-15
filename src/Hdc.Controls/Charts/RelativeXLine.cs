using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Controls.Charts
{
    public class RelativeXLine : System.Windows.Controls.Control
    {
        static RelativeXLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RelativeXLine),
                                                           new System.Windows.FrameworkPropertyMetadata(
                                                               typeof (RelativeXLine)));
        }
    }
}
