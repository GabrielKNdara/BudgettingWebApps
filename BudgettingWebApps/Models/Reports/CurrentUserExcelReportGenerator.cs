﻿using ClosedXML.Excel;


namespace BudgettingWebApps.Models.Reports
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
            _worksheet.Range(1,1,1 ,3).Merge().SetValue("List of users").Style.Font.SetBold().Border
                .SetBottomBorder(XLBorderStyleValues.Thin);
            var row = 3;
            _worksheet.Cell(row, 1).Value = "Username";
            _worksheet.ColumnWidth = 15;
            _worksheet.Cell(row, 2).Value = "Email";
            _worksheet.ColumnWidth = 25;
            _worksheet.Cell(row, 3).Value = "First Name";
            _worksheet.ColumnWidth = 15;
            _worksheet.Cell(row, 4).Value = "Surname ";
            _worksheet.ColumnWidth = 15;
            _worksheet.Cell(row, 5).Value = "Role";
            _worksheet.ColumnWidth = 15;
                        
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
