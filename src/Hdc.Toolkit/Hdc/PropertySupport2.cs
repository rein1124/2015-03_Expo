using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Data;
using Hdc.Linq;

namespace Hdc
{
    public static class PropertySupport2
    {
        /// <summary>
        /// Returns the value of the specified string property if <paramref name="owner"/> is not <c>null</c>, 
        /// and an empty string otherwise.
        /// copy from: The CQRS Kitchen\thecqrskitchen-71470
        /// </summary>
        public static string EmptyOr<T>(this T owner, Expression<Func<T, string>> propertySelector) where T : class
        {
            return (owner != null) ? propertySelector.Compile()(owner) : string.Empty;
        }

    }
}