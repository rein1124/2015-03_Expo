using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Diagnostics.CodeAnalysis;

namespace Hdc.Windows
{
    // Thanks to Daniel Grunwald for this implementation
    // See: http://www.codeproject.com/KB/cs/WeakEvents.aspx
    public abstract class WeakEventManagerBase<TManager, TSource> : WeakEventManager
        where TManager : WeakEventManagerBase<TManager, TSource>, new()
        where TSource : class
    {
        protected WeakEventManagerBase() { }


        private static TManager CurrentManager
        {
            get
            {
                Type managerType = typeof(TManager);
                TManager currentManager = (TManager)GetCurrentManager(managerType);
                if (currentManager == null)
                {
                    currentManager = new TManager();
                    SetCurrentManager(managerType, currentManager);
                }
                return currentManager;
            }
        }


        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static void AddListener(TSource source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        public static void RemoveListener(TSource source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        protected sealed override void StartListening(object source)
        {
            StartListening((TSource)source);
        }

        protected sealed override void StopListening(object source)
        {
            StopListening((TSource)source);
        }

        protected abstract void StartListening(TSource source);

        protected abstract void StopListening(TSource source);
    }
}
