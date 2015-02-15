using System.Collections.Generic;
using Hdc.Mercury.Communication;
using Hdc.Mercury.Communication.OPC;

namespace Hdc.Mercury.Communication.OPC
{
    public class SimOpcServer : ISimOpcServer
    {
        public SimOpcServer()
        {
            Tags = new Dictionary<uint, ISimTag>();
        }

        public IDictionary<uint, ISimTag> Tags { get; set; }
    }
}