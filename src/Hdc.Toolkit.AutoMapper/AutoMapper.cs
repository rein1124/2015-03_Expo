using System;
using AutoMapper;
using Hdc.Patterns;

namespace Hdc.AutoMapper
{
    public class AutoMapper : IMapper
    {
        public TTarget Map<TSource, TTarget>(TSource source)
        {
            var target = Mapper.Map<TSource, TTarget>(source);
            return target;
        }

        public TTarget Map<TSource, TTarget>(TSource source, TTarget target)
        {
            var result = Mapper.Map<TSource, TTarget>(source, target);
            return result;
        }
    }
}