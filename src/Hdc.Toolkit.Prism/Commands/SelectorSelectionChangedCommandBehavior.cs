using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity;

namespace Hdc.Prism.Commands
{
    public class SelectorSelectionChangedCommandBehavior : CommandBehaviorBase<Selector>
    {
        public SelectorSelectionChangedCommandBehavior(Selector selector)
            : base(selector)
        {
            selector.SelectionChanged += OnSelectionChanged;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ExecuteCommand(null);
        }
    }
}