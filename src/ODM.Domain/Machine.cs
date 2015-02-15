using Hdc.Mercury;
using Hdc.Patterns;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;
using Shared;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace ODM.Domain
{
    internal class Machine : DomainFacilityNodeMiddleTerminal<IMachine>, IMachine
    {
        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        [Dependency]
        public IDeviceAccessManager DeviceAccessManager { get; set; }

        [Dependency]
        public IEventBus EventBus { get; set; }

        [Dependency]
        public ICommandBus CommandBus { get; set; }

        protected override void OnInitialized(IDeviceGroup context)
        {
            base.OnInitialized(context);
        }

        // General
        [Device]
        public IDevice<bool> General_UpdateWatchdogCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> General_PlcStartedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> General_PlcStartedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> General_AppStartedPcEventDevice { get; private set; }

        [Device]
        public IDevice<int> General_RunningModeCodeDevice { get; private set; }
        [Device]
        public IDevice<bool> General_SwitchRunningModeToProductionCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> General_SwitchRunningModeToTestCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> General_SwitchRunningModeToLockCommandDevice { get; private set; }

        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceFrontEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceBackEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceTopEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceBottomEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceLeftEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsInspectSurfaceRightEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsTurnOverEnabledOperationDevice { get; private set; }
        [Device]
        public IDevice<bool> General_EN_IsUnloadEnabledOperationDevice { get; private set; }

        //  Production
        [Device]
        public IDevice<bool> Production_StartCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_StopCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_ResetCommandDevice { get; private set; }
        [Device]
        public IDevice<int> Production_ProductionSpeedDevice { get; private set; }
        [Device]
        public IDevice<int> Production_TotalCountDevice { get; private set; }
        [Device]
        public IDevice<int> Production_TotalRejectCountDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_ResetTotalCounterCommandDevice { get; private set; }
        [Device]
        public IDevice<int> Production_JobCountDevice { get; private set; }
        [Device]
        public IDevice<int> Production_JobRejectCountDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_ResetJobCounterCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_P2GrabReadyPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Production_P2GrabReadyPlcEventAckDevice { get; private set; }

        // Inspection
        [Device]
        public IDevice<bool> Inspection_SurfaceFront_GrabReadyPlcEventDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceFront_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceFront_GrabPassedPlcEventDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceFront_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceFront_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceFront_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceFront_InspectionFinishedWithRejectedPcEventDevice { get; private set; }

        [Device]
        public IDevice<bool> Inspection_Forward_MotionStartedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_Forward_MotionFinishedPlcEventDevice { get; private set; }

        [Device]
        public IDevice<bool> Inspection_SurfaceBack_GrabReadyPlcEventDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceBack_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceBack_GrabPassedPlcEventDevice { get; private set; }
//        [Device]
//        public IDevice<bool> Inspection_SurfaceBack_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBack_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBack_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBack_InspectionFinishedWithRejectedPcEventDevice { get; private set; }

        [Device]
        public IDevice<bool> Inspection_Backward_MotionStartedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_Backward_MotionFinishedPlcEventDevice { get; private set; }

        [Device]
        public IDevice<bool> Inspection_SurfaceTop_GrabReadyPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_GrabPassedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceTop_InspectionFinishedWithRejectedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_GrabReadyPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_GrabPassedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceBottom_InspectionFinishedWithRejectedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_GrabReadyPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_GrabPassedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceLeft_InspectionFinishedWithRejectedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_GrabReadyPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_GrabReadyPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_GrabPassedPlcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_GrabPassedPlcEventAcknowledgeDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_GrabFinishedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_InspectionFinishedWithAcceptedPcEventDevice { get; private set; }
        [Device]
        public IDevice<bool> Inspection_SurfaceRight_InspectionFinishedWithRejectedPcEventDevice { get; private set; }

        // TestME
        [Device]
        public IDevice<bool> TestME_Slider_StartMoveToFarEndCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_Slider_StartMoveToOriginCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_Slider_StopMoveCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_Slider_MoveSpeedDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_Slider_StartVacuumCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_TurnOver_StartCylinderCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_TurnOver_StartVacuumCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_TurnOver_MoveUpCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_TurnOver_MoveDownCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_TurnOver_StopCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_Robot_StartVacuumCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> TestME_IntelliCam_StartGrabCommandDevice { get; private set; }

        [Device]
        public IDevice<int> TestME_Slider_JogSpeedDevice { get; private set; }
        [Device]
        public IDevice<int> TestME_Slider_ScanSpeedDevice { get; private set; }
        [Device]
        public IDevice<int> TestME_Slider_StartSpeedDevice { get; private set; }

        // Alarms
        [Device]
        public IDevice<bool> Alarms_ResetAlarmsCommandDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_ForwardClampingCylinderDownErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_ForwardClampingCylinderUpErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_BackwardClampingCylinderDownErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_BackwardClampingCylinderUpErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_ForwardClampingCylinderForwardErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_ForwardClampingCylinderBackwardErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_BackwardClampingCylinderForwardErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_BackwardClampingCylinderBackwardErrorDevice { get; private set; }
        [Device]
        public IDevice<bool> Alarms_AirPressureErrorDevice { get; private set; }
    }

}
// ReSharper restore UnusedAutoPropertyAccessor.Local
// ReSharper restore InconsistentNaming