#pragma once
#include "DefStruct.h"
#include <windows.h>

class CBaseClass
{
public:
	CBaseClass();
	virtual ~CBaseClass();
public:
	int m_ImWidth[6];
	int m_ImHeight[6];
	int m_ImBitCount[6];
	int m_ByteLine[6];
	PIMAGE m_pImBuffer[6];
	InspectInfo m_IspInfo[6];
	DefectInfo* m_pDftInfo[6];
	MeasurementInfo* m_pMeaInfo[6];
public:
	int ShMv_Init();
	int ShMv_LoadParameters();
	void ShMv_FreeObject(); 
	void ShMv_RegisterInspectionCompletedCallBack(void(*pFunction)(int));
	int ShMv_Inspect(ImageInfo imageInfo);
	InspectInfo ShMv_GetInspectInfo(int acqIndex);
	DefectInfo ShMv_GetDefectInfo(int acqIndex, int defectIndex);
	MeasurementInfo ShMv_GetMeasurementInfo(int acqIndex, int measurementIndex);

	BOOL GetModelPath();
	void Init_MIL();
	void Unit_MIL();
	void InspectSurface0(ImageInfo iminfo);
	void InspectSurface1(ImageInfo iminfo);
	void InspectSurface2(ImageInfo iminfo);
	void InspectSurface3(ImageInfo iminfo);
	void InspectSurface4(ImageInfo iminfo);
	void InspectSurface5(ImageInfo iminfo);
};