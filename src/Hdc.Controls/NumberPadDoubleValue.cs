using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace Hdc.Controls
{
    [TemplateVisualState(Name = "s", GroupName = "Normal")]
    [TemplateVisualState(Name = "s", GroupName = "Exception")]
    public class NumberPadDoubleValue : Control
    {
        private bool _isInputed;
        private bool _isDot;
        private bool _isMoreThanOneDot;
        //        private bool _isValid;
        private int _isDotCount;
        public static RoutedCommand DigitalCommand = new RoutedCommand();
        public static RoutedCommand StringCommand = new RoutedCommand();
        public static RoutedCommand EnterCommand = new RoutedCommand();
        //        public static RoutedCommand BackspaceCommand = new RoutedCommand();
        //        public static RoutedCommand EscapeCommand = new RoutedCommand();
        public static RoutedCommand ClearCommand = new RoutedCommand();
        public static RoutedCommand MaxCommand = new RoutedCommand();
        public static RoutedCommand MinCommand = new RoutedCommand();
        public static RoutedCommand NegativeCommand = new RoutedCommand();

        static NumberPadDoubleValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (NumberPadDoubleValue),
                                                     new FrameworkPropertyMetadata(typeof (NumberPadDoubleValue)));
        }

        public NumberPadDoubleValue()
        {
            CommandBindings.AddRange(new[]
                                         {
                                             new CommandBinding(DigitalCommand, DigitalCommandExecuted),
                                             new CommandBinding(StringCommand, StringCommandExecuted),
                                             new CommandBinding(EnterCommand, EnterCommandExecuted,
                                                                EnterCommandCanExecute),
//                                             new CommandBinding(BackspaceCommand,BackspaceCommandExecuted),
//                                             new CommandBinding(EscapeCommand,EscapeCommandExecuted),
                                             new CommandBinding(ClearCommand, ClearCommandExecuted),
                                             new CommandBinding(MaxCommand, MaxCommandExecuted, MaxCommandCanExecute),
                                             new CommandBinding(MinCommand, MinCommandExecuted, MinCommandCanExecute),
                                             new CommandBinding(NegativeCommand, NegativeCommandExecuted),
                                         });
        }

        #region Title

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (NumberPadDoubleValue));

        #endregion

        #region ValueCurrent

        public double? ValueCurrent
        {
            get { return (double?) GetValue(ValueCurrentProperty); }
            set { SetValue(ValueCurrentProperty, value); }
        }

        public static readonly DependencyProperty ValueCurrentProperty = DependencyProperty.Register(
            "ValueCurrent", typeof (double?), typeof (NumberPadDoubleValue),
            new PropertyMetadata((s, e) =>
                                     {
                                         //var me = s as NumberPadDoubleValue;
                                         //me.StringValueCurrent = e.NewValue.ToString();
                                     }));

        #endregion

        #region StringValueCurrent

        public string StringValueCurrent
        {
            get { return (string) GetValue(StringValueCurrentProperty); }
            set { SetValue(StringValueCurrentProperty, value); }
        }

        public static readonly DependencyProperty StringValueCurrentProperty = DependencyProperty.Register(
            "StringValueCurrent", typeof (string), typeof (NumberPadDoubleValue));

        #endregion

        #region ValueDefault

        public double? ValueDefault
        {
            get { return (double?) GetValue(ValueDefaultProperty); }
            set { SetValue(ValueDefaultProperty, value); }
        }

        public static readonly DependencyProperty ValueDefaultProperty = DependencyProperty.Register(
            "ValueDefault", typeof (double?), typeof (NumberPadDoubleValue),
            new PropertyMetadata((s, e) =>
                                     {
                                         var me = s as NumberPadDoubleValue;

                                         var newV = e.NewValue as double?;

                                         if (newV == null)
                                             me.StringValueCurrent = null;
                                         else
                                         {
                                             me.StringValueCurrent = newV.ToString();
                                             me.ValueCurrent = newV;
                                             me._isDotCount = 0;
                                             me.IsValid = false;
                                             me._isInputed = false;
                                         }
                                     }));

        #endregion

        #region ValueMax

        public double ValueMax
        {
            get { return (double) GetValue(ValueMaxProperty); }
            set { SetValue(ValueMaxProperty, value); }
        }

        public static readonly DependencyProperty ValueMaxProperty = DependencyProperty.Register(
            "ValueMax", typeof (double), typeof (NumberPadDoubleValue));

        #endregion

        #region ValueMin

        public double ValueMin
        {
            get { return (double) GetValue(ValueMinProperty); }
            set { SetValue(ValueMinProperty, value); }
        }

        public static readonly DependencyProperty ValueMinProperty = DependencyProperty.Register(
            "ValueMin", typeof (double), typeof (NumberPadDoubleValue));

        #endregion

        #region IsValid

        public bool IsValid
        {
            get { return (bool) GetValue(IsValidProperty); }
            set { SetValue(IsValidProperty, value); }
        }

        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register(
            "IsValid", typeof (bool), typeof (NumberPadDoubleValue));

        #endregion

        #region ConfirmCommand

        public ICommand ConfirmCommand
        {
            get { return (ICommand) GetValue(ConfirmCommandProperty); }
            set { SetValue(ConfirmCommandProperty, value); }
        }

        public static readonly DependencyProperty ConfirmCommandProperty = DependencyProperty.Register(
            "ConfirmCommand", typeof (ICommand), typeof (NumberPadDoubleValue));

        #endregion

        #region CancelCommand

        public ICommand CancelCommand
        {
            get { return (ICommand) GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
            "CancelCommand", typeof (ICommand), typeof (NumberPadDoubleValue));

        #endregion

        private void StringCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            var s = (string) e.Parameter;
            _isInputed = true;
            _isDot = false;
            if (s == ".")
            {
                if (_isDotCount == 0)
                {
                    _isDot = true;
                    StringValueCurrent = StringValueCurrent + s;
                }
                else
                {
                    _isMoreThanOneDot = true;
                }
                _isDotCount++;
            }
            //            var str = StringValueCurrent + s;
            //            ValueCurrent = Double.Parse(str);
            //            StringValueCurrent = str;
        }

        private void DigitalCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            var number = double.Parse((string) e.Parameter);

            if (!_isInputed)
            {
                ValueCurrent = number;
                StringValueCurrent = ValueCurrent.ToString();
                _isInputed = true;
            }
            else if (_isDotCount == 1)
            {
                StringValueCurrent = StringValueCurrent + number;
                ValueCurrent = StringValueCurrent.ToDouble();
                _isInputed = true;
            }
            else
            {
                var str = StringValueCurrent + number;
                ValueCurrent = Double.Parse(str);
                StringValueCurrent = str;
                _isInputed = true;
            }
            if (ValueCurrent < ValueMin || ValueCurrent > ValueMax)
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }

        private void DigitalCommandCanExecute(object Sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Parameter != null)
            {
                double number = double.Parse((string) e.Parameter);

                if (!_isInputed)
                {
                    e.CanExecute = ValidateValueRange(number);
                }
                else
                {
                    try
                    {
                        if (_isDot)
                        {
                            if (_isDotCount == 1)
                            {
                                var str = Double.Parse(ValueCurrent.ToString() + "." + number);
                                e.CanExecute = ValidateValueRange(str);
                            }
                        }
                        else
                        {
                            var str = Double.Parse(ValueCurrent.ToString() + number);
                            e.CanExecute = ValidateValueRange(str);
                        }
                        //                        _isDot = false;
                        //                        StringValueCurrent = ValueCurrent.ToString();

                        //                        var newValue = Double.Parse(ValueCurrent.ToString() + number);
                        //                        e.CanExecute = ValidateValueRange(newValue);
                    }
                    catch (FormatException)
                    {
                        e.CanExecute = false;
                        return;
                    }
                }
            }
        }

        private void EnterCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            if (!ValueCurrent.HasValue || !ValidateValue(ValueCurrent.Value))
            {
                return;
            }

            //            return;
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

        private void ClearCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            StringValueCurrent = "";
            ValueCurrent = 0;
            _isDotCount = 0;
            IsValid = false;
        }

        private void MaxCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            _isInputed = true;
            ValueCurrent = ValueMax;
            StringValueCurrent = ValueCurrent.ToString();
        }

        private void MaxCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void MinCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            _isInputed = true;
            ValueCurrent = ValueMin;
            StringValueCurrent = ValueCurrent.ToString();
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
                StringValueCurrent = "";
                ValueCurrent = 0;
            }
            ValueCurrent -= 2*ValueCurrent;
            if (ValueCurrent == 0)
            {
                StringValueCurrent = "";
            }
            else
            {
                StringValueCurrent = ValueCurrent.ToString();
            }
        }

        private void BackspaceCommandExecuted(object Sender, ExecutedRoutedEventArgs e)
        {
            /*var str = ValueCurrent.ToString();
            if (str.Length == 1)
            {
                ValueCurrent = null;
                return;
            }

            string tempStr;
            if (str.Length >= 2)
            {
                tempStr = str.Remove(str.Length - 1);
                _matches = Regex.Matches(tempStr, ".");
                foreach (Match matche in _matches)
                {
                    string m = matche.ToString();
                }
                ValueCurrent = Double.Parse(tempStr);
            }*/
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
                StringValueCurrent = "";
                ValueCurrent = null;
                ValueDefault = null;
                _isDotCount = 0;
                IsValid = false;
            }
        }
    }
}