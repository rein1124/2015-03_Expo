using System.Collections.Specialized;

namespace Hdc.Collections.ObjectModel
{
    public static class BindableCollectionExtensions
    {
         public static void Sync<T>(this BindableCollection<T> target, IObservableCollection<T> source )
         {
             source.CollectionChanged += (sender, e) =>
             {
                 target.Clear();
                 target.AddRange(source);
             };
         }
    }
}