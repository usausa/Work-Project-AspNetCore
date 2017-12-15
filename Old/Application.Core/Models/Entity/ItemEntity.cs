namespace Application.Models.Entity
{
    using System;

    public class ItemEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
    }
}
