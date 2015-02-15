using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Hdc.Collections.ObjectModel;

namespace Hdc.Patterns
{
    public static class MapperExtensions
    {
        private static IMapper Mapper
        {
            get { return MapperServiceLocator.Mapper; }
        }

        public static void InjectFromByMapper<T>(this T target, T source)
        {
            throw new NotImplementedException("somethine wrong");
            //how to use map<T,T>:result?? return value equals targe??
            //Mapper.Map<T, T>(source, target);
        }

        public static TTarget Map<TSource, TTarget>(this TSource source)
        {
            return Mapper.Map<TSource, TTarget>(source);
        }

        //        public static T CloneByMapper<T, TVia>(this T source)
        //        {
        //            var via = Mapper.Map<T, TVia>(source);
        //            var target = Mapper.Map<TVia, T>(via);
        //            return target;
        //        }

        public static T CloneByMapper<T, TVia>(this T source)
        {
            var xdata = source.Map<T, TVia>();
            var cloneData = xdata.Map<TVia, T>();

            return cloneData;
        }

        public static T CloneByMapper<T>(this T source)
        {
            return source.CloneByMapper<T, T>();
        }

        public static ObservableCollection<TTarget> MapToObservableCollection<TSource, TTarget>(
            this IEnumerable<TSource> sources)
        {
            var tvms = new ObservableCollection<TTarget>();
            foreach (var tv in sources)
            {
                var tvm = tv.Map<TSource, TTarget>();
                tvms.Add(tvm);
            }
            return tvms;
        }

        public static BindableCollection<TTarget> MapToBindableCollection<TSource, TTarget>(
         this IEnumerable<TSource> sources)
        {
            var tvms = new BindableCollection<TTarget>();
            foreach (var source in sources)
            {
                var tvm = source.Map<TSource, TTarget>();
                tvms.Add(tvm);
            }
            return tvms;
        }

        public static List<TTarget> MapToList<TSource, TTarget>(
         this IEnumerable<TSource> sources)
        {
            var tvms = new List<TTarget>();
            foreach (var source in sources)
            {
                var tvm = source.Map<TSource, TTarget>();
                tvms.Add(tvm);
            }
            return tvms;
        }

        public static Collection<TTarget> MapToCollection<TSource, TTarget>(
         this IEnumerable<TSource> sources)
        {
            var tvms = new Collection<TTarget>();
            foreach (var source in sources)
            {
                var tvm = source.Map<TSource, TTarget>();
                tvms.Add(tvm);
            }
            return tvms;
        }
    }
}