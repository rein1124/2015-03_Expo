

using System;

namespace Hdc
{
	/// <summary>
	/// Defines a generic range class.
	/// </summary>
	/// <typeparam name="T">The type of the range.</typeparam>
	public sealed class Range<T> where T : IComparable<T>
	{
		/// <summary>
		/// Creates a new <see cref="Range&lt;T&gt;"/> instance.
		/// </summary>
		/// <param name="start">The start of the range.</param>
		/// <param name="end">The end of the range.</param>
		/// <remarks>
		/// If <paramref name="end"/> is less than <paramref name="start"/>,
		/// the values are reversed.
		/// </remarks>
		public Range(T start, T end)
			: base()
		{
			if(start.CompareTo(end) <= 0)
			{
				this.Start = start;
				this.End = end;
			}
			else
			{
				this.Start = end;
				this.End = start;
			}
		}

		/// <summary>
		/// Checks to see if the given value is within the current range (inclusive).
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>Returns <c>true</c> if <paramref name="value"/> is in the range; otherwise, <c>false</c>.</returns>
		public bool Contains(T value)
		{
			return value.CompareTo(this.Start) >= 0 &&
				value.CompareTo(this.End) <= 0;
		}


		/// <summary>
		/// Gets the intersection of the current <see cref="Range&lt;T&gt;" /> 
		/// and the target <see cref="Range&lt;T&gt;" />.
		/// </summary>
		/// <param name="target">The target <see cref="Range&lt;T&gt;" />.</param>
		/// <returns>A new <see cref="Range&lt;T&gt;" /> instance that is the intersection, 
		/// or <c>null</c> if there is no intersection</returns>
		public Range<T> Intersect(Range<T> target)
		{
			Range<T> intersection = null;

			if(this.Contains(target.Start) || this.Contains(target.End))
			{
				T intersectionStart = this.Start.CompareTo(target.Start) >= 0 ? this.Start : target.Start;
				T intersectionEnd = this.End.CompareTo(target.End) <= 0 ? this.End : target.End;
				intersection = new Range<T>(intersectionStart, intersectionEnd);
			}

			return intersection;
		}

		/// <summary>
		/// Provides a string representation of the current <see cref="Range&lt;T&gt;"/>.
		/// </summary>
		/// <returns>Returns a string in the format "(start,end)".</returns>
		public override string ToString()
		{
			return this.Start + "," + this.End;
		}
		
		/// <summary>
		/// Gets the end of the range.
		/// </summary>
		public T End
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the start of the range.
		/// </summary>
		public T Start
		{
			get;
			private set;
		}
	}
}
