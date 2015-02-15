namespace ODM.Domain.Inspection
{
    public enum InspectState
    {
        Default,
        Ready,
        Grabbing,
        Grabbed,
        Calibrating,
        Calibrated,
        Inspecting,
//        Inspected,
        InspectedWithAccepted,
        InspectedWithRejected,
    }
}