using System;
using System.Windows;

namespace Hdc.Mvvm.Dialogs
{
    public class NumberInputDialogArg : InputDialogArg<double>
    {
        public NumberInputDialogArg(Action cancelAction, Action<double> confirmAction)
            : base(cancelAction, confirmAction)
        {
        }

        #region ValueName

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

        public static readonly DependencyProperty ValueNameProperty = DependencyProperty.Register("ValueName",
            typeof(string),
            typeof(NumberInputDialogArg));

        #endregion

        #region MaxValue

        public double MaxValue
        {
            get
            {
                return (double)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue",
            typeof(double),
            typeof(NumberInputDialogArg));

        #endregion

        #region MinValue

        public double MinValue
        {
            get
            {
                return (double)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue",
            typeof(double),
            typeof(NumberInputDialogArg));

        #endregion

        #region DefaultValue

        public double DefaultValue
        {
            get
            {
                return (double)GetValue(DefaultValueProperty);
            }
            set
            {
                SetValue(DefaultValueProperty, value);
            }
        }

        public static readonly DependencyProperty DefaultValueProperty = DependencyProperty.Register("DefaultValue",
            typeof(double),
            typeof(NumberInputDialogArg));

        #endregion

        #region Unit

        public double Unit
        {
            get
            {
                return (double)GetValue(UnitProperty);
            }
            set
            {
                SetValue(UnitProperty, value);
            }
        }

        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit",
            typeof(double),
            typeof(NumberInputDialogArg));

        #endregion
    }
}