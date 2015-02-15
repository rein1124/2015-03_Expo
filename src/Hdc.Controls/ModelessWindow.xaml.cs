using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Hdc.Controls
{
    /// <summary>
    /// Interaction logic for ModelessWindow.xaml
    /// </summary>
    public partial class ModelessWindow : Window
    {
        private bool? ModalDialogResult = null;
        private IntPtr ownerHandle;
        private IntPtr handle;

        public ModelessWindow(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;

            ownerHandle = (new System.Windows.Interop.WindowInteropHelper(this.Owner)).Handle;
            handle = (new System.Windows.Interop.WindowInteropHelper(this)).Handle;
            NativeMethods.EnableWindow(handle, true);
            NativeMethods.SetForegroundWindow(handle);
            this.Closing += new System.ComponentModel.CancelEventHandler(Window_Closing);
        }

        public bool? ShowModelessDialog()
        {
            NativeMethods.EnableWindow(ownerHandle, false);
            new ShowAndWaitHelper(this).ShowAndWait();
            return ModalDialogResult;
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Closing -= new System.ComponentModel.CancelEventHandler(Window_Closing);
            NativeMethods.EnableWindow(handle, false);
            NativeMethods.EnableWindow(ownerHandle, true);
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            ModalDialogResult = true;
            this.Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ModalDialogResult = false;
            this.Close();
        }
    }



    public class NativeMethods
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hwnd, int index);
    }

    internal sealed class ShowAndWaitHelper
    {
        private readonly Window _window;
        private DispatcherFrame _dispatcherFrame;
        internal ShowAndWaitHelper(Window window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("panel");
            }
            this._window = window;
        }
        internal void ShowAndWait()
        {
            if (this._dispatcherFrame != null)
            {
                throw new InvalidOperationException("Cannot call ShowAndWait while waiting for a previous call to ShowAndWait to return.");
            }
            this._window.Closed += new EventHandler(this.OnPanelClosed);
            _window.Show();
            this._dispatcherFrame = new DispatcherFrame();
            Dispatcher.PushFrame(this._dispatcherFrame);
        }
        private void OnPanelClosed(object source, EventArgs eventArgs)
        {
            this._window.Closed -= new EventHandler(this.OnPanelClosed);
            if (this._dispatcherFrame == null)
            {
                return;
            }
            this._window.Closed -= new EventHandler(this.OnPanelClosed);
            this._dispatcherFrame.Continue = false;
            this._dispatcherFrame = null;
        }
    }
}
