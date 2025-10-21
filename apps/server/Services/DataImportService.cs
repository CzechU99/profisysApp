using profisysApp.Config;
using profisysApp.Data;
using profisysApp.Entities;
using Microsoft.EntityFrameworkCore;
using CsvHelper;
using CsvHelper.Configuration;

namespace profisysApp.Services
{
  public class DataImportService
  {
    private readonly DatabaseContext _context;
    
    private readonly AppSettings _settings;

    public DataImportService(DatabaseContext context, AppSettings settings)
    {
      _context = context;
      _settings = settings;
    }

    public async Task Import(string documentsPath, string itemsPath)
    {
      var documents = ReadCsv<Documents>(documentsPath);
      var items = ReadCsv<DocumentItems>(itemsPath);

      var itemsByDocumentId = GroupItemsByDocumentId(items);
      
      AssignItemsToDocuments(documents, itemsByDocumentId);
      
      await AddNewDocuments(documents);
      
      await _context.SaveChangesAsync();
    }

    private Dictionary<int, List<DocumentItems>> GroupItemsByDocumentId(List<DocumentItems> items)
    {
      return items
        .GroupBy(i => i.DocumentId)
        .ToDictionary(g => g.Key, g => g.ToList());
    }

    private void AssignItemsToDocuments(
      List<Documents> documents, 
      Dictionary<int, List<DocumentItems>> itemsByDocumentId)
    {
      foreach (var doc in documents)
      {
        if (itemsByDocumentId.TryGetValue(doc.Id, out var docItems))
        {
          doc.DocumentItem = docItems;

          foreach (var item in docItems)
          {
            item.DocumentId = doc.Id;
            item.Document = doc;
          }
        }
      }
    }

    private async Task AddNewDocuments(List<Documents> documents)
    {
      var existingDocumentIds = await _context.Documents
        .Select(d => d.Id)
        .ToListAsync();

      var newDocuments = documents
        .Where(doc => !existingDocumentIds.Contains(doc.Id))
        .ToList();

      if (newDocuments.Any())
      {
        await _context.Documents.AddRangeAsync(newDocuments);
      }
    }

    private List<T> ReadCsv<T>(string path)
    {
      using var reader = new StreamReader(path);
      using var csv = new CsvReader(reader, CreateCsvConfiguration());
      
      return csv.GetRecords<T>().ToList();
    }

    private CsvConfiguration CreateCsvConfiguration()
    {
      return new CsvConfiguration(_settings.CSV_POLISH_CULTURE)
      {
        Delimiter = ";",
        HeaderValidated = null,
        MissingFieldFound = null
      };
    }
  }
}