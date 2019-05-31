using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace BlazorEssentials.ComponentLib.Areas.Components
{
    public class AutoTableComponentBase<T> : ComponentBase
    {
        [Parameter]
        protected string TableClass { get; set; } = "table table-striped";

        [Parameter]
        protected string SelectedClass { get; set; } = "bg-primary text-white";

        [Parameter]
        protected IEnumerable<string> Properties { get; set; } = new List<string>();

        [Parameter]
        protected RenderFragment<T> ExpandedRowTemplate { get; set; }

        [Parameter]
        protected IReadOnlyList<T> Items { get; set; }

        [Parameter]
        protected bool Selectable { get; set; } = true;

        [Parameter]
        protected bool Expandable { get; set; } = true;

        [Parameter]
        protected bool MultiSelect { get; set; } = false;

        [Parameter]
        protected bool MultiExpand { get; set; } = false;

        [Parameter]
        public IList<T> SelectedItems { get; private set; } = new List<T>();

        [Parameter]
        public IList<T> ExpandedItems { get; private set; } = new List<T>();

        [Parameter]
        public EventCallback<T> OnSelect { get; set; }

        [Parameter]
        public EventCallback<T> OnExpand { get; set; }


        protected IList<PropertyInfo> PropertyInfos = new List<PropertyInfo>();

        public event Action<T> OnItemSelected;
        public event Action<T> OnItemExpanded;

        protected override void OnInit()
        {
            var typeProps = typeof(T).GetProperties();
            if (!Properties.Any()) // Take All Properties if Parameter is not Set
            {
                Properties = typeProps.Select(p => p.Name);
            }
            foreach (var prop in Properties)
            {
                var propertyInfo = typeProps.FirstOrDefault(tp => tp.Name == prop);
                if (propertyInfo != null)
                    PropertyInfos.Add(propertyInfo);
            }
            Properties = PropertyInfos.Select(p => p.Name).ToList();
        }

        public void SelectItem(T item)
        {
            if (!Selectable) return;
            if (SelectedItems.Contains(item))
            {
                SelectedItems.Remove(item);
            }
            else
            {
                if (!MultiSelect)
                    SelectedItems = new List<T>();
                SelectedItems.Add(item);
            }
            OnItemSelected?.Invoke(item);
            OnSelect.InvokeAsync(item);
           
        }

        public void ExpandItem(T item)
        {
            if (!Expandable) return;
            if (ExpandedItems.Contains(item))
            {
                ExpandedItems.Remove(item);
            }
            else
            {
                if (!MultiExpand)
                    ExpandedItems = new List<T>();
                ExpandedItems.Add(item);
            }
            OnItemExpanded?.Invoke(item);
            OnExpand.InvokeAsync(item);
        }

        public bool IsExpanded(T item)
        {
            return ExpandedItems.Contains(item);
        }

        public bool IsSelected(T item)
        {
            return SelectedItems.Contains(item);
        }

        public string GetRowClass(T item)
        {
            return IsSelected(item) ? SelectedClass : "";
        }

        public void ExpandAll()
        {
            if (!MultiExpand) return;
            ExpandedItems = Items.ToList();
        }

        public void CollapseAll()
        {
            ExpandedItems = new List<T>();
        }

        public void DeselectAll()
        {
            SelectedItems = new List<T>();
        }

        public void SelectAll()
        {
            if (!MultiSelect) return;
            SelectedItems = Items.ToList();
        }
    }
}
