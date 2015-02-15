using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;

namespace Hdc.AutoMapper
{
    public static class MapperExtensions
    {
        public static void CreateMapBetween<T1, T2>()
        {
            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }

//        public static TSource MapCopy<TSource, TTarget>(this TSource source)
//        {
//            var target = Mapper.Map<TSource, TTarget>(source);
//            var newSource = Mapper.Map<TTarget, TSource>(target);
//            return newSource;
//        }

        [Obsolete("use MapperExtensions.Map instead")]
        public static TTarget Map<TSource, TTarget>(this TSource source)
        {
            var target = Mapper.Map<TSource, TTarget>(source);
            return target;
        }

        public static TTarget Map<TSource, TTarget>(this TSource source, TTarget target)
        {
            var result = Mapper.Map<TSource, TTarget>(source, target);
            return result;
        }
    }
}