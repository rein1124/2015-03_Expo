using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{

    [TemplateVisualState(Name = "Left", GroupName = "DirectionStates")]
    [TemplateVisualState(Name = "Right", GroupName = "DirectionStates")]
    [TemplateVisualState(Name = "Up", GroupName = "DirectionStates")]
    [TemplateVisualState(Name = "Down", GroupName = "DirectionStates")]


    public class DirectButton : Button
    {
        static DirectButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DirectButton), new FrameworkPropertyMetadata(typeof(DirectButton)));
        }

        public Direction Direction
        {
            get { return (Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction", typeof(Direction), typeof(DirectButton), new PropertyMetadata(PropertyChangedCallback));


        private static void PropertyChangedCallback(DependencyObject s,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var me = s as DirectButton;
            if (me == null) return;

            me.UpdateItemStates(true);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateItemStates(false);
        }

        


        protected virtual void UpdateItemStates(bool useTransitions)
        {
            
            switch (Direction)
            {
                case Direction.Up:
                    VisualStateManager.GoToState(this, "Up", useTransitions);
                    break;
                case Direction.Down:
                    VisualStateManager.GoToState(this, "Down", useTransitions);
                    break;
                case Direction.Left:
                    VisualStateManager.GoToState(this, "Left", useTransitions);
                    break;
                case Direction.Right:
                    VisualStateManager.GoToState(this, "Right", useTransitions);
                    break;

            }
        }
    }
}
