namespace Hdc.Mvvm.Resources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;

    public interface IIconImageSourceLoader
    {
        ImageSource Load(string iconName);
    }
}