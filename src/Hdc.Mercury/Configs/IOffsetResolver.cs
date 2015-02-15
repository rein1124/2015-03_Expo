using System.Collections.Generic;

namespace Hdc.Mercury.Configs
{
    public interface IOffsetResolver
    {
        IEnumerable<int> GetOffsets(IEnumerable<OffsetMark> offsetMarks);
    }
}