using System.Windows;

namespace Hdc.Controls.Charts
{
    public class XTickbar : System.Windows.Controls.Control
    {
        static XTickbar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XTickbar),
                                                           new FrameworkPropertyMetadata(typeof (XTickbar)));
        }
    }
}
