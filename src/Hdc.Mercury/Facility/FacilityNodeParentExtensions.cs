namespace Hdc.Mercury
{
    public static class FacilityNodeParentExtensions
    {
        public static TC GetDescendant<TP, TC>(this IFacilityNodeParent<TP, TC> node,
                                               int index0)
            where TP : IFacilityNodeParent<TP, TC>
            where TC : IFacilityNodeChild<TC, TP>
        {
            return node.Children[index0];
        }

        public static TC GetDescendant<TP, TM, TC>(this IFacilityNodeParent<TP, TM> node,
                                                   int index0,
                                                   int index1)
            where TP : IFacilityNodeParent<TP, TM>
            where TM : IFacilityNodeMiddle<TM, TP, TC>
            where TC : IFacilityNodeChild<TC, TM>
        {
            return node.GetDescendant<TP, TM>(index0).Children[index1];
        }

        public static TC GetDescendant<TP, TM1, TM2, TC>(this IFacilityNodeParent<TP, TM1> node,
                                                         int index0,
                                                         int index1,
                                                         int index2)
            where TP : IFacilityNodeParent<TP, TM1>
            where TM1 : IFacilityNodeMiddle<TM1, TP, TM2>
            where TM2 : IFacilityNodeMiddle<TM2, TM1, TC>
            where TC : IFacilityNodeChild<TC, TM2>
        {
            return node.GetDescendant<TP, TM1, TM2>(index0, index1).Children[index2];
        }

        public static TC GetDescendant<TP, TM1, TM2, TM3, TC>(this IFacilityNodeParent<TP, TM1> node,
                                                              int index0,
                                                              int index1,
                                                              int index2,
                                                              int index3)
            where TP : IFacilityNodeParent<TP, TM1>
            where TM1 : IFacilityNodeMiddle<TM1, TP, TM2>
            where TM2 : IFacilityNodeMiddle<TM2, TM1, TM3>
            where TM3 : IFacilityNodeMiddle<TM3, TM2, TC>
            where TC : IFacilityNodeChild<TC, TM3>
        {
            return node.GetDescendant<TP, TM1, TM2, TM3>(index0, index1, index2).Children[index3];
        }
    }
}