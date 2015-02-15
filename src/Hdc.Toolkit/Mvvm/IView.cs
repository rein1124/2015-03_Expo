//from compositeextensions-16590

namespace Hdc.Mvvm
{
    /// <summary>
    /// Represents a view
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets or sets the data context of the view.
        /// </summary>
        /// <value>The data context.</value>
        object DataContext { get; set; }
    }
}