using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hdc.Collections.Generic;
using Hdc.Patterns;
using Microsoft.Practices.ServiceLocation;
using ODM.Domain.Inspection;
using ODM.Domain.Schemas;
using Omu.ValueInjecter;

namespace ODM.Presentation.ViewModels.Schemas
{
    public static class Ex
    {
        static Ex()
        {
            Mapper.CreateMap<IProductionSchemaViewModel, ProductionSchema>();
            Mapper.CreateMap<ProductionSchema, IProductionSchemaViewModel>()
                  .ConstructUsing(new Func<ProductionSchema, IProductionSchemaViewModel>(
                                      x => ServiceLocator.Current.GetInstance<IProductionSchemaViewModel>()));

            Mapper.CreateMap<IParameterEntryViewModel, ParameterEntry>();
            Mapper.CreateMap<ParameterEntry, IParameterEntryViewModel>()
                  .ConstructUsing(new Func<ParameterEntry, IParameterEntryViewModel>(
                                      x => ServiceLocator.Current.GetInstance<IParameterEntryViewModel>()));

            Mapper.CreateMap<List<ParameterEntry>, IList<IParameterEntryViewModel>>()
                  .ConvertUsing(tis => tis.MapToObservableCollection<ParameterEntry, IParameterEntryViewModel>());
        }

        public static IProductionSchemaViewModel ToViewModel(this ProductionSchema entity)
        {
            var dto = entity.Map<ProductionSchema, IProductionSchemaViewModel>();
            return dto;
        }

        public static ProductionSchema ToEntity(this IProductionSchemaViewModel vm)
        {
            var dto = vm.Map<IProductionSchemaViewModel, ProductionSchema>();
            return dto;
        }

        public static IParameterEntryViewModel ToViewModel(this ParameterEntry entity)
        {
            var dto = entity.Map<ParameterEntry, IParameterEntryViewModel>();
            return dto;
        }

        public static ParameterEntry ToEntity(this IParameterEntryViewModel vm)
        {
            var dto = vm.Map<IParameterEntryViewModel, ParameterEntry>();
            return dto;
        }


        public static IParameterEntryViewModel GetParameterEntry(this IProductionSchemaViewModel ps,
                                                                 string parameterName)
        {
            return ps.ParameterEntries.SingleOrDefault(x => x.Name == parameterName);
        }

        public static int GetParameterValue(this IProductionSchemaViewModel ps, string parameterName)
        {
            var pe = ps.GetParameterEntry(parameterName);
            if (pe == null)
                return -1;

            return pe.Value;
        }

        public static void SetParameterValue(this IProductionSchemaViewModel ps,
                                             string parameterName, int value)
        {
            ps.GetParameterEntry(parameterName).Value = value;
        }

        public static void CopyValuesFrom(this IProductionSchemaViewModel target, IProductionSchemaViewModel source)
        {
            target.Index = source.Index;
            target.Name = source.Name;
            target.Comment = source.Comment;
            target.IsActive = source.IsActive;
            target.ModifiedDateTime = source.ModifiedDateTime;
            target.DownloadDateTime = source.DownloadDateTime;

            target.ParameterEntries
                  .CopyValuesFrom<IParameterEntryViewModel, IParameterEntryViewModel>(source.ParameterEntries,
                                                                                      (t, s) => t.CopyValuesFrom(s));
        }

        public static void CopyValuesFrom(this IParameterEntryViewModel target, IParameterEntryViewModel source)
        {
            target.Name = source.Name;
            target.Description = source.Description;
            target.Value = source.Value;
            target.GroupName = source.GroupName;
            target.GroupDescription = source.GroupDescription;
            target.CatalogName = source.CatalogName;
            target.CatalogDescription = source.CatalogDescription;
        }
    }
}