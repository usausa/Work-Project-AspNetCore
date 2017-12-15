namespace Application.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Paged<T> : IPaged, IEnumerable<T>
    {
        private readonly IList<T> items;

        public int Page { get; }

        public int Size { get; }

        public int Count { get; }

        public T this[int index] => items[index];

        public bool HasPrev => Page > 1;

        public bool HasNext => Page * Size < Count;

        public int TotalPage => Count == 0 ? 1 : (int)Math.Ceiling((decimal)Count / Size);

        public Paged(int page, int size, IList<T> items, int count)
        {
            Page = page;
            Size = size;
            this.items = items;
            Count = count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class Paged
    {
        public static Paged<T> From<T>(Pageable pageable, IList<T> items, int count)
        {
            return new Paged<T>(pageable.Page, pageable.Size, items, count);
        }
    }
}
