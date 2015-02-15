using System.Windows;

namespace Hdc.Controls.Charts
{
    public class XLine : System.Windows.Controls.Control
    {
        static XLine()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(XLine),
                                                           new FrameworkPropertyMetadata(typeof (XLine)));
        }
    }
}