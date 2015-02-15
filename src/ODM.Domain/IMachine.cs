using Hdc.Mercury;

namespace ODM.Domain
{
    // ReSharper disable InconsistentNaming
    public interface IMachine : IFacilityNodeMiddleTerminal<IMachine>
    {
        // General
        IDevice<int> General_RunningModeCodeDevice { get; }
        IDevice<bool> General_SwitchRunningModeToProductionCommandDevice { get; }
        IDevice<bool> General_SwitchRunningModeToTestCommandDevice { get; }
        IDevice<bool> General_SwitchRunningModeToLockCommandDevice { get; }

        IDevice<bool> General_UpdateWatchdogCommandDevice { get; }
        IDevice<bool> General_AppStartedPcEventDevice { get; }
        IDevice<bool> General_PlcStartedPlcEventDevice { get; }

        IDevice<bool> General_EN_IsInspectSurfaceFrontEnabledOperationDevice { get; }
        IDevice<bool> General_EN_IsInspectSurfaceBackEnabledOperationDevice { get; }
        IDevice<bool> General_EN_IsUnloadEnabledOperationDevice { get; }

        // Production
        IDevice<bool> Production_StartCommandDevice { get; }
        IDevice<bool> Production_StopCommandDevice { get; }
        IDevice<bool> Production_ResetCommandDevice { get; }

        IDevice<int> Production_ProductionSpeedDevice { get; }
        IDevice<int> Production_TotalCountDevice { get; }
        IDevice<int> Production_TotalRejectCountDevice { get; }
        IDevice<bool> Production_ResetTotalCounterCommandDevice { get; }

        IDevice<int> Production_JobCountDevice { get; }
        IDevice<int> Production_JobRejectCountDevice { get; }
        IDevice<bool> Production_ResetJobCounterCommandDevice { get; }

        // Inspection
        IDevice<bool> Inspection_SurfaceFront_GrabReadyPlcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceFront_GrabFinishedPcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceFront_InspectionFinishedWithAcceptedPcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceFront_InspectionFinishedWithRejectedPcEventDevice { get; }

        IDevice<bool> Inspection_Forward_MotionStartedPlcEventDevice { get; }
        IDevice<bool> Inspection_Forward_MotionFinishedPlcEventDevice { get; }

        IDevice<bool> Inspection_SurfaceBack_GrabReadyPlcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceBack_GrabFinishedPcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceBack_InspectionFinishedWithAcceptedPcEventDevice { get; }
        IDevice<bool> Inspection_SurfaceBack_InspectionFinishedWithRejectedPcEventDevice { get; }

        IDevice<bool> Inspection_Backward_MotionStartedPlcEventDevice { get; }
        IDevice<bool> Inspection_Backward_MotionFinishedPlcEventDevice { get; }


        // TestME
        IDevice<bool> TestME_Slider_StartMoveToFarEndCommandDevice { get; }
        IDevice<bool> TestME_Slider_StartMoveToOriginCommandDevice { get; }
        IDevice<bool> TestME_Slider_StopMoveCommandDevice { get; }
        IDevice<bool> TestME_Slider_MoveSpeedDevice { get; }
        IDevice<bool> TestME_Slider_StartVacuumCommandDevice { get; }

        IDevice<int> TestME_Slider_JogSpeedDevice { get; }
        IDevice<int> TestME_Slider_ScanSpeedDevice { get; }
        IDevice<int> TestME_Slider_StartSpeedDevice { get; }

        // Alarms
        IDevice<bool> Alarms_ResetAlarmsCommandDevice { get; }
        IDevice<bool> Alarms_ForwardClampingCylinderDownErrorDevice { get; }
        IDevice<bool> Alarms_ForwardClampingCylinderUpErrorDevice { get; }
        IDevice<bool> Alarms_BackwardClampingCylinderDownErrorDevice { get; }
        IDevice<bool> Alarms_BackwardClampingCylinderUpErrorDevice { get; }
        IDevice<bool> Alarms_ForwardClampingCylinderForwardErrorDevice { get; }
        IDevice<bool> Alarms_ForwardClampingCylinderBackwardErrorDevice { get; }
        IDevice<bool> Alarms_BackwardClampingCylinderForwardErrorDevice { get; }
        IDevice<bool> Alarms_BackwardClampingCylinderBackwardErrorDevice { get; }
        IDevice<bool> Alarms_AirPressureErrorDevice { get; }

    }

    // ReSharper restore InconsistentNaming
}