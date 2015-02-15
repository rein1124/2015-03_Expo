using System.Collections.Generic;

namespace Hdc.Modularity
{
    public interface IComponentReader
    {
//        IDictionary<string,string> Read(string path);
        IList<ComponentValueEntry> Read();
    }
}