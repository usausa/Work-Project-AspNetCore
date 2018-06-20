namespace PdfSharpExample.Controllers
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.AspNetCore.Mvc;

    using PdfSharp;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;

    using ZXing;
    using ZXing.QrCode;

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
                    var pen = new XPen(XColors.Black);
                    g.DrawLine(pen, 0, 0, page.Width, page.Height);

                    var font36 = new XFont("Gothic", 36, XFontStyle.Regular);
                    g.DrawString("Hello うさうさ", font36, XBrushes.Black, 100, 100);

                    var image = CreateQrImage("https://localhost", 240, 240);
                    g.DrawImage(image, 300, 200);
                }

                document.Save(ms);

                return File(ms.ToArray(), "application/pdf", "example1.pdf");
            }
        }

        private XImage CreateQrImage(string text, int width, int height)
        {
            var writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = width,
                    Height = height
                }
            };

            var data = writer.Write(text);

            using (var bitmap = new Bitmap(data.Width, data.Height, PixelFormat.Format32bppRgb))
            {
                var bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, data.Width, data.Height),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppRgb);
                try
                {
                    Marshal.Copy(data.Pixels, 0, bitmapData.Scan0, data.Pixels.Length);
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }

                var ms = new MemoryStream();
                bitmap.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                return XImage.FromStream(ms);
            }
        }
    }
}
