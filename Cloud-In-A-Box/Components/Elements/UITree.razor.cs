using Cloud_In_A_Box.Components.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cloud_In_A_Box.Components
{
    public class UITreeBase<T> : ComponentBase
    {
        [Parameter]
        public List<UiTreeNode<T>> Nodes { get; set; } = new List<UiTreeNode<T>>();

        [Parameter]
        public Action<UIMouseEventArgs, UiTreeNode<T>> SelectChangeDelegate { get; set; } = null;

        [Parameter]
        public Func<int?, Task<List<UiTreeNode<T>>>> LazyLoadNodesAsyncDelegate { get; set; }

        [Parameter]
        public Func<int, Task<bool>> ExpandAsyncDelegate { get; set; } = null;

        [Parameter]
        public Func<int, Task<bool>> CollapseAsyncDelegate { get; set; } = null;


        protected override async Task OnInitAsync()
        {
            if (!Nodes.Any())
            {
                Nodes = await LazyLoadNodesAsyncDelegate(0);
            }
        }

        protected void OnNodeClick(UIMouseEventArgs e, UiTreeNode<T> node)
        {
            if (!Nodes.Any()) // Only Nodes without children can be selected
            {
                node.IsSelected = !node.IsSelected;
                SelectChangeDelegate(e, node);
            }
        }

        protected async void OnExpandClick(UIMouseEventArgs e, UiTreeNode<T> node)
        {
            node.IsExpanded = !node.IsExpanded;
            if (node.IsExpanded)
            {
                var expandResult = await ExpandAsyncDelegate(node.Id);
            }
            else
            {
                var collapseResult = await CollapseAsyncDelegate(node.Id);
            }
        }

        protected string GetIconClass(UiTreeNode<T> node)
        {
            return node.IsExpanded ? "fas fa-caret-down" : "fas fa-caret-right";
        }

        protected string GetNodeClass(UiTreeNode<T> node)
        {
            var defaultClass = "list-group-item";
            return node.IsSelected ? defaultClass + " active" : defaultClass + " text-black";
        }


        protected string GetNodesAsMarkupRecurse(List<UiTreeNode<T>> nodes)
        {
            var result = "";
            foreach (var node in nodes)
            {
                result += $@"
                <li class='list-group-item'>
                    <span class=\'caret'>
                        <i class=''></i>
                        {node.Text}
                    </span>
                    <ul class='list-group-flush'>
                        {GetNodesAsMarkupRecurse(node.Children)}
                    </ul>
                </li>
                ";
            }
            return result;
        }
    }
}
