// TCInspection.Core.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "TCInspection.Core.h"
#include <malloc.h>
#include <iostream>


void(*pInspectionCompletedCallBack)(int); //define callback function
ImageInfo _imageInfo;
HANDLE InspectionProcessThread;
int _inspectCounter;


// This is an example of an exported variable
TCINSPECTIONCORE_API int nTCInspectionCore = 0;

// This is an example of an exported function.
TCINSPECTIONCORE_API int fnTCInspectionCore(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see TCInspection.Core.h for the class definition
CTCInspectionCore::CTCInspectionCore()
{
	return;
}

int __stdcall Init()
{
	_imageInfo.BitsPerPixel = -1;
	_imageInfo.Width = -1;
	_imageInfo.Height = -1;
	_imageInfo.Index = -1;
	_imageInfo.SurfaceTypeIndex = -1;
	_imageInfo.Buffer = NULL;

	InspectionProcessThread = NULL;

	_inspectCounter = 1;

	return 0;
}

int __stdcall LoadParameters()
{
	return 0;
}

void __stdcall FreeObject()
{
	;
}

InspectInfo __stdcall Inspect(
	ImageInfo imageInfo,
	DefectInfo defectInfos[],
	MeasurementInfo MeasurementInfos[])
{
	int inspectCounter = _inspectCounter;
	_inspectCounter++;

	InspectInfo ii;
	ii.Index = (imageInfo.SurfaceTypeIndex + 1) * 100 + imageInfo.Index;
	ii.SurfaceTypeIndex = imageInfo.SurfaceTypeIndex;
	ii.HasError = 0;
	ii.DefectsCount = 3;
	ii.MeasurementsCount = 4 * 3;

	for (size_t i = 0; i < ii.DefectsCount; i++)
	{
		//defectInfos[i].Index = (imageInfo.SurfaceTypeIndex + 1) * 100 + i;
		defectInfos[i].Index = i;
		defectInfos[i].TypeCode = i;

		defectInfos[i].X = 10 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Y = 20 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Width = 30 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Height = 40 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Size = 50 * i + imageInfo.SurfaceTypeIndex;

		defectInfos[i].X_Real = 10.001 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Y_Real = 20.002 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Width_Real = 30.003 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Height_Real = 40.004 * i + imageInfo.SurfaceTypeIndex;
		defectInfos[i].Size_Real = 50.005 * i + imageInfo.SurfaceTypeIndex;
	}

	for (size_t j = 0; j < 3; j++)
	{
		for (size_t i = 0; i < 4; i++)
		{
			//MeasurementInfos[j * 4 + i].Index = (imageInfo.SurfaceTypeIndex + 1) * 100 + j * 3 + i;
			MeasurementInfos[j * 4 + i].Index = j * 4 + i;
			MeasurementInfos[j * 4 + i].GroupIndex = j;

			MeasurementInfos[j * 4 + i].StartPointX = j * 100 + 10 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].StartPointY = j * 100 + 20 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].EndPointX = j * 100 + 30 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].EndPointY = j * 100 + 40 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].Value = j * 100 + 50 * i + imageInfo.SurfaceTypeIndex;

			MeasurementInfos[j * 4 + i].StartPointX_Real = j * 100.001 + 10 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].StartPointY_Real = j * 100.002 + 20 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].EndPointX_Real = j * 100.003 + 30 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].EndPointY_Real = j * 100.004 + 40 * i + imageInfo.SurfaceTypeIndex;
			MeasurementInfos[j * 4 + i].Value_Real = j * 100.005 + 50 * i + imageInfo.SurfaceTypeIndex;
		}
	}

	Sleep(1000);
	return ii;
}