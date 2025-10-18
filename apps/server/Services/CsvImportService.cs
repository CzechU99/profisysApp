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

        var itemsGrouped = items.GroupBy(i => i.DocumentId).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var doc in documents)
        {
            if (itemsGrouped.TryGetValue(doc.Id, out var docItems))
            {
                doc.DocumentItem = docItems;

                foreach (var item in docItems)
                {
                    item.DocumentId = doc.Id;
                    item.Document = doc;
                }
            }
        }

        _context.Documents.AddRange(documents);
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