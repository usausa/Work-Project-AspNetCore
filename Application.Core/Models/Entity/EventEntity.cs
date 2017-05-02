namespace Application.Models.Entity
{
    using System;

    using Application.Domain;

    public class EventEntity
    {
        public long Id { get; set; }

        public DateTimeOffset EventAt { get; set; }

        public EventType EventType { get; set; }

        public string Detail { get; set; }
    }
}
