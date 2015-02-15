using System.Windows;
using System.Windows.Input;

namespace Hdc.Controls
{
    public class VirtualKey : DependencyObject
    {
        public VirtualKey()
        {

        }

        public VirtualKey(Key key, string lower, string upper, string text)
        {
            Key = key;
            LowerLetter = lower;
            UpperLetter = upper;
            Text = text;
        }

        public VirtualKey(Key key, string lower, string upper)
            : this(key, lower, upper, null)
        {
        }

        #region Key

        public Key Key
        {
            get { return (Key)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key",
                                                                                            typeof(Key),
                                                                                            typeof(VirtualKey));

        #endregion

        #region IsUpper

        public bool IsUpper
        {
            get { return (bool)GetValue(IsUpperProperty); }
            set { SetValue(IsUpperProperty, value); }
        }

        public static readonly DependencyProperty IsUpperProperty = DependencyProperty.Register("IsUpper",
                                                                                                typeof(bool),
                                                                                                typeof(VirtualKey));

        #endregion

        #region LowerLetter

        public string LowerLetter
        {
            get { return (string)GetValue(LowerLetterProperty); }
            set { SetValue(LowerLetterProperty, value); }
        }

        public static readonly DependencyProperty LowerLetterProperty = DependencyProperty.Register("LowerLetter",
                                                                                                    typeof(string),
                                                                                                    typeof(VirtualKey));

        #endregion

        #region UpperLetter

        public string UpperLetter
        {
            get { return (string)GetValue(UpperLetterProperty); }
            set { SetValue(UpperLetterProperty, value); }
        }

        public static readonly DependencyProperty UpperLetterProperty = DependencyProperty.Register("UpperLetter",
                                                                                                    typeof(string),
                                                                                                    typeof(VirtualKey));

        #endregion

        #region Text

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
                                                                                             typeof(string),
                                                                                             typeof(VirtualKey));

        #endregion
    }
}