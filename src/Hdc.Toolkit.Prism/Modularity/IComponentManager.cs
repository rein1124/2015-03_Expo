namespace Hdc.Modularity
{
    public interface IComponentManager
    {
        //bool TryGetElement(string key, out IComponentElement value);

        IComponentItem GetComponentItem(string itemName);

        IComponentRegion GetComponentRegion(string regionName);
    }
}