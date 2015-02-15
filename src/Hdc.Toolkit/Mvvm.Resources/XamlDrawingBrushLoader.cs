using System.Reflection;
using Hdc.Reflection;

namespace Hdc.Mvvm.Resources
{
    using System.Windows.Media;

    public class XamlDrawingBrushLoader : IDrawingBrushLoader
    {
        private readonly ResourceLoaderManager _iconResourceLoaderManager;

        private static string defaultIconsDir =
            Assembly.GetExecutingAssembly().GetAssemblyDirectoryPath().Combine("icon");

        public XamlDrawingBrushLoader():
            this(defaultIconsDir)
        {
        }

        public XamlDrawingBrushLoader(string iconDir)
        {
            _iconResourceLoaderManager = new ResourceLoaderManager(iconDir);
        }

//        protected abstract string GetXamlDirectory();

        public DrawingBrush Load(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return _iconResourceLoaderManager.LoadXaml(name) as DrawingBrush;
        }
    }
}