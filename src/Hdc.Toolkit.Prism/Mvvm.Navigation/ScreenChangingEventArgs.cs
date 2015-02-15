using System;

namespace Hdc.Mvvm.Navigation
{
    public class ScreenChangingEventArgs:EventArgs
    {
        public bool IsHandled { get; set; }

        public bool IsCanceled { get; set; }
    }
}