using System;

namespace Hdc.Windows
{
    public class WeakEventListener<TInstance, TSender, TEventArgs>
           where TInstance : class
    {
        private readonly WeakReference instanceReference;
        private Action<TInstance, TSender, TEventArgs> handlerAction;
        private Action<WeakEventListener<TInstance, TSender, TEventArgs>> detachAction;

        public WeakEventListener(
            TInstance instance,
            Action<TInstance, TSender, TEventArgs> handlerAction,
            Action<WeakEventListener<TInstance, TSender, TEventArgs>> detachAction)
        {
            this.instanceReference = new WeakReference(instance);
            this.handlerAction = handlerAction;
            this.detachAction = detachAction;
        }

        public void Detach()
        {
            if (this.detachAction != null)
            {
                this.detachAction(this);
                this.detachAction = null;
            }
        }

        public void OnEvent(TSender sender, TEventArgs e)
        {
            var instance = this.instanceReference.Target as TInstance;
            if (instance != null)
            {
                if (this.handlerAction != null)
                {
                    this.handlerAction((TInstance)this.instanceReference.Target, sender, e);
                }
            }
            else
            {
                this.Detach();
            }
        }
    }
}