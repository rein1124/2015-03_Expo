#include "stdafx.h"
#include "ShMv.h"
#include <stdio.h>
#include <malloc.h>

extern HMODULE g_hINSTANCE;
char ModelPath[MAX_PATH];
void (*pFun)(int);

CBaseClass::CBaseClass()
{
	memset(m_IspInfo, 0, 6 * sizeof(InspectInfo));
	for(int i=0; i<6; ++i)
	{
		m_pDftInfo[i] = NULL;
		m_pMeaInfo[i] = NULL;
		m_ImWidth[i] = 0;
		m_ImHeight[i] = 0;
		m_ImBitCount[i] = 0;
		m_ByteLine[i] = 0;
		m_pImBuffer[i] = NULL;
	}
	OutputDebugStringA("CBaseClass Construct");
}
CBaseClass::~CBaseClass()
{
	OutputDebugStringA("CBaseClass Destruct");
}
int CBaseClass::ShMv_Init()
{
	if(GetModelPath() == FALSE)
	{
		OutputDebugStringA("GetModelPath Fail");
		return 1;
	}
	ShMv_LoadParameters();
	m_pImBuffer[0] = (PIMAGE)malloc(m_ByteLine[0] * m_ImHeight[0] * sizeof(unsigned char));
	memset(m_pImBuffer[0], 0, m_ByteLine[0] * m_ImHeight[0] * sizeof(unsigned char));
	m_pImBuffer[1] = (PIMAGE)malloc(m_ByteLine[1] * m_ImHeight[1] * sizeof(unsigned char));
	memset(m_pImBuffer[1], 0, m_ByteLine[1] * m_ImHeight[1] * sizeof(unsigned char));
	m_pImBuffer[2] = (PIMAGE)malloc(m_ByteLine[2] * m_ImHeight[2] * sizeof(unsigned char));
	memset(m_pImBuffer[2], 0, m_ByteLine[2] * m_ImHeight[2] * sizeof(unsigned char));
	m_pImBuffer[3] = (PIMAGE)malloc(m_ByteLine[3] * m_ImHeight[3] * sizeof(unsigned char));
	memset(m_pImBuffer[3], 0, m_ByteLine[3] * m_ImHeight[3] * sizeof(unsigned char));
	m_pImBuffer[4] = (PIMAGE)malloc(m_ByteLine[4] * m_ImHeight[4] * sizeof(unsigned char));
	memset(m_pImBuffer[4], 0, m_ByteLine[4] * m_ImHeight[4] * sizeof(unsigned char));
	m_pImBuffer[5] = (PIMAGE)malloc(m_ByteLine[5] * m_ImHeight[5] * sizeof(unsigned char));
	memset(m_pImBuffer[5], 0, m_ByteLine[5] * m_ImHeight[5] * sizeof(unsigned char));
	Init_MIL();

	return 0;
}
int CBaseClass::ShMv_LoadParameters()
{
	char strPath[MAX_PATH];
	strcpy(strPath,ModelPath);
	strcat(strPath,"\\inspectpara.ini");
	OutputDebugStringA(strPath);
	//g_ImageWidth = GetPrivateProfileIntA("PARAMETER","ImageWidth",0,strPath);
	//GetPrivateProfileStringA("PARAMETER","PlcConnectMac","",m_MacAddress,18,strPath);
	m_ImWidth[0] = GetPrivateProfileIntA("PARAMETER","ImageWidth1",0,strPath);
	m_ImHeight[0] = GetPrivateProfileIntA("PARAMETER","ImageHeight1",0,strPath);
	m_ImBitCount[0] = GetPrivateProfileIntA("PARAMETER","ImageBitCount1",0,strPath);
	m_ByteLine[0] = (m_ImWidth[0] * (m_ImBitCount[0] / 8) + 3) / 4 * 4;
	m_ImWidth[1] = GetPrivateProfileIntA("PARAMETER","ImageWidth2",0,strPath);
	m_ImHeight[1] = GetPrivateProfileIntA("PARAMETER","ImageHeight2",0,strPath);
	m_ImBitCount[1] = GetPrivateProfileIntA("PARAMETER","ImageBitCount2",0,strPath);
	m_ByteLine[1] = (m_ImWidth[1] * (m_ImBitCount[1] / 8) + 3) / 4 * 4;
	m_ImWidth[2] = GetPrivateProfileIntA("PARAMETER","ImageWidth3",0,strPath);
	m_ImHeight[2] = GetPrivateProfileIntA("PARAMETER","ImageHeight3",0,strPath);
	m_ImBitCount[2] = GetPrivateProfileIntA("PARAMETER","ImageBitCount3",0,strPath);
	m_ByteLine[2] = (m_ImWidth[2] * (m_ImBitCount[2] / 8) + 3) / 4 * 4;
	m_ImWidth[3] = GetPrivateProfileIntA("PARAMETER","ImageWidth4",0,strPath);
	m_ImHeight[3] = GetPrivateProfileIntA("PARAMETER","ImageHeight4",0,strPath);
	m_ImBitCount[3] = GetPrivateProfileIntA("PARAMETER","ImageBitCount4",0,strPath);
	m_ByteLine[3] = (m_ImWidth[3] * (m_ImBitCount[3] / 8) + 3) / 4 * 4;
	m_ImWidth[4] = GetPrivateProfileIntA("PARAMETER","ImageWidth5",0,strPath);
	m_ImHeight[4] = GetPrivateProfileIntA("PARAMETER","ImageHeight5",0,strPath);
	m_ImBitCount[4] = GetPrivateProfileIntA("PARAMETER","ImageBitCount5",0,strPath);
	m_ByteLine[4] = (m_ImWidth[4] * (m_ImBitCount[4] / 8) + 3) / 4 * 4;
	m_ImWidth[5] = GetPrivateProfileIntA("PARAMETER","ImageWidth6",0,strPath);
	m_ImHeight[5] = GetPrivateProfileIntA("PARAMETER","ImageHeight6",0,strPath);
	m_ImBitCount[5] = GetPrivateProfileIntA("PARAMETER","ImageBitCount6",0,strPath);
	m_ByteLine[5] = (m_ImWidth[5] * (m_ImBitCount[5] / 8) + 3) / 4 * 4;

	return 0;
}
void CBaseClass::ShMv_FreeObject()
{
	for(int i=0; i<6; ++i)
	{
		if(m_pDftInfo[i])
		{
			free(m_pDftInfo[i]);
			m_pDftInfo[i] = NULL;
		}
		if(m_pMeaInfo[i])
		{
			free(m_pMeaInfo[i]);
			m_pMeaInfo[i] = NULL;
		}
		if(m_pImBuffer[i])
		{
			free(m_pImBuffer[i]);
			m_pImBuffer[i] = NULL;
		}
	}
	Unit_MIL();
}
void CBaseClass::ShMv_RegisterInspectionCompletedCallBack(void(*pFunction)(int))
{
	pFun = pFunction;
}
int CBaseClass::ShMv_Inspect(ImageInfo imageInfo)
{
	switch(imageInfo.SurfaceTypeIndex)
	{
	case 0:
		InspectSurface0(imageInfo);
		break;
	case 1:
		InspectSurface1(imageInfo);
		break;
	case 2:
		InspectSurface2(imageInfo);
		break;
	case 3:
		InspectSurface3(imageInfo);
		break;
	case 4:
		InspectSurface4(imageInfo);
		break;
	case 5:
		InspectSurface5(imageInfo);
		break;
	default:
		break;
	}
	pFun(imageInfo.Index);

	return 0;
}
InspectInfo CBaseClass::ShMv_GetInspectInfo(int acqIndex)
{
	return m_IspInfo[acqIndex];
}
DefectInfo CBaseClass::ShMv_GetDefectInfo(int acqIndex, int defectIndex)
{
	DefectInfo di;
	di.Height = m_pDftInfo[acqIndex][defectIndex].Height;
	di.Index = m_pDftInfo[acqIndex][defectIndex].Index;
	di.Size = m_pDftInfo[acqIndex][defectIndex].Size;
	di.TypeCode = m_pDftInfo[acqIndex][defectIndex].TypeCode;
	di.Width = m_pDftInfo[acqIndex][defectIndex].Width;
	di.X = m_pDftInfo[acqIndex][defectIndex].X;
	di.Y = m_pDftInfo[acqIndex][defectIndex].Y;
	return di;
}
MeasurementInfo CBaseClass::ShMv_GetMeasurementInfo(int acqIndex, int measurementIndex)
{
	MeasurementInfo mi;
	mi.EndPointX = m_pMeaInfo[acqIndex][measurementIndex].EndPointX;
	mi.EndPointY = m_pMeaInfo[acqIndex][measurementIndex].EndPointY;
	mi.GroupIndex = m_pMeaInfo[acqIndex][measurementIndex].GroupIndex;
	mi.Index = m_pMeaInfo[acqIndex][measurementIndex].Index;
	mi.StartPointX = m_pMeaInfo[acqIndex][measurementIndex].StartPointX;
	mi.StartPointY = m_pMeaInfo[acqIndex][measurementIndex].StartPointY;
	mi.TypeCode = m_pMeaInfo[acqIndex][measurementIndex].TypeCode;
	mi.Value = m_pMeaInfo[acqIndex][measurementIndex].Value;
	return mi;
}
////////////////////////////////////////////////////////////////////////////////////////////////

