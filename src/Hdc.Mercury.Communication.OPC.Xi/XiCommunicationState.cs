namespace Hdc.Mercury.Communication.OPC.Xi
{
    public enum XiCommunicationState
    {
        Initialized = 0,

        CanNotConnectToServer = 1,

        RegisterItemsFailed = 2,

        AddToUpdateListFailed = 3,

        ReadItemError = 4,

        WriteItemError = 5
    }
}