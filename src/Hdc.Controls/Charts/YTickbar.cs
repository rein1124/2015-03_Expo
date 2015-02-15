using System.Windows;

namespace Hdc.Controls.Charts
{
    public class YTickbar : System.Windows.Controls.Control
    {
        static YTickbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YTickbar),
                                                           new FrameworkPropertyMetadata(typeof (YTickbar)));
        }
    }
}
