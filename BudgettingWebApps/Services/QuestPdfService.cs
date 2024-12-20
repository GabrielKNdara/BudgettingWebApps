﻿using BudgettingWebApps.Models;
using BudgettingWebApps.Reposiotories;
using Microsoft.AspNetCore.Components;
using QuestPDF.Fluent;
using QuestPDF.Helpers;


namespace BudgettingWebApps.Services
{
    public class QuestPdfService
    {
        private readonly IincomeRepository _repository;
        private readonly IExpenseRepository _expenseRepository;

        public QuestPdfService(IincomeRepository repository, IExpenseRepository expenseRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _expenseRepository = expenseRepository ?? throw new ArgumentNullException(nameof(expenseRepository));
        }

        public async Task<byte[]> GenerateBudgetReportBytesAsync(int userId, int month)
        {
            var allIncome = await _repository.GetIncome(userId);
            if (allIncome == null || !allIncome.Any())
            {
                throw new InvalidOperationException("No Income data found for the given user");
            }

            var filteredIncome = allIncome.Where(x => x.TransactionDate.Month == month).ToList();
            //This is for expenses
            var allExpenses = await _expenseRepository.GetExpenses(userId);
            if (allExpenses == null || !allExpenses.Any())
            {
                throw new InvalidOperationException("No Expense data found for the given user");
            }

            var filteredExpenses = allExpenses.Where(x => x.BudgetMonth.Month == month).ToList();

            // Compute totals
            var totalIncome = filteredIncome.Sum(x => x.Amount);
            var totalExpense = filteredExpenses.Sum(x => x.Amount);
            var balance = totalIncome - totalExpense;

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
                            column.Item().Text("Income Details").FontSize(14).Bold();
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Income Name
                                    columns.RelativeColumn(); // Amount
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

                                column.Spacing(10);
                                column.Item().LineHorizontal(1);
                                column.Item().Text($"Total Income: {totalIncome:C}");
                                column.Item().LineHorizontal(1);
                            });
                            column.Spacing(10);
                            column.Item().Text("Expense Details").FontSize(14).Bold();

                            //Expense Section
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(); // Expense Name
                                    columns.RelativeColumn(); // Amount
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
                                    table.Cell().Text(expense.ExpenseName);
                                    table.Cell().Text(expense.Amount.ToString("C"));
                                    table.Cell().Text(expense.BudgetMonth.ToShortDateString());
                                }
                            });
                            // column.Spacing(10);
                            column.Item().LineHorizontal(1);
                            column.Item().Text($"Total Expenses: {totalExpense:C}");
                            column.Item().LineHorizontal(1);
                            column.Spacing(10);
                            
                            column.Item().LineHorizontal(1);
                            column.Item().Text($"Balance: {balance:C}");
                            column.Item().LineHorizontal(1);
                         
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
    }
}