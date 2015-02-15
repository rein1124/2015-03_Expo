// MobileDll.cpp : 定义 DLL 应用程序的导出函数。
//

#include "stdafx.h"
#include "MobileDll.h"
#include <iostream>
#include <malloc.h>
#include <windows.h>
#include <tchar.h>

extern HMODULE g_hINSTANCE;
char ModelPath[MAX_PATH];
char SavePath[MAX_PATH];

long ImageWidth=0;
long ImageHeight=0;
long ChildCounts=0;
long hh=0;
long ww=0;

HANDLE g_CopyOverEvent[3];
HANDLE g_SaveThread;
DWORD WINAPI SaveProcessThread(LPVOID lpParameter);
unsigned long g_loop1 = 0;
unsigned long g_loop2 = 0;

void InitParameters()
{
	char exeFullPath[MAX_PATH];
	char strPath[MAX_PATH];
	if(g_hINSTANCE)
	{
		OutputDebugStringA("Return dll full path"); 
		GetModuleFileNameA(g_hINSTANCE,exeFullPath,MAX_PATH); 
		DWORD erro = GetLastError();
		char str[MAX_PATH];
		sprintf(str,"Error Code:%d",erro);
		OutputDebugStringA(str);
		LPVOID lpMsgBuf;
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL,erro,MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR) &lpMsgBuf,0,NULL );
        OutputDebugStringW((TCHAR*)lpMsgBuf);
		OutputDebugStringA(exeFullPath);
	}
	else
	{
		OutputDebugStringA("Return exe full path"); 
		GetModuleFileNameA(NULL,exeFullPath,MAX_PATH); 
		DWORD erro = GetLastError();
		char str[MAX_PATH];
		sprintf(str,"Error Code:%d",erro);
		OutputDebugStringA(str);
		LPVOID lpMsgBuf;
		FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS,
			NULL,erro,MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPTSTR) &lpMsgBuf,0,NULL );
		OutputDebugStringW((TCHAR*)lpMsgBuf);
		OutputDebugStringA(exeFullPath);
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
		strPath[k] = exeFullPath[k];
	}
	strPath[k] = '\0';
	OutputDebugStringA(strPath);
	strcpy(ModelPath,strPath);
	strcpy(SavePath, strPath);
	strcat(strPath,"\\SoliosGrab.ini");
	OutputDebugStringA(strPath);
	ImageWidth = GetPrivateProfileIntA("PARAMETER","ImageWidth",0,strPath);
	ChildCounts = GetPrivateProfileIntA("PARAMETER","ChildCounts",0,strPath);
	g_bIsSaveBmp = GetPrivateProfileIntA("PARAMETER", "IsSaveBmp", 0, strPath);
	char ss[MAX_PATH];
	sprintf(ss,"ChildCount = %d, Width = %d, IsSaveBmp = %d\n",ChildCounts,ImageWidth,g_bIsSaveBmp);
	OutputDebugStringA(ss);
}
int __stdcall InitGrabDevice()
{
	InitParameters();

	MappAlloc(M_DEFAULT, &g_Application);
	MsysAlloc(M_SYSTEM_SOLIOS, M_DEFAULT, M_DEFAULT, &g_System);
	strcat(ModelPath,"\\e2v8k.dcf");
	wchar_t str[MAX_PATH];
	size_t n = (size_t)MultiByteToWideChar(CP_ACP,0,ModelPath,-1,NULL,0);
	if (n >= MAX_PATH) n = MAX_PATH - 1;
	MultiByteToWideChar(CP_ACP,0,ModelPath,-1,str,int(n));
	str[n+1] = _TEXT('\0');
	OutputDebugStringW(str);
	MdigAlloc(g_System,M_DEV0, str, M_DEFAULT, &g_Digital);
	//void *pInt = &hh;
	hh = MdigInquire(g_Digital, M_SIZE_Y, M_NULL);
	ww = MdigInquire(g_Digital, M_SIZE_X, M_NULL);

	//hh = 500;
	char ss[MAX_PATH];
	sprintf(ss,"Device Height  = %d,Device Width = %d\n",hh,ww);
	OutputDebugStringA(ss);

	//ChildCounts = 37;
	ImageHeight = hh * ChildCounts;

	sprintf(ss,"Capture Height Required = %d,Capture Width Required = %d\n",ImageHeight,ImageWidth);
	OutputDebugStringA(ss);

	pImage = (unsigned char *)malloc(ImageWidth * ImageHeight * sizeof(unsigned char));  
	memset(pImage, 0 , ImageWidth * ImageHeight);
	MbufCreate2d(g_System, ImageWidth, ImageHeight, M_UNSIGNED+8,M_IMAGE+M_DISP+M_PROC,M_HOST_ADDRESS+M_PITCH,ImageWidth,(void *)pImage,&g_Image);
	
	MbufAlloc2d(g_System,ImageWidth,ImageHeight,M_UNSIGNED+8,M_IMAGE+M_GRAB+M_DISP+M_PROC,&g_GrabImage);

	MbufAlloc2d(g_System, ImageWidth, ImageHeight, M_UNSIGNED + 8, M_IMAGE + M_GRAB + M_DISP + M_PROC, &g_SaveImage[0]);
	MbufAlloc2d(g_System, ImageWidth, ImageHeight, M_UNSIGNED + 8, M_IMAGE + M_GRAB + M_DISP + M_PROC, &g_SaveImage[1]);
	MbufAlloc2d(g_System, ImageWidth, ImageHeight, M_UNSIGNED + 8, M_IMAGE + M_GRAB + M_DISP + M_PROC, &g_SaveImage[2]);

	g_SubImage = (MIL_ID*)malloc(ChildCounts * sizeof(MIL_ID));

	for(int MilGrabBufferListIdx = 0; MilGrabBufferListIdx<ChildCounts; MilGrabBufferListIdx++)
	{
		MbufChild2d(g_GrabImage, 0L, hh*MilGrabBufferListIdx,
			ImageWidth, 
			hh, 
			&g_SubImage[MilGrabBufferListIdx]);
	}

	g_UserHookData.MilImageDisp = g_Image;
	g_UserHookData.ProcessedImageCount = 0;

	g_CopyOverEvent[0] = ::CreateEvent(NULL, TRUE, FALSE, NULL);
	g_CopyOverEvent[1] = ::CreateEvent(NULL, TRUE, FALSE, NULL);
	g_CopyOverEvent[2] = ::CreateEvent(NULL, TRUE, FALSE, NULL);
	g_SaveThread = ::CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)SaveProcessThread,NULL,0,NULL);

	return 0;
}

