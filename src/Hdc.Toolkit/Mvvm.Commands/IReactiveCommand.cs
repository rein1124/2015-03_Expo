//see ReactiveUI 2.4.5.0: ReactiveUI.Xaml: Interfaces.cs
using System;
using System.Windows.Input;

namespace Hdc.Mvvm.Commands
{
    /// <summary>
    /// IReactiveCommand is an Rx-enabled version of ICommand that is also an
    /// Observable. Its Observable fires once for each invocation of
    /// ICommand.Execute and its value is the CommandParameter that was
    /// provided.
    /// </summary>
    public interface IReactiveCommand : ICommand, IObservable<object>
    {
        /// <summary>
        /// Fires whenever the CanExecute of the ICommand changes. Note that
        /// this should not fire notifications unless the CanExecute changes
        /// (i.e. it should not fire 'true', 'true').
        /// </summary>
        IObservable<bool> CanExecuteObservable { get; }
    }
}