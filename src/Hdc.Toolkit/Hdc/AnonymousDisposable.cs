using System;

namespace Hdc
{
    class AnonymousDisposable : IDisposable
    {
        Action dispose;
        public AnonymousDisposable(Action dispose)
        {
            this.dispose = dispose;
        }

        public void Dispose()
        {
            dispose();
        }
    }
}