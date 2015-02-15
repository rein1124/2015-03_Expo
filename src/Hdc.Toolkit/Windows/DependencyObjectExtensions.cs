using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;

namespace Hdc.Windows
{
    public static class DependencyObjectExtensions
    {
        //private static int counter;

        public static void Binding<TSource>(this DependencyObject target,
                                            DependencyProperty targetDependencyProperty,
                                            TSource source,
                                            string propertyName)
        {
//            Debug.WriteLine("Binding counter=" + counter++);

//            BindingOperations.ClearBinding(target, targetDependencyProperty);
            if(target==null) return;
            BindingOperations.SetBinding(target, targetDependencyProperty,
                                         new Binding(propertyName)
                                             {
                                                 Source = source,
                                                 Mode = BindingMode.TwoWay,
                                             });
        }

        public static void Binding<TSource, TRetrun>(this DependencyObject target,
                                                     DependencyProperty targetDependencyProperty,
                                                     TSource source,
                                                     Expression<Func<TSource, TRetrun>> propertyExpresssion)
        {
            var propertyName = ExtractPropertyName(propertyExpresssion);

            Binding<TSource>(target, targetDependencyProperty, source, propertyName);
        }

        private static string ExtractPropertyName<TSource, TR>(Expression<Func<TSource, TR>> propertyExpression)
        {
            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                throw new ArgumentException("'x' should be a member expression");
            }

            string propertyName = body.Member.Name;

            return propertyName;
        }
    }
}