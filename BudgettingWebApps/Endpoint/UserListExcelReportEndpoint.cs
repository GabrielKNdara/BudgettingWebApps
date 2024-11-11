using BudgettingWebApps.Models.Reports;
using BudgettingWebApps.Reposiotories;
using FastEndpoints;

namespace BudgettingWebApps.Endpoint
{
    public class UserListExcelReportEndpoint : EndpointWithoutRequest
    {
        private readonly IUserRepository _userRepository;
        public UserListExcelReportEndpoint(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public override void Configure()
        {
            Get("/users/excel/report/");
        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            try
            {
                var data = await _userRepository.GetAlluser();
                var report = new CurrentUserExcelReportGenerator();
                var attachment = report.GenerateReport(data);
                await SendBytesAsync(attachment.ByteArray, attachment.FileName, cancellation: ct);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
