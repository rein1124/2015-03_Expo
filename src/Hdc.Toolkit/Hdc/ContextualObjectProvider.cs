using Microsoft.Practices.Unity;

namespace Hdc
{
    /*namespace Hdc
{
    public interface IContextualObjectProvider<out TObject, TObjectContext>
        where TObject : IContextNode<TObjectContext>
    {
        TObject Object { get; }
    }
}*/

/*    public class ContextualObjectProvider<T, TContext> : IContextualObjectProvider<T, TContext>
        where T : IContextNode<TContext>
    {
        [Dependency]
        public IObjectProvider<TContext> ObjectProvider { get; set; }

        [Dependency]
        public T Object { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Object.Initialize(ObjectProvider.Object);

            Object.BindingTo(ObjectProvider.Object);
        }
    }*/

    public class ContextualObjectProvider<TObject, TContext, TContextProvider>
        where TObject : IContextNode<TContext>
        where TContextProvider : IObjectProvider<TContext>
    {
        [Dependency]
        public TContextProvider ObjectProvider { get; set; }

        [Dependency]
        public TObject Object { get; set; }

        [InjectionMethod]
        public void Init()
        {
            Object.Initialize(ObjectProvider.Object);

            Object.BindingTo(ObjectProvider.Object);
        }
    }
}