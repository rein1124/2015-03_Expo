//ref: Prism 4.0
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Hdc.Windows;

namespace Hdc.Controls
{
    public class SynchronizeSelectedItems : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectionsProperty =
            DependencyProperty.Register(
                "Selections",
                typeof(IList),
                typeof(SynchronizeSelectedItems),
                new PropertyMetadata(null, OnSelectionsPropertyChanged));

        private bool updating;
        private WeakEventListener<SynchronizeSelectedItems, object, NotifyCollectionChangedEventArgs> currentWeakHandler;

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly",
            Justification = "Dependency property")]
        public IList Selections
        {
            get { return (IList)this.GetValue(SelectionsProperty); }
            set { this.SetValue(SelectionsProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectionChanged += this.OnSelectedItemsChanged;
            this.UpdateSelectedItems();
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.SelectionChanged += this.OnSelectedItemsChanged;

            base.OnDetaching();
        }

        private static void OnSelectionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = d as SynchronizeSelectedItems;

            if (behavior != null)
            {
                if (behavior.currentWeakHandler != null)
                {
                    behavior.currentWeakHandler.Detach();
                    behavior.currentWeakHandler = null;
                }

                if (e.NewValue != null)
                {
                    var notifyCollectionChanged = e.NewValue as INotifyCollectionChanged;
                    if (notifyCollectionChanged != null)
                    {
                        behavior.currentWeakHandler =
                            new WeakEventListener<SynchronizeSelectedItems, object, NotifyCollectionChangedEventArgs>(
                                behavior,
                                (instance, sender, args) => instance.OnSelectionsCollectionChanged(sender, args),
                                (listener) => notifyCollectionChanged.CollectionChanged -= listener.OnEvent);
                        notifyCollectionChanged.CollectionChanged += behavior.currentWeakHandler.OnEvent;
                    }

                    behavior.UpdateSelectedItems();
                }
            }
        }

        private void OnSelectedItemsChanged(object sender, SelectionChangedEventArgs e)
        {
            this.UpdateSelections(e);
        }

        private void UpdateSelections(SelectionChangedEventArgs e)
        {
            this.ExecuteIfNotUpdating(
                () =>
                {
                    if (this.Selections != null)
                    {
                        foreach (var item in e.AddedItems)
                        {
                            this.Selections.Add(item);
                        }

                        foreach (var item in e.RemovedItems)
                        {
                            this.Selections.Remove(item);
                        }
                    }
                });
        }

        private void OnSelectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateSelectedItems();
        }

        private void UpdateSelectedItems()
        {
            this.ExecuteIfNotUpdating(
                () =>
                {
                    if (this.AssociatedObject != null)
                    {
                        this.AssociatedObject.SelectedItems.Clear();
                        foreach (var item in this.Selections ?? new object[0])
                        {
                            this.AssociatedObject.SelectedItems.Add(item);
                        }
                    }
                });
        }

        private void ExecuteIfNotUpdating(Action execute)
        {
            if (!this.updating)
            {
                try
                {
                    this.updating = true;
                    execute();
                }
                finally
                {
                    this.updating = false;
                }
            }
        }
    }
}