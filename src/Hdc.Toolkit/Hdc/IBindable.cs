namespace Hdc
{
    public interface IBindable<in TSource>
    {
        void BindingTo(TSource context);
    }
}