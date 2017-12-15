namespace Application.Domain
{
    using System.ComponentModel.DataAnnotations;

    public enum EventType
    {
        [Display(Name = "情報")]
        Information,
        [Display(Name = "警告")]
        Warning,
        [Display(Name = "異常")]
        Error
    }
}
