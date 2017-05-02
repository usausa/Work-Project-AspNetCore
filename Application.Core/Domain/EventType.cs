namespace Application.Domain
{
    using System.ComponentModel;

    public enum EventType
    {
        [DisplayName("情報")]
        Information,
        [DisplayName("警告")]
        Warning,
        [DisplayName("異常")]
        Error
    }
}
