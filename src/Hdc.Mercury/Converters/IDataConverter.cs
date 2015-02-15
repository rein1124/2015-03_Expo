namespace Hdc.Mercury
{
    public interface IDataConverter<T1, T2>
    {
        T1 Convert(T2 t2);

        T2 Convert(T1 t1);
    }
}