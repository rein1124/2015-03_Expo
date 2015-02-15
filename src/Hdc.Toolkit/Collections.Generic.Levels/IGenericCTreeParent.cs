namespace Hdc.Collections.Generic.Levels
{
    public interface IGenericCTreeParent<TThis,
                                         TChild,
                                         TThisContext,
                                         TChildContext> : IGenericCTreeBase<TThisContext>,
                                                          ICTreeParent<TChild>
        where TThis : IGenericCTreeParent<TThis,
                          TChild,
                          TThisContext,
                          TChildContext>
        where TChild : IGenericCTreeChild<TChildContext>
    {
    }
}