ImageInfo __stdcall GrabSingleFrame()
{
	ImageInfo ii;
	ii.BitsPerPixel = 8;
	ii.Height = ImageHeight;
	ii.Index = 0;
	ii.SurfaceTypeIndex = 0;
	ii.Width = ImageWidth;
	MbufClear(g_GrabImage,0);
	MdigProcess(g_Digital, g_SubImage, ChildCounts,M_SEQUENCE+M_COUNT(ChildCounts), M_DEFAULT, (MIL_DIG_HOOK_FUNCTION_PTR)ProcessingFunction, &g_UserHookData);
	MdigProcess(g_Digital, g_SubImage, ChildCounts,M_STOP+M_WAIT, M_DEFAULT, (MIL_DIG_HOOK_FUNCTION_PTR )ProcessingFunction, &g_UserHookData);
	MimFlip(g_GrabImage, g_GrabImage, M_FLIP_HORIZONTAL, M_DEFAULT);
	MimFlip(g_GrabImage, g_GrabImage, M_FLIP_VERTICAL, M_DEFAULT); //20141225 gtc delete
	if (g_bIsSaveBmp)
	{
		MbufCopy(g_GrabImage,g_SaveImage[g_loop1 % 3]);
		::SetEvent(g_CopyOverEvent[g_loop1 % 3]);
		g_loop1++;
	}
	MbufCopy(g_GrabImage, g_Image);
	ii.Buffer = pImage;
	return ii;
}
void __stdcall FreeDevice()
{
	if (g_SaveThread)
	{
		::TerminateThread(g_SaveThread,0);
		::CloseHandle(g_SaveThread);
		g_SaveThread = NULL;
	}
	if (g_CopyOverEvent[0])
	{
		::CloseHandle(g_CopyOverEvent[0]);
		g_CopyOverEvent[0] = NULL;
	}
	if (g_CopyOverEvent[1])
	{
		::CloseHandle(g_CopyOverEvent[1]);
		g_CopyOverEvent[1] = NULL;
	}
	if (g_CopyOverEvent[2])
	{
		::CloseHandle(g_CopyOverEvent[2]);
		g_CopyOverEvent[2] = NULL;
	}
	if(pImage)
	{
		free(pImage);
		pImage = NULL;
	}
	for(int i=0;i<ChildCounts;i++)
	{
		if(g_SubImage[i])
		{
			MbufFree(g_SubImage[i]);
			g_SubImage[i] = M_NULL;
		}
	}
	for (int j = 0; j<3; j++)
	{
		if (g_SaveImage[j])
		{
			MbufFree(g_SaveImage[j]);
			g_SaveImage[j] = M_NULL;
		}
	}
	if(g_GrabImage)
	{
		MbufFree(g_GrabImage);
		g_GrabImage = M_NULL;
	}
	if(g_Image)
	{
		MbufFree(g_Image);
		g_Image = M_NULL;
	}
	if(g_Digital)
	{
		MdigFree(g_Digital);
		g_Digital = M_NULL;
	}
	if(g_System)
	{
		MsysFree(g_System);
		g_System = M_NULL;
	}
	if(g_Application)
	{
		MappFree(g_Application);
		g_Application = M_NULL;
	}
}


MIL_INT MFTYPE ProcessingFunction(long HookType, MIL_ID HookId, void MPTYPE *HookDataPtr)
{
	HookDataStruct *UserHookDataPtr = (HookDataStruct *)HookDataPtr;
	MIL_ID ModifiedBufferId;
	MdigGetHookInfo(HookId, M_MODIFIED_BUFFER+M_BUFFER_ID, &ModifiedBufferId);
	UserHookDataPtr->ProcessedImageCount++;
	char str[MAX_PATH];
	sprintf(str,"Processing frame #%d    Buffer ID = %d\r", UserHookDataPtr->ProcessedImageCount, ModifiedBufferId);
	OutputDebugStringA(str);
	if(UserHookDataPtr->ProcessedImageCount >= ChildCounts)
	{

	}
	return 0;
}
DWORD WINAPI SaveProcessThread(LPVOID lpParameter)
{
	while (TRUE)
	{
		::WaitForSingleObject(g_CopyOverEvent[g_loop2 % 3], INFINITE);
		::ResetEvent(g_CopyOverEvent[g_loop2 % 3]);
		char dirPath[MAX_PATH];
		char tempFile[MAX_PATH];
		sprintf(tempFile,"%05d.bmp",g_loop2);
		strcpy(dirPath,SavePath);
		strcat(dirPath,"\\ImageFile\\");
		::CreateDirectoryA(dirPath,NULL);
		strcat(dirPath,tempFile);
		OutputDebugStringA(dirPath);
		MbufExportA(dirPath,M_BMP,g_SaveImage[g_loop2 % 3]);
		g_loop2++;
	}
	return 1;
}