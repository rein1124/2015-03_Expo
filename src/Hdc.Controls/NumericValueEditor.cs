using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class NumericValueEditor : Control
    {
        static NumericValueEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericValueEditor),
                                                     new FrameworkPropertyMetadata(typeof(NumericValueEditor)));
        }

        public double Unit
        {
            get { return (double)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Unit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(double), typeof(NumericValueEditor),
                                        new UIPropertyMetadata(1.0));


        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(NumericValueEditor),
                                        new UIPropertyMetadata(0.0));

        private ICommand _increaseCommand;

        private ICommand _decreaseCommand;


        public NumericValueEditor()
        {
            // TODO: 2014-04-06, removed temporay, should implement DelegateCommand in Hdc.Toolkit
//            _increaseCommand = new DelegateCommand(() =>
//            {
//                var addedValue = Value + Unit;
//
//                Value = LimiteValue(addedValue);
//            });


            // TODO: 2014-04-06, removed temporay, should implement DelegateCommand in Hdc.Toolkit
//            _decreaseCommand = new DelegateCommand(() =>
//            {
//                var subtractedValue = Value - Unit;
//                Value = LimiteValue(subtractedValue);
//            });
        }

        private double LimiteValue(double testValue)
        {
            if (testValue > Max) return Max;
            if (testValue < Min) return Min;
            return testValue;
        }

        public ICommand IncreaseCommand
        {
            get { return _increaseCommand; }
        }

        public ICommand DecreaseCommand
        {
            get { return _decreaseCommand; }
        }

        #region Max

        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }

        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            "Max", typeof(double), typeof(NumericValueEditor));

        #endregion

        #region Min

        public double Min
        {
            get { return (double)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }

        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            "Min", typeof(double), typeof(NumericValueEditor));

        #endregion

        #region ChangeValueCommand

        public ICommand ChangeValueCommand
        {
            get { return (ICommand)GetValue(ChangeValueCommandProperty); }
            set { SetValue(ChangeValueCommandProperty, value); }
        }

        public static readonly DependencyProperty ChangeValueCommandProperty = DependencyProperty.Register(
            "ChangeValueCommand", typeof(ICommand), typeof(NumericValueEditor));

        #endregion

        #region ChangeValueCommandParameter

        public object ChangeValueCommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
            "ChangeValueCommandParameter", typeof(object), typeof(NumericValueEditor));

        #endregion
    }
}