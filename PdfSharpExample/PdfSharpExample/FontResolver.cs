namespace PdfSharpExample
{
    using System.IO;

    using PdfSharp.Fonts;

    public class FontResolver : IFontResolver
    {
        private readonly string path;

        public FontResolver(string path)
        {
            this.path = path;
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName == "Gothic")
            {
                return new FontResolverInfo("IPA ex Gothic");
            }

            return null;
        }

        public byte[] GetFont(string faceName)
        {
            if (faceName == "IPA ex Gothic")
            {
                return File.ReadAllBytes(Path.Combine(path, "ipaexg.ttf"));
            }

            return null;
        }
    }
}
