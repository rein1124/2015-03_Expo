namespace Hdc.Generators
{
    /// <summary>
    /// copy from: Apworks, apworks-66967.zip
    /// rein, 2011-05-18
    /// Represents that the implemented classes are identity generators.
    /// </summary>
    public interface IIdentityGenerator
    {
        /// <summary>
        /// Generates the identity.
        /// </summary>
        /// <returns>The generated identity instance.</returns>
        long Generate();
    }
}