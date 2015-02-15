using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    public class CornerRadiusButton:Button
    {

        static CornerRadiusButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadiusButton), new FrameworkPropertyMetadata(typeof(CornerRadiusButton)));
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(
            "CornerRadius", typeof(CornerRadius), typeof(CornerRadiusButton));




    }
}