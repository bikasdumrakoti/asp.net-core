using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using OfficeOpenXml;
using RepositoryContracts;
using Microsoft.Extensions.Logging;
using Serilog;
using SerilogTimings;

namespace Services
{
    public class PersonsGetterService : IPersonsGetterService
    {
        //private field
        private readonly IPersonsRepository _personsRepository;
        private readonly ILogger<PersonsGetterService> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        //constructor
        public PersonsGetterService(IPersonsRepository personsRepository, ILogger<PersonsGetterService> logger, IDiagnosticContext diagnosticContext)
        {
            _personsRepository = personsRepository;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
        }

        public virtual async Task<List<PersonResponse>> GetAllPersons()
        {
            _logger.LogInformation("GetAllPersons of PersonsService");

            var persons = await _personsRepository.GetAllPersons();

            return persons
                .Select(temp => temp.ToPersonResponse()).ToList();
        }

        public virtual async Task<PersonResponse?> GetPersonByPersonID(Guid? personID)
        {
            if (personID == null)
                return null;

            Person? person = await _personsRepository.GetPersonByPersonID(personID.Value);

            if (person == null)
                return null;

            return person.ToPersonResponse();
        }

        public virtual async Task<List<PersonResponse>> GetFilteredPersons(string searchBy, string? searchString)
        {
            _logger.LogInformation("GetFilteredPersons of PersonsService");

            List<Person> persons;

            using (Operation.Time("Time for Filtered Persons from Database"))
            {
                persons = searchBy switch
                {
                    nameof(PersonResponse.PersonName) =>
                        await _personsRepository.GetFilteredPersons(temp => temp.PersonName.Contains(searchString)),

                    nameof(PersonResponse.Email) =>
                    await _personsRepository.GetFilteredPersons(temp => temp.Email.Contains(searchString)),

                    nameof(PersonResponse.DateOfBirth) =>
                        await _personsRepository.GetFilteredPersons(temp => temp.DateOfBirth.Value.ToString("dd MMM yyyy").Contains(searchString)),

                    nameof(PersonResponse.Gender) =>
                    await _personsRepository.GetFilteredPersons(temp => temp.Gender.Contains(searchString)),

                    nameof(PersonResponse.CountryID) =>
                        await _personsRepository.GetFilteredPersons(temp => temp.Country.CountryName.Contains(searchString)),

                    nameof(PersonResponse.Address) =>
                    await _personsRepository.GetFilteredPersons(temp => temp.Address.Contains(searchString)),

                    _ => await _personsRepository.GetAllPersons()
                }; //end of "using block" of serilog timings
            }

            _diagnosticContext.Set("Persons", persons);

            return persons.Select(temp => temp.ToPersonResponse()).ToList();
        }

        public virtual async Task<MemoryStream> GetPersonsCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);

            CsvConfiguration csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture);
            CsvWriter csvWriter = new CsvWriter(streamWriter, csvConfiguration);

            //Personname,Email,DateOfBirth,Age,Country,Address,ReceiveNewsLetters
            csvWriter.WriteField(nameof(PersonResponse.PersonName));
            csvWriter.WriteField(nameof(PersonResponse.Email));
            csvWriter.WriteField(nameof(PersonResponse.DateOfBirth));
            csvWriter.WriteField(nameof(PersonResponse.Age));
            csvWriter.WriteField(nameof(PersonResponse.Country));
            csvWriter.WriteField(nameof(PersonResponse.Address));
            csvWriter.WriteField(nameof(PersonResponse.ReceiveNewsLetters));
            csvWriter.NextRecord();

            List<PersonResponse> personResponses = await GetAllPersons();

            foreach (PersonResponse personResponse in personResponses)
            {
                csvWriter.WriteField(personResponse.PersonName);
                csvWriter.WriteField(personResponse.Email);
                if (personResponse.DateOfBirth.HasValue)
                    csvWriter.WriteField(personResponse.DateOfBirth.Value.ToString("yyyy-MM-dd"));
                else
                    csvWriter.WriteField("");
                csvWriter.WriteField(personResponse.Age);
                csvWriter.WriteField(personResponse.Country);
                csvWriter.WriteField(personResponse.Address);
                csvWriter.WriteField(personResponse.ReceiveNewsLetters);
                csvWriter.NextRecord();
                csvWriter.Flush();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public virtual async Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("PersonsSheet");
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Email";
                worksheet.Cells["C1"].Value = "Date of Birth";
                worksheet.Cells["D1"].Value = "Age";
                worksheet.Cells["E1"].Value = "Gender";
                worksheet.Cells["F1"].Value = "Country";
                worksheet.Cells["G1"].Value = "Address";
                worksheet.Cells["H1"].Value = "Receive News Letters";

                using (ExcelRange headerCells = worksheet.Cells["A1:H1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    headerCells.Style.Font.Bold = true;
                }

                int row = 2;
                List<PersonResponse> personResponses = await GetAllPersons();

                foreach (PersonResponse personResponse in personResponses)
                {
                    worksheet.Cells[row, 1].Value = personResponse.PersonName;
                    worksheet.Cells[row, 2].Value = personResponse.Email;
                    if (personResponse.DateOfBirth.HasValue)
                        worksheet.Cells[row, 3].Value = personResponse.DateOfBirth.Value.ToString("yyyy-MM-dd");
                    else
                        worksheet.Cells[row, 3].Value = "";
                    worksheet.Cells[row, 4].Value = personResponse.Age;
                    worksheet.Cells[row, 5].Value = personResponse.Gender;
                    worksheet.Cells[row, 6].Value = personResponse.Country;
                    worksheet.Cells[row, 7].Value = personResponse.Address;
                    worksheet.Cells[row, 8].Value = personResponse.ReceiveNewsLetters;

                    row++;
                }

                worksheet.Cells[$"A1:H{row}"].AutoFitColumns();

                await excelPackage.SaveAsync();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
    }
}
