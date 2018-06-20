namespace PdfSharpExample.Controllers
{
    using System.IO;

    using Microsoft.AspNetCore.Mvc;

    using PdfSharp;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;

    public class ReportController : Controller
    {
        public IActionResult Example1()
        {
            using (var ms = new MemoryStream())
            using (var document = new PdfDocument())
            {
                document.Info.Title = "Example";

                var page = document.AddPage();
                page.Size = PageSize.A4;
                page.Orientation = PageOrientation.Portrait;

                using (var g = XGraphics.FromPdfPage(page))
                {
                    var font36 = new XFont("Gothic", 36, XFontStyle.Regular);

                    g.DrawString("Hello うさうさ", font36, XBrushes.Black, 100, 100);
                }

                document.Save(ms);

                return File(ms.ToArray(), "application/pdf", "example1.pdf");
            }
        }
    }
}
