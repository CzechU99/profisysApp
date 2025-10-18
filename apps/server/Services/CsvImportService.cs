using profisysApp.Data;
using CsvHelper;
using System.Globalization;
using profisysApp.Entities;

namespace profisysApp.Services
{

  public class CsvImportService
  {
    private readonly DatabaseContext _context;

    public CsvImportService(DatabaseContext context)
    {
      _context = context;
    }

    public void Import(string documentsPath, string itemsPath)
    {
        var documents = ReadCsv<Documents>(documentsPath);
        var items = ReadCsv<DocumentItems>(itemsPath);

        _context.Documents.AddRange(documents);
        _context.DocumentItems.AddRange(items);
        _context.SaveChanges();
    }

    private List<T> ReadCsv<T>(string path)
    {
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
        {
          Delimiter = ";",
          HeaderValidated = null,
          MissingFieldFound = null,
        });
        return csv.GetRecords<T>().ToList();
    }
  }

}