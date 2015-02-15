namespace Hdc.Generators
{
    /// <summary>
    /// copy from: Apworks, apworks-66967.zip
    /// rein, 2011-05-18
    /// Represents that the implemented classes are sequence generators.
    /// </summary>
    public interface ISequenceGenerator
    {
        /// <summary>
        /// Gets the next value of the sequence.
        /// </summary>
        long Next { get; }
    }
}