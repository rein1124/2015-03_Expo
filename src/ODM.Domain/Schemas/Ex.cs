using System.Collections.Generic;
using AutoMapper;
using Hdc.Patterns;

namespace ODM.Domain.Schemas
{
    public static class Ex
    {
        static Ex()
        {
            Init();
        }

        public static void Init()
        {
            Mapper.CreateMap<ProductionSchema, ProductionSchema>();
            Mapper.CreateMap<ParameterEntry, ParameterEntry>()
                .ForMember(x => x.ProductionSchema, o => o.Ignore());
            Mapper.CreateMap<List<ParameterEntry>, List<ParameterEntry>>()
                .ConvertUsing(tis => tis.MapToList<ParameterEntry, ParameterEntry>());
        }
    }
}