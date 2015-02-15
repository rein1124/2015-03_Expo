using System;
using System.Collections.Generic;
using System.Linq;
using Hdc.Mvvm.Navigation;
using Microsoft.Practices.Unity;

namespace Hdc.Mercury.Navigation
{
    public class Recorder : IRecorder
    {
        private IList<IEnumerable<Entry>> _memo = new List<IEnumerable<Entry>>();

        private class Entry
        {
            public string ScreenName { get; set; }
            public bool IsActive { get; set; }
            public int ActiveIndex { get; set; }
        }

        private int cursor = -1;

        [Dependency]
        public IScreenProvider ScreenProvider { get; set; }

        [InjectionMethod]
        public void Init()
        {
            ScreenProvider.TopScreen.ActiveChangedEvent.Subscribe(ScreenTree_ActiveChanged);

            Record();
        }

        private void ScreenTree_ActiveChanged(IScreen obj)
        {
            Record();
        }

        private void Record()
        {
            var record = ScreenProvider.Screens.Select(
                x =>
                    {
                        return new Entry()
                                   {
                                       ScreenName = x.Name,
                                       IsActive = x.IsActive,
                                       ActiveIndex = x.ActiveIndex,
                                   };
                    }
                ).ToList();

            _memo.Insert(0, record);


            if (cursor != -1)
            {
                for (int i = 0; i <= cursor; i++)
                {
                    _memo.RemoveAt(i);
                }
            }
            cursor = -1;
        }

        private void Recovery(IEnumerable<Entry> enumerable)
        {
            foreach (var entry in enumerable)
            {
                var s = ScreenProvider.FindScreen(entry.ScreenName);
                s.IsActive = entry.IsActive;
                s.ActiveIndex = entry.ActiveIndex;
            }
        }


        public void Undo()
        {
            if (cursor >= _memo.Count() - 1)
                return;
            cursor++;
            IEnumerable<Entry> enumerable = _memo[cursor+1];
            Recovery(enumerable);
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }
    }
}