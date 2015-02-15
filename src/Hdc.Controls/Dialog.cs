using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;
using System.Windows.Media.Media3D;

namespace Hdc.Controls
{   
    //TODO: Test Validation Rule
    //TODO: Add events
    //TODO: Try a ContentPresenter in the WindowTemplate            
    //TODO: ContentTemplate is DataTemplate : Which defines how content in a ContentControl in an is displayed: http://msdn.microsoft.com/en-us/library/system.windows.controls.contentcontrol.contenttemplate.aspx
    //TODO: ControlTemplate/WindowTemplate : the ControlTemplate, which defines the default appearance and behavior of a control
    /// <summary>
    /// A dialog that acts like a normal ContentControl.
    /// </summary>    
    public class Dialog : ContentControl, INotifyPropertyChanged
    {
        #region Static Constructor
        static Dialog()
        {
            //Standard default style.
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Dialog), new FrameworkPropertyMetadata(typeof(Dialog)));
        }
        #endregion

        #region DependencyProperties

        public static readonly DependencyProperty ShowingProperty =
            DependencyProperty.Register("Showing", typeof(bool), typeof(Dialog), new FrameworkPropertyMetadata(false, ShowingPropertyChangedCallback));

        /// <summary>
        /// Whether the dialog is showing or not. Set this to true to show the dialog, false to hide it.
        /// </summary>
        public bool Showing
        {
            get { return (bool)GetValue(ShowingProperty); }
            set { SetValue(ShowingProperty, value); }
        }

        public static readonly DependencyProperty CancelCommandProperty =
            DependencyProperty.Register("CancelCommand", typeof(ICommand), typeof(Dialog), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// This command, if present, is invoked when the user cancels the dialog.
        /// </summary>
        public ICommand CancelCommand
        {
            get { return (ICommand)GetValue(CancelCommandProperty); }
            set { SetValue(CancelCommandProperty, value); }
        }

        public static readonly DependencyProperty WindowTemplateProperty =
            DependencyProperty.Register("WindowTemplate", typeof(ControlTemplate), typeof(Dialog), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// This command, if present, is invoked when the user cancels the dialog.
        /// </summary>
        public ControlTemplate WindowTemplate
        {
            get { return (ControlTemplate)GetValue(WindowTemplateProperty); }
            set { SetValue(WindowTemplateProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Dialog), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// This command, if present, is invoked when the user cancels the dialog.
        /// </summary>
        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion 

        private static void ShowingPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //We're in the middle of handling shifting Dependency Property
            //values. We don't know what value Showing is going to end up
            //with, so we don't want to do anything yet. So we just leave
            //a message for ourselves in the next message pump go around to
            //check the showing value.
            Dialog dialog = (Dialog)d;
            //Only leave the message once.
            dialog.DialogStateChanged((bool)e.OldValue);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ContentControl.ContentTemplateSelectorProperty)
            {
                SurrogateContentTemplateSelector = (DataTemplateSelector)e.NewValue;
            }
            else if (e.Property == ContentControl.ContentTemplateProperty)
            {
                NotifySurrogateContentTemplateSelectorIfDifferent();
            }
        }

        #region Local Variables
        //The actual window that's shown and hidden. All Windows are root elements, so they don't share this control's logical tree.
        Window _window;
        //The closes Window parent in the logical tree or null if there isn't one.
        Window _parentWindow;
        //A data template selector that acts as a bridge between this control's logical tree and the Window's tree.
        TunnelDataTemplateSelector _tunnelDataTemplateSelector;        
        //An event handler to catch the Window closing event and Hide() the Window instead.
        EventHandler _dialogWindowClosedEventHandler;
        //Whether we've already dispatched a message to ourselves to update the state of the dialog
        //at the next chance in the message pump. Delay doing this expensive work until all the properties
        //are settled.
        bool _hasDispatched = false;        
        #endregion

        #region Constructor
        public Dialog()
        {
            //The a ContentPresenter which finds the DataTemplate for our logical tree and gives it to the TunnelDataTemplateSelector to give to the Window.
            TemplateFinder templateFinder = new TemplateFinder();            
            this.AddLogicalChild(templateFinder);
            this._tunnelDataTemplateSelector = new TunnelDataTemplateSelector(templateFinder);
            this._dialogWindowClosedEventHandler = new EventHandler(WindowClosedHandler);
        }
        #endregion

        /// <summary>
        /// When the parent changes, tries to find the parent Window.
        /// </summary>
        /// <param name="oldParent">The previous parent.</param>
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
            this._parentWindow = LogicalTreeWalker.FindParentOfType<Window>(this);            
        }

        #region Dialog Window setup/teardown
        private void DialogStateChanged(bool oldShowing)
        {
            if (!this._hasDispatched)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    (ThreadStart)delegate()
                    {
                        SynchronizeDialogShowing(oldShowing);
                    });
                this._hasDispatched = true;
            }            
        }

        private void SynchronizeDialogShowing(bool oldShowing)
        {
            bool newShowing = this.Showing;
            //If the state has changed, hide the Window.
            if (oldShowing == true && newShowing == false)
            {
                CloseAndDestroyDialog();
            }
            else if (oldShowing == false && newShowing == true)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    (ThreadStart)delegate()
                    {
                        CreateAndShowDialog();
                    });                
            }
            this._hasDispatched = false;
        }

        private void CreateAndShowDialog()
        {
            if (WindowTemplate != null)
            {
                //Quietly fail if the cast is no good.                
                this._window = WindowTemplate.LoadContent() as Window;
                if (this._window != null)
                {
                    this._window.Owner = this._parentWindow;
                    this._window.Closed += this._dialogWindowClosedEventHandler;
                    Binding contentTemplateSelectorBinding = new Binding("SurrogateContentTemplateSelector");                    
                    contentTemplateSelectorBinding.Source = this;
                    BindingOperations.SetBinding(this._window, ContentControl.ContentTemplateSelectorProperty, contentTemplateSelectorBinding);
                    //this._window.ContentTemplateSelector = this._tunnelDataTemplateSelector;                    
                    BindControlToWindow("ContentStringFormat", ContentControl.ContentStringFormatProperty);
                    BindControlToWindow("ContentTemplate", ContentControl.ContentTemplateProperty);
                    BindControlToWindow("DataContext", FrameworkElement.DataContextProperty);
                    BindControlToWindow("Content", ContentControl.ContentProperty);
                    string originalTitle = this._window.Title;
                    BindControlToWindow("Title", Window.TitleProperty);
                    if (this.Title == null)
                    {
                        this._window.Title = originalTitle;
                    }
                    this._window.ShowDialog();
                }
            }
        }

        Binding BindControlToWindow(string propertyName, DependencyProperty dependencyProperty)
        {
            Binding binding = new Binding(propertyName);
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            BindingOperations.SetBinding(this._window, dependencyProperty, binding);
            return binding;
        }

        void WindowClosedHandler(object sender, EventArgs e)
        {
            this.ClearValue(Dialog.ShowingProperty);
            //The Window was cancelled probably by the user pressing the X close. Execute the CancelCommand if one is provided to inform the ViewModel that this happened.
            if (this.CancelCommand != null && this.CancelCommand.CanExecute(null))
            {
                this.CancelCommand.Execute(null);
            }
        }

        private void CloseAndDestroyDialog()
        {
            if (this._window != null)
            {
                this._window.Closed -= this._dialogWindowClosedEventHandler;
                this._window.Close();
                this._window.ContentTemplateSelector = null;
                BindingOperations.ClearBinding(this._window, ContentControl.ContentTemplateSelectorProperty);
                BindingOperations.ClearBinding(this._window, ContentControl.ContentStringFormatProperty);
                BindingOperations.ClearBinding(this._window, ContentControl.ContentTemplateProperty);
                BindingOperations.ClearBinding(this._window, FrameworkElement.DataContextProperty);
                BindingOperations.ClearBinding(this._window, ContentControl.ContentProperty);
                BindingOperations.ClearBinding(this._window, Window.TitleProperty);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private DataTemplateSelector _surrogateContentTemplateSelector;
        private DataTemplateSelector _oldSurrogateContentTemplateSelector;
        public DataTemplateSelector SurrogateContentTemplateSelector
        {
            get
            {
                if (this._surrogateContentTemplateSelector == null)
                {
                    if (this.ContentTemplate == null)
                    {
                        return this._tunnelDataTemplateSelector;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return this._surrogateContentTemplateSelector;
                }
            }
            set
            {
                if (this._surrogateContentTemplateSelector != value)
                {
                    this._surrogateContentTemplateSelector = value;
                    NotifySurrogateContentTemplateSelectorIfDifferent();
                }
            }
        }

        private void NotifySurrogateContentTemplateSelectorIfDifferent()
        {
            if (SurrogateContentTemplateSelector != this._oldSurrogateContentTemplateSelector)
            {
                this._oldSurrogateContentTemplateSelector = SurrogateContentTemplateSelector;
                Notify("SurrogateContentTemplateSelector");
            }
        }

        #endregion
    }
}
