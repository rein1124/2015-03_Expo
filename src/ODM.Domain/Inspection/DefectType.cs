namespace ODM.Domain.Inspection
{
    public enum DefectType : int
    {
        //        Undefined=0,
        //        SpotBlue,
        //        SpotWhite,
        //        Bubble,
        //        Scratch,
        //        Fingermark,
        //        Impurity,
        //        Line,
        //        Chromatism,

        Undefined = 990, // Undefined
        ForeignSpot = 991, // 异物
        DirtyPoint = 993, // 污点
        NonuniformSpot = 996, // 不匀斑块
        HorizontalNonuniform = 997, // 横向不匀
        VerticalNonuniform = 998, // 纵向不匀

        //
        DefectType00 = 0, 
        DefectType01 = 1, 
        DefectType02 = 2, 
        DefectType04 = 4, 
        DefectType05 = 5, 
        DefectType06 = 6, 
        DefectType07 = 7, 
        DefectType08 = 8,
        DefectType09 = 9,
        DefectType10 = 10,
        DefectType11 = 11,
        DefectType12 = 12,
        DefectType13 = 13,
        DefectType14 = 14,
        DefectType15 = 15,
        DefectType16 = 16,

        PartExist = 100,
        PartNoExist = 101,
    }
}