/*using System;
using System.Linq.Expressions;
using System.Reactive.Linq;
using Hdc.Mvvm;

namespace Hdc.Reactive
{
    public static class ValueMonitorExtensions
    {
        public static IRaisePropertyChangedOnEntry RaisePropertyChangedOn<T>(
            this ViewModel viewModel,
            IObservable<T> observable)
        {
            return new RaisePropertyChangedOnEntry<T>(viewModel, observable);
        }

        public interface IRaisePropertyChangedOnEntry
        {
            IRaisePropertyChangedOnEntry On<T>(Expression<Func<T>> propertyExpression);
            IRaisePropertyChangedOnEntry OnUsingDispatcher<T>(Expression<Func<T>> propertyExpression);
        }

        private class RaisePropertyChangedOnEntry<T> : IRaisePropertyChangedOnEntry
        {
            private ViewModel _viewModel;
            private IObservable<T> _observable;

            public RaisePropertyChangedOnEntry(ViewModel viewModel, IObservable<T> observable)
            {
                _viewModel = viewModel;
                _observable = observable;
            }

            public IRaisePropertyChangedOnEntry On<T1>(Expression<Func<T1>> propertyExpression)
            {
                _observable
                    .Subscribe(v => _viewModel.RaisePropertyChanged(propertyExpression));
                return this;
            }

            public IRaisePropertyChangedOnEntry OnUsingDispatcher<T>(
                Expression<Func<T>> propertyExpression)
            {
                _observable
                    .ObserveOnDispatcher()
                    .Subscribe(v => _viewModel.RaisePropertyChanged(propertyExpression));
                return this;
            }
        }
    }
}*/