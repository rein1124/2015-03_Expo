using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Subjects;
using Hdc.Collections.Generic;
using System.Linq;
using Hdc.Reactive;
using Microsoft.Practices.Prism.Commands;

namespace Hdc.Mvvm.Navigation
{
    public class Screen : Tree<IScreen>, IScreen, INotifyPropertyChanged
    {
        private string _name;

        private bool _isMutual = true;

        private int _activeIndex;

        private int _defaultIndex;

        private Subject<IScreen> _activeChangedEvent = new Subject<IScreen>();

        //TODO temperory commented
        //private IList<IDeviceUpdateObserver> _deviceUpdateObservers = new List<IDeviceUpdateObserver>();

        private bool _isActived;

        private bool _isDefaultActive = true;

        private bool _isActive;

        private DelegateCommand<IScreen> _activateScreenCommand;

        private DelegateCommand<IScreen> _deactivateScreenCommand;

        private Subject<IScreen> _onInitialEvent = new Subject<IScreen>();

        private Subject<IScreen> _onEnterEvent = new Subject<IScreen>();

        private Subject<IScreen> _onExitEvent = new Subject<IScreen>();

        private DelegateCommand<string> _activateScreenCommandWithName;

        private DelegateCommand<string> _deactivateScreenCommandWithName;

        private Subject<ScreenChangingEventArgs> _screenChangingEvent = new Subject<ScreenChangingEventArgs>();
        public event PropertyChangedEventHandler PropertyChanged;

        public Screen()
        {
            _activateScreenCommand = new DelegateCommand<IScreen>(
                x => { ActivateChildScreen(x); });

            _deactivateScreenCommand = new DelegateCommand<IScreen>(
                x => { DeactivateScreen(x); });


            _activateScreenCommandWithName = new DelegateCommand<string>(
                x => { ActivateChildScreen(this[x]); });

            _deactivateScreenCommandWithName = new DelegateCommand<string>(
                x => { DeactivateScreen(this[x]); });

            //
            IsEnabledSubject = new NotifyPropertyChangedRetainedSubject<bool> {Value = true};
            IsEnabledSubject.Subscribe(b => { RaisePropertyChanged("IsEnabled"); });
        }

        public Screen(string name) : this()
        {
            Name = name;
        }


        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) return;
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string GroupName { get; set; }

        public IScreen ParentScreen
        {
            get { return ParentNode; }
        }

        public bool IsMutual
        {
            get { return _isMutual; }
            set
            {
                if (Equals(_isMutual, value)) return;
                _isMutual = value;
                RaisePropertyChanged("IsMutual");
            }
        }

        public int ActiveIndex
        {
            get { return _activeIndex; }
            set
            {
                if (Equals(_activeIndex, value)) return;
                _activeIndex = value;
                RaisePropertyChanged("ActiveIndex");
            }
        }

        public int DefaultIndex
        {
            get { return _defaultIndex; }
            set
            {
                if (Equals(_defaultIndex, value)) return;
                _defaultIndex = value;
                RaisePropertyChanged("DefaultIndex");
            }
        }

        public bool IsActived
        {
            get { return _isActived; }
            set
            {
                if (Equals(_isActived, value)) return;
                _isActived = value;
                RaisePropertyChanged("IsActived");
            }
        }

        public bool IsDefaultActive
        {
            get { return _isDefaultActive; }
            set
            {
                if (Equals(_isDefaultActive, value)) return;
                _isDefaultActive = value;
                RaisePropertyChanged("IsDefaultActive");
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                if (Equals(_isActive, value)) return;
                _isActive = value;
                RaisePropertyChanged("IsActive");


                if (value)
                    _onEnterEvent.OnNext(this);
                else
                {
                    _onExitEvent.OnNext(this);
                }
            }
        }

        public DelegateCommand<IScreen> ActivateScreenCommand
        {
            get { return _activateScreenCommand; }
        }

        public IList<IScreen> Screens
        {
            get { return ChildNodes; }
        }

        public IObservable<IScreen> ActiveChangedEvent
        {
            get { return _activeChangedEvent; }
        }

