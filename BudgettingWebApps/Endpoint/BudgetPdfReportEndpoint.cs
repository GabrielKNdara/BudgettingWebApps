using System.Net;
using BudgettingWebApps.Reposiotories;
using BudgettingWebApps.Services;
using FastEndpoints;
using Google.Protobuf.Compiler;

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
          // var byteArray =new BudgetDocument();
          // var bytea = byteArray.GetMetadata();
          // await SendBytesAsync(byteArray.GetMetadata(),  "IncomeReport.pdf","application/pdf");
          
          
          var byteArray = QuestPdfService.GenerateBudgetReportBytes();
          await SendBytesAsync(byteArray,  "IncomeReport.pdf","application/pdf");

        }
        catch (Exception e)
        {
          Console.WriteLine(e.ToString());
        }
    }
}