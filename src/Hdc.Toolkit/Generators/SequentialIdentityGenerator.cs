using System;
using System.Runtime.CompilerServices;

namespace Hdc.Generators
{
    /// <summary>
    /// copy from: Apworks, apworks-66967.zip
    /// rein, 2011-05-18
    /// Represents the sequential identity generator.
    /// </summary>
    public class SequentialIdentityGenerator : IIdentityGenerator, ISequenceGenerator
    {
        #region Private Constants
        private const int Year = 2010;
        private const int Month = 11;
        private const int Day = 23;
        private const int Hour = 0;
        private const int Minute = 0;
        private const int Second = 0;
        private const int Millisecond = 0;
        #endregion

        #region Private Fields
        private UInt16 counter = 0;
        private readonly object syncObj = new object();
        private readonly DateTime startingPoint = new DateTime(Year, Month, Day, Hour, Minute, Second, Millisecond).ToUniversalTime();
        #endregion

        #region Protected Properties
        /// <summary>
        /// Gets the next counter value.
        /// </summary>
        protected UInt16 Counter
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                lock (syncObj)
                {
                    counter++;
                    if (counter == UInt16.MaxValue)
                        counter = 0;
                    return counter;
                }
            }
        }
        #endregion

        #region IIdentityGenerator Members
        /// <summary>
        /// Generates the identity.
        /// </summary>
        /// <returns>The generated identity instance.</returns>
        public long Generate()
        {
            return (Convert.ToInt64((DateTime.UtcNow - startingPoint).TotalMilliseconds) / 2) * 100000 + Counter;
        }

        #endregion

        #region ISequence Members
        /// <summary>
        /// Gets the next value of the sequence.
        /// </summary>
        public long Next
        {
            get { return Generate(); }
        }

        #endregion
    }
}