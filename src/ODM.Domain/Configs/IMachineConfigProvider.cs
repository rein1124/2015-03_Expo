namespace ODM.Domain.Configs
{
    public interface IMachineConfigProvider
    {
        MachineConfig MachineConfig { get; }

        void Commit();
    }
}