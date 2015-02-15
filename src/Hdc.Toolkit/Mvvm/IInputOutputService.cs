namespace Hdc.Mvvm
{
    public interface IInputOutputService<in TInput, out TOutput> : IInputService<TInput>, IOutputService<TOutput>
    {
    }

    public interface IInputOutputService<TInputOutput> : IInputOutputService<TInputOutput, TInputOutput>
    {
    }
}