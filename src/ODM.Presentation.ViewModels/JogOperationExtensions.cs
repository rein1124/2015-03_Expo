using Hdc.Mercury;

namespace ODM.Presentation.ViewModels
{
    public static class JogOperationExtensions
    {
        public static JogOperation GetJogOperation(this IDevice<bool> device)
        {
            return new JogOperation(device);
        }
    }
}