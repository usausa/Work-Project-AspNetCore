namespace SkiaSharpPdfExample.Controllers
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    using Microsoft.AspNetCore.Mvc;

    using SkiaSharp;

    using ZXing;
    using ZXing.QrCode;

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

                        canvas.DrawBitmap(CreateQrBitmap("https://localhost", 240, 240), 240, 0, paint);
                    }

                    document.EndPage();
                }

                document.Close();

                return File(ms.ToArray(), "application/pdf", "example1.pdf");
            }

        }

        private SKBitmap CreateQrBitmap(string text, int width, int height)
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
            using (var ms = new MemoryStream())
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

                bitmap.Save(ms, ImageFormat.Bmp);

                ms.Seek(0, SeekOrigin.Begin);

                using (var stream = new SKManagedStream(ms))
                {
                    return SKBitmap.Decode(stream);
                }
            }
        }
    }
}