BOOL CBaseClass::GetModelPath()
{
	char exeFullPath[MAX_PATH];
	DWORD retval;
	if(g_hINSTANCE)
	{
		OutputDebugStringA("Return dll full path"); 
		retval = GetModuleFileNameA(g_hINSTANCE,exeFullPath,MAX_PATH); 
		DWORD erro = GetLastError();
		char str[MAX_PATH];
		sprintf(str,"Error Code:%d",erro);
		OutputDebugStringA(str);
		LPVOID lpMsgBuf;
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL,erro,MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR) &lpMsgBuf,0,NULL );
		OutputDebugStringW((TCHAR*)lpMsgBuf);
		OutputDebugStringA(exeFullPath);
		if(retval == 0)
		{
			return FALSE;
		}
	}
	else
	{
		OutputDebugStringA("Return exe full path"); 
		retval = GetModuleFileNameA(NULL,exeFullPath,MAX_PATH); 
		DWORD erro = GetLastError();
		char str[MAX_PATH];
		sprintf(str,"Error Code:%d",erro);
		OutputDebugStringA(str);
		LPVOID lpMsgBuf;
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL,erro,MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR) &lpMsgBuf,0,NULL );
		OutputDebugStringW((TCHAR*)lpMsgBuf);
		OutputDebugStringA(exeFullPath);
		if(retval == 0)
		{
			return FALSE;
		}
	}
	size_t length = strlen(exeFullPath);
	size_t pos = 0;
	for(size_t i=length-1;i>=0;i--)
	{
		if(exeFullPath[i] == '\\')
		{
			pos = i;
			break;
		}
	}
	size_t k=0;
	for(k=0;k<pos;k++)
	{
		ModelPath[k] = exeFullPath[k];
	}
	ModelPath[k] = '\0';
	OutputDebugStringA(ModelPath);
	return TRUE;
}
void CBaseClass::Init_MIL()
{

}
void CBaseClass::Unit_MIL()
{

}
void CBaseClass::InspectSurface0(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_0 Inspect");
	if(iminfo.Height != m_ImHeight[0] || iminfo.Width != m_ImWidth[0] || iminfo.BitsPerPixel != m_ImBitCount[0])
	{
		m_IspInfo[0].DefectsCount = 0;
		m_IspInfo[0].HasError = 0;
		m_IspInfo[0].Index = iminfo.Index;
		m_IspInfo[0].MeasurementsCount = 0;
		m_IspInfo[0].SurfaceTypeIndex = 0;
		if(m_pDftInfo[0])
		{
			free(m_pDftInfo[0]);
			m_pDftInfo[0] = NULL;
		}
		OutputDebugStringA("Buffer 0 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[0], iminfo.Buffer, m_ByteLine[0] * m_ImHeight[0]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[0].DefectsCount = 0;
	m_IspInfo[0].HasError = 0;
	m_IspInfo[0].Index = iminfo.Index;
	m_IspInfo[0].MeasurementsCount = 0;
	m_IspInfo[0].SurfaceTypeIndex = 0;
	if(m_IspInfo[0].DefectsCount > 0)
	{
		if(m_pDftInfo[0])
		{
			free(m_pDftInfo[0]);
			m_pDftInfo[0] = NULL;
		}
		m_pDftInfo[0] = (DefectInfo*)malloc(m_IspInfo[0].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[0], 0, m_IspInfo[0].DefectsCount * sizeof(DefectInfo));
	}
}
void CBaseClass::InspectSurface1(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_1 Inspect");
	if(iminfo.Height != m_ImHeight[1] || iminfo.Width != m_ImWidth[1] || iminfo.BitsPerPixel != m_ImBitCount[1])
	{
		m_IspInfo[1].DefectsCount = 0;
		m_IspInfo[1].HasError = 0;
		m_IspInfo[1].Index = iminfo.Index;
		m_IspInfo[1].MeasurementsCount = 0;
		m_IspInfo[1].SurfaceTypeIndex = 1;
		if(m_pDftInfo[1])
		{
			free(m_pDftInfo[1]);
			m_pDftInfo[1] = NULL;
		}
		OutputDebugStringA("Buffer 1 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[1], iminfo.Buffer, m_ByteLine[1] * m_ImHeight[1]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[1].DefectsCount = 0;
	m_IspInfo[1].HasError = 0;
	m_IspInfo[1].Index = iminfo.Index;
	m_IspInfo[1].MeasurementsCount = 0;
	m_IspInfo[1].SurfaceTypeIndex = 1;
	if(m_IspInfo[1].DefectsCount > 0)
	{
		if(m_pDftInfo[1])
		{
			free(m_pDftInfo[1]);
			m_pDftInfo[1] = NULL;
		}
		m_pDftInfo[1] = (DefectInfo*)malloc(m_IspInfo[1].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[1], 0, m_IspInfo[1].DefectsCount * sizeof(DefectInfo));
	}
}
void CBaseClass::InspectSurface2(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_2 Inspect");
	if(iminfo.Height != m_ImHeight[2] || iminfo.Width != m_ImWidth[2] || iminfo.BitsPerPixel != m_ImBitCount[2])
	{
		m_IspInfo[2].DefectsCount = 0;
		m_IspInfo[2].HasError = 0;
		m_IspInfo[2].Index = iminfo.Index;
		m_IspInfo[2].MeasurementsCount = 0;
		m_IspInfo[2].SurfaceTypeIndex = 2;
		if(m_pDftInfo[2])
		{
			free(m_pDftInfo[2]);
			m_pDftInfo[2] = NULL;
		}
		OutputDebugStringA("Buffer 2 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[2], iminfo.Buffer, m_ByteLine[2] * m_ImHeight[2]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[2].DefectsCount = 0;
	m_IspInfo[2].HasError = 0;
	m_IspInfo[2].Index = iminfo.Index;
	m_IspInfo[2].MeasurementsCount = 0;
	m_IspInfo[2].SurfaceTypeIndex = 2;
	if(m_IspInfo[2].DefectsCount > 0)
	{
		if(m_pDftInfo[2])
		{
			free(m_pDftInfo[2]);
			m_pDftInfo[2] = NULL;
		}
		m_pDftInfo[2] = (DefectInfo*)malloc(m_IspInfo[2].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[2], 0, m_IspInfo[2].DefectsCount * sizeof(DefectInfo));
	}
}
void CBaseClass::InspectSurface3(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_3 Inspect");
	if(iminfo.Height != m_ImHeight[3] || iminfo.Width != m_ImWidth[3] || iminfo.BitsPerPixel != m_ImBitCount[3])
	{
		m_IspInfo[3].DefectsCount = 0;
		m_IspInfo[3].HasError = 0;
		m_IspInfo[3].Index = iminfo.Index;
		m_IspInfo[3].MeasurementsCount = 0;
		m_IspInfo[3].SurfaceTypeIndex = 3;
		if(m_pDftInfo[3])
		{
			free(m_pDftInfo[3]);
			m_pDftInfo[3] = NULL;
		}
		OutputDebugStringA("Buffer 3 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[3], iminfo.Buffer, m_ByteLine[3] * m_ImHeight[3]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[3].DefectsCount = 0;
	m_IspInfo[3].HasError = 0;
	m_IspInfo[3].Index = iminfo.Index;
	m_IspInfo[3].MeasurementsCount = 0;
	m_IspInfo[3].SurfaceTypeIndex = 3;
	if(m_IspInfo[3].DefectsCount > 0)
	{
		if(m_pDftInfo[3])
		{
			free(m_pDftInfo[3]);
			m_pDftInfo[3] = NULL;
		}
		m_pDftInfo[3] = (DefectInfo*)malloc(m_IspInfo[3].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[3], 0, m_IspInfo[3].DefectsCount * sizeof(DefectInfo));
	}
}
void CBaseClass::InspectSurface4(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_4 Inspect");
	if(iminfo.Height != m_ImHeight[4] || iminfo.Width != m_ImWidth[4] || iminfo.BitsPerPixel != m_ImBitCount[4])
	{
		m_IspInfo[4].DefectsCount = 0;
		m_IspInfo[4].HasError = 0;
		m_IspInfo[4].Index = iminfo.Index;
		m_IspInfo[4].MeasurementsCount = 0;
		m_IspInfo[4].SurfaceTypeIndex = 4;
		if(m_pDftInfo[4])
		{
			free(m_pDftInfo[4]);
			m_pDftInfo[4] = NULL;
		}
		OutputDebugStringA("Buffer 4 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[4], iminfo.Buffer, m_ByteLine[4] * m_ImHeight[4]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[4].DefectsCount = 0;
	m_IspInfo[4].HasError = 0;
	m_IspInfo[4].Index = iminfo.Index;
	m_IspInfo[4].MeasurementsCount = 0;
	m_IspInfo[4].SurfaceTypeIndex = 4;
	if(m_IspInfo[4].DefectsCount > 0)
	{
		if(m_pDftInfo[4])
		{
			free(m_pDftInfo[4]);
			m_pDftInfo[4] = NULL;
		}
		m_pDftInfo[4] = (DefectInfo*)malloc(m_IspInfo[4].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[4], 0, m_IspInfo[4].DefectsCount * sizeof(DefectInfo));
	}
}
void CBaseClass::InspectSurface5(ImageInfo iminfo)
{
	OutputDebugStringA("Enter into Surface_5 Inspect");
	if(iminfo.Height != m_ImHeight[5] || iminfo.Width != m_ImWidth[5] || iminfo.BitsPerPixel != m_ImBitCount[5])
	{
		m_IspInfo[5].DefectsCount = 0;
		m_IspInfo[5].HasError = 0;
		m_IspInfo[5].Index = iminfo.Index;
		m_IspInfo[5].MeasurementsCount = 0;
		m_IspInfo[5].SurfaceTypeIndex = 5;
		if(m_pDftInfo[5])
		{
			free(m_pDftInfo[5]);
			m_pDftInfo[5] = NULL;
		}
		OutputDebugStringA("Buffer 5 size not equal to predefined");
		return;
	}
	///////////////////////////////////////////////////////////////////
	memcpy(m_pImBuffer[5], iminfo.Buffer, m_ByteLine[5] * m_ImHeight[5]);
	///////////////////////////////////////////////////////////////////
	m_IspInfo[5].DefectsCount = 0;
	m_IspInfo[5].HasError = 0;
	m_IspInfo[5].Index = iminfo.Index;
	m_IspInfo[5].MeasurementsCount = 0;
	m_IspInfo[5].SurfaceTypeIndex = 5;
	if(m_IspInfo[5].DefectsCount > 0)
	{
		if(m_pDftInfo[5])
		{
			free(m_pDftInfo[5]);
			m_pDftInfo[5] = NULL;
		}
		m_pDftInfo[5] = (DefectInfo*)malloc(m_IspInfo[5].DefectsCount * sizeof(DefectInfo));
		memset(m_pDftInfo[5], 0, m_IspInfo[5].DefectsCount * sizeof(DefectInfo));
	}
}