using Hdc.Mvvm;
using Microsoft.Practices.Unity;

namespace Hdc.Mvvm.Dialogs
{
    public abstract class EditDataDialogService<T> : GeneralInputOutputDialogService<T>, IEditDataDialogService<T>
    {
        [InjectionMethod]
        public override void Init()
        {
            base.Init();

            Editor = GetEditor();
        }

        public IEditor<T> Editor { get; private set; }

        protected abstract IEditor<T> GetEditor();

        protected override void OnShowing(T inputData)
        {
            Editor.Input(inputData);
        }

        protected override T GetOutput()
        {
            return Editor.Output();
        }
    }
}