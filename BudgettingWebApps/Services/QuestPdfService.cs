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
                    page.Margin(20);
                    page.Size(PageSizes.A4);

                    page.Header().Text("Budgeting Report")
                        .FontSize(18)
                        .SemiBold()
                        .AlignCenter();

                    page.Content().Column(column =>
                    {
                        column.Spacing(10);

                        // Summary
                        column.Item().Text($"Total Income: {TotalIncome:C}");
                        column.Item().Text($"Total Expenses: {TotalExpense:C}");
                        column.Item().Text($"Balance: {Balance:C}");

                        column.Item().Text("Income Details").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(200); // Income Name
                                columns.ConstantColumn(100); // Amount
                                columns.RelativeColumn(); // Date
                            });

                            // Header Row
                            table.Header(header =>
                            {
                                header.Cell().Text("Income Name").Bold();
                                header.Cell().Text("Amount").Bold();
                                header.Cell().Text("Date").Bold();
                            });

                            // Data Rows
                            foreach (var income in filteredIncome)
                            {
                                table.Cell().Text(income.Name);
                                table.Cell().Text(income.Amount.ToString("C"));
                                table.Cell().Text(income.TransactionDate.ToShortDateString());
                            }
                        });

                        column.Item().Text("Expense Details").FontSize(14).Bold();
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(200); // Expense Name
                                columns.ConstantColumn(100); // Amount
                                columns.RelativeColumn(); // Date
                            });

                            // Header Row
                            table.Header(header =>
                            {
                                header.Cell().Text("Expense Name").Bold();
                                header.Cell().Text("Amount").Bold();
                                header.Cell().Text("Date").Bold();
                            });

                            // Data Rows
                            foreach (var expense in filteredExpenses)
                            {
                                table.Cell().Text(expense.Name);
                                table.Cell().Text(expense.Amount.ToString("C"));
                                table.Cell().Text(expense.BudgetMonth.ToShortDateString());
                            }
                        });
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Generated on: ");
                        x.Span(DateTime.Now.ToString("f")).Bold();
                    });
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
        public static List<Income> filteredIncome = new List<Income>()
        {
            new Income() { Id = 1, Name = "Salary", Amount = 2500 ,TransactionDate = DateTime.Today},
            new Income() { Id = 2, Name = "Side Hustle", Amount = 500,TransactionDate = DateTime.Today }
        };
        public static List<Expense> filteredExpenses = new List<Expense>()
        {
            new Expense() { Id = 1, Name = "Rent", Amount = 1500,BudgetMonth = DateTime.Today},
            new Expense() { Id = 2, Name = "Food", Amount = 1000,BudgetMonth = DateTime.Today},
            new Expense() { Id = 3, Name = "Transport", Amount = 500,BudgetMonth = DateTime.Today},
            new Expense() { Id = 4, Name = "Fun", Amount = 500,BudgetMonth = DateTime.Today},
            new Expense() { Id = 5, Name = "Save", Amount = 500,BudgetMonth = DateTime.Today},
        };


        public static int TotalIncome = 3000;
        public static int TotalExpense = 2800;
        public static int Balance = 200;
    }

    public class Order
    {
        public int Id { get; set; }
        public string Product { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
    public class Income
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class Expense
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime BudgetMonth { get; set; }
    }
}