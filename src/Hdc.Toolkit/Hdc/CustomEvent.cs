namespace Hdc
{
    public class CustomEvent<TSource, TArgs> : ICustomEvent<TSource, TArgs>
    {
        public CustomEvent()
        {
        }

        public CustomEvent(TSource source, TArgs eventArgs)
        {
            Source = source;
            EventArgs = eventArgs;
        }

        public TSource Source { get; set; }
        public TArgs EventArgs { get; set; }
    }
}