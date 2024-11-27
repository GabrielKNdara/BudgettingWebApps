using BudgettingWebApps.Models.Request;
using BudgettingWebApps.Reposiotories;
using BudgettingWebApps.Services;
using FastEndpoints;
using Serilog;


namespace BudgettingWebApps.Endpoint;

public class BudgetPdfReportEndpoint : Endpoint<ReportRequest>
{
    private readonly IincomeRepository _repository;
    private readonly IExpenseRepository _expenseRepository;
    public BudgetPdfReportEndpoint(IincomeRepository repository, IExpenseRepository expenseRepository)
    {
        _repository = repository;
        _expenseRepository = expenseRepository;
    }

    public override void Configure()
    {
        Get("/budgeting/pdf/report/userId={currentuser:int}&month={monthNumber:int}");
    }

    public override async Task HandleAsync(ReportRequest request,CancellationToken ct)
    {
        try
        {
            var userId = request.CurrentUser;
            var month = request.MonthNumber;
           Log.Information($"user id {userId}: month {month}");
           
           Console.WriteLine($"console user id {userId}: months {month}");
           
            var report = new QuestPdfService(_repository, _expenseRepository);
            var byteArray = report.GenerateBudgetReportBytesAsync(userId,month);
          await SendBytesAsync(await byteArray, "IncomeReport.pdf", "application/pdf");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}