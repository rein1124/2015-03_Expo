using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hdc.Mvvm;
using Microsoft.Practices.ServiceLocation;

namespace Hdc.Collections.Generic
{
    public abstract class ViewModelContextNodeMiddleChildTerminal<TThis,
                                                                  TThisContext,
                                                                  TParent,
                                                                  TParentContext> : ViewModelContextNodeMiddle<
                                                                                        TThis,
                                                                                        TThisContext,
                                                                                        TParent,
                                                                                        TParentContext,
                                                                                        IContextNodeChildTerminal
                                                                                        <TThis, TThisContext>,
                                                                                        object>,
                                                                                    IContextNodeMiddleChildTerminal<
                                                                                        TThis,
                                                                                        TThisContext,
                                                                                        TParent,
                                                                                        TParentContext>
        where TParent : IContextNodeParent<
                            TParent,
                            TParentContext,
                            TThis,
                            TThisContext>
        where TThis : class, IContextNodeMiddleChildTerminal<
                                 TThis,
                                 TThisContext,
                                 TParent,
                                 TParentContext>
    {
        protected override IEnumerable<object> GetChildContexts(TThisContext thisContext)
        {
            yield break;
        }
    }
}