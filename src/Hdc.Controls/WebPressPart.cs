using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "Active", GroupName = "ShowState")]
    [TemplateVisualState(Name = "Deactive", GroupName = "ShowState")]


    [TemplateVisualState(Name = "Tower", GroupName = "Item")]
    [TemplateVisualState(Name = "Folder", GroupName = "Item")]
    public class WebPressPart : SwitchButton
    {
        public new static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(WebPressPart),
            new PropertyMetadata(new PropertyChangedCallback(IsActiveChangedCallback)));

        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register(
            "Index", typeof(int), typeof(WebPressPart),
            new PropertyMetadata(new PropertyChangedCallback(IndexChangedCallback)));

        public static readonly DependencyProperty IndexTagProperty = DependencyProperty.Register(
            "IndexTag", typeof(string), typeof(WebPressPart),
            new PropertyMetadata(new PropertyChangedCallback(IndexTagChangedCallback)));

        public static readonly DependencyProperty PartTypeProperty = DependencyProperty.Register(
            "PartType", typeof(PartType), typeof(WebPressPart),
            new PropertyMetadata(new PropertyChangedCallback(PartTypeChangedCallback)));

        static WebPressPart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WebPressPart),
                                                     new FrameworkPropertyMetadata(typeof(WebPressPart)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            UpdateItemStates(false);
            UpdateShowStateStates(false);
        }

        public new bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public bool IsTower
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void IsActiveChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as WebPressPart;
            if (sw == null) return;
            var newValue = (bool)e.NewValue;

            sw.UpdateShowStateStates(true);


        }

        private static void PartTypeChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as WebPressPart;
            if (sw == null) return;
            var newValue = (PartType)e.NewValue;

            sw.UpdateShowStateStates(true);


        }


        private static void IndexChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as WebPressPart;
            if (sw == null) return;
            var newValue = (int)e.NewValue;

            sw.UpdateShowStateStates(true);


        }

        private static void IndexTagChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var sw = s as WebPressPart;
            if (sw == null) return;
            var newValue = (string)e.NewValue;

            sw.UpdateShowStateStates(true);


        }


        protected virtual void UpdateShowStateStates(bool useTransitions)
        {
            if (IsActive)
            {
                VisualStateManager.GoToState(this, "Active", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Deactive", useTransitions);
            }
        }

        protected virtual void UpdateItemStates(bool useTransitions)
        {
            switch (PartType)
            {
                case PartType.Tower:
                    VisualStateManager.GoToState(this, "Tower", useTransitions);
                    break;
                case PartType.Folder:
                    VisualStateManager.GoToState(this, "Folder", useTransitions);
                    break;

            }
        }




        [Bindable(true), Category("Common Properties")]
        public PartType PartType
        {
            get { return (PartType)GetValue(PartTypeProperty); }
            set { SetValue(PartTypeProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public int Index
        {
            get { return (int)GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }

        [Bindable(true), Category("Common Properties")]
        public string IndexTag
        {
            get { return (string)GetValue(IndexTagProperty); }
            set { SetValue(IndexTagProperty, value); }
        }





    }
}
