using System.Reactive.Subjects;
using Hdc.Mercury.Configs;

namespace Hdc.Mercury.Communication
{
    public interface IAccessItemRegistration : ISubject<ReadData>
    {
        DeviceConfig Config { get; }

        string ServerDataType { get; set; }

        uint ClientAlias { get; }

        uint ServerAlias { get; set; }

        IAccessChannel Channel { get; }
    }
}