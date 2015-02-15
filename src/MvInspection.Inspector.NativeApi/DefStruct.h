#pragma once

typedef unsigned char* PIMAGE;

struct ImageInfo
{
	int Index;
	int SurfaceTypeIndex;
	int Width;
	int Height;
	int BitsPerPixel;
	unsigned char* Buffer;
};

struct InspectInfo
{
	int Index;
	int SurfaceTypeIndex;
	int HasError;
	int DefectsCount;
	int MeasurementsCount;
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
};
