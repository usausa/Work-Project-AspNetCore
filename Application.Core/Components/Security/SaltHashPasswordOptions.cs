namespace Application.Components.Security
{
    public class SaltHashPasswordOptions
    {
        public int SaltLength { get; set; }

        public string SaltCharacters { get; set; }
    }
}
