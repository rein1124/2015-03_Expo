namespace Hdc.Collections.Generic
{
    public interface IStructureMiddle<TThis, TParent, TChild> : IStructureParent<TThis, TChild>,
                                                                IStructureChild<TParent>
        where TThis : IStructureMiddle<TThis, TParent, TChild>
        where TChild : IStructureChild<TThis>
    {
    }
}