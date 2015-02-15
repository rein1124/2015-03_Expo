namespace Hdc.Patterns
{
    using System.Collections.Generic;

    /// <summary>
    /// Compositie object with id and name.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompositeWithIdAndName<T> : Composite<T> where T : CompositeWithIdAndName<T>
    {
        private Dictionary<string, T> _childrenByName = new Dictionary<string, T>();

        private Dictionary<int, T> _childrenById = new Dictionary<int, T>();

        /// <summary>
        /// Default name to empty.
        /// </summary>
        public CompositeWithIdAndName()
        {
            Name = string.Empty;
        }

        /// <summary>
        /// Id of the node.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the node.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Add node.
        /// </summary>
        /// <param name="node"></param>
        public override void Add(T node)
        {
            base.Add(node);
            _childrenById[node.Id] = node;
            _childrenByName[node.Name] = node;
        }

        /// <summary>
        /// Return node by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T this[string name]
        {
            get { return _childrenByName[name]; }
        }

        /// <summary>
        /// Return by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T this[int id]
        {
            get { return _childrenById[id]; }
        }
    }
}