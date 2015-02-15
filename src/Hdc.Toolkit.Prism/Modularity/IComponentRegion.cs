namespace Hdc.Modularity
{
    public interface IComponentRegion
    {
        string Name { get; set; }

        bool IsEnabled { get; set; }

        bool IsActive { get; set; }

//        bool IsVisible { get; set; }

//        bool IsCollapsed { get; set; }
    }
}