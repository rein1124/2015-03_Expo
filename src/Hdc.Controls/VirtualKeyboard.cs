using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

namespace Hdc.Controls
{
    [TemplatePart(Type = typeof(TextBox), Name = "PART_TEXTBOX_INPUT")]
    public class VirtualKeyboard : Control //, Dialog.IDialogableView
    {
        private readonly VirtualKeyDictionary virtualKeyDictionary = new VirtualKeyDictionary();

        private const string PART_TEXTBOX_INPUT = "PART_TEXTBOX_INPUT";

        private TextBox tbxInput;

        public static ICommand PressLetterKeyCommand = new RoutedCommand();

        public static ICommand PressStickyKeyCommand = new RoutedCommand();

        public static ICommand PressFunctionKeyCommand = new RoutedCommand();

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message",
            typeof(string),
            typeof(VirtualKeyboard));

        public static readonly DependencyProperty CurrentTextProperty = DependencyProperty.Register("CurrentText",
            typeof(string),
            typeof(VirtualKeyboard),
            new PropertyMetadata(""));

        public static readonly DependencyProperty ConfirmCommandProperty = DependencyProperty.Register(
            "ConfirmCommand", typeof(ICommand), typeof(VirtualKeyboard));

        //        public event EventHandler<DialogResultEventArgs> ValidateDialog;

