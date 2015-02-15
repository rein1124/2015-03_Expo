// MvInspection.Matrox.NativeApi.Simulation.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "MvInspectionMatroxNativeApiSimulation.h"
#include <malloc.h>
#include <iostream>
#include <tchar.h>


// This is an example of an exported variable
MVINSPECTIONMATROXNATIVEAPISIMULATION_API int nMvInspectionMatroxNativeApiSimulation=0;

// This is an example of an exported function.
MVINSPECTIONMATROXNATIVEAPISIMULATION_API int fnMvInspectionMatroxNativeApiSimulation(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see MvInspection.Matrox.NativeApi.Simulation.h for the class definition
CMvInspectionMatroxNativeApiSimulation::CMvInspectionMatroxNativeApiSimulation()
{
	return;
}

int __stdcall InitGrabDevice()
{
	return 0;
	;
}

void __stdcall InitParameters()
{
	;
}

void __stdcall FreeDevice()
{
	;
}

ImageInfo __stdcall GrabSingleFrameFromFile(TCHAR*  pFilePath)
{
	Sleep(1000);
	ImageInfo im;

	int nSize;
	char* pszMultiByte;
	nSize = WideCharToMultiByte(CP_ACP, 0, pFilePath, -1, NULL, 0, NULL, NULL);
	//printf("nSize=%d\n",nSize);
	pszMultiByte = (char*)malloc((nSize + 1));
	WideCharToMultiByte(CP_ACP, 0, pFilePath, -1, pszMultiByte, nSize, NULL, NULL);
	pszMultiByte[nSize] = '\0';
	printf(pszMultiByte);
	printf("\n");

	FILE *fr;
	errno_t err;
	err = fopen_s(&fr, pszMultiByte, "rb");
	free(pszMultiByte);
	BITMAPINFOHEADER bi;
	fseek(fr, 14, SEEK_SET);
	fread(&bi, sizeof(BITMAPINFOHEADER), 1, fr);
	im.Width = bi.biWidth;
	im.Height = bi.biHeight;
	im.BitsPerPixel = bi.biBitCount;
	unsigned char* p = (unsigned char*)malloc(bi.biHeight * ((bi.biWidth * (bi.biBitCount / 8) + 3) / 4 * 4));
	im.Buffer = (unsigned char*)malloc(bi.biHeight * ((bi.biWidth * (bi.biBitCount / 8) + 3) / 4 * 4));
	if (bi.biBitCount == 24)
	{
		fseek(fr, 54, SEEK_SET);
		fread(p, sizeof(unsigned char), bi.biHeight * ((bi.biWidth * (bi.biBitCount / 8) + 3) / 4 * 4), fr);
	}
	else if (bi.biBitCount == 8)
	{
		fseek(fr, 1078, SEEK_SET);
		fread(p, sizeof(unsigned char), bi.biHeight * ((bi.biWidth * (bi.biBitCount / 8) + 3) / 4 * 4), fr);
	}
	memcpy(im.Buffer, p, bi.biHeight * ((bi.biWidth * (bi.biBitCount / 8) + 3) / 4 * 4));
	free(p);
	fclose(fr);
	return im;
}

ImageInfo __stdcall GrabSingleFrame()
{
	//return GrabSingleFrameFromFile(L"sample\SurfaceFront_720x1280.bmp");
	//return GrabSingleFrameFromFile(L"D:\@\Vins.Phone\Vins.Phone_3.x(git)\Vins.Phone_3.x(git)\bin\Debug\sample\SurfaceFront_720x1280.bmp");
	//return GrabSingleFrameFromFile(_T("D:\@\Vins.Phone\Vins.Phone_3.x(git)\Vins.Phone_3.x(git)\bin\Debug\sample\SurfaceFront_720x1280.bmp"));

	TCHAR* pFileName;
	//pFileName = _T("D:\\@\\Vins.Phone\\Vins.Phone_3.x(git)\\Vins.Phone_3.x(git)\\bin\\Debug\\sample\\SurfaceFront_720x1280.bmp");
	pFileName = _T("sample\\2014-12-06_12.00.33.tif");
	return GrabSingleFrameFromFile(pFileName);
}