/*//see: ViewModelCommandBehavior.zip
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Hdc.Mvvm.Commands;

namespace Hdc.Windows.Interactivity
{
    public class InvokeVMCommandBehavior : Behavior<FrameworkElement>
    {
        #region ViewModel Command
        public ICommand VMCommand
        {
            get { return (ICommand)GetValue(VMCommandProperty); }
            set { SetValue(VMCommandProperty, value); }
        }

        public static readonly DependencyProperty VMCommandProperty =
            DependencyProperty.Register(
            "VMCommand", typeof(ICommand), typeof(InvokeVMCommandBehavior), new PropertyMetadata(null));

        #endregion

        #region DelegateCommand
        public DelegateCommandEx DelegateCommand
        {
            get { return (DelegateCommandEx)GetValue(DelegateCommandProperty); }
            set { SetValue(DelegateCommandProperty, value); }
        }

        public static readonly DependencyProperty DelegateCommandProperty =
            DependencyProperty.Register(
            "DelegateCommand", typeof(DelegateCommandEx), typeof(InvokeVMCommandBehavior), new PropertyMetadata(null));


        #endregion

        public InvokeVMCommandBehavior()
        {

        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.Loaded +=
                (s, e) =>
                {
                    this.DelegateCommand.CanExecuteMethod = (x) => VMCommand.CanExecute(x);
                    this.DelegateCommand.ExecuteMethod = (x) => VMCommand.Execute(x);
                };

        }



        protected override void OnDetaching()
        {
            base.OnDetaching();


        }


    }
}*/