// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the MVINSPECTIONMATROXNATIVEAPISIMULATION_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// MVINSPECTIONMATROXNATIVEAPISIMULATION_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MVINSPECTIONMATROXNATIVEAPISIMULATION_EXPORTS
#define MVINSPECTIONMATROXNATIVEAPISIMULATION_API __declspec(dllexport)
#else
#define MVINSPECTIONMATROXNATIVEAPISIMULATION_API __declspec(dllimport)
#endif

// This class is exported from the MvInspection.Matrox.NativeApi.Simulation.dll
class MVINSPECTIONMATROXNATIVEAPISIMULATION_API CMvInspectionMatroxNativeApiSimulation {
public:
	CMvInspectionMatroxNativeApiSimulation(void);
	// TODO: add your methods here.
};

extern MVINSPECTIONMATROXNATIVEAPISIMULATION_API int nMvInspectionMatroxNativeApiSimulation;

MVINSPECTIONMATROXNATIVEAPISIMULATION_API int fnMvInspectionMatroxNativeApiSimulation(void);

struct ImageInfo
{
	int Index;
	int SurfaceTypeIndex;
	int Width;
	int Height;
	int BitsPerPixel;
	unsigned char* Buffer;
};

extern "C" MVINSPECTIONMATROXNATIVEAPISIMULATION_API int __stdcall InitGrabDevice();
extern "C" MVINSPECTIONMATROXNATIVEAPISIMULATION_API ImageInfo __stdcall GrabSingleFrame();
extern "C" MVINSPECTIONMATROXNATIVEAPISIMULATION_API ImageInfo __stdcall GrabSingleFrameFromFile(TCHAR*  pFilePath);
extern "C" MVINSPECTIONMATROXNATIVEAPISIMULATION_API void __stdcall FreeDevice();