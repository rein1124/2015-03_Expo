//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 11.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void FindGrowingRegion (HObject ho_Image, out HObject ho_FoundRegion, out HObject ho_MaskRegion, 
      HTuple hv_MaskCenterX, HTuple hv_MaskCenterY, HTuple hv_MaskWidth, HTuple hv_MaskHeight, 
      HTuple hv_Angle, HTuple hv_Features, HTuple hv_Min, HTuple hv_Max)
  {



    // Stack for temporary objects 
    HObject[] OTemp = new HObject[20];

    // Local iconic variables 

    HObject ho_MaskImage, ho_ImageMedian, ho_Regions;
    HObject ho_RegionFillUp, ho_RegionClosing;

    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_FoundRegion);
    HOperatorSet.GenEmptyObj(out ho_MaskRegion);
    HOperatorSet.GenEmptyObj(out ho_MaskImage);
    HOperatorSet.GenEmptyObj(out ho_ImageMedian);
    HOperatorSet.GenEmptyObj(out ho_Regions);
    HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
    HOperatorSet.GenEmptyObj(out ho_RegionClosing);

    ho_MaskRegion.Dispose();
    HOperatorSet.GenRectangle2(out ho_MaskRegion, hv_MaskCenterY, hv_MaskCenterX, 
        hv_Angle.TupleRad(), hv_MaskWidth, hv_MaskHeight);
    ho_MaskImage.Dispose();
    HOperatorSet.ReduceDomain(ho_Image, ho_MaskRegion, out ho_MaskImage);

    //get_domain (MaskImage, RegionDomain)


    ho_ImageMedian.Dispose();
    HOperatorSet.MedianImage(ho_MaskImage, out ho_ImageMedian, "circle", 6, "continued");
    ho_Regions.Dispose();
    HOperatorSet.Regiongrowing(ho_ImageMedian, out ho_Regions, 1, 1, 1.2, 10000);
    ho_RegionFillUp.Dispose();
    HOperatorSet.FillUpShape(ho_Regions, out ho_RegionFillUp, "area", 1, 100);
    //closing_circle (RegionFillUp, RegionClosing, 200)
    ho_RegionClosing.Dispose();
    HOperatorSet.ClosingCircle(ho_RegionFillUp, out ho_RegionClosing, 5);
    HOperatorSet.ClosingRectangle1(ho_RegionClosing, out OTemp[0], 200, 200);
    ho_RegionClosing.Dispose();
    ho_RegionClosing = OTemp[0];
    //select_shape (RegionClosing, FoundRegion, 'area', 'and', Min, Max)
    ho_FoundRegion.Dispose();
    HOperatorSet.SelectShape(ho_RegionClosing, out ho_FoundRegion, hv_Features, "and", 
        hv_Min, hv_Max);

    ho_MaskImage.Dispose();
    ho_ImageMedian.Dispose();
    ho_Regions.Dispose();
    ho_RegionFillUp.Dispose();
    ho_RegionClosing.Dispose();

    return;
  }

  public void FindS1404SurfaceA (HObject ho_Image, out HObject ho_BinImage, HTuple hv_DilationRadius)
  {



    // Stack for temporary objects 
    HObject[] OTemp = new HObject[20];

    // Local iconic variables 

    HObject ho_MaskRegions, ho_FoundRegions, ho_LineRegions;
    HObject ho_FoundRegion, ho_MaskRegion, ho_ConnectedFoundRegions;
    HObject ho_ClosedRegions, ho_ReducedImage;


    // Local control variables 

    HTuple hv_Width = null, hv_Height = null;

    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_BinImage);
    HOperatorSet.GenEmptyObj(out ho_MaskRegions);
    HOperatorSet.GenEmptyObj(out ho_FoundRegions);
    HOperatorSet.GenEmptyObj(out ho_LineRegions);
    HOperatorSet.GenEmptyObj(out ho_FoundRegion);
    HOperatorSet.GenEmptyObj(out ho_MaskRegion);
    HOperatorSet.GenEmptyObj(out ho_ConnectedFoundRegions);
    HOperatorSet.GenEmptyObj(out ho_ClosedRegions);
    HOperatorSet.GenEmptyObj(out ho_ReducedImage);

    ho_MaskRegions.Dispose();
    HOperatorSet.GenEmptyRegion(out ho_MaskRegions);
    ho_FoundRegions.Dispose();
    HOperatorSet.GenEmptyRegion(out ho_FoundRegions);
    ho_LineRegions.Dispose();
    HOperatorSet.GenEmptyRegion(out ho_LineRegions);


    //****** Top Left
    //** TL3
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2662, 766, 
        400, 36, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(766-30)).TupleConcat(
        2662-50), (new HTuple(766+30)).TupleConcat(2662+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** TL0
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2072, 1280, 
        36, 400, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(1280-50)).TupleConcat(
        2072-30), (new HTuple(1280+50)).TupleConcat(2072+30));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** TL1.5
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2202, 907, 
        100, 320, -45, (new HTuple("row")).TupleConcat("column"), (new HTuple(907-50)).TupleConcat(
        2202-50), (new HTuple(907+50)).TupleConcat(2202+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];


    //****** Top Right
    //** TR3
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6342, 740, 
        400, 40, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(740-30)).TupleConcat(
        6342-50), (new HTuple(740+30)).TupleConcat(6342+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** TR0
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6892, 1327, 
        40, 400, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(1327-50)).TupleConcat(
        6892-30), (new HTuple(1327+50)).TupleConcat(6892+30));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** TR1.5
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6752, 907, 
        80, 300, 45, (new HTuple("row")).TupleConcat("column"), (new HTuple(907-50)).TupleConcat(
        6752-50), (new HTuple(907+50)).TupleConcat(6752+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];





    //****** Bottom Left
    //** BL Vertical
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2114, 9600, 
        40, 400, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(9600-50)).TupleConcat(
        2114-30), (new HTuple(9600+50)).TupleConcat(2114+30));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** BL Horizontal
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2802, 10200, 
        400, 40, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(10200-30)).TupleConcat(
        2802-50), (new HTuple(10200+30)).TupleConcat(2802+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** BL Center
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2246, 10030, 
        100, 300, 45, (new HTuple("row")).TupleConcat("column"), (new HTuple(10040-50)).TupleConcat(
        2256-50), (new HTuple(10040+50)).TupleConcat(2256+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];



    //****** Bottom Right
    //** BR Hori
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6352, 10140, 
        400, 45, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(10140-30)).TupleConcat(
        6352-50), (new HTuple(10140+30)).TupleConcat(6352+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** BR Vertical
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6912, 9600, 
        45, 400, 0, (new HTuple("row")).TupleConcat("column"), (new HTuple(9600-50)).TupleConcat(
        6912-30), (new HTuple(9600+50)).TupleConcat(6912+30));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];

    //** BR Center
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6802, 10030, 
        100, 300, -45, (new HTuple("row")).TupleConcat("column"), (new HTuple(10030-50)).TupleConcat(
        6802-50), (new HTuple(10030+50)).TupleConcat(6802+50));
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];



    //****** Left
    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 2071, 1839, 50, 512, 0, ['row','column'], [(1839-75),(2071-20)], [(1839+75),(2071+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2071, 1839, 
        50, 512, 0, "column", 2071-20, 2071+20);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 2087, 3839, 50, 1536, 0, ['row','column'], [(3839-75),(2087-20)], [(3839+75),(2087+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2087, 3839, 
        55, 1536, 0, "column", 2087-30, 2087+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 2090, 5839, 50, 1024, 0, ['row','column'], [(5839-75),(2090-20)], [(5839+75),(2090+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2090, 5839, 
        55, 1024, 0, "column", 2090-30, 2090+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 2111, 7839, 50, 1536, 0, ['row','column'], [(7839-75),(2111-20)], [(7839+75),(2111+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2111, 7839, 
        55, 1536, 0, "column", 2111-30, 2111+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 2123, 9439, 50, 300, 0, ['row','column'], [(9439-75),(2123-20)], [(9439+75),(2123+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 2123, 9439, 
        55, 300, 0, "column", 2123-30, 2123+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];



    //*Right
    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 6897, 1839, 50, 512, 0, ['row','column'], [(1839-100),(6897-20)], [(1839+100),(6897+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6897, 1839, 
        55, 512, 0, "column", 6897-30, 6897+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 6906, 3839, 50, 1536, 0, ['row','column'], [(3839-100),(6906-20)], [(3839+100),(6906+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6906, 3839, 
        55, 1536, 0, "column", 6906-30, 6906+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 6918, 5839, 50, 1024, 0, ['row','column'], [(5839-100),(6918-20)], [(5839+100),(6918+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6918, 5839, 
        55, 1024, 0, "column", 6918-30, 6918+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 6930, 7839, 50, 1536, 0, ['row','column'], [(7839-100),(6930-20)], [(7839+100),(6930+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6930, 7839, 
        55, 1536, 0, "column", 6930-30, 6930+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 6942, 9439, 50, 300, 0, ['row','column'], [(9439-100),(6942-20)], [(9439+100),(6942+20)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 6942, 9439, 
        55, 300, 0, "column", 6942-30, 6942+30);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];




    //* Top
    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 5850, 732, 768, 50, 0, ['row','column'], [(732-20),(5850-100)], [(732+20),(5850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 5850, 732, 
        768, 50, 0, "row", 732-25, 732+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 4850, 736, 768, 50, 0, ['row','column'], [(736-20),(4850-100)], [(736+20),(4850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 4850, 736, 
        768, 50, 0, "row", 736-25, 736+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 3850, 740, 768, 50, 0, ['row','column'], [(740-20),(3850-100)], [(740+20),(3850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 3850, 740, 
        768, 50, 0, "row", 740-25, 740+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 3050, 745, 512, 50, 0, ['row','column'], [(745-20),(3050-100)], [(745+20),(3050+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 3050, 745, 
        512, 50, 0, "row", 745-25, 745+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];




    //****** Bottom
    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 5850, 10191, 768, 50, 0, ['row','column'], [(10191-20),(5850-100)], [(10191+20),(5850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 5850, 10191, 
        768, 50, 0, "row", 10191-25, 10191+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 4850, 10195, 768, 50, 0, ['row','column'], [(10195-20),(4850-100)], [(10195+20),(4850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 4850, 10195, 
        768, 50, 0, "row", 10195-25, 10195+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 3850, 10201, 768, 50, 0, ['row','column'], [(10201-20),(3850-100)], [(10201+20),(3850+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 3850, 10201, 
        768, 50, 0, "row", 10201-25, 10201+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];

    //FindGrowingRegion (Image, FoundRegion, MaskRegion, 3050, 10203, 512, 50, 0, ['row','column'], [(10203-20),(3050-100)], [(10203+20),(3050+100)])
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    FindGrowingRegion(ho_Image, out ho_FoundRegion, out ho_MaskRegion, 3050, 10203, 
        512, 50, 0, "row", 10203-25, 10203+25);
    //dilation_circle (FoundRegion, FoundRegion, DilationRadius)
    HOperatorSet.Union2(ho_MaskRegion, ho_MaskRegions, out OTemp[0]);
    ho_MaskRegions.Dispose();
    ho_MaskRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_FoundRegions, out OTemp[0]);
    ho_FoundRegions.Dispose();
    ho_FoundRegions = OTemp[0];
    HOperatorSet.Union2(ho_FoundRegion, ho_LineRegions, out OTemp[0]);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];



    //****** Get result

    ho_ConnectedFoundRegions.Dispose();
    HOperatorSet.Connection(ho_FoundRegions, out ho_ConnectedFoundRegions);
    HOperatorSet.SelectShape(ho_ConnectedFoundRegions, out OTemp[0], "area", "and", 
        300000, 9999999999);
    ho_ConnectedFoundRegions.Dispose();
    ho_ConnectedFoundRegions = OTemp[0];

    HOperatorSet.Intersection(ho_ConnectedFoundRegions, ho_LineRegions, out OTemp[0]
        );
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];
    HOperatorSet.DilationCircle(ho_LineRegions, out OTemp[0], hv_DilationRadius);
    ho_LineRegions.Dispose();
    ho_LineRegions = OTemp[0];
    HOperatorSet.Union2(ho_ConnectedFoundRegions, ho_LineRegions, out OTemp[0]);
    ho_ConnectedFoundRegions.Dispose();
    ho_ConnectedFoundRegions = OTemp[0];

    //closing_rectangle1 (ConnectedFoundRegions, ClosedRegions, 1000, 1000)
    //dilation_circle (ConnectedFoundRegions, DilationRegions, DilationRadius)
    //closing_circle (DilationRegions, DilationRegions, 500)
    //region_to_bin (RDs, BinImage2, 255, 0, Width, Height)
    //closing_circle (DilationRegions, DilationRegions, 50)
    ho_ClosedRegions.Dispose();
    HOperatorSet.ClosingRectangle1(ho_ConnectedFoundRegions, out ho_ClosedRegions, 
        100, 100);

    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
    ho_BinImage.Dispose();
    HOperatorSet.RegionToBin(ho_ClosedRegions, out ho_BinImage, 255, 0, hv_Width, 
        hv_Height);

    ho_ReducedImage.Dispose();
    HOperatorSet.ReduceDomain(ho_Image, ho_ClosedRegions, out ho_ReducedImage);

    //stop ()
    ho_MaskRegions.Dispose();
    ho_FoundRegions.Dispose();
    ho_LineRegions.Dispose();
    ho_FoundRegion.Dispose();
    ho_MaskRegion.Dispose();
    ho_ConnectedFoundRegions.Dispose();
    ho_ClosedRegions.Dispose();
    ho_ReducedImage.Dispose();

    return;
  }


}
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
public class HDevelopExportApp
{
  static void Main(string[] args)
  {
    new HDevelopExport();
  }
}
#endif

