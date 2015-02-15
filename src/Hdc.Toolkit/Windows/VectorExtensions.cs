using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Hdc.Windows
{
    public static class VectorExtensions
    {
        public static Vector GetCenterVector(this IEnumerable<Vector> vectors)
        {
            var list = vectors as IList<Vector> ?? vectors.ToList();
            var avgX = list.Average(x => x.X);
            var avgY = list.Average(x => x.Y);
            var centerVector = new Vector(avgX, avgY);
            return centerVector;
        }
    }
}