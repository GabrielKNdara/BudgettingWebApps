using BudgettingWebApps.Reposiotories;
using BudgettingWebApps.Services;
using FastEndpoints;


namespace BudgettingWebApps.Endpoint;

public class BudgetPdfReportEndpoint : EndpointWithoutRequest
{
    private readonly IincomeRepository _repository;

    public BudgetPdfReportEndpoint(IincomeRepository repository)
    {
        _repository = repository;
    }

    public override void Configure()
    {
        Get("/budgeting/pdf/report");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        try
        {
            var byteArray = await QuestPdfService.GenerateBudgetReportBytesAsync();
            await SendBytesAsync(byteArray, "IncomeReport.pdf", "application/pdf");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}