using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public class DomainFacilityNodeMiddleChildTerminal<TThis, TParent> :
        DomainFacilityNodeMiddle<TThis, TParent, IFacilityNodeChildTerminal<TThis>>,
        IFacilityNodeMiddleChildTerminal<TThis, TParent>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TThis : class, IFacilityNodeMiddleChildTerminal<TThis, TParent>
    {
    }
}