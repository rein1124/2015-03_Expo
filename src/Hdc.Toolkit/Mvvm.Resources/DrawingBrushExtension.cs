using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace Hdc.Mvvm.Resources
{
    [MarkupExtensionReturnTypeAttribute(typeof(DrawingBrush))]
    public class DrawingBrushExtension : MarkupExtension
    {
        private readonly string _name;
        private readonly double _opacity = 1;

        public DrawingBrushExtension(string name)
        {
            _name = name;
        }

        public DrawingBrushExtension(string name, double opacity = 1)
        {
            _name = name;
            _opacity = opacity;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var drawingBrush = DrawingBrushServiceLocator.Loader.Load(_name);
            if (drawingBrush == null) 
                return null;

            var ob = drawingBrush.CloneCurrentValue();
            ob.TileMode = TileMode.None;
            ob.Stretch = Stretch.Uniform;
            ob.Opacity = _opacity;

            return ob;
        }
    }
}