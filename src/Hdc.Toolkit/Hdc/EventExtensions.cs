using System.Diagnostics;

namespace Hdc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Diagnostics;

    public static class EventExtensions
    {
        [DebuggerHidden]
        public static void BeginRaise(
                this EventHandler handler, object sender, AsyncCallback callback, object asyncState)
        {
            EventHelper.BeginRaise(handler, sender, callback, asyncState);
        }

        [DebuggerHidden]
        public static void Raise(this EventHandler handler, object sender)
        {
            EventHelper.Raise(handler, sender);
        }

        [DebuggerHidden]
        public static void BeginRaise(
                this Delegate handler, object sender, EventArgs e, AsyncCallback callback, object asyncState)
        {
            EventHelper.BeginRaise(handler, sender, e, callback, asyncState);
        }

        [DebuggerHidden]
        public static void Raise(this Delegate handler, object sender, EventArgs e)
        {
            EventHelper.Raise(handler, sender, e);
        }

        [DebuggerHidden]
        public static void BeginRaise<T>(
                this EventHandler<T> handler, object sender, T e, AsyncCallback callback, object asyncState)
                where T : EventArgs
        {
            EventHelper.BeginRaise(handler, sender, e, callback, asyncState);
        }

        [DebuggerHidden]
        public static void Raise<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs
        {
            EventHelper.Raise(handler, sender, e);
        }

        [DebuggerHidden]
        public static void BeginRaise<T>(
                this EventHandler<T> handler,
                object sender,
                EventHelper.CreateEventArguments<T> createEventArguments,
                AsyncCallback callback,
                object asyncState) where T : EventArgs
        {
            EventHelper.BeginRaise(handler, sender, createEventArguments, callback, asyncState);
        }

        [DebuggerHidden]
        public static void Raise<T>(
                this EventHandler<T> handler, object sender, EventHelper.CreateEventArguments<T> createEventArguments)
                where T : EventArgs
        {
            EventHelper.Raise(handler, sender, createEventArguments);
        }
    }
}