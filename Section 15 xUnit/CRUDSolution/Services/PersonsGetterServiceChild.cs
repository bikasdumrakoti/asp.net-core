using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using RepositoryContracts;
using Serilog;
using ServiceContracts.DTOs;

namespace Services
{
    public class PersonsGetterServiceChild : PersonsGetterService
    {
        public PersonsGetterServiceChild(IPersonsRepository personsRepository, ILogger<PersonsGetterService> logger, IDiagnosticContext diagnosticContext) : base(personsRepository, logger, diagnosticContext)
        {
        }

        public async override Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheet");
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Age";
                worksheet.Cells["C1"].Value = "Gender";

                using (ExcelRange headerCells = worksheet.Cells["A1:C1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;
                List<PersonResponse> personResponses = await GetAllPersons();

                if (personResponses.Count == 0)
                {
                    throw new InvalidOperationException("No persons data");
                }

                foreach (PersonResponse personResponse in personResponses)
                {
                    worksheet.Cells[row, 1].Value = personResponse.PersonName;
                    worksheet.Cells[row, 2].Value = personResponse.Age;
                    worksheet.Cells[row, 3].Value = personResponse.Gender;

                    row++;
                }

                worksheet.Cells[$"A1:C{row}"].AutoFitColumns();

                await excelPackage.SaveAsync();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
