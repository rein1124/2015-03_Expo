using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    /// <summary>
    /// A DataTemplateSelector that bases its DataTemplate decisions on a TemplateFinder which
    /// is possibly in a different logical tree.
    /// </summary>
    public class TunnelDataTemplateSelector : DataTemplateSelector
    {
        TemplateFinder _templateFinder;

        public TunnelDataTemplateSelector(TemplateFinder templateFinder)
            : base()
        {
            this._templateFinder = templateFinder;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template = this._templateFinder.FindTemplate(item);
            return template;
        }
    }
}
