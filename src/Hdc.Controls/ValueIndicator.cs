using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Hdc.Windows;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "OnlineMode", GroupName = "ValueModeStates")]
    [TemplateVisualState(Name = "LockedMode", GroupName = "ValueModeStates")]
    [TemplateVisualState(Name = "FaultedMode", GroupName = "ValueModeStates")]
    [TemplateVisualState(Name = "OfflineMode", GroupName = "ValueModeStates")]
    [TemplateVisualState(Name = "ObjectiveValueUnchanged", GroupName = "ObjectiveValueStates")]
    [TemplateVisualState(Name = "ObjectiveValueChanged", GroupName = "ObjectiveValueStates")]
    [TemplateVisualState(Name = "UpperSide", GroupName = "SideStates")]
    [TemplateVisualState(Name = "LowerSide", GroupName = "SideStates")]
    [TemplatePart(Name = "PART_ActualValueIndicator", Type = typeof (RangeBase))]
    [TemplatePart(Name = "PART_ActualValueTextBlock", Type = typeof (TextBlock))]
    [TemplatePart(Name = "PART_ObjectiveValueIndicator", Type = typeof (RangeBase))]
    [TemplatePart(Name = "PART_ObjectiveValueTextBlock", Type = typeof (TextBlock))]
    [TemplatePart(Name = "PART_DragDropPanel", Type = typeof (Panel))]
    public class ValueIndicator : Button
    {
        private RangeBase _actualValueIndicator;
        private TextBlock _actualValueTextBlock;
        private RangeBase _objectiveValueIndicator;
        private TextBlock _objectiveValueTextBlock;
        private Panel _dragDropPanel;
        private bool m_IsDown;
        private Point m_StartPoint;

        public static ICommand SetObjectiveValueToMaxCommand = new RoutedCommand();

        public static ICommand SetObjectiveValueToMinCommand = new RoutedCommand();

        public static readonly DependencyProperty SideIndexProperty = DependencyProperty.Register(
            "SideIndex", typeof(int), typeof(ValueIndicator), new PropertyMetadata(OnSideIndexChangedCallback));
        
        public static readonly DependencyProperty DisplaySideIndexProperty = DependencyProperty.Register(
            "DisplaySideIndex", typeof(int), typeof(ValueIndicator));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (double), typeof (ValueIndicator), new UIPropertyMetadata(99.0));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof (double), typeof (ValueIndicator), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty OnlineActualValueProperty = DependencyProperty.Register(
            "OnlineActualValue", typeof (double), typeof (ValueIndicator), new UIPropertyMetadata(0.0));

        public static readonly DependencyProperty OnOfflineOperationDoneCommandProperty = DependencyProperty.Register(
            "OnOfflineOperationDoneCommand", typeof (ICommand), typeof (ValueIndicator));

        public static readonly DependencyProperty OnOnlineOperationDoneCommandProperty = DependencyProperty.Register(
            "OnOnlineOperationDoneCommand", typeof (ICommand), typeof (ValueIndicator));

        public static readonly DependencyProperty OnlineObjectiveValueProperty = DependencyProperty.Register(
            "OnlineObjectiveValue", typeof (double), typeof (ValueIndicator),
            new UIPropertyMetadata(0.0, (s, e) =>
                                            {
                                                var me = s as ValueIndicator;
                                                if (me == null) return;
                                                me.RaiseOnlineObjectiveValueChangedEvent();
                                            }));

        public static readonly DependencyProperty IsOnlineObjectiveValueChangedProperty =
            DependencyProperty.Register("IsOnlineObjectiveValueChanged", typeof (bool), typeof (ValueIndicator),
                                        new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register(
            "IsLocked", typeof (bool), typeof (ValueIndicator), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsFaultedProperty = DependencyProperty.Register(
            "IsFaulted", typeof (bool), typeof (ValueIndicator), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty OfflineActualValueProperty = DependencyProperty.Register(
            "OfflineActualValue", typeof (double), typeof (ValueIndicator));

        public static readonly DependencyProperty OfflineObjectiveValueProperty = DependencyProperty.Register(
            "OfflineObjectiveValue", typeof (double), typeof (ValueIndicator));

        public static readonly DependencyProperty IsOfflineProperty = DependencyProperty.Register(
            "IsOffline", typeof (bool), typeof (ValueIndicator), new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsOfflineObjectiveValueChangedProperty = DependencyProperty.Register(
            "IsOfflineObjectiveValueChanged", typeof (bool), typeof (ValueIndicator),
            new PropertyMetadata(OnPropertyChangedCallback));

        public static readonly DependencyProperty IsOnlineValueReadOnlyProperty = DependencyProperty.Register(
            "IsOnlineValueReadOnly", typeof (bool), typeof (ValueIndicator));

        public static readonly DependencyProperty IsOfflineValueReadOnlyProperty = DependencyProperty.Register(
            "IsOfflineValueReadOnly", typeof (bool), typeof (ValueIndicator));

        public static readonly DependencyProperty ValueBrushProperty = DependencyProperty.Register(
            "ValueBrush", typeof (Brush), typeof (ValueIndicator), new PropertyMetadata(Brushes.GreenYellow));

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ValueIndicator;
            if (me == null) return;
            me.UpdateStates(true);
        }

        #region OnlineObjectiveValueChanged

        public static readonly RoutedEvent OnlineObjectiveValueChangedEvent =
            EventManager.RegisterRoutedEvent("OnlineObjectiveValueChanged",
                                             RoutingStrategy.Bubble,
                                             typeof (
                                                 RoutedEventHandler),
                                             typeof (ValueIndicator));

        public event RoutedEventHandler OnlineObjectiveValueChanged
        {
            add { AddHandler(OnlineObjectiveValueChangedEvent, value); }
            remove { RemoveHandler(OnlineObjectiveValueChangedEvent, value); }
        }

        private void RaiseOnlineObjectiveValueChangedEvent()
        {
            var newEventArgs = new RoutedEventArgs(OnlineObjectiveValueChangedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


            _actualValueIndicator = (RangeBase) GetTemplateChild("PART_ActualValueIndicator");
            _actualValueTextBlock = (TextBlock) GetTemplateChild("PART_ActualValueTextBlock");
            _objectiveValueIndicator = (RangeBase) GetTemplateChild("PART_ObjectiveValueIndicator");
            _objectiveValueTextBlock = (TextBlock) GetTemplateChild("PART_ObjectiveValueTextBlock");
            _dragDropPanel = (Panel) GetTemplateChild("PART_DragDropPanel");

            UpdateStates(false);
        }

        static ValueIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof (ValueIndicator),
                new FrameworkPropertyMetadata(typeof (ValueIndicator), FrameworkPropertyMetadataOptions.Inherits));
        }


        protected virtual void UpdateStates(bool useTransitions)
        {
            if (IsOffline)
            {
                _actualValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.OfflineActualValue);
                _actualValueTextBlock.Binding(TextBlock.TextProperty, this, x => x.OfflineActualValue);
                _objectiveValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.OfflineObjectiveValue);
                _objectiveValueTextBlock.Binding(TextBlock.TextProperty, this, x => x.OfflineObjectiveValue);

                VisualStateManager.GoToState(this, "OfflineMode", useTransitions);

                if (IsOfflineObjectiveValueChanged)
                {
                    VisualStateManager.GoToState(this, "ObjectiveValueChanged", useTransitions);
                }
                else
                {
                    VisualStateManager.GoToState(this, "ObjectiveValueUnchanged", useTransitions);
                }
            }
            else
            {
                _actualValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.OnlineActualValue);
                _actualValueTextBlock.Binding(TextBlock.TextProperty, this, x => x.OnlineActualValue);
                _objectiveValueIndicator.Binding(RangeBase.ValueProperty, this, x => x.OnlineObjectiveValue);
                _objectiveValueTextBlock.Binding(TextBlock.TextProperty, this, x => x.OnlineObjectiveValue);


                if (IsFaulted)
                {
                    VisualStateManager.GoToState(this, "FaultedMode", useTransitions);
                }
                else
                {
                    if (IsLocked)
                    {
                        VisualStateManager.GoToState(this, "LockedMode", useTransitions);
                    }
                    else
                    {
                        VisualStateManager.GoToState(this, "OnlineMode", useTransitions);

                        if (IsOnlineObjectiveValueChanged)
                        {
                            VisualStateManager.GoToState(this, "ObjectiveValueChanged", useTransitions);
                        }
                        else
                        {
                            VisualStateManager.GoToState(this, "ObjectiveValueUnchanged", useTransitions);
                        }
                    }
                }
            }

            switch (SideIndex)
            {
                case 0:
                    VisualStateManager.GoToState(this, "UpperSide", useTransitions);
                    break;
                case 1:
                    VisualStateManager.GoToState(this, "LowerSide", useTransitions);
                    break;
            }
        }

        private static void OnSideIndexChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as ValueIndicator;
            if (me == null) return;
            var newValue = (int)e.NewValue;

            me.UpdateStates(true);

            me.DisplaySideIndex = newValue + 1;
        }

        private void BindingTo(string propertyName)
        {
            if (_actualValueIndicator == null) return;
            if (_actualValueTextBlock == null) return;
            BindingOperations.SetBinding(_actualValueIndicator,
                                         RangeBase.ValueProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
            BindingOperations.SetBinding(_actualValueTextBlock,
                                         TextBlock.TextProperty,
                                         new Binding(propertyName) {Source = this, Mode = BindingMode.OneWay});
        }


        public ValueIndicator()
        {
            AllowDrop = true;

            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(
                            SetObjectiveValueToMaxCommand,
                            (sender, e) => { OnlineObjectiveValue = Maximum; },
                            (sender1, e1) => { e1.CanExecute = true; }),
                        new CommandBinding(
                            SetObjectiveValueToMinCommand,
                            (sender2, e2) => { OnlineObjectiveValue = Minimum; },
                            (sender3, e3) => { e3.CanExecute = true; }),
                    });
        }

        [Bindable(true), Category("Common Properties")]
        public int SideIndex
        {
            get { return (int)GetValue(SideIndexProperty); }
            set { SetValue(SideIndexProperty, value); }
        }

           [Bindable(true), Category("Common Properties")]
        public bool IsSideIndexEnabled
        {
            get { return (bool) GetValue(IsSideIndexEnabledProperty); }
            set { SetValue(IsSideIndexEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsSideIndexEnabledProperty = DependencyProperty.Register(
            "IsSideIndexEnabled", typeof (bool), typeof (ValueIndicator));
         

        [Bindable(true), Category("Common Properties")]
        public int DisplaySideIndex
        {
            get { return (int)GetValue(DisplaySideIndexProperty); }
            private set { SetValue(DisplaySideIndexProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double Maximum
        {
            get { return (double) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double Minimum
        {
            get { return (double) GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double OnlineActualValue
        {
            get { return (double) GetValue(OnlineActualValueProperty); }
            set { SetValue(OnlineActualValueProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double OnlineObjectiveValue
        {
            get { return (double) GetValue(OnlineObjectiveValueProperty); }
            set { SetValue(OnlineObjectiveValueProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsOnlineObjectiveValueChanged
        {
            get { return (bool) GetValue(IsOnlineObjectiveValueChangedProperty); }
            set { SetValue(IsOnlineObjectiveValueChangedProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsLocked
        {
            get { return (bool) GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsFaulted
        {
            get { return (bool) GetValue(IsFaultedProperty); }
            set { SetValue(IsFaultedProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsOffline
        {
            get { return (bool) GetValue(IsOfflineProperty); }
            set { SetValue(IsOfflineProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double OfflineActualValue
        {
            get { return (double) GetValue(OfflineActualValueProperty); }
            set { SetValue(OfflineActualValueProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public double OfflineObjectiveValue
        {
            get { return (double) GetValue(OfflineObjectiveValueProperty); }
            set { SetValue(OfflineObjectiveValueProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsOfflineObjectiveValueChanged
        {
            get { return (bool) GetValue(IsOfflineObjectiveValueChangedProperty); }
            set { SetValue(IsOfflineObjectiveValueChangedProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public Brush ValueBrush
        {
            get { return (Brush) GetValue(ValueBrushProperty); }
            set { SetValue(ValueBrushProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsOnlineValueReadOnly
        {
            get { return (bool) GetValue(IsOnlineValueReadOnlyProperty); }
            set { SetValue(IsOnlineValueReadOnlyProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsOfflineValueReadOnly
        {
            get { return (bool) GetValue(IsOfflineValueReadOnlyProperty); }
            set { SetValue(IsOfflineValueReadOnlyProperty, value); }
        }

        protected override void OnPreviewDragOver(DragEventArgs e)
        {
            base.OnPreviewDragOver(e);

            
            Point pos = e.GetPosition(_dragDropPanel);

            SetLevel(pos.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);

            

            if (m_IsDown)
            {
                var position = GetPosition(e);
//               var b = Math.Abs(position.X - m_StartPoint.X) > SystemParameters.MinimumHorizontalDragDistance;
                var b1 = Math.Abs(position.Y - m_StartPoint.Y) > SystemParameters.MinimumVerticalDragDistance;
//                var b = Math.Abs(position.X - m_StartPoint.X) > 2.0;
//                var b1 = Math.Abs(position.Y - m_StartPoint.Y) > 2.0;
                if (b1)
                {
                    Debug.WriteLine("Drag Y: " + position.Y);
                    Debug.WriteLine("Drag Y: " + m_StartPoint.Y);

                    DragStarted();
                }
            }
        }

        private void DragStarted()
        {
            m_IsDown = false;

            if (IsOffline)
            {
                if (IsOfflineValueReadOnly) return;
            }
            else
            {
                if (IsOnlineValueReadOnly) return;
                if (IsLocked) return;
                if (IsFaulted) return;
            }


            DragDrop.DoDragDrop(this, new object(), DragDropEffects.All);
        }

        private Point GetPosition(MouseEventArgs e)
        {
            return e.GetPosition(_dragDropPanel);
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            if (IsOnline && IsOnlineValueReadOnly)
                return;

            if (IsOffline && IsOfflineValueReadOnly)
                return;

            m_IsDown = true;
            m_StartPoint = e.GetPosition(_dragDropPanel);

            Debug.WriteLine("Start Y: " + m_StartPoint.Y);


            Point pos = e.GetPosition(_dragDropPanel);

            SetLevel(pos.Y);
        }

        public bool IsOnline
        {
            get { return !IsOffline; }
        }

        protected override void OnClick()
        {
            base.OnClick();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);

            m_IsDown = false;

            Debug.WriteLine("ExecuteOnOperationDoneCommand from OnPreviewMouseUp");
            ExecuteOnOperationDoneCommand();
        }

        protected override void OnPreviewDrop(DragEventArgs e)
        {
            base.OnPreviewDrop(e);

            Debug.WriteLine("ExecuteOnOperationDoneCommand from OnPreviewDrop");
            ExecuteOnOperationDoneCommand();
        }

        private void ExecuteOnOperationDoneCommand()
        {
            if (IsOffline)
            {
                if (OnOfflineOperationDoneCommand != null)
                    OnOfflineOperationDoneCommand.Execute(new object());
            }
            else
            {
                if (OnOnlineOperationDoneCommand != null)
                    OnOnlineOperationDoneCommand.Execute(new object());
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
        }

        private void SetLevel(double mousePositionY)
        {
            if (IsOffline && IsOfflineValueReadOnly)
                return;

            if (IsOnline && IsOnlineValueReadOnly)
                return;

            if (IsOffline)
            {
                IsOfflineObjectiveValueChanged = true;
            }
            else 
            {
                IsOnlineObjectiveValueChanged = true;
            }


            if (_dragDropPanel.ActualHeight == 0)
                return;

            double ratio = (_dragDropPanel.ActualHeight - mousePositionY)/_dragDropPanel.ActualHeight;
            double objTemp = Maximum*ratio;
            objTemp = objTemp > Maximum ? Maximum : objTemp;
            objTemp = objTemp < Minimum ? Minimum : objTemp;

            if (IsOffline)
            {
                OfflineObjectiveValue = (int) objTemp;
            }
            else
            {
                OnlineObjectiveValue = (int) objTemp;
            }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public ICommand OnOfflineOperationDoneCommand
        {
            get { return (ICommand) GetValue(OnOfflineOperationDoneCommandProperty); }
            set { SetValue(OnOfflineOperationDoneCommandProperty, value); }
        }

        [Bindable(true), Category("ValueIndicator Properties")]
        public ICommand OnOnlineOperationDoneCommand
        {
            get { return (ICommand) GetValue(OnOnlineOperationDoneCommandProperty); }
            set { SetValue(OnOnlineOperationDoneCommandProperty, value); }
        }


        [Bindable(true), Category("ValueIndicator Properties")]
        public bool IsShowTickbar
        {
            get { return (bool) GetValue(IsShowTickbarProperty); }
            set { SetValue(IsShowTickbarProperty, value); }
        }

        public static readonly DependencyProperty IsShowTickbarProperty = DependencyProperty.Register(
            "IsShowTickbar", typeof (bool), typeof (ValueIndicator));
    }
}