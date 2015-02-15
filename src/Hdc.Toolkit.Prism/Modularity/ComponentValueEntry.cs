namespace Hdc.Modularity
{
    public class ComponentValueEntry
    {
        public ComponentValueEntry()
        {
        }

        public ComponentValueEntry(string regionName, string itemName)
        {
            RegionName = regionName;
            ItemName = itemName;
        }

        public  string RegionName { get; set; }
        public string ItemName { get; set; }
    }
}