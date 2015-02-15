using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Hdc.Controls
{
    public class HeaderButton : Button
    {
        static HeaderButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderButton), new FrameworkPropertyMetadata(typeof(HeaderButton)));
        }

        public String String
        {
            get { return (String)GetValue(StringProperty); }
            set { SetValue(StringProperty, value); }
        }

        public static readonly DependencyProperty StringProperty = DependencyProperty.Register(
            "String", typeof(String), typeof(HeaderButton));

    }
}
