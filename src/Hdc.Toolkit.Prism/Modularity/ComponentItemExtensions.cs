namespace Hdc.Modularity
{
    public static class ComponentItemExtensions
    {
        public static void Activate(this IComponentItem  item)
        {
            item.IsActive = true;
        }
        public static void Deactivate(this IComponentItem  item)
        {
            item.IsActive = false;
        }
    }
}