using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Hdc.Controls.Effects {

  /// <summary>
  /// Desaturating Shader Effect
  /// </summary>
  /// <remarks>
  /// This code is almost verbatim from a blog post by Anders Bursjoo
  /// See <see href="http://bursjootech.blogspot.com/2008/06/grayscale-effect-pixel-shader-effect-in.html"/>.
  /// Arbitrarily decided to put it in the MS effects namespace.
  /// </remarks>
  public class GrayScaleEffect : ShaderEffect {

// ReSharper disable InconsistentNaming
    private static readonly PixelShader _pixelShader = new PixelShader {
        UriSource = "Effects/GrayScaleEffect.ps".PackUri()
    };
// ReSharper restore InconsistentNaming

    public GrayScaleEffect() {
      PixelShader = _pixelShader;
      UpdateShaderValue(InputProperty);
      UpdateShaderValue(DesaturationFactorProperty);
    }

    public static readonly DependencyProperty InputProperty = 
      RegisterPixelShaderSamplerProperty("Input", typeof(GrayScaleEffect), 0);

    public Brush Input {
      get { return (Brush)GetValue(InputProperty); }
      set { SetValue(InputProperty, value); }
    }

    public static readonly DependencyProperty DesaturationFactorProperty = 
      DependencyProperty.Register("DesaturationFactor", typeof(double), typeof(GrayScaleEffect), new PropertyMetadata(0.0, PixelShaderConstantCallback(0)));

    public double DesaturationFactor {
      get { return (double)GetValue(DesaturationFactorProperty); }
      set { SetValue(DesaturationFactorProperty, value); }
    }

  }
}
