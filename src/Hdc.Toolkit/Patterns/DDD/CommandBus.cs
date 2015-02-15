using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Subjects;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Hdc.Patterns
{
    /// <summary>
    /// Implements <see cref="ICommandBus"/> using an IoC container.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();


        [Dependency]
        public IServiceLocator ServiceLocator { get; set; }

        public void Send<T>(T cmd) where T : ICommand
        {
//            var handler = ServiceLocator.GetInstance<ICommandHandler<T>>();
//            handler.OnNext(cmd);


            object subjectObj;

            if (!_subjects.TryGetValue(typeof (T), out subjectObj))
            {
                var subject = new Subject<T>();
                _subjects.GetOrAdd(typeof (T), subject);
                subjectObj = subject;

                try
                {
                    var handler = ServiceLocator.GetInstance<ICommandHandler<T>>();
                    handler.Handle(subject);
                }
                catch (ActivationException)
                {
                    throw;
                }
                
            }

            ((ISubject<T>) subjectObj).OnNext(cmd);
        }
    }
}