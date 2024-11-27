using BudgettingWebApps.Models;
using BudgettingWebApps.Reposiotories;
using Microsoft.AspNetCore.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;


namespace BudgettingWebApps.Services
{
    public class QuestPdfService
    {
        private readonly IincomeRepository _repository;

        public QuestPdfService(IincomeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<byte[]> GenerateBudgetReportBytesAsync(int userId, int month)
        {
        
           var allIncome = await _repository.GetIncome(userId);
           if (allIncome == null || !allIncome.Any())
           {
               throw new InvalidOperationException("No Income data found for the given user");
           }
           
           var filteredIncome = allIncome.Where(x => x.TransactionDate.Month == month).ToList();
            return await Task.Run(() =>
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
                                table.Cell().Text(income.IncomeName);
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
            });

          
        
     
        }
           // private static List<IncomeDto> _income = new List<IncomeDto>(); 
           //      private static List<IncomeDto> filteredIncome = new List<IncomeDto>();
                    
             
                
                // private static List<ExpenseDto> _expense = new List<ExpenseDto>();
                // private static List<ExpenseDto> filteredExpenses = new List<ExpenseDto>();
        
             //   private static IExpenseRepository expenseRepository { get; set; } = default!;
        // public static List<Income> filteredIncome = new List<Income>()
        // {
        //     new Income() { Id = 1, Name = "Salary", Amount = 2500 ,TransactionDate = DateTime.Today},
        //     new Income() { Id = 2, Name = "Side Hustle", Amount = 500,TransactionDate = DateTime.Today }
        // };
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


}

// public class Income
// {
//     public int Id { get; set; }
//     public string Name { get; set; }=string.Empty;
//     public decimal Amount { get; set; }
//     public DateTime TransactionDate { get; set; }
// }
public class Expense
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime BudgetMonth { get; set; }
}