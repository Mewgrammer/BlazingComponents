using System.Collections.Generic;

namespace BlazorEssentials.ComponentLib.Models
{
    public class BlazorTreeNode<T>
    {
        public int Id { get; set; }
        public T Data { get; set; }
        public BlazorTreeNode<T> Parent { get; set; }
        public List<BlazorTreeNode<T>> Children { get; set; } = new List<BlazorTreeNode<T>>();
        public int Deep { get; set; }
        public string Text { get; set; }
        public bool IsExpanded { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public bool ChildrenLoaded { get; set; } = false;
        public bool IsVisible { get; set; } = true;
    }
}
