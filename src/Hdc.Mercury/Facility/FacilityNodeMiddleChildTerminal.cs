using System;
using System.Collections.Generic;
using Hdc.Collections.Generic;
using Hdc.Mercury;

namespace Hdc.Mercury
{
    public abstract class FacilityNodeMiddleChildTerminal<TThis, TParent> :
        FacilityNodeMiddle<TThis, TParent, IFacilityNodeChildTerminal<TThis>>,
        IFacilityNodeMiddleChildTerminal<TThis, TParent>
        where TParent : IFacilityNodeParent<TParent, TThis>
        where TThis : class, IFacilityNodeMiddleChildTerminal<TThis, TParent>
    {
    }
}