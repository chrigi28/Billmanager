using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using SkiaSharp;
using Xamarin.Forms.Internals;

namespace SkiaSharpSample.Samples
{
    public static class PdfTest
    {
        public static void GenerateDocument(string path)
        {
            var metadata = new SKDocumentPdfMetadata
            {
                Author = "Cool Developer",
                Creation = DateTime.Now,
                Creator = "Cool Developer Library",
                Keywords = "SkiaSharp, Sample, PDF, Developer, Library",
                Modified = DateTime.Now,
                Producer = "SkiaSharp",
                Subject = "SkiaSharp Sample PDF",
                Title = "Sample PDF",
            };

            using var document = SKDocument.CreatePdf(path, metadata);

            if (document == null)
            {
                return;
            }

            using var paint = new SKPaint
            {
                TextSize = 64.0f,
                IsAntialias = true,
                Color = 0xFF9CAFB7,
                IsStroke = true,
                StrokeWidth = 3,
                TextAlign = SKTextAlign.Center
            };

            var pageWidth = 840;
            var pageHeight = 1188;

            // draw page 1
            using (var pdfCanvas = document.BeginPage(pageWidth, pageHeight))
            {
                // draw contents
                pdfCanvas.DrawText("...PDF 1/2...", pageWidth / 2, pageHeight / 4, paint);
                document.EndPage();
            }

            // draw page 2
            using (var pdfCanvas = document.BeginPage(pageWidth, pageHeight))
            {
                // draw contents
                pdfCanvas.DrawText("...PDF 2/2...", pageWidth / 2, pageHeight / 4, paint);
                document.EndPage();
            }

            // end the doc
            document.Close();
        }
    }
}
