namespace Application.Domain
{
    using System.ComponentModel;

    public enum ItemType
    {
        [DisplayName("通常")]
        Normal,
        [DisplayName("特別")]
        Special
    }
}
