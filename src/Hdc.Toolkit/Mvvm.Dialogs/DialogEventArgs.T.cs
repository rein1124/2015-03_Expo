using Hdc;

namespace Hdc.Mvvm.Dialogs
{
    public class DialogEventArgs<T> : EventArgs<T>
    {
        public DialogEventArgs()
        {
        }

        public DialogEventArgs(T data) : base(data)
        {
        }

        public DialogEventArgs(bool isCanceled = false)
        {
            IsCanceled = isCanceled;
        }

        public DialogEventArgs(bool isCanceled = false, T data = default(T))
            : base(data)
        {
            IsCanceled = isCanceled;
        }

        public bool IsCanceled { get; set; }
    }
}