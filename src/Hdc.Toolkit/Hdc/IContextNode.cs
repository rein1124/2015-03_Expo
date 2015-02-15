namespace Hdc
{
    public interface IContextNode<TContext> : IBindable<TContext>
    {
        void Initialize(TContext context);

        void Reset();

        TContext Context { get; }
    }
}