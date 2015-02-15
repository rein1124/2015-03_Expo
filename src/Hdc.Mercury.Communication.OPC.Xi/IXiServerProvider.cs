using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Advosol.Paxi;

namespace Hdc.Mercury.Communication.OPC.Xi
{
    public interface IXiServerProvider
    {
        XiServer XiServer { get; }
    }
}
