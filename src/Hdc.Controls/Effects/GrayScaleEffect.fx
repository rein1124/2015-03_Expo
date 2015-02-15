//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- GrayScale
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float desatFactor : register(c0);

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D implicitInput : register(S0);

//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------

float4 main(float2 uv : TEXCOORD) : COLOR
{
    float4 color = tex2D(implicitInput, uv);
    float gray = color.r * 0.3 + color.g * 0.59 + color.b *0.11;    
    
    float4 result;    
    result.r = (color.r - gray) * desatFactor + gray;
    result.g = (color.g - gray) * desatFactor + gray;
    result.b = (color.b - gray) * desatFactor + gray;
    result.a = color.a;
    
    return result;
}