        static VirtualKeyboard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VirtualKeyboard),
                new FrameworkPropertyMetadata(typeof(VirtualKeyboard)));
        }

        public VirtualKeyboard()
        {
            CommandBindings.AddRange(new[]
                {
                    new CommandBinding(PressLetterKeyCommand,
                        PressLetterKeyCommandExecuted,
                        PressLetterKeyCommandCanExecute),
                    new CommandBinding(PressStickyKeyCommand,
                        PressStickyKeyCommandExecuted,
                        PressStickyKeyCommandCanExecute),
                    new CommandBinding(PressFunctionKeyCommand,
                        PressFunctionKeyCommandExecuted,
                        PressFunctionKeyCommandCanExecute),
                });
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            tbxInput = (TextBox)base.GetTemplateChild(PART_TEXTBOX_INPUT);

            var textBinding = new Binding("CurrentText")
                { Source = this, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            BindingOperations.SetBinding(tbxInput, TextBox.TextProperty, textBinding);
        }

        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }
            set
            {
                SetValue(MessageProperty, value);
            }
        }

        private VirtualKey Shift
        {
            get
            {
                return virtualKeyDictionary[Key.LeftShift];
            }
        }

        private VirtualKey CapsLock
        {
            get
            {
                return virtualKeyDictionary[Key.CapsLock];
            }
        }

        public string CurrentText
        {
            get
            {
                return (string)GetValue(CurrentTextProperty);
            }
            set
            {
                SetValue(CurrentTextProperty, value);
            }
        }

        public VirtualKey this[string keyName]
        {
            get
            {
                var key = (Key)Enum.Parse(typeof(Key), keyName);
                return virtualKeyDictionary[key];
            }
        }

        #region CancelCommand

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

        public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register("CancelCommand",
            typeof(ICommand),
            typeof(VirtualKeyboard));

        #endregion

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

        #region CommandExecuted

        #region PressKeyCommandExecuted

        private void PressLetterKeyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var parameter = (VirtualKey)e.Parameter;
            CurrentText += GetVirtualKeyValue(parameter);

            if (Shift.IsUpper)
            {
                Shift.IsUpper = !Shift.IsUpper;
                UpdateShiftStates();
            }
        }

        private void PressLetterKeyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region PressStickyKeyCommandExecuted

        private void PressStickyKeyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var parameter = (VirtualKey)e.Parameter;

            switch (parameter.Key)
            {
                case Key.LeftShift:
                    Shift.IsUpper = !Shift.IsUpper;
                    UpdateShiftStates();
                    break;
                case Key.CapsLock:
                    CapsLock.IsUpper = !CapsLock.IsUpper;
                    UpdateShiftStates();
                    break;
            }
        }

        private void PressStickyKeyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #region PressFunctionKeyCommandExecuted

        private void PressFunctionKeyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var parameter = (VirtualKey)e.Parameter;

            switch (parameter.Key)
            {
                case Key.Tab:
                    CurrentText += "\t";
                    break;
                case Key.Space:
                    CurrentText += " ";
                    break;
                case Key.Back:
                    if (CurrentText.Length > 0)
                    {
                        CurrentText = CurrentText.Remove(CurrentText.Length - 1);
                    }
                    break;
                case Key.Enter:
                    //                    ValidateDialog(this, new DialogResultEventArgs(true, CurrentText));
                    Confirm(CurrentText);
                    break;
                case Key.Escape:
                    //                    ValidateDialog(this, new DialogResultEventArgs(false, null));
                    Cancel();
                    break;
            }
        }

        private void PressFunctionKeyCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion

        #endregion

        private void UpdateShiftStates()
        {
            foreach (var pair in virtualKeyDictionary)
            {
                if (pair.Key == Key.LeftShift)
                {
                    continue;
                }
                if (pair.Key == Key.CapsLock)
                {
                    continue;
                }
                pair.Value.IsUpper = Shift.IsUpper
                                         ? (CapsLock.IsUpper ? false : true)
                                         : (CapsLock.IsUpper ? true : false);
            }
        }

        private string GetVirtualKeyValue(VirtualKey key)
        {
            return Shift.IsUpper
                       ? (CapsLock.IsUpper ? key.LowerLetter : key.UpperLetter)
                       : (CapsLock.IsUpper ? key.UpperLetter : key.LowerLetter);
        }

        public void Confirm(string text)
        {
            if (ConfirmCommand != null)
            {
                ConfirmCommand.Execute(text);
            }

            RaiseConfirmedEvent();
        }

        public void Cancel()
        {
            if (CancelCommand != null)
            {
                CancelCommand.Execute(new object());
            }

            RaiseCanceledEvent();
        }

        public class VirtualKeyDictionary : Dictionary<System.Windows.Input.Key, VirtualKey>
        {
            public VirtualKeyDictionary()
            {
                foreach (var entry in KeyDataMapping.KeyDatas)
                {
                    var data = entry.Value;
                    Add(data.Key, new VirtualKey(data.Key, data.LowerLetter, data.UpperLetter, data.Text));
                }
            }
        }


        public class KeyData
        {
            public Key Key { get; set; }

            public string LowerLetter { get; set; }

            public string UpperLetter { get; set; }

            public string Text { get; set; }

            public KeyData()
            {
            }

            public KeyData(Key key, string lowerLetter, string upperLetter)
            {
                Key = key;
                LowerLetter = lowerLetter;
                UpperLetter = upperLetter;
            }

            public KeyData(Key key, string text)
            {
                Key = key;
                Text = text;
            }
        }
        public class KeyDataMapping
        {
            private static readonly Dictionary<Key, KeyData> keyDatas = new Dictionary<Key, KeyData>();

            public static Dictionary<Key, KeyData> KeyDatas
            {
                get { return keyDatas; }
            }

            static KeyDataMapping()
            {
                //Numberic
                KeyDatas.Add(Key.D1, new KeyData(Key.D1, "1", "!"));
                KeyDatas.Add(Key.D2, new KeyData(Key.D2, "2", "@"));
                KeyDatas.Add(Key.D3, new KeyData(Key.D3, "3", "#"));
                KeyDatas.Add(Key.D4, new KeyData(Key.D4, "4", "$"));
                KeyDatas.Add(Key.D5, new KeyData(Key.D5, "5", "%"));
                KeyDatas.Add(Key.D6, new KeyData(Key.D6, "6", "^"));
                KeyDatas.Add(Key.D7, new KeyData(Key.D7, "7", "&"));
                KeyDatas.Add(Key.D8, new KeyData(Key.D8, "8", "*"));
                KeyDatas.Add(Key.D9, new KeyData(Key.D9, "9", "("));
                KeyDatas.Add(Key.D0, new KeyData(Key.D0, "0", ")"));

                //Letter
                KeyDatas.Add(Key.A, new KeyData(Key.A, "a", "A"));
                KeyDatas.Add(Key.B, new KeyData(Key.B, "b", "B"));
                KeyDatas.Add(Key.C, new KeyData(Key.C, "c", "C"));
                KeyDatas.Add(Key.D, new KeyData(Key.D, "d", "D"));
                KeyDatas.Add(Key.E, new KeyData(Key.E, "e", "E"));
                KeyDatas.Add(Key.F, new KeyData(Key.F, "f", "F"));
                KeyDatas.Add(Key.G, new KeyData(Key.G, "g", "G"));
                KeyDatas.Add(Key.H, new KeyData(Key.H, "h", "H"));
                KeyDatas.Add(Key.I, new KeyData(Key.I, "i", "I"));
                KeyDatas.Add(Key.J, new KeyData(Key.J, "j", "J"));
                KeyDatas.Add(Key.K, new KeyData(Key.K, "k", "K"));
                KeyDatas.Add(Key.L, new KeyData(Key.L, "l", "L"));
                KeyDatas.Add(Key.M, new KeyData(Key.M, "m", "M"));
                KeyDatas.Add(Key.N, new KeyData(Key.N, "n", "N"));
                KeyDatas.Add(Key.O, new KeyData(Key.O, "o", "O"));
                KeyDatas.Add(Key.P, new KeyData(Key.P, "p", "P"));
                KeyDatas.Add(Key.Q, new KeyData(Key.Q, "q", "Q"));
                KeyDatas.Add(Key.R, new KeyData(Key.R, "r", "R"));
                KeyDatas.Add(Key.S, new KeyData(Key.S, "s", "S"));
                KeyDatas.Add(Key.T, new KeyData(Key.T, "t", "T"));
                KeyDatas.Add(Key.U, new KeyData(Key.U, "u", "U"));
                KeyDatas.Add(Key.V, new KeyData(Key.V, "v", "V"));
                KeyDatas.Add(Key.W, new KeyData(Key.W, "w", "W"));
                KeyDatas.Add(Key.X, new KeyData(Key.X, "x", "X"));
                KeyDatas.Add(Key.Y, new KeyData(Key.Y, "y", "Y"));
                KeyDatas.Add(Key.Z, new KeyData(Key.Z, "z", "Z"));

                //Symbol
                KeyDatas.Add(Key.OemTilde, new KeyData(Key.OemTilde, "`", "~"));
                KeyDatas.Add(Key.Add, new KeyData(Key.Add, "=", "+"));
                KeyDatas.Add(Key.Subtract, new KeyData(Key.Subtract, "-", "_"));
                KeyDatas.Add(Key.Divide, new KeyData(Key.Divide, "/", "?"));
                KeyDatas.Add(Key.OemBackslash, new KeyData(Key.OemBackslash, @"\", "|"));
                KeyDatas.Add(Key.OemComma, new KeyData(Key.OemComma, ",", "<"));
                KeyDatas.Add(Key.Decimal, new KeyData(Key.Decimal, ".", ">"));
                KeyDatas.Add(Key.OemOpenBrackets, new KeyData(Key.OemOpenBrackets, "[", "{"));
                KeyDatas.Add(Key.OemCloseBrackets, new KeyData(Key.OemCloseBrackets, "]", "}"));
                KeyDatas.Add(Key.OemSemicolon, new KeyData(Key.OemSemicolon, ";", ":"));
                KeyDatas.Add(Key.OemQuotes, new KeyData(Key.OemQuotes, "'", "\""));

                //Sticky
                KeyDatas.Add(Key.LeftShift, new KeyData(Key.LeftShift, "SHIFT", ""));
                KeyDatas.Add(Key.CapsLock, new KeyData(Key.CapsLock, "CapsLK", ""));

                //Function
                KeyDatas.Add(Key.Tab, new KeyData(Key.Tab, "TAB"));
                KeyDatas.Add(Key.Space, new KeyData(Key.Space, "SPACE"));
                KeyDatas.Add(Key.Back, new KeyData(Key.Back, "BACK"));
                KeyDatas.Add(Key.Enter, new KeyData(Key.Enter, "ENTER"));
                KeyDatas.Add(Key.Escape, new KeyData(Key.Escape, "ESC"));
            }
        }    
        
        #region Confirmed

        public static readonly RoutedEvent ConfirmedEvent = EventManager.RegisterRoutedEvent("Confirmed",
                                                                                     RoutingStrategy.Bubble,
                                                                                     typeof (
                                                                                         RoutedEventHandler),
                                                                                     typeof(VirtualKeyboard));

        public event RoutedEventHandler Confirmed
        {
            add { AddHandler(ConfirmedEvent, value); }
            remove { RemoveHandler(ConfirmedEvent, value); }
        }

        private void RaiseConfirmedEvent()
        {
            var newEventArgs = new RoutedEventArgs(ConfirmedEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion

        #region Canceled

        public static readonly RoutedEvent CanceledEvent = EventManager.RegisterRoutedEvent("Canceled",
                                                                                     RoutingStrategy.Bubble,
                                                                                     typeof (
                                                                                         RoutedEventHandler),
                                                                                     typeof(VirtualKeyboard));

        public event RoutedEventHandler Canceled
        {
            add { AddHandler(CanceledEvent, value); }
            remove { RemoveHandler(CanceledEvent, value); }
        }

        private void RaiseCanceledEvent()
        {
            var newEventArgs = new RoutedEventArgs(CanceledEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion

        #region Title

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof (string), typeof (VirtualKeyboard));

        #endregion
    }
}