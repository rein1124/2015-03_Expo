using System;
using System.Windows;
using System.Windows.Threading;
using Hdc.Mvvm;

namespace Hdc.Mvvm
{
    public class WindowController : IWindowController
    {
        public static IWindowController Create(Window window)
        {
            return new WindowController(window);
        }

        private readonly Window _window;

        public WindowController(Window window)
        {
            _window = window;
        }

        public void Minimize()
        {
            if (!_window.CheckAccess())
            {
                _window.Dispatcher.BeginInvoke(new Action(Minimize));
            }
            else
            {
                _window.WindowState = WindowState.Minimized;
            }
        }

        public void Maximize()
        {
            if (!_window.CheckAccess())
            {
                _window.Dispatcher.BeginInvoke(new Action(Maximize));
            }
            else
            {
                _window.WindowState = WindowState.Maximized;
            }
        }

        public void Show()
        {
            _window.Show();
        }

        public void Hide()
        {
            _window.Hide();
        }

        public bool Activate()
        {
            return _window.Activate();
        }
    }
}