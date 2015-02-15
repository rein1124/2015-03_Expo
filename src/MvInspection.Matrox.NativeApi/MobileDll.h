// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the INSPECTOR_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// INSPECTOR_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MOBILEDLL_EXPORTS
#define INSPECTOR_API __declspec(dllexport)
#else
#define INSPECTOR_API __declspec(dllimport)
#endif
#define BUFFERING_SIZE_MAX 17

#include <mil.h>


typedef unsigned char* PIMAGE;
typedef unsigned char  IIMAGE;	

struct ImageInfo
{
	int Index;
	int SurfaceTypeIndex;
	int Width;
	int Height;
	int BitsPerPixel;
	unsigned char* Buffer;
};

MIL_ID g_Application = M_NULL;
MIL_ID g_System = M_NULL;
MIL_ID g_Digital = M_NULL;
MIL_ID g_Image = M_NULL;
MIL_ID g_GrabImage = M_NULL;
//MIL_ID g_SubImage[BUFFERING_SIZE_MAX] = {M_NULL};
MIL_ID* g_SubImage = M_NULL;
int g_bIsSaveBmp = 0;
MIL_ID g_SaveImage[3] = { M_NULL };

unsigned char* pImage = NULL;

typedef struct
{
	MIL_ID  MilImageDisp;
	long    ProcessedImageCount;
} HookDataStruct;

void InitParameters();   
extern "C" INSPECTOR_API int __stdcall InitGrabDevice(); 
extern "C" INSPECTOR_API ImageInfo __stdcall GrabSingleFrame();   
extern "C" INSPECTOR_API void __stdcall FreeDevice();      


HookDataStruct g_UserHookData;
MIL_INT MFTYPE ProcessingFunction(long HookType, MIL_ID HookId, void MPTYPE *HookDataPtr);
