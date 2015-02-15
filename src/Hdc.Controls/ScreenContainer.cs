using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class ScreenContainer : TabControl
    {
        static ScreenContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenContainer),
                                                     new FrameworkPropertyMetadata(typeof(ScreenContainer)));
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            this.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(ScreenContainer_PreviewKeyDown);
        }

        void ScreenContainer_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.End || e.Key == Key.Home)
                e.Handled = true;
        }
    }
}