// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the HDCMVINSPECTIONMILNATIVEAPISIM_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// HDCMVINSPECTIONMILNATIVEAPISIM_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef HDCMVINSPECTIONMILNATIVEAPISIM_EXPORTS
#define HDCMVINSPECTIONMILNATIVEAPISIM_API __declspec(dllexport)
#else
#define HDCMVINSPECTIONMILNATIVEAPISIM_API __declspec(dllimport)
#endif

// This class is exported from the Hdc.Mv.Inspection.Mil.NativeApi.Sim.dll
class HDCMVINSPECTIONMILNATIVEAPISIM_API CHdcMvInspectionMilNativeApiSim {
public:
	CHdcMvInspectionMilNativeApiSim(void);
	// TODO: add your methods here.
};

extern HDCMVINSPECTIONMILNATIVEAPISIM_API int nHdcMvInspectionMilNativeApiSim;

HDCMVINSPECTIONMILNATIVEAPISIM_API int fnHdcMvInspectionMilNativeApiSim(void);

#include "DataTypes.h"

#ifdef __cplusplus
extern "C"{
#endif

	// 1. Initialize App
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall InitApp();

	// 2. add object definitions for edge/circle/etc.
	// polarity - 0: NEGATIVE, 1: POSITIVE, 2: ANY
	// orientation - 0: HORIZONTAL, 1: VERTICAL, 2: ANY
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall AddEdgeDefinition(
		_In_ double startPointX,
		_In_ double startPointY,
		_In_ double endPointX,
		_In_ double endPointY,
		_In_ double roiWidth, // half of width
		_In_ int polarity,
		_In_ int orientation
		);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall AddCircleDefinition(
		_In_ double circleCenterX,
		_In_ double circleCenterY,
		_In_ double innerCircleRadius,
		_In_ double outerCircleRadius,
		_In_ int lowThreshold,
		_In_ int highThreshold
		);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetEdgeDefinitionsCount(
		_Out_ int* count);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetCircleDefinitionsCount(
		_Out_ int* count);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CleanDefinitions();

	// 3. calibration
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CalibrateImage(
		_In_ ImageInfo originalImageInfo,
		_Out_ ImageInfo* calibratedImageInfo);

	// 4. Calculate image
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall Calculate(
		_In_ ImageInfo imageInfo);

	// 5. get object result
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetEdgeResult(
		_In_ int index,
		_Out_ Line* edgeLine,
		_Out_ Point* intersectionPoint
		);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetCircleResult(
		_In_ int index,
		_Out_ Circle* foundCircle,
		_Out_ double* roundness
		);
	
	// 6. create relative coordinate
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall CreateRelativeCoordinate(
		_In_ Line baseLine,
		_In_ double angle
		);

	// 7. measurement
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetDistanceBetweenLines(
		_In_ Line line1,
		_In_ Line line2,
		_Out_ double* distanceInPixel,
		_Out_ double* distanceInWorld,
		_Out_ double* angle,
		_Out_ Point* footPoint1,
		_Out_ Point* footPoint2
		);

	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall GetDistanceBetweenPoints(
		_In_ Point point1,
		_In_ Point point2,
		_Out_ double* distanceInPixel,
		_Out_ double* distanceInWorld,
		_Out_ double* angle
		);

	// 8. free
	HDCMVINSPECTIONMILNATIVEAPISIM_API int __stdcall FreeApp();

#ifdef __cplusplus
}
#endif