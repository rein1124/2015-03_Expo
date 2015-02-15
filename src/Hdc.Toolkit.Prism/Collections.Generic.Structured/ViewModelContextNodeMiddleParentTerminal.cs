using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hdc.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Collections.Generic
{
    public abstract class ViewModelContextNodeMiddleParentTerminal<TThis,
                                                                   TThisContext,
                                                                   TChild,
                                                                   TChildContext> : ViewModelContextNodeMiddle<
                                                                                        TThis,
                                                                                        TThisContext,
                                                                                        IContextNodeParentTerminal
                                                                                        <TThis, TThisContext>,
                                                                                        object,
                                                                                        TChild,
                                                                                        TChildContext>,
                                                                                    IContextNodeMiddleParentTerminal
                                                                                        <TThis,
                                                                                        TThisContext,
                                                                                        TChild,
                                                                                        TChildContext>
        where TChild : IContextNodeChild<
                           TChild,
                           TChildContext,
                           TThis,
                           TThisContext>
        where TThis : class, IContextNodeMiddleParentTerminal<
                                 TThis,
                                 TThisContext,
                                 TChild,
                                 TChildContext>
    {
    }
}