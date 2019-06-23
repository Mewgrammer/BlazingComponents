using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingComponents.Lib.Models;
using Microsoft.AspNetCore.Components;

namespace BlazingComponents.Lib.Areas.Components
{
    /// <summary>
    /// Blazor Tree Component for hierarchical Data
    /// </summary>
    /// <typeparam name="T">Type for Node Data</typeparam>
    public class BlazorTreeBase<T> : ComponentBase
    {
        [Parameter]
        public Func<BlazorTreeNode<T>, object> NodeKeyDelegate { get; set; } = (BlazorTreeNode<T> item) => { return item; };

        [Parameter]
        public List<BlazorTreeNode<T>> Nodes { get; set; } = new List<BlazorTreeNode<T>>();

        [Parameter]
        public Func<int?, Task<List<BlazorTreeNode<T>>>> LazyLoadNodesAsyncDelegate { get; set; } = null;

        [Parameter]
        public EventCallback<BlazorTreeNode<T>> OnCollapse { get; set; }

        [Parameter]
        public EventCallback<BlazorTreeNode<T>> OnExpand { get; set; }

        [Parameter]
        public EventCallback<BlazorTreeNode<T>> OnSelect { get; set; }


        protected override async Task OnInitAsync()
        {
            if (!Nodes.Any() && LazyLoadNodesAsyncDelegate != null)
            {
                Nodes = await LazyLoadNodesAsyncDelegate(0);
            }
        }

        protected void OnNodeClick(UIMouseEventArgs e, BlazorTreeNode<T> node)
        {
            if (!node.Children.Any()) // Only Nodes without children can be selected
            {
                node.IsSelected = !node.IsSelected;
                OnSelect.InvokeAsync(node);
            }
        }

        protected async void OnExpandClick(UIMouseEventArgs e, BlazorTreeNode<T> node)
        {
            node.IsExpanded = !node.IsExpanded;
            if (node.IsExpanded)
            {
                await OnExpand.InvokeAsync(node);
            }
            else
            {
                await OnCollapse.InvokeAsync(node);
            }
        }

        protected string GetIconClass(BlazorTreeNode<T> node)
        {
            return node.IsExpanded ? "fas fa-caret-down" : "fas fa-caret-right";
        }

        protected string GetNodeClass(BlazorTreeNode<T> node)
        {
            var defaultClass = "list-group-item";
            return node.IsSelected ? defaultClass + " active" : defaultClass + " text-black";
        }


        protected string GetNodesAsMarkupRecurse(List<BlazorTreeNode<T>> nodes)
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
