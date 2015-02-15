namespace Hdc.Collections.Generic
{
    public interface IStructureChild<TParent>
    {
        TParent Parent { get; set; }

        int Index { get; set; }
    }
}