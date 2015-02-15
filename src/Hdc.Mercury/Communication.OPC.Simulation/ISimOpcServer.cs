using System.Collections.Generic;

namespace Hdc.Mercury.Communication.OPC
{
    public interface ISimOpcServer
    {
        IDictionary<uint, ISimTag> Tags { get; set; }
    }
}
