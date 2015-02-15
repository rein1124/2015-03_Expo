using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class NumberPadZeroDot : Control
    {
        private bool _isInputed;
        private bool _isPositiveNum;
        public static RoutedCommand DigitalCommand = new RoutedCommand();
        public static RoutedCommand StringCommand = new RoutedCommand();
        public static RoutedCommand EnterCommand = new RoutedCommand();
        public static RoutedCommand ClearCommand = new RoutedCommand();
        public static RoutedCommand MaxCommand = new RoutedCommand();
        public static RoutedCommand MinCommand = new RoutedCommand();
        public static RoutedCommand NegativeCommand = new RoutedCommand();

        static NumberPadZeroDot()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumberPadZeroDot),
                                                     new FrameworkPropertyMetadata(typeof(NumberPadZeroDot)));
        }

        public NumberPadZeroDot()
        {
            CommandBindings.AddRange(new[]
                                         {
                                             new CommandBinding(DigitalCommand, DigitalCommandExecuted),
                                             new CommandBinding(EnterCommand,EnterCommandExecuted,EnterCommandCanExecute),
                                             new CommandBinding(ClearCommand,ClearCommandExecuted),
                                             new CommandBinding(MaxCommand,MaxCommandExecuted,MaxCommandCanExecute),
                                             new CommandBinding(MinCommand,MinCommandExecuted,MinCommandCanExecute),
                                             new CommandBinding(NegativeCommand,NegativeCommandExecuted),
        });
            StringValueCurrent = "0.";
            ValueCurrent = 0;
            IsValid = true;
        }


        #region ValueCurrent

        public double? ValueCurrent
        {
            get { return (double?)GetValue(ValueCurrentProperty); }
            set { SetValue(ValueCurrentProperty, value); }
        }

        public static readonly DependencyProperty ValueCurrentProperty = DependencyProperty.Register(
            "ValueCurrent", typeof(double?), typeof(NumberPadZeroDot));

        #endregion

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(string), typeof(NumberPadZeroDot));

        #endregion

        #region StringValueCurrent

        public string StringValueCurrent
        {
            get { return (string)GetValue(StringValueCurrentProperty); }
            set { SetValue(StringValueCurrentProperty, value); }
        }

        public static readonly DependencyProperty StringValueCurrentProperty = DependencyProperty.Register(
            "StringValueCurrent", typeof(string), typeof(NumberPadZeroDot));

        #endregion

        #region ValueDefault

        public double ValueDefault
        {
            get { return (double)GetValue(ValueDefaultProperty); }
            set { SetValue(ValueDefaultProperty, value); }
        }

        public static readonly DependencyProperty ValueDefaultProperty = DependencyProperty.Register(
            "ValueDefault", typeof(double), typeof(NumberPadZeroDot));

        #endregion

        #region ValueMax

        public double ValueMax
        {
            get { return (double)GetValue(ValueMaxProperty); }
            set { SetValue(ValueMaxProperty, value); }
        }

        public static readonly DependencyProperty ValueMaxProperty = DependencyProperty.Register(
            "ValueMax", typeof(double), typeof(NumberPadZeroDot));

        #endregion

        #region ValueMin

        public double ValueMin
        {
            get { return (double)GetValue(ValueMinProperty); }
            set { SetValue(ValueMinProperty, value); }
        }

        public static readonly DependencyProperty ValueMinProperty = DependencyProperty.Register(
            "ValueMin", typeof(double), typeof(NumberPadZeroDot));

        #endregion

        #region IsValid

        public bool IsValid
        {
            get { return (bool)GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register(
            "IsValid", typeof(bool), typeof(NumberPadZeroDot));

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand
        {
            get { return (ICommand)GetValue(ConfirmCommandProperty); }
            set { SetValue(ConfirmCommandProperty, value); }
        }

        public static readonly DependencyProperty ConfirmCommandProperty = DependencyProperty.Register(
            "ConfirmCommand", typeof(ICommand), typeof(NumberPadZeroDot));

        #endregion

        #region CancelCommand

        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            "CancelCommand", typeof(ICommand), typeof(NumberPadZeroDot));

        #endregion

        private void DigitalCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            //var str = (string) e.Parameter;
            var number = double.Parse((string)e.Parameter);

            if (!_isInputed)
            {
                StringValueCurrent = "0." + number;
                ValueCurrent = double.Parse(StringValueCurrent);
                _isInputed = true;
            }
            else
            {
                //                if (_isPositiveNum == false)
                //                {
                //                    StringValueCurrent = "-" + StringValueCurrent + number;
                //
                //                }
                //                else
                //                {
                StringValueCurrent = StringValueCurrent + number;
                //                }
                ValueCurrent = StringValueCurrent.ToDouble();
            }
            if (StringValueCurrent.ToDouble() < ValueMin || StringValueCurrent.ToDouble() > ValueMax || StringValueCurrent.Length > 5)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }

        private void EnterCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            if (!ValueCurrent.HasValue || !ValidateValue(ValueCurrent.Value) || StringValueCurrent.Length > 5)
            {
                return;
            }
            Confirm();
            //            return;
        }

        private void EnterCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (!_isInputed || (StringValueCurrent.Length > 5))
            {
                return;
            }
            if (ValueCurrent.HasValue && ValidateValue(ValueCurrent.Value))
            {
                e.CanExecute = true;
            }
        }

        private void ClearCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            //            var str = ValueCurrent.ToString();
            //            string tempStr;
            //            tempStr = str.Remove(0);
            //           var zero= tempStr.ToDouble();
            _isInputed = true;
            //            ValueCurrent = Double.Parse(zero);
            StringValueCurrent = "0.";
            IsValid = true;
            ValueCurrent = StringValueCurrent.ToDouble();
        }

        private void MaxCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            _isInputed = true;
            if (ValueCurrent < 0)
            {
                ValueCurrent -= 2 * ValueCurrent;

                StringValueCurrent = ValueCurrent.ToString();
            }
            else if (ValueCurrent == 0)
            {
                StringValueCurrent = "0.";
            }
            _isPositiveNum = true;

            //            ValueCurrent = ValueMax;
            //            IsValid = true;
            if ((ValueCurrent < ValueMin) || (ValueCurrent > ValueMax) || StringValueCurrent.Length > 5)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }

        private void MaxCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MinCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                _isInputed = true;
                _isPositiveNum = false;
                StringValueCurrent = "-" + StringValueCurrent;
                ValueCurrent = StringValueCurrent.ToDouble();
            }
            _isInputed = true;
            if (ValueCurrent > 0)
            {
                ValueCurrent -= 2 * ValueCurrent;
                StringValueCurrent = ValueCurrent.ToString();
            }
            else if (ValueCurrent == 0)
            {
                StringValueCurrent = "-0.";
                ValueCurrent = StringValueCurrent.ToDouble();
            }
            //            ValueCurrent = ValueMin;
            //            IsValid = true;
            if ((ValueCurrent < ValueMin) || (ValueCurrent > ValueMax) || StringValueCurrent.Length > 5)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
        }

        private void MinCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NegativeCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            if (!_isInputed)
            {
                _isInputed = true;
                _isPositiveNum = false;
                StringValueCurrent = "-" + StringValueCurrent;
                ValueCurrent = StringValueCurrent.ToDouble();
            }
            else if (ValueCurrent == 0)
            {
                _isPositiveNum = (!_isPositiveNum);
                if (!_isPositiveNum)
                {
                    StringValueCurrent = "-0.";
                }
                else
                {
                    StringValueCurrent = "0.";
                }
            }

            ValueCurrent -= 2 * ValueCurrent;
            if (ValueCurrent != 0)
            {
                StringValueCurrent = ValueCurrent.ToString();
            }
            if ((ValueCurrent < ValueMin) || (ValueCurrent > ValueMax) || StringValueCurrent.Length > 5)
            {
                IsValid = false;
            }
            else
            {
                IsValid = true;
            }
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

    }
}