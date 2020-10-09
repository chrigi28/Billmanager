using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Billmanager.Database.Tables;
using Billmanager.Interfaces.Database.Datatables;
using Billmanager.Interfaces.Service;
using Billmanager.Translations.Texts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SkiaSharpSample.Samples
{
    public static class PdfFactory
    {
        private static float offsetTop = 180;
        private static float offsetBottom = 100;
        private static float offsetLeft = 100;
        private static float offsetRight = 100;
        private static float pageWidth = 840;
        private static float pageHeight = 1188;
        private static float textHeight = 14;
        private static float rowSpacing = 20;

        private static float startBill = pageHeight / 3;

        public async static void CreateBill(string path, BillDbt bill)
        {
            var setting = (await DependencyService.Get<IBaseService>().GetAllAsync<ISettingsDbt>()).FirstOrDefault();

            using var document = SKDocument.CreatePdf(path);

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
            using (var page1 = document.BeginPage(pageWidth, pageHeight))
            {
                if (setting == null)
                {
                    Console.WriteLine("No settings available");
                }

                DrawLogo(page1, setting.Image);
                DrawCustomerAdress(page1, defaultPaint, bill);
                DrawHolderAdress(page1, setting, defaultPaint);

                DrawDetails(document, page1, defaultPaint, bill);

                document.EndPage();
            }
            
            // end the doc
            document.Close();
        }

        private static void DrawLogo(SKCanvas pdfCanvas, string imagePath)
        {
            SKImage.FromEncodedData()
            image
            //// todo var path to image
            pdfCanvas.DrawImage(image, );
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

        private static void DrawDetails(SKDocument document, SKCanvas currentPage, SKPaint textPaint, BillDbt bill)
        {

            #region Styles

            var titelHeight = textHeight + textHeight / 3 + 3;
            using var boldTitel = new SKPaint
            {
                TextSize = titelHeight,
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
            currentPage.DrawText(Resources.Bill, offsetLeft, yStart, boldTitel);
            yStart += titelHeight + 5;
            currentPage.DrawText($"{Resources.BillNumber}: {bill.BillNumber}", offsetLeft, yStart, textPaint);
            yStart += rowSpacing;
            currentPage.DrawText($"{Resources.Date}: {bill.Date.ToShortDateString()}", offsetLeft, yStart, textPaint);
            yStart += rowSpacing;
            #endregion
            
            #region BillItemHeader

            yStart += 10;
            float hStart;
            DrawItemHeader(currentPage, textPaint, ref yStart, linePaint);

            #endregion

            #region BillItems

            var pageCount = 1;

            int pos = 0;
            foreach (var item in bill.ItemPositions)
            {
                hStart = offsetLeft;
                currentPage.DrawText($"{pos}", hStart, yStart, textPaint);
                hStart += 50;
                currentPage.DrawText($"{item.Description}", hStart, yStart, textPaint);
                hStart += 350;
                currentPage.DrawText($"{item.Amount}", hStart, yStart, textPaint);
                hStart += 50;
                currentPage.DrawText($"{item.Price}", hStart, yStart, textPaint);
                hStart += 100;
                currentPage.DrawText($"{item.Total}", hStart, yStart, textPaint);
                pos++;
                yStart += rowSpacing+5;

                // end of page is close make a new one
                if (pageHeight / 5 * 4 <= yStart)
                {
                    // todo add some page info  pagesize etc.
                    WritePageNumber(currentPage, textPaint, pageCount);                    
                    document.EndPage();
                    currentPage = document.BeginPage(pageWidth, pageHeight);
                    pageCount++;
                    DrawLogo(currentPage);
                    yStart = offsetTop;
                    DrawItemHeader(currentPage, textPaint, ref yStart, linePaint);
                }
            }

            if (pageCount > 1)
            {
                WritePageNumber(currentPage, textPaint, pageCount);   
            }

            #endregion

            #region BillSummary
            currentPage.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;
            hStart = offsetLeft + 450; // begin price

            var bruto = "Brutobetrag";
            var currency = "CHF";
            currentPage.DrawText($"{bruto}: ", hStart, yStart, textPaint);
            hStart += 100; // width of price
            currentPage.DrawText($"{bill.ItemPositions.Sum(f => f.Total)} {currency}", hStart, yStart, textPaint);

            yStart += rowSpacing;
            
            hStart -= 100;
            var fat = textPaint;
            fat.FakeBoldText = true;
            var netto = "Netto: ";
            currentPage.DrawText($"{netto}", hStart, yStart, fat);
            hStart += 100; // width of price
            currentPage.DrawText($"{bill.NettoPrice} {currency}", hStart, yStart, fat);
            yStart += rowSpacing;

            #endregion

            ////pdfCanvas.DrawText($"{bill.Customer.Address}", offsetLeft, offsetTop + logoheight + 30, textPaint);
            ////pdfCanvas.DrawText($"{bill.Customer.Zip} {bill.Customer.Location}", offsetLeft, offsetTop + logoheight + 45, textPaint);
        }

        private static void WritePageNumber(SKCanvas currentPage, SKPaint textPaint, int pageCount)
        {
            currentPage.DrawText($"Seite {pageCount}", pageWidth - offsetRight - 50, pageHeight - offsetBottom, textPaint);
        }

        private static float DrawItemHeader(SKCanvas pdfCanvas, SKPaint textPaint, ref float yStart, SKPaint linePaint)
        {
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
            return yStart;
        }
    }
}
