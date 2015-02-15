using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    public abstract class IndicatorSeriesBase : Control
    {
        public abstract void Refresh();

        public Canvas Canvas { protected get; set; }

        public IndicatorViewer IndicatorViewer { protected get; set; }

        public static object GetPropertyValue(object src, string propName)
        {
            Type type = src.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propName);
            return propertyInfo.GetValue(src, null);
        }

        #region StrokePath

        public string StrokePath
        {
            get { return (string)GetValue(StrokePathProperty); }
            set { SetValue(StrokePathProperty, value); }
        }

        public static readonly DependencyProperty StrokePathProperty = DependencyProperty.Register(
            "StrokePath", typeof(string), typeof(IndicatorSeriesBase));

        #endregion

        #region StrokeThicknessPath

        public string StrokeThicknessPath
        {
            get { return (string)GetValue(StrokeThicknessPathProperty); }
            set { SetValue(StrokeThicknessPathProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessPathProperty = DependencyProperty.Register(
            "StrokeThicknessPath", typeof(string), typeof(IndicatorSeriesBase));

        #endregion

        #region StrokeDashArrayPath

        public string StrokeDashArrayPath
        {
            get { return (string)GetValue(StrokeDashArrayPathProperty); }
            set { SetValue(StrokeDashArrayPathProperty, value); }
        }

        public static readonly DependencyProperty StrokeDashArrayPathProperty = DependencyProperty.Register(
            "StrokeDashArrayPath", typeof(string), typeof(IndicatorSeriesBase));

        #endregion
    }
}