using System.Collections.Generic;
using System.Linq;

namespace Hdc.Mvvm
{
    public static class EntityViewModelExtensions
    {
        public static T GetById<T>(this IEnumerable<T> entities, long id) where T:IEntityViewModel
        {
            return entities.FirstOrDefault(x => x.Id == id);
        }
    }
}