namespace Cloud_In_A_Box.Components.Models
{
    public class GalleryItem<T>
    {
        public string ImageSource { get; set; }
        public T Data { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
    }
}
