using Hdc;

namespace Hdc
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Provides a number of extension methods for the <see cref="Action"/> delegate.
    /// </summary>
    public static class ActionExtensions
    {
        /// <summary>
        /// Calculates the time it takes to run a given <see cref="Action"/>.
        /// </summary>
        /// <param name="this">The <see cref="Action"/> to time.</param>
        /// <returns>A <see cref="TimeSpan"/> object that contains the time it took to run <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static TimeSpan Time(this Action @this)
        {
            @this.CheckParameterForNull("@this");
            var watch = Stopwatch.StartNew();
            @this();
            watch.Stop();
            return watch.Elapsed;
        }

        /// <summary>
        /// Calculates the time it takes to run a given <see cref="Action"/> with one parameter.
        /// </summary>
        /// <typeparam name="T">The type of the parameter to <paramref name="this"/>.</typeparam>
        /// <param name="this">The <see cref="Action"/> to time.</param>
        /// <param name="obj">The parameter for <paramref name="this"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> object that contains the time it took to run <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static TimeSpan Time<T>(this Action<T> @this, T obj)
        {
            @this.CheckParameterForNull("@this");
            var watch = Stopwatch.StartNew();
            @this(obj);
            watch.Stop();
            return watch.Elapsed;
        }

        /// <summary>
        /// Calculates the time it takes to run a given <see cref="Action"/> with two parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter to <paramref name="this"/>.</typeparam>
        /// <param name="this">The <see cref="Action"/> to time.</param>
        /// <param name="arg1">The first parameter to <paramref name="this"/>.</param>
        /// <param name="arg2">The second parameter to <paramref name="this"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> object that contains the time it took to run <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static TimeSpan Time<T1, T2>(this Action<T1, T2> @this, T1 arg1, T2 arg2)
        {
            @this.CheckParameterForNull("@this");
            var watch = Stopwatch.StartNew();
            @this(arg1, arg2);
            watch.Stop();
            return watch.Elapsed;
        }

        /// <summary>
        /// Calculates the time it takes to run a given <see cref="Action"/> with three parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter to <paramref name="this"/>.</typeparam>
        /// <param name="this">The <see cref="Action"/> to time.</param>
        /// <param name="arg1">The first parameter to <paramref name="this"/>.</param>
        /// <param name="arg2">The second parameter to <paramref name="this"/>.</param>
        /// <param name="arg3">The third parameter to <paramref name="this"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> object that contains the time it took to run <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static TimeSpan Time<T1, T2, T3>(this Action<T1, T2, T3> @this, T1 arg1, T2 arg2, T3 arg3)
        {
            @this.CheckParameterForNull("@this");
            var watch = Stopwatch.StartNew();
            @this(arg1, arg2, arg3);
            watch.Stop();
            return watch.Elapsed;
        }

        /// <summary>
        /// Calculates the time it takes to run a given <see cref="Action"/> with four parameters.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter to <paramref name="this"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter to <paramref name="this"/>.</typeparam>
        /// <param name="this">The <see cref="Action"/> to time.</param>
        /// <param name="arg1">The first parameter to <paramref name="this"/>.</param>
        /// <param name="arg2">The second parameter to <paramref name="this"/>.</param>
        /// <param name="arg3">The third parameter to <paramref name="this"/>.</param>
        /// <param name="arg4">The fourth parameter to <paramref name="this"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> object that contains the time it took to run <paramref name="this"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="this"/> is <c>null</c>.</exception>
        public static TimeSpan Time<T1, T2, T3, T4>(
            this Action<T1, T2, T3, T4> @this, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            @this.CheckParameterForNull("@this");
            var watch = Stopwatch.StartNew();
            @this(arg1, arg2, arg3, arg4);
            watch.Stop();
            return watch.Elapsed;
        }
    }
}