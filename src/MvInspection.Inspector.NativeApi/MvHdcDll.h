#ifdef MVHDCDLL_EXPORTS
#define TCINSPECTIONCORE_API __declspec(dllexport)
#else
#define TCINSPECTIONCORE_API __declspec(dllimport)
#endif

#include "DefStruct.h"
#include "ShMv.h"

CBaseClass *pBase;

extern "C"
{
	TCINSPECTIONCORE_API int __stdcall Init(); // invoke on app starting;
	TCINSPECTIONCORE_API int __stdcall LoadParameters(); // invoke when parameter file will update;
	TCINSPECTIONCORE_API void __stdcall FreeObject(); // invoke when app exiting;

	TCINSPECTIONCORE_API void __stdcall RegisterInspectionCompletedCallBack(void(*pFunction)(int));
	TCINSPECTIONCORE_API int __stdcall Inspect(ImageInfo imageInfo);
	TCINSPECTIONCORE_API InspectInfo __stdcall GetInspectInfo(int acqIndex);
	TCINSPECTIONCORE_API DefectInfo __stdcall GetDefectInfo(int acqIndex, int defectIndex);
	TCINSPECTIONCORE_API MeasurementInfo __stdcall GetMeasurementInfo(int acqIndex, int measurementIndex);
}