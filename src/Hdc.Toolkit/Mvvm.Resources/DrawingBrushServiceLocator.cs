//using System.ComponentModel;
//using System.Windows;

namespace Hdc.Mvvm.Resources
{
    public static class DrawingBrushServiceLocator
    {
        private static IDrawingBrushLoader _loader = new MockDrawingBrushLoader();

/*        static DrawingBrushServiceLocator()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                //_loader = new XamlDrawingBrushLoader();    
            }
            else
            {
                _loader = new MockDrawingBrushLoader();    
            }
        }*/

        public static IDrawingBrushLoader Loader
        {
            get
            {
                return _loader;
            }
            set
            {
                _loader = value;
            }
        }
    }
}