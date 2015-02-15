using System.Windows;

namespace Hdc.Controls
{
    public class ContentSwitchButton : SwitchButton
    {
        static ContentSwitchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ContentSwitchButton),
                                                     new FrameworkPropertyMetadata(typeof (ContentSwitchButton)));
        }

        #region ActiveContent

        public object ActiveContent
        {
            get { return (object) GetValue(ActiveContentProperty); }
            set { SetValue(ActiveContentProperty, value); }
        }

        public static readonly DependencyProperty ActiveContentProperty = DependencyProperty.Register(
            "ActiveContent", typeof (object), typeof (ContentSwitchButton));

        #endregion

        #region DeactiveContent

        public object DeactiveContent
        {
            get { return (object) GetValue(DeactiveContentProperty); }
            set { SetValue(DeactiveContentProperty, value); }
        }

        public static readonly DependencyProperty DeactiveContentProperty = DependencyProperty.Register(
            "DeactiveContent", typeof (object), typeof (ContentSwitchButton));

        #endregion
    }
}