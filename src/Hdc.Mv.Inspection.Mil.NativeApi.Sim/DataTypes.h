
struct Point
{
	double X;
	double Y;
};

struct Line
{
	double X1;
	double X2;
	double Y1;
	double Y2;
};

struct Circle
{
	double CenterX;
	double CenterY;
	double Radius;
};

struct ImageInfo
{
	int Index;
	int SurfaceTypeIndex;
	int PixelWidth;
	int PixelHeight;
	int BitsPerPixel;
	unsigned char* BufferPtr;
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
