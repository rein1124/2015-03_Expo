namespace Hdc.Mercury.Communication
{
    /// <summary>
    /// Syntax: (*~(OPC_PROP[0005] : <Item Access Rights> : <remark>)*)
    /// <Item Access Rights> 1 = Read Only, 2 = Write Only, 3 = Read/Write.
    /// <remark> Optional and ignored.
    /// Read Only Example: dwTemperature:DWORD; (*~(OPC_PROP[0005]:1:Read Only)*)
    /// </summary>
    public enum AccessMode
    {
        None = 0,
        Read = 1,
        Write = 2,
        ReadWrite = 3,
    }
}