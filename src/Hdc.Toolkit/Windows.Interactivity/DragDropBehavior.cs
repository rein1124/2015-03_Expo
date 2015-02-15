//see WPF Behavior Library 0.2+:
//wpfbehaviorlibrary-07774e9acae5.zip
using System;
using System.Collections;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using Hdc.Collections;
using Hdc.Windows.Interactivity.Utilities;

namespace Hdc.Windows.Interactivity
{
    /// <summary>
    /// An attached behavior that allows you to drag and drop items among various ItemsControls, e.g. ItemsControl, ListBox, TabControl, etc.
    /// </summary>
    public class DragDropBehavior
        : Behavior<ItemsControl>, IDisposable
    {
        private const int DRAG_WAIT_COUNTER_LIMIT = 10;
        private const string DEFAULT_DATA_FORMAT_STRING = "WPFBehaviorLibrary.DragDropBehavior.DataFormat";

        private bool myIsMouseDown;
        private bool myIsDragging;
        private object myData;
        private Point myDragStartPosition;
        private DragAdorner myDragAdorner;
        private DropAdorner myDropAdorner;
        private int myDragScrollWaitCounter = DRAG_WAIT_COUNTER_LIMIT;

        /// <summary>
        /// Called when attached to an ItemsControl.
        /// </summary>
        protected override void OnAttached()
        {
            this.AssociatedObject.AllowDrop = true;

            this.AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            this.AssociatedObject.MouseMove += OnMouseMove;
            this.AssociatedObject.PreviewMouseLeftButtonUp += OnPreviewMouseLeftButtonUp;
            this.AssociatedObject.Drop += OnDrop;
            this.AssociatedObject.PreviewDrop += OnPreviewDrop;
            this.AssociatedObject.QueryContinueDrag += OnPreviewQueryContinueDrag;
            this.AssociatedObject.DragEnter += OnDragEnter;
            this.AssociatedObject.DragOver += OnDragOver;
            this.AssociatedObject.DragLeave += OnDragLeave;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the type of the items in the ItemsControl. 
        /// </summary>
        /// <value>The type of the item.</value>
        public Type ItemType { get; set; }

        /// <summary>
        /// Gets or sets the data template of the items to use while dragging. 
        /// </summary>
        /// <value>The data template.</value>
        public DataTemplate DataTemplate { get; set; }

        /// <summary>
        /// Gets or sets the drop indication.
        /// </summary>
        /// <value>The drop indication.</value>
        /// <remarks>The default is vertical.</remarks>
        public Orientation DropIndication { get { return _DropIndication; } set { _DropIndication = value; } }
        private Orientation _DropIndication = Orientation.Vertical;

        #endregion

        #region Button Events

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemsControl itemsControl = (ItemsControl)sender;
            Point p = e.GetPosition(itemsControl);

            myData = UIHelpers.GetItemFromPointInItemsControl(itemsControl, p);
            if (myData != null)
            {
                myIsMouseDown = true;
                myDragStartPosition = p;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (myIsMouseDown)
            {
                ItemsControl itemsControl = (ItemsControl)sender;
                Point currentPosition = e.GetPosition(itemsControl);

                if ((myIsDragging == false) &&
                    (Math.Abs(currentPosition.X - myDragStartPosition.X) > SystemParameters.MinimumHorizontalDragDistance) ||
                    (Math.Abs(currentPosition.Y - myDragStartPosition.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    DragStarted(itemsControl);
                }
            }
        }

        private void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ResetState();
            DetachAdorners();
        }

        #endregion

        #region Drag Events

        private void OnDragOver(object sender, DragEventArgs e)
        {
            ItemsControl itemsControl = (ItemsControl)sender;
            if (DataIsPresent(e) && CanDrop(itemsControl, GetData(e)))
            {
                UpdateDragAdorner(e.GetPosition(itemsControl));
                UpdateDropAdorner(itemsControl, e);
                HandleDragScrolling(itemsControl, e);
                e.Handled = true;
            }
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            ItemsControl itemsControl = (ItemsControl)sender;
            if (DataIsPresent(e) && CanDrop(itemsControl, GetData(e)))
            {
                object data = GetData(e);
                InitializeDragAdorner(itemsControl, data, e.GetPosition(itemsControl));
                InitializeDropAdorner(itemsControl, e);
                e.Handled = true;
            }
        }

        private void OnPreviewQueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (e.EscapePressed)
            {
                e.Action = DragAction.Cancel;
                ResetState();
                DetachAdorners();
                e.Handled = true;
            }
        }

        private void OnPreviewDrop(object sender, DragEventArgs e)
        {
            DetachAdorners();
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            ItemsControl itemsControl = (ItemsControl)sender;

            DetachAdorners();
            e.Handled = true;

            if (DataIsPresent(e) && CanDrop(itemsControl, GetData(e)))
            {
                object itemToAdd = GetData(e);
                bool isControlPressed = ((e.KeyStates & DragDropKeyStates.ControlKey) != 0);

                if (isControlPressed &&
                    !itemsControl.Items.IsNullOrEmpty() &&
                    itemsControl.Items.Contains(itemToAdd))
                {
                    e.Effects = DragDropEffects.None;
                    return;
                }

                e.Effects = isControlPressed ? DragDropEffects.Copy : DragDropEffects.Move;

                AddItem(itemsControl, itemToAdd, GetInsertionIndex(itemsControl, e));
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void OnDragLeave(object sender, DragEventArgs e)
        {
            DetachAdorners();
            e.Handled = true;
        }

        #endregion

        #region Overridable by Inheritors

        /// <summary>
        /// Called when an item is added to <paramref name="itemsControl"/>.
        /// </summary>
        /// <param name="itemsControl">The items control <paramref name="item"/> is to be added to.</param>
        /// <param name="item">The item to be added.</param>
        /// <param name="insertIndex">Index <paramref name="item"/> should be inserted at.</param>
        protected virtual void AddItem(ItemsControl itemsControl, object item, int insertIndex)
        {
            if (itemsControl == null) return;

            IList iList = itemsControl.ItemsSource as IList;
            if (iList != null)
            {
                iList.Insert(insertIndex, item);
            }
            else
            {
                itemsControl.Items.Insert(insertIndex, item);
            }
        }

        /// <summary>
        /// Removes the item from <paramref name="itemsControl"/>.
        /// </summary>
        /// <param name="itemsControl">The items control to remove <paramref name="itemToRemove"/> from.</param>
        /// <param name="itemToRemove">The item to remove.</param>
        protected virtual void RemoveItem(ItemsControl itemsControl, object itemToRemove)
        {
            if (itemsControl == null) return;

            if (itemToRemove != null)
            {
                if (itemsControl.ItemsSource != null)
                {
                    IList source = itemsControl.ItemsSource as IList;
                    source.Remove(itemToRemove);
                }
                else
                {
                    itemsControl.Items.Remove(itemToRemove);
                }
            }
        }

        /// <summary>
        /// Determines whether <paramref name="item"/> can be dragged from or within the specified items control.
        /// </summary>
        /// <param name="itemsControl">The drag source.</param>
        /// <param name="item">The item to be dragged.</param>
        /// <returns>
        /// 	<c>true</c> if <paramref name="item"/> can be dragged; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanDrag(ItemsControl itemsControl, object item)
        {
            return (this.ItemType == null) || this.ItemType.IsInstanceOfType(item);
        }

        /// <summary>
        /// Determines whether <paramref name="item"/> can be dropped onto the specified items control.
        /// </summary>
        /// <param name="itemsControl">The drop target.</param>
        /// <param name="item">The item that would be dropped.</param>
        /// <returns>
        /// 	<c>true</c> if <paramref name="item"/> can be dropped; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CanDrop(ItemsControl itemsControl, object item)
        {
            return (this.ItemType == null) || this.ItemType.IsInstanceOfType(item);
        }

        #endregion

        private void DragStarted(ItemsControl itemsControl)
        {
            if (!CanDrag(itemsControl, myData))
            {
                return;
            }

            myIsDragging = true;

            DataObject dObject;
            if (this.ItemType != null)
            {
                dObject = new DataObject(this.ItemType, myData);
            }
            else
            {
                dObject = new DataObject(DEFAULT_DATA_FORMAT_STRING, myData);
            }

            DragDropEffects effects =System.Windows.DragDrop.DoDragDrop(itemsControl, dObject, DragDropEffects.Copy | DragDropEffects.Move);
            if ((effects & DragDropEffects.Move) != 0)
            {
                RemoveItem(itemsControl, myData);
            }

            ResetState();
        }

        private void HandleDragScrolling(ItemsControl itemsControl, DragEventArgs e)
        {
            var verticalMousePosition = UIHelpers.GetRelativeVerticalMousePosition(itemsControl, e.GetPosition(itemsControl));

            if (verticalMousePosition != UIHelpers.RelativeVerticalMousePosition.Middle)
            {
                if (myDragScrollWaitCounter == DRAG_WAIT_COUNTER_LIMIT)
                {
                    myDragScrollWaitCounter = 0;

                    ScrollViewer scrollViewer = UIHelpers.GetVisualDescendent<ScrollViewer>(itemsControl);
                    if (scrollViewer != null && scrollViewer.ComputedVerticalScrollBarVisibility == Visibility.Visible)
                    {
                        e.Effects = DragDropEffects.Scroll;

                        double movementSize = (verticalMousePosition == UIHelpers.RelativeVerticalMousePosition.Top) ? 1.0 : -1.0;
                        scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + movementSize);
                    }
                }
                else
                {
                    myDragScrollWaitCounter++;
                }
            }
            else
            {
                e.Effects = ((e.KeyStates & DragDropKeyStates.ControlKey) != 0) ? DragDropEffects.Copy : DragDropEffects.Move;
            }
        }

        private int GetInsertionIndex(ItemsControl itemsControl, DragEventArgs e)
        {
            UIElement dropTargetContainer = UIHelpers.GetItemContainerFromPointInItemsControl(itemsControl, e.GetPosition(itemsControl));
            if (dropTargetContainer != null)
            {
                int index = itemsControl.ItemContainerGenerator.IndexFromContainer(dropTargetContainer);
                if (IsDropPointBeforeItem(itemsControl, e))
                {
                    return index;
                }
                else
                {
                    return index + 1;
                }
            }
            return itemsControl.Items.Count;
        }

        private bool IsDropPointBeforeItem(ItemsControl itemsControl, DragEventArgs e)
        {
            FrameworkElement selectedItemContainer = UIHelpers.GetItemContainerFromPointInItemsControl(itemsControl, e.GetPosition(itemsControl)) as FrameworkElement;
            if (selectedItemContainer == null) return false;

            Point relativePosition = e.GetPosition(selectedItemContainer);

            if (this.DropIndication == Orientation.Horizontal)
            {
                return relativePosition.X < selectedItemContainer.ActualWidth / 2;
            }
            else
            {
                return relativePosition.Y < selectedItemContainer.ActualHeight / 2;
            }
        }

        private void ResetState()
        {
            myIsMouseDown = false;
            myIsDragging = false;
            myData = null;
            myDragScrollWaitCounter = DRAG_WAIT_COUNTER_LIMIT;
        }

        private void InitializeDragAdorner(ItemsControl itemsControl, object dragData, Point startPosition)
        {
            if (this.DataTemplate == null) return;
            if (myDragAdorner != null) return;

            var adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
            if (adornerLayer == null) return;

            myDragAdorner = new DragAdorner(dragData, DataTemplate, itemsControl, adornerLayer);
            myDragAdorner.UpdatePosition(startPosition.X, startPosition.Y);
        }

        private void UpdateDragAdorner(Point currentPosition)
        {
            if (myDragAdorner != null)
            {
                myDragAdorner.UpdatePosition(currentPosition.X, currentPosition.Y);
            }
        }

        private void InitializeDropAdorner(ItemsControl itemsControl, DragEventArgs e)
        {
            if (myDropAdorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(itemsControl);
                UIElement itemContainer = UIHelpers.GetItemContainerFromPointInItemsControl(itemsControl, e.GetPosition(itemsControl));
                if (adornerLayer != null && itemContainer != null)
                {
                    bool isPointInTopHalf = IsDropPointBeforeItem(itemsControl, e);
                    bool isOrientationHorizontal = (this.DropIndication == Orientation.Horizontal);
                    myDropAdorner = new DropAdorner(isPointInTopHalf, isOrientationHorizontal, itemContainer, adornerLayer);
                }
            }
        }

        private void UpdateDropAdorner(ItemsControl itemsControl, DragEventArgs e)
        {
            if (myDropAdorner != null)
            {
                myDropAdorner.IsTopHalf = IsDropPointBeforeItem(itemsControl, e);
                myDropAdorner.InvalidateVisual();
            }
        }

        private void DetachAdorners()
        {
            if (myDropAdorner != null)
            {
                myDropAdorner.Dispose();
                myDropAdorner = null;
            }
            if (myDragAdorner != null)
            {
                myDragAdorner.Dispose();
                myDragAdorner = null;
            }
        }

        private bool DataIsPresent(DragEventArgs e)
        {
            if (this.ItemType != null)
            {
                if (e.Data.GetDataPresent(this.ItemType)) return true;

                string format = e.Data.GetFormats().FirstOrDefault();
                if (string.IsNullOrEmpty(format)) return false;

                object data = e.Data.GetData(format);
                if (data == null) return false;

                return this.ItemType.IsInstanceOfType(data);
            }
            else
            {
                return !(e.Data.GetFormats().IsNullOrEmpty());
            }
        }

        private object GetData(DragEventArgs e)
        {
            if ((this.ItemType != null) && (e.Data.GetDataPresent(this.ItemType)))
            {
                return e.Data.GetData(ItemType);
            }

            string format = e.Data.GetFormats().FirstOrDefault();
            if (string.IsNullOrEmpty(format)) return null;

            object data = e.Data.GetData(format);
            if (data == null) return null;

            if (this.ItemType != null && !this.ItemType.IsInstanceOfType(data)) return null;

            return data;
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Free managed resources
                DetachAdorners();
            }
        }

        #endregion
    }
}