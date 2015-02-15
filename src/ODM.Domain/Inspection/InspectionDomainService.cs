using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hdc.Mercury;
using Hdc.Patterns;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;
using Shared;

namespace ODM.Domain.Inspection
{
    public class InspectionDomainService : IInspectionDomainService
    {
        [Dependency]
        public IWorkpieceInfoRepository Repository { get; set; }

        [Dependency]
        public IMachineProvider MachineProvider { get; set; }

        public IMachine Machine
        {
            get { return MachineProvider.Machine; }
        }

        [Dependency]
        public IEventBus EventBus { get; set; }

        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }

        private readonly Queue<WorkpieceInfo> _workpieceInfos = new Queue<WorkpieceInfo>(6);
        private readonly Queue<SurfaceInspectInfo> _surfaceInspectInfos = new Queue<SurfaceInspectInfo>();

        [InjectionMethod]
        public void Init()
        {
        }

        private object locker = new object();

        public void HandleSurfaceInspectionInfo(SurfaceInspectInfo inspectInfo)
        {
            Debug.WriteLine("InspectionDomainService.HandleSurfaceInspectionInfo(): " + inspectInfo.SurfaceTypeIndex);

            lock (locker)
            {
                WorkpieceInfo wi;
                wi = _workpieceInfos.SingleOrDefault(x => x.Index == inspectInfo.WorkpieceIndex);
                if (wi == null)
                {
                    wi = new WorkpieceInfo
                         {
                             Index = inspectInfo.WorkpieceIndex,
                             InspectDateTime = DateTime.Now,
                         };
                    Repository.Add(wi);
                    Repository.UnitOfWork.Commit();
                    _workpieceInfos.Enqueue(wi);
                }
                _surfaceInspectInfos.Enqueue(inspectInfo);

                var siis = new List<StoredImageInfo>();

                wi.InspectDateTime = DateTime.Now;
                wi.DefectInfos.AddRange(_surfaceInspectInfos.SelectMany(x => x.InspectInfo.DefectInfos));
                wi.MeasurementInfos.AddRange(_surfaceInspectInfos.SelectMany(x => x.InspectInfo.MeasurementInfos));

                if (MachineConfigProvider.MachineConfig.Reporting_StoreAcceptedImageEnabled && !wi.DefectInfos.Any())
                {
                    foreach (var surfaceInspectInfo in _surfaceInspectInfos)
                    {
                        var sii = surfaceInspectInfo.SaveImage();
                        siis.Add(sii);
                    }
                }

                if (MachineConfigProvider.MachineConfig.Reporting_StoreRejectedImageEnabled && wi.DefectInfos.Any())
                {
                    foreach (var surfaceInspectInfo in _surfaceInspectInfos)
                    {
                        var sii = surfaceInspectInfo.SaveImage();
                        siis.Add(sii);
                    }
                }

                wi.StoredImageInfo.AddRange(siis);
                Repository.Update(wi);
                Repository.UnitOfWork.Commit();

                _surfaceInspectInfos.Clear();
            }
        }

        public IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries()
        {
            return Repository
                .GetAll()
                .Select(x => x.ToEntry())
                .ToList();
        }

        public IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries(int lastCount)
        {
            return Repository
                .GetQuery()
                .TakeLast(lastCount)
                .Select(x => x.ToEntry())
                .ToList();
        }

        public IList<WorkpieceInfoEntry> GetWorkpieceInfoEntries(DateTime beginDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }

        public WorkpieceInfo GetWorkpieceInfoById(long id)
        {
            return Repository.GetById(id);
        }

        public void AddWorkpieceInfo(WorkpieceInfo workpieceInfo)
        {
            Repository.Add(workpieceInfo);
            Repository.UnitOfWork.Commit();

            EventBus.Publish(new WorkpieceInfoAddedDomainEvent()
                             {
                                 WorkpieceInfo = workpieceInfo,
                             });
        }

        public void DeleteWorkpieceInfo(long id)
        {
            var e = Repository.GetById(id);
            e.DeleteImages();
            //ImageSaveLoadService.DeleteImage(e.InspectDateTime.Value,e.ImageGuid);

            Repository.Delete(e);
            Repository.UnitOfWork.Commit();

            EventBus.Publish(new WorkpieceInfoRemovedDomainEvent()
                             {
                                 Id = id,
                             });
        }

        public void AddDefectInfo(long workpieceInfoId, DefectInfo defectInfo)
        {
            var di = Repository.GetById(workpieceInfoId);
            di.DefectInfos.Add(defectInfo);
            Repository.Update(di);
            Repository.UnitOfWork.Commit();

            EventBus.Publish(new DefectInfoAddedDomainEvent()
                             {
                                 WorkpieceInfoId = workpieceInfoId,
                                 DefectInfo = defectInfo,
                             });
        }

        public void DeleteDefectInfo(long workpieceInfoId, long defectInfoId)
        {
            var di = Repository.GetById(workpieceInfoId);
            var def = di.DefectInfos.SingleOrDefault(x => x.Id == defectInfoId);
            if (def == null)
                return;

            di.DefectInfos.Remove(def);
            Repository.Update(di);
            Repository.UnitOfWork.Commit();

            EventBus.Publish(new DefectInfoRemovedDomainEvent()
                             {
                                 WorkpieceInfoId = workpieceInfoId,
                                 DefectInfoId = defectInfoId,
                             });
        }
    }
}