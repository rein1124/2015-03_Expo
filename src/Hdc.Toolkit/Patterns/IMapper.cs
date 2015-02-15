namespace Hdc.Patterns
{
//    public interface IMapper<T1, T2>
//    {
//        T2 Map(T1 t1);
//
//        T1 Map(T2 t2);
//    }

    public interface IMapper
    {
        TTarget Map<TSource, TTarget>(TSource source);

        TTarget Map<TSource, TTarget>(TSource source, TTarget target);
    }

//    public interface IInverseMapper<out TSource, in TTarget>
//    {
//        TSource MapFrom(TTarget target);
//    }
//
//    public interface IBidirectionMapper<T1, T2> : IMapper<T1, T2>, IInverseMapper<T1, T2>
//    {
//    }
}