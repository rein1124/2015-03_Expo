// Hdc.Mv.Inspection.Mil.NativeApi.Sim.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "Hdc.Mv.Inspection.Mil.NativeApi.Sim.h"


// This is an example of an exported variable
HDCMVINSPECTIONMILNATIVEAPISIM_API int nHdcMvInspectionMilNativeApiSim=0;

// This is an example of an exported function.
HDCMVINSPECTIONMILNATIVEAPISIM_API int fnHdcMvInspectionMilNativeApiSim(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see Hdc.Mv.Inspection.Mil.NativeApi.Sim.h for the class definition
CHdcMvInspectionMilNativeApiSim::CHdcMvInspectionMilNativeApiSim()
{
	return;
}


HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall InitApp()
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall AddEdgeDefinition(
	_In_ double startPointX,
	_In_ double startPointY,
	_In_ double endPointX,
	_In_ double endPointY,
	_In_ double roiWidth, // half of width
	_In_ int polarity,
	_In_ int orientation
	)
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall AddCircleDefinition(
	_In_ double circleCenterX,
	_In_ double circleCenterY,
	_In_ double innerCircleRadius,
	_In_ double outerCircleRadius,
	_In_ int lowThreshold,
	_In_ int highThreshold
	)
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetEdgeDefinitionsCount(
	_Out_ int* count)
{
	*count = 11;
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetCircleDefinitionsCount(
	_Out_ int* count)
{
	*count = 22;
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CleanDefinitions()
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CalibrateImage(
	_In_ ImageInfo originalImageInfo,
	_Out_ ImageInfo* calibratedImageInfo)
{
	calibratedImageInfo->Index = 11;
	calibratedImageInfo->SurfaceTypeIndex = 22;
	calibratedImageInfo->PixelWidth = 33;
	calibratedImageInfo->PixelHeight = 44;
	calibratedImageInfo->BitsPerPixel = 55;
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall Calculate(
	_In_ ImageInfo imageInfo)
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetEdgeResult(
	_In_ int index,
	_Out_ Line* edgeLine,
	_Out_ Point* intersectionPoint
	)
{
	edgeLine->X1 = 11;
	edgeLine->Y1 = 22;
	edgeLine->X2 = 33;
	edgeLine->Y2 = 44;

	intersectionPoint->X = 101;
	intersectionPoint->Y = 102;

	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetCircleResult(
	_In_ int index,
	_Out_ Circle* foundCircle,
	_Out_ double* roundnes
	)
{
	foundCircle->CenterX = 11;
	foundCircle->CenterY = 22;
	foundCircle->Radius = 33;

	*roundnes = 101;

	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CreateRelativeCoordinate(
	_In_ Line baseLine,
	_In_ double angle
	)
{
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetDistanceBetweenLines(
	_In_ Line line1,
	_In_ Line line2,
	_Out_ double* distanceInPixel,
	_Out_ double* distanceInWorld,
	_Out_ double* angle,
	_Out_ Point* footPoint1,
	_Out_ Point* footPoint2
	)
{
	*distanceInPixel = 11;
	*distanceInWorld = 22;
	*angle = 33;
	footPoint1->X = 101;
	footPoint1->Y = 102;
	footPoint2->X = 201;
	footPoint2->Y = 202;
	return 99;
}

HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetDistanceBetweenPoints(
	_In_ Point point1,
	_In_ Point point2,
	_Out_ double* distanceInPixel,
	_Out_ double* distanceInWorld,
	_Out_ double* angle
	)
{
	*distanceInPixel = 11;
	*distanceInWorld = 22;
	*angle = 33;
	return 99;
}