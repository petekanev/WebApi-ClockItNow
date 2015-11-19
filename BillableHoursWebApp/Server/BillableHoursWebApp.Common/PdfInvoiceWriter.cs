namespace BillableHoursWebApp.Common
{
    using System;
    using System.Globalization;
    using System.IO;
    using Data.Models;
    using iTextSharp.text;
    using iTextSharp.text.pdf;

    public class PdfInvoiceWriter
    {
        public static Stream GeneratePdf(object invoice)
        {
            Byte[] bytes;

            using (var ms = new MemoryStream())
            {
                using (var document = new Document(PageSize.A4, 50, 50, 25, 25))
                {
                    using (var writer = PdfWriter.GetInstance(document, ms))
                    {
                        var invoiceToWrite = (Invoice)invoice;

                        document.Open();
                        var header = new Paragraph("Billable Hours - Clock It Project Invoice",
                    new Font(Font.FontFamily.HELVETICA, 15));
                        header.Alignment = 1;

                        document.Add(header);
                        document.Add(new Paragraph("Project Name: " + invoiceToWrite.ProjectTitle));
                        document.Add(new Paragraph("Project Id: " + invoiceToWrite.ProjectId));
                        document.Add(new Paragraph("Project Category: " + invoiceToWrite.CategoryName));
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph("Issued on: " + invoiceToWrite.IssuedOn));
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph("Client: " + invoiceToWrite.ClientName));
                        document.Add(new Paragraph(new string(' ', 10) + invoiceToWrite.ClientEmail));
                        document.Add(new Paragraph("Employee: " + invoiceToWrite.EmployeeName));
                        document.Add(new Paragraph(new string(' ', 10) + invoiceToWrite.EmployeeEmail));
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph("Offered payment/hour: " + invoiceToWrite.PricePerHour));

                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph("Detailed Work Log:"));
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph(""));

                        var table = new PdfPTable(5);

                        var headerCell = new PdfPCell(new Phrase("Session Name"));
                        headerCell.BackgroundColor = new BaseColor(232, 232, 232);
                        headerCell.HorizontalAlignment = 1;
                        table.AddCell(headerCell);

                        table.AddCell("Start Time");
                        table.AddCell("End Time");
                        table.AddCell("Minutes");
                        table.AddCell("Price");

                        decimal totalPrice = 0;

                        foreach (var workLog in invoiceToWrite.WorkLogs)
                        {
                            var endTimeUpdated = workLog.EndTime.GetValueOrDefault(DateTime.Now);

                            table.AddCell(workLog.ShortDescription);
                            table.AddCell(workLog.StartTime.ToShortTimeString());
                            table.AddCell(endTimeUpdated.ToShortTimeString());

                            var minutes = (int)(endTimeUpdated - workLog.StartTime).TotalMinutes;

                            table.AddCell(minutes.ToString());

                            var price = invoiceToWrite.PricePerHour * ((decimal)minutes / 60);

                            totalPrice += price;

                            table.AddCell(string.Format("{0:0.00}", price));
                        }

                        document.Add(table);
                        var priceFooter = (new Paragraph(string.Format("Total price: {0:0.00}", totalPrice)));
                        priceFooter.Alignment = 1;
                        document.Add(priceFooter);
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph(""));
                        document.Add(new Paragraph("Please establish contact with employee shortly to arrange payment."));
                        document.Close();
                    }
                }

                bytes = ms.ToArray();
            }

            return new MemoryStream(bytes);
        }
    }
}
