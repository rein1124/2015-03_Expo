namespace Hdc.Collections.Generic
{
    public interface IGenericStructureBase<TContext>
    {
        void Initialize(TContext context);

        TContext Context { get; }
    }
}