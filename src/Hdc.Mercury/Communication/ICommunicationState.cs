using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Mercury.Communication
{
    public interface ICommunicationState
    {
        int StateCode { get; }
    }
}
