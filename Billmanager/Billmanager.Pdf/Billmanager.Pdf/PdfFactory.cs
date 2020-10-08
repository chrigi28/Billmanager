using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Translations.Texts;
using SkiaSharp;
using Xamarin.Forms.Internals;

namespace SkiaSharpSample.Samples
{
    public static class PdfFactory
    {
        private static float offsetTop = 180;
        private static float offsetBottom = 300;
        private static float offsetLeft = 100;
        private static float offsetRight = 100;
        private static float pageWidth = 840;
        private static float pageHeight = 1188;
        private static float textHeight = 14;
        private static float rowSpacing = 20;

        private static float startBill = pageHeight / 3;

        public static void CreateBill(string path, BillDbt bill)
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

            using var defaultPaint = new SKPaint
            {
                TextSize = textHeight,
                IsAntialias = true,
                Color = SKColors.Black,
                IsStroke = false,
                TextAlign = SKTextAlign.Left,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Normal,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            // draw page 1
            using (var pdfCanvas = document.BeginPage(pageWidth, pageHeight))
            {
                DrawLogo(pdfCanvas);
                DrawCustomerAdress(pdfCanvas, defaultPaint, bill);
                DrawHolderAdress(pdfCanvas, defaultPaint);

                DrawDetails(pdfCanvas, defaultPaint, bill);

                // draw contents
                //pdfCanvas.DrawText("...PDF 1/2...", pageWidth / 2, pageHeight / 4, paint);
                document.EndPage();
            }

            // draw page 2
            using (var pdfCanvas = document.BeginPage(pageWidth, pageHeight))
            {
                DrawLogo(pdfCanvas);
                // draw contents
                pdfCanvas.DrawText("...PDF 2/2...", pageWidth / 2, pageHeight / 4, defaultPaint);
                document.EndPage();
            }

            // end the doc
            document.Close();
        }

        private static void DrawLogo(SKCanvas pdfCanvas)
        {
            //// todo var path to image
            ////pdfCanvas.DrawImage();
        }

        private static void DrawCustomerAdress(SKCanvas pdfCanvas, SKPaint paint, BillDbt bill)
        {
            float logoheight = 30;
            var yStart = offsetTop + logoheight;
            pdfCanvas.DrawText(bill.Customer.Title_customer, offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.FirstName} {bill.Customer.LastName}", offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.Address}", offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.Zip} {bill.Customer.Location}", offsetLeft, yStart, paint);
        }

        private static void DrawHolderAdress(SKCanvas pdfCanvas, SKPaint paint)
        {
            
            float logoheight = 30;
            var yStart = offsetTop + logoheight;
            float startRight = pageWidth - offsetRight - 150;

            var firm = "Züger Fahrzeugelektronik";
            var name = "Edgar Züger";
            var adress = "Kantonsstrasse 6";
            var zip = 8863;
            var loc = "Schübelbach";
            var tel = "078 638 29 95";

            pdfCanvas.DrawText($"{firm}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{name}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{adress}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{zip} {loc}", startRight, yStart, paint);
            yStart += rowSpacing + 5;
            pdfCanvas.DrawText($"{tel}", startRight, yStart, paint);
        }

        private static void DrawDetails(SKCanvas pdfCanvas, SKPaint textPaint, BillDbt bill)
        {

            #region Styles
            using var boldTitel = new SKPaint
            {
                TextSize = textHeight + textHeight / 3 + 3,
                IsAntialias = true,
                Color = SKColors.Black,
                IsStroke = false,
                TextAlign = SKTextAlign.Left,
                Typeface = SKTypeface.FromFamilyName(
                    "Arial",
                    SKFontStyleWeight.Bold,
                    SKFontStyleWidth.Normal,
                    SKFontStyleSlant.Upright)
            };

            using SKPaint linePaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                IsAntialias = true,
                Color = SKColors.Black,
                StrokeWidth = 2,
                StrokeCap = SKStrokeCap.Butt,
            };

            #endregion

            #region BillDetails
            var yStart = startBill;
            pdfCanvas.DrawText(Resources.Bill, offsetLeft, yStart, boldTitel);
            yStart += 20;
            pdfCanvas.DrawText($"{Resources.BillNumber}: {bill.BillNumber}", offsetLeft, yStart, textPaint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{Resources.Date}: {bill.Date.ToShortDateString()}", offsetLeft, yStart, textPaint);
            yStart += rowSpacing;
            #endregion
            
            #region BillItemHeader

            yStart += 10;
            pdfCanvas.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;

            //// todo translate
            var hStart = offsetLeft;
            pdfCanvas.DrawText($"Pos", hStart, yStart, textPaint);
            hStart += 50;
            pdfCanvas.DrawText($"Bezeichnung", hStart, yStart, textPaint);
            hStart += 350;
            pdfCanvas.DrawText($"Menge", hStart, yStart, textPaint);
            hStart += 50;
            pdfCanvas.DrawText($"Preis/Stk", hStart, yStart, textPaint);
            hStart += 100;
            pdfCanvas.DrawText($"Gesamt", hStart, yStart, textPaint);
            
            yStart += 10;
            pdfCanvas.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;
            
            #endregion

            #region BillItems
            
            int pos = 0;
            foreach (var item in bill.ItemPositions)
            {
                hStart = offsetLeft;
                pdfCanvas.DrawText($"{pos}", hStart, yStart, textPaint);
                hStart += 50;
                pdfCanvas.DrawText($"{item.Description}", hStart, yStart, textPaint);
                hStart += 350;
                pdfCanvas.DrawText($"{item.Amount}", hStart, yStart, textPaint);
                hStart += 50;
                pdfCanvas.DrawText($"{item.Price}", hStart, yStart, textPaint);
                hStart += 100;
                pdfCanvas.DrawText($"{item.Total}", hStart, yStart, textPaint);

                pos++;
                yStart += rowSpacing+5;
            }
            
            #endregion

            #region BillSummary
            pdfCanvas.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;
            hStart = offsetLeft + 450; // begin price

            var bruto = "Brutobetrag";
            var currency = "CHF";
            pdfCanvas.DrawText($"{bruto}: ", hStart, yStart, textPaint);
            hStart += 100; // width of price
            pdfCanvas.DrawText($"{bill.ItemPositions.Sum(f => f.Total)} {currency}", hStart, yStart, textPaint);

            yStart += rowSpacing;
            
            hStart -= 100;
            var fat = textPaint;
            fat.FakeBoldText = true;
            var netto = "Netto: ";
            pdfCanvas.DrawText($"{netto}: ", hStart, yStart, fat);
            hStart += 100; // width of price
            pdfCanvas.DrawText($"{bill.NettoPrice} {currency}", hStart, yStart, fat);
            yStart += rowSpacing;

            #endregion

            ////pdfCanvas.DrawText($"{bill.Customer.Address}", offsetLeft, offsetTop + logoheight + 30, textPaint);
            ////pdfCanvas.DrawText($"{bill.Customer.Zip} {bill.Customer.Location}", offsetLeft, offsetTop + logoheight + 45, textPaint);
        }
    }
}
