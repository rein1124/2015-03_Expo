using System;

namespace Hdc
{
    /// <summary>
    /// Get current DateTime for the system
    /// </summary>
    /// <remarks>Supports testing with arbitrary DateTimes.</remarks>
    public static class SystemTime
    {

        /// <summary>
        /// Get or set a function returning the current DateTime on on this machine
        /// </summary>
        /// <remarks>
        /// Replace with a different function during testing in order to pretend
        /// that machine time is different.
        /// Useful for machine time sensitive tests.
        /// </remarks>
        public static Func<DateTime> Now = () => DateTime.Now;

        public static DateTime Today { get { return Now().Date; } }

        /// <summary>
        /// Restore function to return current DateTime on this machine
        /// </summary>
        public static void Reset()
        {
            Now = () => DateTime.Now;
        }
    }
}