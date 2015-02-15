using Hdc.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Hdc.Unity
{
    public static class ContextNodeUnityExtensions
    {
        public static void RegisterTypes<T,
                                         TImpl,
                                         TContext>(
            this IUnityContainer container)
            where TImpl : T
            where T : IContextNodeMiddleTerminal<T, TContext>
        {
            container.RegisterType<T, TImpl>();
        }

        public static void RegisterTypesForContextNode<TParent,
                                                       TParentImpl,
                                                       TParentContext,
                                                       TChild,
                                                       TChildImpl,
                                                       TChildContext>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IContextNodeMiddleParentTerminal<TParent,
                                TParentContext,
                                TChild,
                                TChildContext>
            where TChild : IContextNodeMiddleChildTerminal<TChild,
                               TChildContext,
                               TParent,
                               TParentContext>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypesForContextNode<TParent,
                                                       TParentImpl,
                                                       TParentContext,
                                                       TMiddle,
                                                       TMiddleImpl,
                                                       TMiddleContext,
                                                       TChild,
                                                       TChildImpl,
                                                       TChildContext
                                                       >(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IContextNodeMiddleParentTerminal<TParent,
                                TParentContext,
                                TMiddle,
                                TMiddleContext>
            where TMiddle : IContextNodeMiddle<TMiddle,
                                TMiddleContext,
                                TParent,
                                TParentContext,
                                TChild,
                                TChildContext>
            where TMiddleImpl : TMiddle
            where TChild : IContextNodeMiddleChildTerminal<TChild,
                               TChildContext,
                               TMiddle,
                               TMiddleContext>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle, TMiddleImpl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypesForContextNode<TParent,
                                                       TParentImpl,
                                                       TParentContext,
                                                       TMiddle1,
                                                       TMiddleImpl1,
                                                       TMiddleContext1,
                                                       TMiddle2,
                                                       TMiddleImpl2,
                                                       TMiddleContext2,
                                                       TChild,
                                                       TChildImpl,
                                                       TChildContext>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IContextNodeMiddleParentTerminal<TParent,
                                TParentContext,
                                TMiddle1,
                                TMiddleContext1>
            where TMiddle1 : IContextNodeMiddle<TMiddle1,
                                 TMiddleContext1,
                                 TParent,
                                 TParentContext,
                                 TMiddle2,
                                 TMiddleContext2>
            where TMiddleImpl1 : TMiddle1
            where TMiddle2 :
                IContextNodeMiddle<TMiddle2,
                    TMiddleContext2,
                    TMiddle1,
                    TMiddleContext1,
                    TChild,
                    TChildContext>
            where TMiddleImpl2 : TMiddle2
            where TChild : IContextNodeMiddleChildTerminal<TChild,
                               TChildContext,
                               TMiddle2,
                               TMiddleContext2>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle1, TMiddleImpl1>();
            container.RegisterType<TMiddle2, TMiddleImpl2>();
            container.RegisterType<TChild, TChildImpl>();
        }
    }
}