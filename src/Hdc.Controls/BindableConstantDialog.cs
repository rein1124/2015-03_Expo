using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Hdc.Controls
{
    public class BindableConstantDialog : Control
    {
        private Window _window;
        private bool _isShowing;

        public static readonly DependencyProperty IsShowProperty = DependencyProperty.Register(
            "IsShow", typeof (bool), typeof (BindableConstantDialog), new PropertyMetadata(OnPropertyChangedCallback
                                                                          ));

        public static readonly DependencyProperty DialogContentTemplateProperty = DependencyProperty.Register(
            "DialogContentTemplate", typeof (DataTemplate), typeof (BindableConstantDialog));

        public static readonly DependencyProperty DialogWindowStyleProperty = DependencyProperty.Register(
            "DialogWindowStyle", typeof (Style), typeof (BindableConstantDialog));

        public static readonly DependencyProperty TopmostProperty = DependencyProperty.Register(
            "Topmost", typeof (bool), typeof (BindableConstantDialog));

        public static readonly DependencyProperty DialogBackgroundProperty = DependencyProperty.Register(
            "DialogBackground", typeof (Brush), typeof (BindableConstantDialog));

        static BindableConstantDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (BindableConstantDialog),
                                                     new FrameworkPropertyMetadata(typeof (BindableConstantDialog)));
        }

        public BindableConstantDialog()
        {
            DialogBackground = new SolidColorBrush(new Color() {A = 48, R = 0, G = 0, B = 0});
        }

        private static void OnPropertyChangedCallback(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var me = s as BindableConstantDialog;
            if (me == null) return;

            if (me._window == null)
            {
                me.Init();
            }

            if ((bool) e.NewValue)
            {
//                new Task(() =>
//                             {
                me._isShowing = true;
                Debug.WriteLine("" + DateTime.Now + ": ShowDialog");
                me._window.ShowDialog();

//                             }).Start(TaskScheduler.FromCurrentSynchronizationContext());
            }

            if (!me._isShowing) return;

            me._window.Hide();
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
            _window.Closing += new System.ComponentModel.CancelEventHandler(_window_Closing);
            _window.Closed += new EventHandler(_window_Closed);
        }

        void _window_Closed(object sender, EventArgs e)
        {
            
        }

        private void _window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            IsShow = false;
            _isShowing = false;
//            _window.Hide();
        }

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