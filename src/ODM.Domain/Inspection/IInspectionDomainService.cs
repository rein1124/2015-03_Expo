using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ODM.Domain.Inspection
{
    public interface IInspectionDomainService
    {
        IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries();

        IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries(int lastCount);

        IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries(DateTime beginDateTime, DateTime endDateTime);

        WorkpieceInfo GetWorkpieceInfoById(long id);

        void AddWorkpieceInfo(WorkpieceInfo workpieceInfo);

        void DeleteWorkpieceInfo(long id);

        void AddDefectInfo(long workpieceInfoId, DefectInfo defectInfo);

        void DeleteDefectInfo(long workpieceInfoId, long defectInfoId);

        void HandleSurfaceInspectionInfo(SurfaceInspectInfo surfaceInspectInfo);

//        void HandleAcceptedWorkpieceInfo(int surfaceTypeIndex, WorkpieceInfo workpieceInfo);
//
//        void HandleRejectedWorkpieceInfo(int surfaceTypeIndex, WorkpieceInfo workpieceInfo);
//
//        void HandleWorkpieceInfo(int surfaceTypeIndex, WorkpieceInfo workpieceInfo);
//
//        Task HandleWorkpieceInfoAsync(int surfaceTypeIndex, WorkpieceInfo workpieceInfo);
    }
}