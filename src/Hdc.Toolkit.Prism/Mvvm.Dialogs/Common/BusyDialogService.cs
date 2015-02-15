namespace Hdc.Mvvm.Dialogs
{
    public class BusyDialogService : MessageDialogService, IBusyDialogService
    {
        protected override string GetDialogRegionName()
        {
            return HdcDialogNames.BusyDialog;
        }

        protected override string GetDialogViewName()
        {
            return HdcDialogNames.BusyDialog;
        }
    }
}