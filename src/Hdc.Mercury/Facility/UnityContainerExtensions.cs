using Microsoft.Practices.Unity;

namespace Hdc.Mercury
{
    public static class UnityContainerExtensions
    {
        public static void RegisterTypes<TParent,
                                         TParentImpl,
                                         TChild,
                                         TChildImpl>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IFacilityNodeMiddleParentTerminal<TParent, TChild>
            where TChild : IFacilityNodeMiddleChildTerminal<TChild, TParent>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypes<TParent,
                                         TParentImpl,
                                         TMiddle,
                                         TMiddleImpl,
                                         TChild,
                                         TChildImpl>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IFacilityNodeMiddleParentTerminal<TParent, TMiddle>
            where TMiddle : IFacilityNodeMiddle<TMiddle, TParent, TChild>
            where TMiddleImpl : TMiddle
            where TChild : IFacilityNodeMiddleChildTerminal<TChild, TMiddle>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle, TMiddleImpl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypes<TParent,
                                         TParentImpl,
                                         TMiddle1,
                                         TMiddle1Impl,
                                         TMiddle2,
                                         TMiddle2Impl,
                                         TChild,
                                         TChildImpl>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IFacilityNodeMiddleParentTerminal<TParent, TMiddle1>
            where TMiddle1 : IFacilityNodeMiddle<TMiddle1, TParent, TMiddle2>
            where TMiddle1Impl : TMiddle1
            where TMiddle2 : IFacilityNodeMiddle<TMiddle2, TMiddle1, TChild>
            where TMiddle2Impl : TMiddle2
            where TChild : IFacilityNodeMiddleChildTerminal<TChild, TMiddle2>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle1, TMiddle1Impl>();
            container.RegisterType<TMiddle2, TMiddle2Impl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypes<TParent,
                                         TParentImpl,
                                         TMiddle1,
                                         TMiddle1Impl,
                                         TMiddle2,
                                         TMiddle2Impl,
                                         TMiddle3,
                                         TMiddle3Impl,
                                         TChild,
                                         TChildImpl>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IFacilityNodeMiddleParentTerminal<TParent, TMiddle1>
            where TMiddle1 : IFacilityNodeMiddle<TMiddle1, TParent, TMiddle2>
            where TMiddle1Impl : TMiddle1
            where TMiddle2 : IFacilityNodeMiddle<TMiddle2, TMiddle1, TMiddle3>
            where TMiddle2Impl : TMiddle2
            where TMiddle3 : IFacilityNodeMiddle<TMiddle3, TMiddle2, TChild>
            where TMiddle3Impl : TMiddle3
            where TChild : IFacilityNodeMiddleChildTerminal<TChild, TMiddle3>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle1, TMiddle1Impl>();
            container.RegisterType<TMiddle2, TMiddle2Impl>();
            container.RegisterType<TMiddle3, TMiddle3Impl>();
            container.RegisterType<TChild, TChildImpl>();
        }

        public static void RegisterTypes<TParent,
                                         TParentImpl,
                                         TMiddle1,
                                         TMiddle1Impl,
                                         TMiddle2,
                                         TMiddle2Impl,
                                         TMiddle3,
                                         TMiddle3Impl,
                                         TMiddle4,
                                         TMiddle4Impl,
                                         TChild,
                                         TChildImpl>(
            this IUnityContainer container)
            where TParentImpl : TParent
            where TParent : IFacilityNodeMiddleParentTerminal<TParent, TMiddle1>
            where TMiddle1 : IFacilityNodeMiddle<TMiddle1, TParent, TMiddle2>
            where TMiddle1Impl : TMiddle1
            where TMiddle2 : IFacilityNodeMiddle<TMiddle2, TMiddle1, TMiddle3>
            where TMiddle2Impl : TMiddle2
            where TMiddle3 : IFacilityNodeMiddle<TMiddle3, TMiddle2, TMiddle4>
            where TMiddle3Impl : TMiddle3
            where TMiddle4 : IFacilityNodeMiddle<TMiddle4, TMiddle3, TChild>
            where TMiddle4Impl : TMiddle4
            where TChild : IFacilityNodeMiddleChildTerminal<TChild, TMiddle4>
            where TChildImpl : TChild
        {
            container.RegisterType<TParent, TParentImpl>();
            container.RegisterType<TMiddle1, TMiddle1Impl>();
            container.RegisterType<TMiddle2, TMiddle2Impl>();
            container.RegisterType<TMiddle3, TMiddle3Impl>();
            container.RegisterType<TMiddle4, TMiddle4Impl>();
            container.RegisterType<TChild, TChildImpl>();
        }
    }
}