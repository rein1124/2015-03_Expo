using System;

namespace Hdc.Mvvm.Dialogs
{
    public interface ICommonDialogServices
    {
        void Message(string message);

        void Message(string message, Action confirmAction);

        void Ask(string message, Action confirmAction);

        void Ask(string message, Action confirmAction, Action cancelAction);

        void Ask(string message, Action yesAction, Action noAction, Action cancelAction);

        void ChangeDateTimeProperty(string title, Func<DateTime> get, Action<DateTime> set);

        void ChangeStringProperty(string title, Func<string> get, Action<string> set);

        void ChangeInt32Property(string title, Func<int> get, Action<int> set, int min, int max);

        void ChangeDoubleProperty(string title, Func<double> get, Action<double> set, double min, double max,
                                  double unit);
        void InputString(string title, string defaultString, Action<string> confirmAction, Action cancelAction);
    }
}