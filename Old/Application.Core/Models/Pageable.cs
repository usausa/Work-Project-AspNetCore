namespace Application.Models
{
    public class Pageable
    {
        public int Page { get; set; }

        public int Size { get; set; }

        public int Offset => (Page - 1) * Size;
    }
}
