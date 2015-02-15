// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the TCINSPECTIONCORE_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// TCINSPECTIONCORE_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef TCINSPECTIONCORE_EXPORTS
#define TCINSPECTIONCORE_API __declspec(dllexport)
#else
#define TCINSPECTIONCORE_API __declspec(dllimport)
#endif

// This class is exported from the TCInspection.Core.dll
class TCINSPECTIONCORE_API CTCInspectionCore {
public:
	CTCInspectionCore(void);
	// TODO: add your methods here.
};

extern TCINSPECTIONCORE_API int nTCInspectionCore;

TCINSPECTIONCORE_API int fnTCInspectionCore(void);


struct ImageInfo
{
	int Index;
	int SurfaceTypeIndex;
	int Width;
	int Height;
	int BitsPerPixel;
	unsigned char* Buffer;
};

struct DefectInfo
{
	int Index;
	int TypeCode;
	int X;
	int Y;
	int Width;
	int Height;
	int Size;
	double X_Real;
	double Y_Real;
	double Width_Real;
	double Height_Real;
	double Size_Real;
};

struct MeasurementInfo
{
	int Index;
	int TypeCode;
	int StartPointX;
	int StartPointY;
	int EndPointX;
	int EndPointY;
	int Value;
	int GroupIndex;
	double StartPointX_Real;
	double StartPointY_Real;
	double EndPointX_Real;
	double EndPointY_Real;
	double Value_Real;
};

struct InspectInfo
{
	int Index;
	int SurfaceTypeIndex;
	int HasError;
	int DefectsCount;
	int MeasurementsCount;
};

extern "C"
{
	// General
	TCINSPECTIONCORE_API int __stdcall Init(); // invoke on app starting;
	TCINSPECTIONCORE_API int __stdcall LoadParameters(); // invoke when parameter file will update;
	TCINSPECTIONCORE_API void __stdcall FreeObject(); // invoke when app exiting;

	// Inspect
	TCINSPECTIONCORE_API InspectInfo __stdcall Inspect(
		ImageInfo imageInfo, 
		DefectInfo defectInfos[], 
		MeasurementInfo MeasurementInfos[]);
}