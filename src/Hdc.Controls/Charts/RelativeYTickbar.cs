using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Controls.Charts
{
    public class RelativeYTickbar : System.Windows.Controls.Control
    {
        static RelativeYTickbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RelativeYTickbar),
                                                           new System.Windows.FrameworkPropertyMetadata(
                                                               typeof (RelativeYTickbar)));
        }
    }
}
