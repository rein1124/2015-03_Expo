using System;
using Microsoft.Practices.Unity;
using Hdc.Mvvm.Dialogs;

namespace Hdc.Mvvm.Dialogs
{
    public class CommonDialogServices : ICommonDialogServices
    {
        [Dependency]
        public IAskDialogService AskDialogService { get; set; }

        [Dependency]
        public IAsk3DialogService Ask3DialogService { get; set; }

        [Dependency]
        public IMessageDialogService MessageDialogService { get; set; }

        [Dependency]
        public IChangeDateDialogService ChangeDateDialogService { get; set; }

        [Dependency]
        public ICalculateDialogService CalculateDialogService { get; set; }

        [Dependency]
        public IStringInputDialogService StringInputDialogService { get; set; }


        public void Message(string message)
        {
            Message(message, () => { });
        }

        public void Message(string message, Action confirmAction)
        {
            MessageDialogService
                .Show(message)
                .Subscribe(
                    x => { confirmAction(); });
        }

        public void Ask(string message, Action confirmAction)
        {
            Ask(message, confirmAction, () => { });
        }

        public void Ask(string message, Action confirmAction, Action cancelAction)
        {
            AskDialogService
                .Show(message)
                .Subscribe(
                    x =>
                        {
                            if (x.IsCanceled)
                            {
                                cancelAction();
                                return;
                            }

                            if (!x.Data)
                            {
                                cancelAction();
                                return;
                            }

                            confirmAction();
                        });
        }

        public void Ask(string message, Action yesAction, Action noAction, Action cancelAction)
        {
            Ask3DialogService
                .Show(message)
                .Subscribe(
                    x =>
                        {
                            if (x.IsCanceled)
                            {
                                cancelAction();
                                return;
                            }

                            if (x.Data == Ask3DialogResult.Cancel)
                            {
                                cancelAction();
                                return;
                            }

                            if (x.Data == Ask3DialogResult.Yes)
                            {
                                yesAction();
                                return;
                            }

                            if (x.Data == Ask3DialogResult.No)
                            {
                                noAction();
                                return;
                            }
                        });
        }

        public void ChangeDateTimeProperty(string title, Func<DateTime> get, Action<DateTime> set)
        {
            ChangeDateDialogService.Show(get())
                  .Subscribe(args =>
                  {
                      if (args.IsCanceled) return;
                      set(args.Data);
                  });
        }

        public void ChangeStringProperty(string title, Func<string> get, Action<string> set)
        {
            StringInputDialogService
                 .Show(title, get())
                 .Subscribe(arg =>
                 {
                     if (arg.IsCanceled) return;
                     set(arg.Data);
                 });
        }

        public void ChangeInt32Property(string title, Func<int> get, Action<int> set, int min, int max)
        {
            CalculateDialogService
               .Show(min, max, 1, get(), title)
               .Subscribe(arg =>
               {
                   if (arg.IsCanceled) return;
                   set((int)arg.Data);
               });
        }

        public void ChangeDoubleProperty(string title, Func<double> get, Action<double> set, double min, double max, double unit)
        {
            CalculateDialogService
              .Show(min, max, unit, get(), title)
              .Subscribe(arg =>
              {
                  if (arg.IsCanceled) return;
                  set(arg.Data);
              });
        }

        public void InputString(string title, string defaultString, Action<string> confirmAction, Action cancelAction)
        {
            StringInputDialogService
                .Show(title, defaultString)
                .Subscribe(arg =>
                {
                    if (arg.IsCanceled)
                    {
                        cancelAction();
                        return;
                    }

                    confirmAction(arg.Data);
                });
        }
    }
}