namespace Hdc.Mercury.Navigation
{
    public class XScreenRepository : IXScreenRepository
    {
        public XScreen Load()
        {
            var xScreenStore = XScreenStore.LoadFromFile("screens.xml");

            return xScreenStore.TopScreen;
        }

        public void Save(XScreen topScreen)
        {
            var xScreenStore = new XScreenStore();

            xScreenStore.TopScreen = topScreen;

            xScreenStore.SaveToFile("screens.xml");
        }
    }
}