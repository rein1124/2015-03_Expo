using System;
using System.Linq.Expressions;
using System.Reactive.Linq;
using Hdc;
using Hdc.Mvvm;

namespace Hdc.Reactive
{
    public static class ValueMonitorExtensions
    {
        public static void ObserveRetained<T>(this IValueMonitor<T> valueMonitor, IRetainedObservable<T> ob)
        {
            valueMonitor.Observe(ob, ob.Value);
        }

        public static void RaisePropertyChangedOn<T>(this ViewModel viewModel,
                                                   IObservable<T> observable,
                                                   Expression<Func<T>> propertyExpression)
        {
            observable
                .Subscribe(v => viewModel.RaisePropertyChanged(propertyExpression));
        }

        public static void RaisePropertyChangedOnGeneric<TViewModel,TProperty>(this ViewModel viewModel,
                                                    IObservable<TViewModel> observable,
                                                    Expression<Func<TProperty>> propertyExpression)
        {
            observable
                .Subscribe(v => viewModel.RaisePropertyChanged(propertyExpression));
        }


        public static ViewModel RaisePropertyChangedOnUsingDispatcher<T>(this ViewModel viewModel,
                                                                         IObservable<T> observable,
                                                                         Expression<Func<T>> propertyExpression)
        {
            observable
                .ObserveOnDispatcher()
                .Subscribe(v => viewModel.RaisePropertyChanged(propertyExpression));
            return viewModel;
        }

        public static ViewModel RaisePropertyChangedOnUsingDispatcher<T>(this ViewModel viewModel,
                                                                         IObservable<T> observable,
                                                                         params Expression<Func<T>>[] propertyExpression)
        {
            foreach (var expression in propertyExpression)
            {
                RaisePropertyChangedOnUsingDispatcher(viewModel, observable, expression);
            }
            return viewModel;
        }

        public static ViewModel RaisePropertyChangedOnUsingDispatcher<T>(this ViewModel viewModel,
                                                                         IObservable<T> observable,
                                                                         string propertyName)
        {
            observable
                .ObserveOnDispatcher()
                .Subscribe(v => viewModel.RaisePropertyChanged(propertyName));
            return viewModel;
        }
    }
}