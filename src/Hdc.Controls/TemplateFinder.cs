using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    /// <summary>
    /// A ContentPresenter that sits in the logical tree and can find the DataTemplate
    /// for any given item as if that item were in the same logical tree.
    /// </summary>
    public class TemplateFinder : ContentPresenter
    {
        public TemplateFinder()
        {
            //Hide ourselves because we don't want to show or take up space.
            //We're only here to find DataTemplates not show anything.
            this.Visibility = Visibility.Collapsed;
        }

        public DataTemplate FindTemplate(object item)
        {
            //Temporarily insert the content so we can find the DataTemplate we would use
            //If we were actually displaying.
            this.Content = item;
            DataTemplate template = base.ChooseTemplate();
            //Remove the content so we don't actually display anything.
            this.Content = null;
            return template;
        }
    }
}
