using System.Windows.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Hdc.Controls
{
    public class BindableDialog : Control
    {
        private Window _window;
        private bool _isShowing;

        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register(
            "IsShow", typeof (bool), typeof (BindableDialog), new PropertyMetadata(IsShowOnPropertyChangedCallback
                                                                  ));

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            "DialogContentTemplate", typeof (DataTemplate), typeof (BindableDialog));

        public static readonly DependencyProperty DialogWindowStyleProperty = DependencyProperty.Register(
            "DialogWindowStyle", typeof (Style), typeof (BindableDialog));

        public static readonly DependencyProperty TopmostProperty = DependencyProperty.Register(
            "Topmost", typeof (bool), typeof (BindableDialog));

        public static readonly DependencyProperty DialogBackgroundProperty = DependencyProperty.Register(
            "DialogBackground", typeof (Brush), typeof (BindableDialog));

        static BindableDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (BindableDialog),
                                                     new FrameworkPropertyMetadata(typeof (BindableDialog)));
        }

        public BindableDialog()
        {
            DialogBackground = new SolidColorBrush(new Color() {A = 48, R = 0, G = 0, B = 0});
        }

        private static void IsShowOnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as BindableDialog;

            if (me == null)
                return;

            if ((bool)e.NewValue == false)
            {
                if (me._isShowing == false)
                    return;

                me._window.Close();
                me._window = null;
                me._isShowing = false;
                return;
            }

            if (me._isShowing)
                return;

            me.Init();

            me._isShowing = true;
            Debug.WriteLine("" + DateTime.Now + ": ShowDialog");

            Application.Current.Dispatcher.BeginInvoke(
                new Action(() =>
                {
                    me._window.ShowDialog();
                }),
                               DispatcherPriority.Normal);

            //me._window.ShowDialog();


            //            if (!me._isShowing)
            //                return;
            //me._window.Hide();
        }

        private void Init()
        {
            _window = new Window();


            if (DialogWindowStyle == null)
            {
                _window.AllowsTransparency = true;
                _window.WindowState = WindowState.Maximized;
                _window.WindowStyle = WindowStyle.None;
                _window.SizeToContent = SizeToContent.Manual;
                _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //                _window.Background = new SolidColorBrush(new Color() { A = 48, R = 0, G = 0, B = 0 });
                _window.Topmost = Topmost;
                _window.Background = DialogBackground;
            }
            else
            {
                _window.Style = DialogWindowStyle;
            }


            var content = DialogContentTemplate.LoadContent();
            _window.Content = content;
//            _window.Closing += new System.ComponentModel.CancelEventHandler(_window_Closing);
//            _window.Closed += new EventHandler(_window_Closed);
        }

//        void _window_Closed(object sender, EventArgs e)
//        {
//
//        }
//
//        private void _window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
//        {
//            e.Cancel = true;
//
//            IsShow = false;
//            _isShowing = false;
        //            _window.Hide();
//        }

        public DataTemplate DialogContentTemplate
        {
            get { return (DataTemplate) GetValue(DialogContentTemplateProperty); }
            set { SetValue(DialogContentTemplateProperty, value); }
        }

        public bool IsShow
        {
            get { return (bool) GetValue(IsShowProperty); }
            set { SetValue(IsShowProperty, value); }
        }


        public Style DialogWindowStyle
        {
            get { return (Style) GetValue(DialogWindowStyleProperty); }
            set { SetValue(DialogWindowStyleProperty, value); }
        }


        public bool Topmost
        {
            get { return (bool) GetValue(TopmostProperty); }
            set { SetValue(TopmostProperty, value); }
        }


        public Brush DialogBackground
        {
            get { return (Brush) GetValue(DialogBackgroundProperty); }
            set { SetValue(DialogBackgroundProperty, value); }
        }
    }
}