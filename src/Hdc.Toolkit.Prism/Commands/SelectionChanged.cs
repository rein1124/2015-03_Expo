using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Hdc.Prism.Commands
{
    /// <summary>
    /// Static Class that holds all Dependency Properties and Static methods to allow 
    /// the SelectionChanged event of the <see cref="Selector"/> class to be attached to a Command. 
    /// </summary>
    /// <remarks>
    /// Modeled on Microsoft.Practices.Composite.Presentation.Commands.Click for ButtonBase. 
    /// </remarks>
    public static class SelectionChanged
    {

        private static readonly DependencyProperty SelectionChangedCommandBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectionChangedCommandBehavior",
                typeof(SelectorSelectionChangedCommandBehavior),
                typeof(SelectionChanged),
                null);


        /// <summary>
        /// Command to execute on click event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(SelectionChanged),
                new PropertyMetadata(OnSetCommandCallback));

        /// <summary>
        /// Command parameter to supply on command execution.
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(SelectionChanged),
                new PropertyMetadata(OnSetCommandParameterCallback));


        /// <summary>
        /// Sets the <see cref="ICommand"/> to execute on the SelectionChanged event.
        /// </summary>
        /// <param name="selector">Selector dependency object to attach command</param>
        /// <param name="command">Command to attach</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Only works for Selector")]
        public static void SetCommand(Selector selector, ICommand command)
        {
            selector.SetValue(CommandProperty, command);
        }

        /// <summary>
        /// Retrieves the <see cref="ICommand"/> attached to the <see cref="Selector"/>.
        /// </summary>
        /// <param name="selector">Selector containing the Command dependency property</param>
        /// <returns>The value of the command attached</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Only works for Selector")]
        public static ICommand GetCommand(Selector selector)
        {
            return selector.GetValue(CommandProperty) as ICommand;
        }


        /// <summary>
        /// Sets the value for the CommandParameter attached property on the provided <see cref="Selector"/>.
        /// </summary>
        /// <param name="selector">Selector to attach CommandParameter</param>
        /// <param name="parameter">Parameter value to attach</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Only works for Selector")]
        public static void SetCommandParameter(Selector selector, object parameter)
        {
            selector.SetValue(CommandParameterProperty, parameter);
        }

        /// <summary>
        /// Gets the value in CommandParameter attached property on the provided <see cref="Selector"/>
        /// </summary>
        /// <param name="selector">Selector that has the CommandParameter</param>
        /// <returns>The value of the property</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters",
            Justification = "Only works for Selector")]
        public static object GetCommandParameter(Selector selector)
        {
            return selector.GetValue(CommandParameterProperty);
        }


        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selector = dependencyObject as Selector;
            if (selector == null) return;
            SelectorSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(selector);
            behavior.Command = e.NewValue as ICommand;
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var selector = dependencyObject as Selector;
            if (selector == null) return;
            SelectorSelectionChangedCommandBehavior behavior = GetOrCreateBehavior(selector);
            behavior.CommandParameter = e.NewValue;
        }

        private static SelectorSelectionChangedCommandBehavior GetOrCreateBehavior(Selector selector)
        {
            var behavior = selector.GetValue(SelectionChangedCommandBehaviorProperty) as SelectorSelectionChangedCommandBehavior;
            if (behavior == null)
            {
                behavior = new SelectorSelectionChangedCommandBehavior(selector);
                selector.SetValue(SelectionChangedCommandBehaviorProperty, behavior);
            }

            return behavior;
        }

    }
}