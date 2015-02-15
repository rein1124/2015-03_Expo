using System;
using System.Collections.Generic;
using Hdc.Mvvm.Navigation;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Linq;

namespace Hdc.Mercury.Navigation
{
    public class XmlScreenProvider : IScreenProvider
    {
//        private IList<IScreenTree> _screenTrees = new List<IScreenTree>();
        private IDictionary<string,IScreen> _screenTreeDic = new Dictionary<string, IScreen>();

        private IScreen _topScreen;

        public IList<IScreen> Screens
        {
            get { return _screenTreeDic.Select(x=>x.Value).ToList(); }
        }

        public IScreen TopScreen
        {
            get { return _topScreen; }
        }

        public IScreen FindScreen(string screenName)
        {
            return _screenTreeDic[screenName];
        }

        [Dependency]
        public IXScreenRepository XScreenRepository { get; set; }

        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        [InjectionMethod]
        public void Init()
        {
            var topScreen = XScreenRepository.Load();
            IEnumerable<XScreen> xScreens = XScreenToEnumerable(topScreen); 
            IDictionary<string, IScreen> screenTreeDic = new Dictionary<string, IScreen>();

            foreach (var pair in xScreens)
            {
                var screenTree = ServiceLocator.GetInstance<IScreen>();
                screenTree.Name = pair.Name;
                screenTree.GroupName = pair.GroupName;
                screenTree.IsMutual = pair.IsMutual;
                screenTree.IsActive = pair.IsDefaultActive;
                screenTree.ActiveIndex = pair.DefaultIndex;
                screenTreeDic.Add(pair.Name,screenTree);
            }

            foreach (var xScreen in xScreens)
            {
                var screenTree = screenTreeDic[xScreen.Name];
                foreach (var child in xScreen.Screens)
                {
                    var childScreenTree = screenTreeDic[child.Name];
                    screenTree.Add(childScreenTree);
                    childScreenTree.ParentNode = screenTree;
                }
            }


            _topScreen = screenTreeDic.FirstOrDefault().Value; 
            _screenTreeDic = screenTreeDic;
        }
         

        private IEnumerable<XScreen> XScreenToEnumerable(XScreen xScreen)
        {
            return Ex.ToEnumerable<XScreen>(xScreen, x => x.Screens);
        }

        public static class Ex
        {
            public static IEnumerable<T> ToEnumerable<T>(T top, Func<T, IEnumerable<T>> getChildren)
            {
                var children = getChildren(top);
                yield return top;
                foreach (var child in children)
                {
//                    yield return child;
                    var grandChildren = ToEnumerable(child, getChildren);
                    foreach (var grandChild in grandChildren)
                    {
                        yield return grandChild;
                    }
                }
            }
        }
    }
}