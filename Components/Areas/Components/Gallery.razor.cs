using System;
using System.Collections.Generic;
using System.Linq;
using Cloud_In_A_Box.Components.Models;
using Microsoft.AspNetCore.Components;

namespace Cloud_In_A_Box.Components.Areas.Components
{
    public class GalleryComponentBase<T> : ComponentBase
    {
        [Parameter]
        public List<GalleryItem<T>> Items { get; private set; } = new List<GalleryItem<T>>();

        public GalleryItem<T> CurrentItem => CurrentIndex < Items.Count ? Items[CurrentIndex] : Items[0];

        public int CurrentIndex { get; private set; }

        public Action<GalleryItem<T>> OnItemSelected;


        public void Navigate(int index)
        {
            if (index < Items.Count && index >= 0)
            {
                CurrentIndex = index;
                SelectedItemHasChanged();
            }
        }

        public void Forward(UIMouseEventArgs e)
        {
            if (CurrentIndex < Items.Count - 1)
            {
                CurrentIndex++;
                SelectedItemHasChanged();
            }
        }

        public void Back(UIMouseEventArgs e)
        {
            CurrentIndex = CurrentIndex == 0 ? 0 : CurrentIndex - 1;
            SelectedItemHasChanged();
        }

        public void SelectedItemHasChanged()
        {
            OnItemSelected?.Invoke(CurrentItem);
        }


        protected override void OnInit()
        {
            if (Items.Any())
            {
                CurrentIndex = 0;
                SelectedItemHasChanged();
            }
        }
    }
}
