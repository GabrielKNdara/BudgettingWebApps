using BudgettingWebApps.Reposiotories;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace BudgettingWebApps.Models.ExcelReport
{
    public class CurrentUserExcelReportGenerator
    {
        private IXLWorksheet _worksheet;
        private IXLWorkbook _workbook;
        public AttachmentDto GenerateReport(List<UserDto> reportData)
        {
            _workbook = new XLWorkbook();
            AddDetailedSheet(reportData);
            MemoryStream memoryStream = new MemoryStream();
            _workbook.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
           
            var content = memoryStream.ToArray();
            memoryStream.Close();

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            return new AttachmentDto() { ByteArray = content, ContentType = contentType, FileName = "UserList.xlsx" };

        }

        private void AddDetailedSheet(List<UserDto> reportData)
        {
            _worksheet = _workbook.Worksheets.Add();
            var row = 1;
            _worksheet.Cell(row, 1).Value = "Username";
            _worksheet.Cell(row, 2).Value = "Email";
            _worksheet.Cell(row, 3).Value = "First Name";
            _worksheet.Cell(row, 4).Value = "Surname ";
            _worksheet.Cell(row, 5).Value = "Role";
                        
            _worksheet.Range(row, 1, row, 5).Style.Font.SetBold().Border.SetTopBorder(XLBorderStyleValues.Thin);
            row++;
            foreach(var user in reportData)
            {
                _worksheet.Cell(row,1).Value = user.UserName;
                _worksheet.Cell(row,2).Value = user.Email;
                _worksheet.Cell(row,3).Value = user.FirstName;
                _worksheet.Cell(row,4).Value = user.Surname;
                _worksheet.Cell(row,5).Value = user.Role;              
                               
                row++;
            }

        }
    }
}
