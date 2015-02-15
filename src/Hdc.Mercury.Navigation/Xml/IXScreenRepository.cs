namespace Hdc.Mercury.Navigation
{
    public interface IXScreenRepository
    {
        XScreen Load();
        void Save(XScreen topScreen);
    }
}