namespace Hdc.Collections.Generic
{
    public interface IChild<TParent>
    {
        TParent Parent { get; set; }

        int Index { get; set; }
    }
}