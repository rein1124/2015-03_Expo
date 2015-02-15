using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Markup;

namespace Hdc.Mvvm.Navigation
{
    [ContentProperty("ScreenInfos")]
    public class ScreenInfo
    {
        private IList<ScreenInfo> _screenInfos = new Collection<ScreenInfo>();

        public ScreenInfo()
        {
        }

        public ScreenInfo(string name, params ScreenInfo[] screenInfos)
        {
            Name = name;
            foreach (var screenInfo in screenInfos)
            {
                _screenInfos.Add(screenInfo);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IList<ScreenInfo> ScreenInfoCollection
        {
            get { return _screenInfos; }
            set { _screenInfos = value; }
        }

        public ICollection<ScreenInfo> ScreenInfos
        {
            get { return _screenInfos; }
        }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsMutual { get; set; }

        public bool IsEnabled { get; set; }
    }
}