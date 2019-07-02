using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace BlazingComponents.Lib.Areas.Components
{
    public class BlazorTableBase<T> : ComponentBase
    {
        [Parameter]
        protected Func<T, object> ItemKeyDelegate { get; set; } = (T item) => { return string.Join(";", item.GetType().GetProperties().Select(p => $"({p.Name}:{p.GetValue(item)})")); };

        [Parameter]
        protected Func<T, object> ExpandedItemKeyDelegate { get; set; } = (T item) => { return string.Join(";", item.GetType().GetProperties().Select(p => $"({p.Name}:{p.GetValue(item)})")); };

        [Parameter]
        protected string TableClass { get; set; } = "table table-striped";

        [Parameter]
        protected string SelectedClass { get; set; } = "bg-primary text-white";

        [Parameter]
        protected RenderFragment TableHeader { get; set; }

        [Parameter]
        protected RenderFragment<T> RowTemplate { get; set; }

        [Parameter]
        protected RenderFragment<T> ExpandedRowTemplate { get; set; }

        [Parameter]
        protected RenderFragment TableFooter { get; set; }

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
        protected bool UsePagination { get; set; } = false;

        protected int ItemsPerPage {
            get => _itemsPerPage;
            set
            {
                _itemsPerPage = value;
                CreatePages();
                StateHasChanged();
            }
        }

        [Parameter]
        protected IList<int> ItemsPerPageOptions { get; set; } = new List<int> { 5, 10, 50, 100, 1000 }; 

        [Parameter]
        public IList<T> SelectedItems { get; private set; } = new List<T>();

        [Parameter]
        public IList<T> ExpandedItems { get; private set; } = new List<T>();

        [Parameter]
        public EventCallback<T> OnSelect { get; set; }

        [Parameter]
        public EventCallback<T> OnExpand { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<T>> OnSelectMany { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<T>> OnExpandMany { get; set; }

        protected IList<T> CurrentPage => Pages[PageIndex];
        protected IList<IList<T>> Pages { get; private set; } = new List<IList<T>>();
        protected int PageIndex { get; private set; } = 0;

        private int _itemsPerPage;

        public event Action<T> OnItemSelected;
        public event Action<T> OnItemExpanded;
        public event Action<IEnumerable<T>> OnItemsSelected;
        public event Action<IEnumerable<T>> OnItemsExpanded;

        protected override void OnInit()
        {
            _itemsPerPage = ItemsPerPageOptions.FirstOrDefault();
            CreatePages();
        }

        protected void CreatePages()
        {
            Pages = new List<IList<T>>();
            if(!UsePagination)
            {
                Pages.Add(Items.ToList());
            }
            if (ItemsPerPage <= 0) return;
            if (!ItemsPerPageOptions.Contains(ItemsPerPage))
            {
                ItemsPerPage = ItemsPerPageOptions.FirstOrDefault();
            }
            var count = 0;
            while(count < Items.Count)
            {
                Pages.Add(Items.Skip(count).Take(ItemsPerPage).ToList());
                count += ItemsPerPage;
            }
            if(Pages.Count <= PageIndex)
            {
                PageIndex = Pages.Count - 1;
            }
        }

        protected void ChangeItemsPerPage(UIChangeEventArgs e)
        {
            try
            {
                ItemsPerPage = (int)e.Value;
                CreatePages();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        protected void ChangePage(int pageIndex)
        {
            if (pageIndex < 0 || pageIndex >= Pages.Count) return;
            PageIndex = pageIndex;
            StateHasChanged();
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
            OnItemsSelected?.Invoke(new List<T> { item });
            OnSelect.InvokeAsync(item);
        }

        public void SelectItems(IEnumerable<T> items)
        {
            if (!Selectable) return;
            if (!MultiSelect)
            {
                SelectItem(items.FirstOrDefault());
            }
            else
            {
                SelectedItems = items.ToList();
                OnItemsSelected?.Invoke(items);
                OnSelectMany.InvokeAsync(items);
            }
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
            OnItemsExpanded?.Invoke(new List<T> { item });
            OnExpand.InvokeAsync(item);
        }

        public void ExpandItems(IEnumerable<T> items)
        {
            if (!Expandable) return;
            if (!MultiExpand)
            {
                ExpandItem(items.FirstOrDefault());
            }
            else
            {
                ExpandedItems = items.ToList();
                OnItemsExpanded?.Invoke(items);
                OnExpandMany.InvokeAsync(items);
            }
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