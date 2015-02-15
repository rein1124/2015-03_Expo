//from "TreeView Selector.htm"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Hdc.Controls
{
    public static class TreeViewSelector
    {
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewSelector),
            new UIPropertyMetadata(null, OnSelectedItemChanged));

        public static object GetSelectedItem(DependencyObject source)
        {
            return source.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject source, object value)
        {
            source.SetValue(SelectedItemProperty, value);
        }

        public static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TreeView tv = sender as TreeView;
            if (tv == null)
                return;

            tv.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(tv_SelectedItemChanged);
        }

        static void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((DependencyObject)sender).SetValue(SelectedItemProperty, e.NewValue);
        }
    }
}