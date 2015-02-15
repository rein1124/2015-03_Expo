namespace Hdc.Collections.Generic.Levels
{
    public interface IGenericCTreeBase<TContext>
    {
        void Initialize(TContext context);

        TContext Context { get; }
    }
}