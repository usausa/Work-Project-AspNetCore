namespace SkiaSharpPdfExample.Controllers
{
    using System.IO;

    using Microsoft.AspNetCore.Mvc;

    using SkiaSharp;

    public class ReportController : Controller
    {
        public IActionResult Example1()
        {
            using (var ms = new MemoryStream())
            using (var stream = new SKManagedWStream(ms))
            using (var document = SKDocument.CreatePdf(stream))
            {
                using (var canvas = document.BeginPage(595f, 847f))
                {
                    using (var paint = new SKPaint())
                    {
                        paint.Color = SKColors.Black;

                        canvas.DrawLine(0, 0, 595f, 847f, paint);

                        paint.TextSize = 36;
                        canvas.DrawText("あいう36", 0, 0 + 36, paint);
                    }

                    document.EndPage();
                }

                document.Close();

                return File(ms.ToArray(), "application/pdf", "example1.pdf");
            }

        }
    }
}
