using BudgettingWebApps.Models;
using Npgsql.Replication.PgOutput;
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
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);

                    // Define Header
                    page.Header()
                        .Text("Budget Report")
                        .FontSize(20)
                        .Bold();

                    // Define Content (make sure to only call this once)
                    page.Content()
                        .Element(container =>
                        {
                            // container.Row(row =>
                            // {
                            //     // Define content elements here
                            // });
                            container.Table(table1 =>
                            {
                                // step 1
                                table1.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(25);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                // step 2
                                table1.Header(header =>
                                {
                                    header.Cell().Element(CellStyle).Text("#");
                                    header.Cell().Element(CellStyle).Text("Income Name");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Amount");
                                    header.Cell().Element(CellStyle).AlignRight().Text("Date");
                                  
                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5)
                                            .BorderBottom(1).BorderColor(Colors.Black);
                                    }
                                });

                                // step 3
                                foreach (var item in OrderList)
                                {
                                    table1.Cell().Element(CellStyle).Text(item.Id);
                                    table1.Cell().Element(CellStyle).Text(item.Product);
                                    table1.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPrice}$");
                                    table1.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                                    // table1.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPrice * item.Quantity}$");

                                    static IContainer CellStyle(IContainer container)
                                    {
                                        return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                            .PaddingVertical(5);
                                    }
                                }
                            });
                        });

                    // Define Footer
                    page.Footer()
                        .Text("Page footer information")
                        .AlignRight();
                });
            });
            reportBytes = document.GeneratePdf();
            return reportBytes;
        }

        public static List<Order> OrderList = new List<Order>()
        {
            new Order() { Id = 1, Product = "Corrupt", UnitPrice = 7879, Quantity = 1 },
            new Order() { Id = 2, Product = "Explicit Nam animal", UnitPrice = 3586, Quantity = 1 },
            new Order() { Id = 3, Product = "Facere Delectus", UnitPrice = 2366, Quantity = 8 },
            new Order() { Id = 4, Product = "Susprits", UnitPrice = 8562, Quantity = 5 },
            new Order() { Id = 5, Product = "Beatae Corrupt", UnitPrice = 9531, Quantity = 7 },
            new Order() { Id = 6, Product = "Consectutur recendis commondi", UnitPrice = 5297, Quantity = 8 },
            new Order() { Id = 7, Product = "Nostrum persictias", UnitPrice = 7879, Quantity = 9 },
            new Order() { Id = 8, Product = "Numquan quae", UnitPrice = 7879, Quantity = 9 },
        };
    }

    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}