//
//  File generated by HDevelop for HALCON/DOTNET (C#) Version 12.0
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
#endif

  // Procedures 
  public void EnhanceEdgeAreaH1403 (HObject ho_Image, out HObject ho_EnhancedImage, 
      out HObject ho_EnhancedEdge, HTuple hv_AreaLightDark, HTuple hv_WidthMin, HTuple hv_WidthMax, 
      HTuple hv_HeightMin, HTuple hv_HeightMax, HTuple hv_SortMode, HTuple hv_Order, 
      HTuple hv_RowOrCol, HTuple hv_SelectIndex, HTuple hv_MeanMaskWidth, HTuple hv_MeanMaskHeight)
  {




    // Local iconic variables 

    HObject ho_Region, ho_ConnectedRegions, ho_SelectedRegions=null;
    HObject ho_SelectedRegions1=null, ho_SortedRegions, ho_ObjectSelected;
    HObject ho_ImageReduced, ho_ImageMean, ho_ImageScaled=null;
    HObject ho_ImageScaleMax;

    // Local control variables 

    HTuple hv_Width = null, hv_Height = null, hv_UsedThreshold = null;
    HTuple hv_Value = null, hv_UsedThreshold1 = null;
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_EnhancedImage);
    HOperatorSet.GenEmptyObj(out ho_EnhancedEdge);
    HOperatorSet.GenEmptyObj(out ho_Region);
    HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
    HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
    HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
    HOperatorSet.GenEmptyObj(out ho_SortedRegions);
    HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
    HOperatorSet.GenEmptyObj(out ho_ImageReduced);
    HOperatorSet.GenEmptyObj(out ho_ImageMean);
    HOperatorSet.GenEmptyObj(out ho_ImageScaled);
    HOperatorSet.GenEmptyObj(out ho_ImageScaleMax);
    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);

    ho_Region.Dispose();
    HOperatorSet.BinaryThreshold(ho_Image, out ho_Region, "max_separability", hv_AreaLightDark, 
        out hv_UsedThreshold);
    ho_ConnectedRegions.Dispose();
    HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);

    if ((int)((new HTuple(hv_WidthMin.TupleNotEqual(0))).TupleAnd(new HTuple(hv_HeightMin.TupleNotEqual(
        0)))) != 0)
    {

      ho_SelectedRegions.Dispose();
      HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "width", 
          "and", hv_WidthMin, hv_WidthMax);
      ho_SelectedRegions1.Dispose();
      HOperatorSet.SelectShape(ho_SelectedRegions, out ho_SelectedRegions1, "height", 
          "and", hv_HeightMin, hv_HeightMax);

    }
    else if ((int)((new HTuple(hv_WidthMin.TupleEqual(0))).TupleAnd(new HTuple(hv_HeightMin.TupleNotEqual(
        0)))) != 0)
    {

      ho_SelectedRegions1.Dispose();
      HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, "height", 
          "and", hv_HeightMin, hv_HeightMax);

    }
    else if ((int)((new HTuple(hv_WidthMin.TupleNotEqual(0))).TupleAnd(new HTuple(hv_HeightMin.TupleEqual(
        0)))) != 0)
    {

      ho_SelectedRegions1.Dispose();
      HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions1, "width", 
          "and", hv_WidthMin, hv_WidthMax);

    }

    ho_SortedRegions.Dispose();
    HOperatorSet.SortRegion(ho_SelectedRegions1, out ho_SortedRegions, hv_SortMode, 
        hv_Order, hv_RowOrCol);
    ho_ObjectSelected.Dispose();
    HOperatorSet.SelectObj(ho_SortedRegions, out ho_ObjectSelected, hv_SelectIndex);

    ho_ImageReduced.Dispose();
    HOperatorSet.ReduceDomain(ho_Image, ho_ObjectSelected, out ho_ImageReduced);
    ho_ImageMean.Dispose();
    HOperatorSet.MeanImage(ho_ImageReduced, out ho_ImageMean, hv_MeanMaskWidth, hv_MeanMaskHeight);
    HOperatorSet.GrayFeatures(ho_ObjectSelected, ho_ImageMean, "mean", out hv_Value);

    if ((int)(new HTuple(hv_AreaLightDark.TupleEqual("light"))) != 0)
    {

      ho_ImageScaled.Dispose();
      HOperatorSet.ScaleImage(ho_ImageMean, out ho_ImageScaled, 1, 255-hv_Value);

    }
    else
    {

      ho_ImageScaled.Dispose();
      HOperatorSet.ScaleImage(ho_ImageMean, out ho_ImageScaled, 1, -hv_Value);

    }

    ho_ImageScaleMax.Dispose();
    HOperatorSet.ScaleImageMax(ho_ImageScaled, out ho_ImageScaleMax);
    ho_EnhancedEdge.Dispose();
    HOperatorSet.BinaryThreshold(ho_ImageScaleMax, out ho_EnhancedEdge, "max_separability", 
        hv_AreaLightDark, out hv_UsedThreshold1);

    ho_EnhancedImage.Dispose();
    HOperatorSet.RegionToBin(ho_EnhancedEdge, out ho_EnhancedImage, 255, 0, hv_Width, 
        hv_Height);

    ho_Region.Dispose();
    ho_ConnectedRegions.Dispose();
    ho_SelectedRegions.Dispose();
    ho_SelectedRegions1.Dispose();
    ho_SortedRegions.Dispose();
    ho_ObjectSelected.Dispose();
    ho_ImageReduced.Dispose();
    ho_ImageMean.Dispose();
    ho_ImageScaled.Dispose();
    ho_ImageScaleMax.Dispose();

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

