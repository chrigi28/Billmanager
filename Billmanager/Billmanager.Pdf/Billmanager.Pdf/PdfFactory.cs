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
        private static float offsetTop = 50;
        private static float logoHeight = 210;
        private static float offsetBottom = 100;
        private static float offsetLeft = 100;
        private static float offsetRight = 100;
        private static float pageWidth = 840;
        private static float pageHeight = 1188;
        private static float textHeight = 14;
        private static float rowSpacing = 20;

        private static float startBill = pageHeight / 3;

        /// <summary>Creates A printable Bill</summary>
        /// <param name="path">file save path</param>
        /// <param name="bill">bill data</param>
        public async static void CreateBill(string path, BillDbt bill)
        {
            var setting = (await DependencyService.Get<IBaseService>().GetAllAsync<SettingsDbt>()).FirstOrDefault();

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

            int pages = ProceedPdf(bill, document, setting, defaultPaint);

            if (pages > 1)
            {
                // HACK cancel document and do it again with page count  very imperformant 
                document.Abort();
                document.Close();
                document.Dispose();
                using var numberedDocument = SKDocument.CreatePdf(path);
                ProceedPdf(bill, numberedDocument, setting, defaultPaint, pages);
                numberedDocument.Close();
            }
            else
            {
                document.Close();
            }
        }

        /// <summary>Proceeds the pdf draws the logo, customer, holder, positions, footer</summary>
        /// <param name="bill">data</param>
        /// <param name="document">document</param>
        /// <param name="setting">settings for holder infos</param>
        /// <param name="defaultPaint">default text style</param>
        /// <param name="totalPages">number of total generated pages</param>
        /// <returns>number of total generated pages</returns>
        private static int ProceedPdf(BillDbt bill, SKDocument document, SettingsDbt setting, SKPaint defaultPaint, int? totalPages = null)
        {
            int pages;
            using (var page1 = document.BeginPage(pageWidth, pageHeight))
            {
                if (setting == null)
                {
                    Console.WriteLine("No settings available");
                }

                DrawLogo(page1, setting.LogoPath);
                DrawCustomerAdress(page1, defaultPaint, bill);
                DrawHolderInfo(page1, setting, defaultPaint);
                pages = DrawDetails(document, page1, setting, defaultPaint, bill, totalPages);

                document.EndPage();
            }

            return pages;
        }

        /// <summary>Draws the logo at the top left corner</summary>
        /// <param name="pdfCanvas">page to draw on</param>
        /// <param name="imagePath">path to logo</param>
        private static void DrawLogo(SKCanvas pdfCanvas, string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                return; 
            }

            var file = File.ReadAllBytes(imagePath);
            using (var bitmap = SKImage.FromEncodedData(file))
            {
                pdfCanvas.DrawImage(bitmap,offsetLeft, offsetTop);
            }
        }

        /// <summary>prints the Customer infos to the pdf</summary>
        /// <param name="pdfCanvas">page</param>
        /// <param name="paint">textstyle</param>
        /// <param name="bill">data</param>
        private static void DrawCustomerAdress(SKCanvas pdfCanvas, SKPaint paint, BillDbt bill)
        {
            var yStart = offsetTop + logoHeight;
            pdfCanvas.DrawText(bill.Customer.Title_customer, offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.FirstName} {bill.Customer.LastName}", offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.Address}", offsetLeft, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{bill.Customer.Zip} {bill.Customer.Location}", offsetLeft, yStart, paint);
        }

        /// <summary>prints the holders infos from the settings to the pdf </summary>
        /// <param name="pdfCanvas">page</param>
        /// <param name="setting">settings</param>
        /// <param name="paint">style</param>
        private static void DrawHolderInfo(SKCanvas pdfCanvas, SettingsDbt setting, SKPaint paint)
        {
            var yStart = logoHeight;
            float startRight = pageWidth - offsetRight - 200;

            var firm = setting.Title;
            var name = setting.FirstName + " " + setting.LastName;
            var adress = setting.Address;
            var zip = setting.Zip;
            var loc = setting.Location;
            var tel = setting.Phone;
            var mail = setting.Email;

            pdfCanvas.DrawText($"{firm}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{name}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{adress}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{zip} {loc}", startRight, yStart, paint);
            yStart += rowSpacing + 5;
            pdfCanvas.DrawText($"{mail}", startRight, yStart, paint);
            yStart += rowSpacing;
            pdfCanvas.DrawText($"{tel}", startRight, yStart, paint);
        }

        /// <summary>Draw the Bill details and Positions </summary>
        /// <param name="document">document needed to add new pages</param>
        /// <param name="currentPage">current page</param>
        /// <param name="settings">settings for holders logo</param>
        /// <param name="textPaint">style</param>
        /// <param name="bill">data</param>
        /// <param name="totalPages">page count</param>
        /// <returns>page count</returns>
        private static int DrawDetails(SKDocument document, SKCanvas currentPage, SettingsDbt settings, SKPaint textPaint, BillDbt bill, int? totalPages = null)
        {
            #region Styles

            var titelHeight = textHeight + textHeight / 3 + 5;
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
                    WritePageNumber(currentPage, textPaint, pageCount, totalPages);                    
                    document.EndPage();
                    currentPage = document.BeginPage(pageWidth, pageHeight);
                    pageCount++;
                    DrawLogo(currentPage, settings.LogoPath);
                    yStart = offsetTop + logoHeight;
                    DrawItemHeader(currentPage, textPaint, ref yStart, linePaint);
                }
            }

            if (pageCount > 1)
            {
                WritePageNumber(currentPage, textPaint, pageCount, totalPages);   
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


            return pageCount;
            ////pdfCanvas.DrawText($"{bill.Customer.Address}", offsetLeft, offsetTop + logoheight + 30, textPaint);
            ////pdfCanvas.DrawText($"{bill.Customer.Zip} {bill.Customer.Location}", offsetLeft, offsetTop + logoheight + 45, textPaint);
        }

        /// <summary>Adds the pagenumber to the bottom right</summary>
        /// <param name="currentPage">page to draw on</param>
        /// <param name="textPaint">style</param>
        /// <param name="pageCount">current page number</param>
        /// <param name="totalPages">total page count</param>
        private static void WritePageNumber(SKCanvas currentPage, SKPaint textPaint, int pageCount, int? totalPages = null)
        {
            if (totalPages != null)
            {
                currentPage.DrawText($"Seite {pageCount} / {totalPages}", pageWidth - offsetRight - 50, pageHeight - offsetBottom, textPaint);
                return;
            }

            currentPage.DrawText($"Seite {pageCount}", pageWidth - offsetRight - 50, pageHeight - offsetBottom, textPaint);
        }

        /// <summary>Draws the header to the bill</summary>
        /// <param name="pdfCanvas">page</param>
        /// <param name="textPaint">style</param>
        /// <param name="yStart">vertical page start</param>
        /// <param name="linePaint">line stlye</param>
        /// <returns></returns>
        private static void DrawItemHeader(SKCanvas pdfCanvas, SKPaint textPaint, ref float yStart, SKPaint linePaint)
        {
            pdfCanvas.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;

            //// todo translate
            var hStart = offsetLeft;
            pdfCanvas.DrawText($"Pos", hStart, yStart, textPaint);
            hStart += 50;
            pdfCanvas.DrawText($"{Resources.Description}", hStart, yStart, textPaint);
            hStart += 350;
            pdfCanvas.DrawText($"{Resources.Amount}", hStart, yStart, textPaint);
            hStart += 50;
            pdfCanvas.DrawText($"{Resources.PricePerPcs}", hStart, yStart, textPaint);
            hStart += 100;
            pdfCanvas.DrawText($"{Resources.Total}", hStart, yStart, textPaint);

            yStart += 10;
            pdfCanvas.DrawLine(offsetLeft, yStart, pageWidth - offsetRight, yStart, linePaint);
            yStart += rowSpacing;
        }
    }
}
