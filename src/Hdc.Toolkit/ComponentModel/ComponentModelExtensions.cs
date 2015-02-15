using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows.Data;

namespace Hdc.ComponentModel
{
   
    public static class ComponentModelExtensions
    {
        public static void NotifyPropertyChanged<TValue>(
            this Object target, Expression<Func<TValue>> propertySelector, Action<string> notifier)
        {
            if (notifier != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    notifier(memberExpression.Member.Name);
                }
            }
        }

        public static void Notify<TValue>(this Action<string> notifier, Expression<Func<TValue>> propertySelector)
        {
            if (notifier != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    notifier(memberExpression.Member.Name);
                }
            }
        }

        public static void Raise<TValue>(
            this PropertyChangedEventHandler handler, Expression<Func<TValue>> propertySelector)
        {
            if (handler != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    var _sender = ((ConstantExpression)memberExpression.Expression).Value;
                    handler(_sender, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }

        public static void Raise(this PropertyChangedEventHandler handler, Expression<Func<object, object>> x)
        {
            if (handler != null)
            {
                var body = x.Body as MemberExpression;
                if (body == null)
                {
                    throw new ArgumentException("'x' should be a member expression");
                }

                var vmExpression = body.Expression as ConstantExpression;
                var vmlambda = Expression.Lambda(vmExpression);
                var vmFunc = vmlambda.Compile();
                var vm = vmFunc.DynamicInvoke();

                string propertyName = body.Member.Name;
                var e = new PropertyChangedEventArgs(propertyName);
                handler(vm, e);
            }
        }

        public static ICollectionView GetDefaultCollectionView(this object source)
        {
            return CollectionViewSource.GetDefaultView(source);
        }
    }
}