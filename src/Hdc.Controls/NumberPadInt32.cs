using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class NumberPadInt32 : Control, IDataErrorInfo
    {
        public static readonly DependencyProperty ValueCurrentProperty = DependencyProperty.Register(
            "ValueCurrent", typeof(int?), typeof(NumberPadInt32));

        public static readonly DependencyProperty ValueMaxProperty = DependencyProperty.Register(
            "ValueMax", typeof(int), typeof(NumberPadInt32));

        public static readonly DependencyProperty ValueMinProperty = DependencyProperty.Register(
            "ValueMin", typeof(int), typeof(NumberPadInt32));

        public static readonly DependencyProperty ValueUnitProperty = DependencyProperty.Register(
            "ValueUnit", typeof(int), typeof(NumberPadInt32), new PropertyMetadata(1.0));

        public static readonly DependencyProperty ValueDefaultProperty = DependencyProperty.Register(
            "ValueDefault", typeof(int), typeof(NumberPadInt32));

        public static readonly DependencyProperty ValueNameProperty = DependencyProperty.Register(
            "ValueName", typeof(string), typeof(NumberPadInt32));

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            "CancelCommand", typeof(ICommand), typeof(NumberPadInt32));

        public static readonly DependencyProperty ConfirmCommandProperty = DependencyProperty.Register(
            "ConfirmCommand", typeof(ICommand), typeof(NumberPadInt32));

        private bool _isInputed;

        public static RoutedCommand BackspaceCommand = new RoutedCommand();

        public static RoutedCommand DigitalCommand = new RoutedCommand();

        public static RoutedCommand IncreaseCommand = new RoutedCommand();

        public static RoutedCommand DecreaseCommand = new RoutedCommand();

        public static RoutedCommand NegativeCommand = new RoutedCommand();

        public static RoutedCommand EnterCommand = new RoutedCommand();

        public static RoutedCommand EscapeCommand = new RoutedCommand();

        public static RoutedCommand MaxCommand = new RoutedCommand();

        public static RoutedCommand MinCommand = new RoutedCommand();
         
         
        static NumberPadInt32()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (NumberPadInt32),
                                                     new FrameworkPropertyMetadata(typeof (NumberPadInt32)));
        }
        
        public NumberPadInt32()
        {
            CommandBindings.AddRange(
                new[]
                    {
                        new CommandBinding(DigitalCommand, DigitalCommandExecuted, DigitalCommandCanExecute),
                        new CommandBinding(BackspaceCommand, BackspaceCommandExecuted, BackspaceCommandCanExecute),
                        new CommandBinding(IncreaseCommand, IncreaseCommandExecuted, IncreaseCommandCanExecute),
                        new CommandBinding(DecreaseCommand, DecreaseCommandExecuted, DecreaseCommandCanExecute),
                        new CommandBinding(NegativeCommand, NegativeCommandExecuted, NegativeCommandCanExecute),
                        new CommandBinding(EscapeCommand, EscapeCommandExecuted, EscapeCommandCanExecute),
                        new CommandBinding(EnterCommand, EnterCommandExecuted, EnterCommandCanExecute),
                        new CommandBinding(MaxCommand, MaxCommandExecuted, MaxCommandCanExecute),
                        new CommandBinding(MinCommand, MinCommandExecuted, MinCommandCanExecute),
                    });
        }

        public int? ValueCurrent
        {
            get
            {
                return (int?)GetValue(ValueCurrentProperty);
            }
            set
            {
                SetValue(ValueCurrentProperty, value);
            }
        }

        public int ValueMax
        {
            get
            {
                return (int)GetValue(ValueMaxProperty);
            }
            set
            {
                SetValue(ValueMaxProperty, value);
            }
        }

        public int ValueMin
        {
            get
            {
                return (int)GetValue(ValueMinProperty);
            }
            set
            {
                SetValue(ValueMinProperty, value);
            }
        }

        public int ValueUnit
        {
            get
            {
                return (int)GetValue(ValueUnitProperty);
            }
            set
            {
                SetValue(ValueUnitProperty, value);
            }
        }

        public int ValueDefault
        {
            get
            {
                return (int)GetValue(ValueDefaultProperty);
            }
            set
            {
                SetValue(ValueDefaultProperty, value);
            }
        }

        public string ValueName
        {
            get
            {
                return (string)GetValue(ValueNameProperty);
            }
            set
            {
                SetValue(ValueNameProperty, value);
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return (ICommand)GetValue(CancelCommandProperty);
            }
            set
            {
                SetValue(CancelCommandProperty, value);
            }
        }

        public ICommand ConfirmCommand
        {
            get
            {
                return (ICommand)GetValue(ConfirmCommandProperty);
            }
            set
            {
                SetValue(ConfirmCommandProperty, value);
            }
        }
         

        public string this[string columnName]
        {
            get
            {
                string result = null;

                if (columnName == "ValueCurrent")
                {
                    if (this.ValueCurrent < ValueMin || this.ValueCurrent > ValueMax)
                    {
                        result = "Age must not be less than 0 or greater than 150.";
                    }
                }
                return result;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
          
         
         

        private void BackspaceCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var str = ValueCurrent.ToString();
            if (str.Length == 1)
            {
                ValueCurrent = null;
                return;
            }

            string tempStr;
            if (str.Length > 2 && str[str.Length - 2] == '.')
            {
                tempStr = str.Remove(str.Length - 2);
            }
            else
            {
                tempStr = str.Remove(str.Length - 1);
            }

            if (tempStr == "-")
            {
                ValueCurrent = null;
                return;
            }

            ValueCurrent = Int32.Parse(tempStr);
        }

        private void BackspaceCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ValueCurrent == null)
            {
                return;
            }

            var str = ValueCurrent.ToString();
            if (str.Length > 0)
            {
                e.CanExecute = true;
            }
        }

        private void DigitalCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var number = Int32.Parse((string)e.Parameter);
            if (!_isInputed)
            {
                ValueCurrent = number;
                _isInputed = true;
            }
            else
            {
                var str = ValueCurrent.ToString() + number;
                ValueCurrent = Int32.Parse(str);
            }
        }

        private void DigitalCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                var number = Int32.Parse((string)e.Parameter);

                if (!_isInputed)
                {
                    e.CanExecute = ValidateValueRange(number);
                }
                else
                {
                    try
                    {
                        var newValue = Int32.Parse(ValueCurrent.ToString() + number);
                        e.CanExecute = ValidateValueRange(newValue);
                    }
                    catch (FormatException)
                    {
                        e.CanExecute = false;
                        return;
                    }
                }
            }
        }

        private void IncreaseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                _isInputed = true;
            }
            if (ValueCurrent.HasValue)
            {
                var remainder = Math.Abs((ValueCurrent.Value + ValueUnit) % ValueUnit);
                ValueCurrent = ValueCurrent + ValueUnit - remainder;
            }
            else
            {
                ValueCurrent = ValueDefault + ValueUnit;
            }
        }

        private void IncreaseCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ValueCurrent.HasValue)
            {
                e.CanExecute = ValueCurrent.Value < ValueMax;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void DecreaseCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                _isInputed = true;
            }

            if (ValueCurrent.HasValue)
            {
                var remainder = Math.Abs((ValueCurrent.Value - ValueUnit) % ValueUnit);
                ValueCurrent = ValueCurrent - ValueUnit + remainder;
            }
            else
            {
                ValueCurrent = ValueDefault - ValueUnit;
            }
        }

        private void DecreaseCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ValueCurrent.HasValue)
            {
                e.CanExecute = ValueCurrent.Value > ValueMin;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void NegativeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                _isInputed = true;
            }
            ValueCurrent -= 2 * ValueCurrent;
        }

        private void NegativeCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (ValueCurrent == null)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = ValidateValueRange(ValueCurrent.Value - 2 * ValueCurrent.Value);
        }

        private void EnterCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (!ValueCurrent.HasValue || !ValidateValue(ValueCurrent.Value))
            {
                return;
            }

            //            ResultState = true;
            //            Exit();

            Confirm();
        }

        private void EnterCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                return;
            }

            if (ValueCurrent.HasValue && ValidateValue(ValueCurrent.Value))
            {
                e.CanExecute = true;
            }
        }

        private void EscapeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            //            if (!_isInputed || ValueCurrent == null)
            //                ResultState = false;
            //            else
            //            ResultState = false;
            //
            //            Exit();

            Cancel();
        }

        private void EscapeCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MaxCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _isInputed = true;
            ValueCurrent = ValueMax;
        }

        private void MaxCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MinCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            _isInputed = true;
            ValueCurrent = ValueMin;
        }

        private void MinCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
         
         
         
        private bool ValidateValue(double value)
        {
            if (value > ValueMax)
            {
                return false;
            }

            if (value < ValueMin)
            {
                return false;
            }

            if ((value - ValueMin) % ValueUnit != 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateValueRange(double value)
        {
            if (value > ValueMax)
            {
                return false;
            }

            if (value < ValueMin)
            {
                return false;
            }

            return true;
        }

        private void Confirm()
        {
            if (ConfirmCommand != null)
            {
                ConfirmCommand.Execute(ValueCurrent);
            }
        }

        private void Cancel()
        {
            if (CancelCommand != null)
            {
                CancelCommand.Execute(new object());
            }
        }
         
    }
}