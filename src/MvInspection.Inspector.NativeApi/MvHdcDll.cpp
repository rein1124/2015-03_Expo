// MvHdcDll.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "MvHdcDll.h"

int __stdcall Init()
{
	pBase = new CBaseClass;
	return pBase->ShMv_Init();
}
int __stdcall LoadParameters()
{
	return pBase->ShMv_LoadParameters();
}
void __stdcall FreeObject()
{
	pBase->ShMv_FreeObject();
	delete pBase;
}
void __stdcall RegisterInspectionCompletedCallBack(void(*pFunction)(int))
{
	pBase->ShMv_RegisterInspectionCompletedCallBack(pFunction);
}
int __stdcall Inspect(ImageInfo imageInfo)
{
	return pBase->ShMv_Inspect(imageInfo);
}
InspectInfo __stdcall GetInspectInfo(int acqIndex)
{
	return pBase->ShMv_GetInspectInfo(acqIndex);
}
DefectInfo __stdcall GetDefectInfo(int acqIndex, int defectIndex)
{
	return pBase->ShMv_GetDefectInfo(acqIndex,defectIndex);
}	
MeasurementInfo __stdcall GetMeasurementInfo(int acqIndex, int measurementIndex)
{
	return pBase->ShMv_GetMeasurementInfo(acqIndex,measurementIndex);
}
