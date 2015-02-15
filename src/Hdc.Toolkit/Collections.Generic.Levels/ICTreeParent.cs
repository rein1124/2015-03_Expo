using System.Collections.Generic;

namespace Hdc.Collections.Generic.Levels
{
//    public interface IGenericStructureMiddle<TThis,
//                                             TChild,
//                                             TThisContext,
//                                             TChildContext> : IGenericCTreeBase<TThisContext>,
//                                                              ICTreeParent<TChild>,
//                                                              ICTreeChild
//        where TThis : IGenericStructureMiddle<TThis,
//                          TChild,
//                          TThisContext,
//                          TChildContext>
//        where TChild : IGenericCTreeChild<TChildContext>
//    {
//    }

//    public interface ISampleMiddle<TChild> : ISampleParent<TChild>, ISampleChild
//        where TChild : ISampleChild
//    {
//    }


    public interface ICTreeParent<TChild> : ICTreeBase
        where TChild : ICTreeChild
    {
        IList<TChild> Children { get; }
    }
}