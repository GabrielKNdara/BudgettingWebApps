using BudgettingWebApps.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;



namespace BudgettingWebApps.Services
{
    public class QuestPdfService
    {
        public static byte[] GenerateBudgetReportBytes()
        {
            byte[] reportBytes;
            Document document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(0.8f, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11).FontFamily("Tahoma", "Arial", ""));

                    page.Content().Border(1);
                 //   page.Content().Element(ComposeBudgetPdfReport);
                  //  page.Footer().Element(composeFooterContext);
                });
            });
            reportBytes = document.GeneratePdf();
            return reportBytes;
        }

       

        static void ComposeBudgetPdfReport(IContainer container)
        {
            var transactions = GetIncomeReport();
            int serialNumber = 0;
            
            container.Column(mainContentColumn =>
            {
                mainContentColumn.Item().Row(row =>
                {
                    row.AutoItem().Column(column =>
                    {
                        column.Item().Width(1, Unit.Inch);//add image later
                    });
                    row.RelativeItem().AlignCenter().Column(column =>
                    {
                        column
                            .Item().Text("MEDIPLUS DIAGNOSTIC CENTRE")
                            .FontSize(20).SemiBold();

                        column
                            .Item().AlignCenter().PaddingBottom(0.5f, Unit.Centimetre).Text("Lagos, Nigeria.")
                            .FontSize(13).SemiBold();

                        column
                            .Item().AlignCenter().Text("PHARMACY INCOME REPORT").Underline()
                            .FontSize(16);
                    });

                    row.AutoItem().AlignRight().Column(column =>
                    {
                        column.Item().Width(1, Unit.Inch); //add image later
                    });
                });
                
                mainContentColumn.Item().PaddingTop(0.8f,Unit.Centimetre).Row(row =>
                {
                    row.RelativeItem().Shrink().Border(1).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(40);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.ConstantColumn(90);
                            columns.ConstantColumn(90);
                            columns.ConstantColumn(100);
                            columns.ConstantColumn(200);
                        });
                        
                        // please be sure to call the 'header' handler!
                        table.Header(header =>
                        {
                            // header.Cell().Element(CellStyle).AlignCenter().Text("S/N").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).Text("Income Name").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).Text("Amount").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).Text("Transaction Date").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Amount Due (₦)").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Amount Paid (₦)").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Outstanding Bal.(₦)").FontSize(9).SemiBold();
                            header.Cell().Element(CellStyle).Text("Remarks").FontSize(9).SemiBold();

                            // you can extend existing styles by creating additional methods
                            IContainer CellStyle(IContainer container) => DefaultCellStyle(container, Colors.Grey.Lighten3);
                        });
                    });
                });
                mainContentColumn.Item().PaddingTop((float)4.0,Unit.Centimetre).Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text(".....................................");
                        column.Item().PaddingLeft(1.2f, Unit.Centimetre).PaddingTop(0.4f, Unit.Centimetre)
                            .Text("Doctor signature & date");
                    });
                    row.RelativeItem().AlignRight().Column(column =>
                    {
                        column.Item().Text("......................................");
                        column.Item().PaddingLeft(1.2f, Unit.Centimetre).PaddingTop(0.4f, Unit.Centimetre)
                            .Text("Account signature && date");
                    });
                });
            });
            
        }

        static IContainer DefaultCellStyle(IContainer container, string backgroundColor = "")
        {
            return container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten1)
                .Background(!string.IsNullOrEmpty(backgroundColor) ? backgroundColor : Colors.White)
                .PaddingVertical(7)
                .PaddingHorizontal(3);
        }
        private static List<IncomeDto> GetIncomeReport()
        {
            var incomeReport = new List<IncomeDto>()
            {
                new IncomeDto(){IncomeName = "Main Salary",Amount = 2500,TransactionDate = DateTime.Now},
                new IncomeDto(){IncomeName = "Second Salary",Amount = 3200,TransactionDate = DateTime.Now},
                new IncomeDto(){IncomeName = "Third Salary",Amount = 2500,TransactionDate = DateTime.Now},
                new IncomeDto(){IncomeName = "Fourth Salary",Amount = 1500,TransactionDate = DateTime.Now},
                new IncomeDto(){IncomeName = "Bonus",Amount = 2500,TransactionDate = DateTime.Now}
            };
            return incomeReport;
        }
    }
}
