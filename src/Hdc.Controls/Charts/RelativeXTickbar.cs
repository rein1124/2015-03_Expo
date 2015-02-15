using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Controls.Charts
{
    public class RelativeXTickbar : System.Windows.Controls.Control
    {
        static RelativeXTickbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RelativeXTickbar),
                                                           new System.Windows.FrameworkPropertyMetadata(
                                                               typeof (RelativeXTickbar)));
        }
    }
}
