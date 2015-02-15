using System.Threading.Tasks;

namespace MvInspection.Inspection
{
    public static class InspectorExtensions
    {
        public static Task<InspectionInfo> InspectAsync(this IInspector inspector, ImageInfo imageInfo)
        {
            return Task.Run(() => inspector.Inspect(imageInfo));
        }
    }
}