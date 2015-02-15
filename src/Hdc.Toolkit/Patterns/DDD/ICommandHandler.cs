using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hdc.Patterns
{
    /// <summary>
    /// Represents a handler of a given command type.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    public interface ICommandHandler<in TCommand>
    {
        /// <summary>
        /// Handles the command.
        /// </summary>
        /// <param name="cmd"></param>
//        void Handle(T cmd);

        void Handle(IObservable<TCommand> commands);
    }
}