        public IObservable<IScreen> OnInitialEvent
        {
            get { return _onInitialEvent; }
        }

        public IObservable<IScreen> OnEnterEvent
        {
            get { return _onEnterEvent; }
        }

        public IObservable<IScreen> OnExitEvent
        {
            get { return _onExitEvent; }
        }


        public DelegateCommand<IScreen> DeactivateScreenCommand
        {
            get { return _deactivateScreenCommand; }
        }

        //TODO temperory commented
//        public IList<IDeviceUpdateObserver> DeviceUpdateObservers
//        {
//            get { return _deviceUpdateObservers; }
//        }

        public DelegateCommand<string> ActivateScreenCommandWithName
        {
            get { return _activateScreenCommandWithName; }
        }

        public DelegateCommand<string> DeactivateScreenCommandWithName
        {
            get { return _deactivateScreenCommandWithName; }
        }

        public bool IsEnabled
        {
            get { return IsEnabledSubject.Value; }
            set { IsEnabledSubject.Value = value; }
        }

        public IRetainedSubject<bool> IsEnabledSubject { get; set; }

        public IScreen this[string screenName]
        {
            get
            {
                var screen = Screens.SingleOrDefault(x => x.Name == screenName);
                return screen;
            }
        }

        public void Initial()
        {
            _onInitialEvent.OnNext(this);
            foreach (var screen in Screens)
            {
                screen.Initial();
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ActivateChildScreen(IScreen screen)
        {
            var child = screen as Screen;
            var args = new ScreenChangingEventArgs();
            child._screenChangingEvent.OnNext(args);


            if(args.IsHandled && args.IsCanceled)
            {
                return;
            }

            if (screen == null)
            {
                Debug.WriteLine("ActivateChildScreen with null");
                return;
            }
            if (!Contains(screen))
                throw new ArgumentException("cannot activate screen, because the screen %0 doesn't contains screen %1");


            if (_isMutual)
            {
                foreach (var childNode in ChildNodes)
                {
                    if (childNode == screen)
                        continue;

                    childNode.IsActive = false;
//                    _onExitEvent.OnNext(this);
                }

                ActiveIndex = screen.Index;
            }

            screen.IsActive = true;

//            _onEnterEvent.OnNext(this);
            if (IsRootNode)
            {
            }
            else
            {
                ParentScreen.ActivateChildScreen(this);
            }


//            if (ActiveChanged != null)
//                ActiveChanged(this);

            _activeChangedEvent.OnNext(this);
        }

        public void Activate()
        {
            if (IsRootNode)
                return;

            ParentScreen.ActivateChildScreen(this);
        }

        public void DeactivateScreen(IScreen screen)
        {
            if (!Contains(screen))
                throw new ArgumentException("cannot activate screen, because the screen %0 doesn't contains screen %1");

            if (_isMutual)
            {
//                ChildNodes.ForEach(x => x.IsActive = false);
//                screenTree.IsActive = true;
                ActiveIndex = _defaultIndex;
            }
            else
            {
                screen.IsActive = false;
//                _onExitEvent.OnNext(this);
            }
        }


        public IEnumerable<IScreen> GetActiveSubTrees()
        {
            return GetActiveSubTrees(this);
        }

        public IObservable<ScreenChangingEventArgs> ScreenChangingEvent
        {
            get { return _screenChangingEvent; }
        }

        public static IEnumerable<IScreen> GetActiveSubTrees(IScreen top)
        {
            var children = top.Screens;
            if (!top.IsActive)
                yield break;

            yield return top;
            foreach (var child in children)
            {
                //                    yield return child;
                var grandChildren = GetActiveSubTrees(child);
                foreach (var grandChild in grandChildren)
                {
                    yield return grandChild;
                }
            }
        }

        public Screen AddChildScreen(Screen screen)
        {
            screen.ParentNode = this;
            Add(screen);
            return this;
        }

        public Screen AddChildScreens(params Screen[] screen)
        {
            foreach (var screenTree in screen)
            {
                AddChildScreen(screenTree);
            }

            return this;
        }
    }
}