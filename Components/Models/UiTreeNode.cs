using System;
using System.Collections.Generic;
using System.Text;

namespace Cloud_In_A_Box.Components.Models
{
    public class UiTreeNode<T>
    {
        public int Id { get; set; }
        public T Data { get; set; }
        public UiTreeNode<T> Parent { get; set; }
        public List<UiTreeNode<T>> Children { get; set; } = new List<UiTreeNode<T>>();
        public int Deep { get; set; }
        public string Text { get; set; }
        public bool IsExpanded { get; set; } = false;
        public bool IsSelected { get; set; } = false;
        public bool ChildrenLoaded { get; set; } = false;
        public bool IsVisible { get; set; } = true;
    }
}
