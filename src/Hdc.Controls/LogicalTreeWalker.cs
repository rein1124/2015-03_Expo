using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace Hdc.Controls
{   
    /// <summary>
    /// Static helper class to walk up the tree. From Josh Smith's article: http://www.codeproject.com/KB/WPF/WpfElementTrees.aspx
    /// </summary>
    public static class LogicalTreeWalker
    {
        /// <summary>
        /// This method is necessary in case the element is not
        /// part of a logical tree.  It finds the closest ancestor
        /// element which is in a logical tree.
        /// </summary>
        /// <param name="initial">The element on which the user clicked.</param>
        private static DependencyObject FindClosestLogicalAncestor(DependencyObject initial)
        {
            DependencyObject current = initial;
            DependencyObject result = initial;

            while (current != null)
            {
                DependencyObject logicalParent = LogicalTreeHelper.GetParent(current);
                if (logicalParent != null)
                {
                    result = current;
                    break;
                }
                current = VisualTreeHelper.GetParent(current);
            }

            return result;
        }

        /// <summary>
        /// Walks up the logical tree starting at 'initial' and returns
        /// the first element of the type T enountered.
        /// </summary>
        /// <param name="initial">It is assumed that this element is in a logical tree.</param>
        public static T FindParentOfType<T>(DependencyObject initial) where T : class
        {
            DependencyObject current = FindClosestLogicalAncestor(initial);

            while (current != null && !(current is T))
            {                
                current = LogicalTreeHelper.GetParent(current);
            }

            return (current as T);
        }
    }
}
