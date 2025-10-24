using profisysApp.Config;
using profisysApp.Entities;
using CsvHelper;
using profisysApp.Repositories;
using CsvHelper.Configuration;

namespace profisysApp.Services
{
  public class DataImportService
  {
    private readonly DocumentsService _documentsService;
    private readonly AppSettings _settings;

    public DataImportService(DocumentsService documentsService, AppSettings settings)
    {
      _documentsService = documentsService;
      _settings = settings;
    }

    public async Task ImportAsync(string documentsPath, string itemsPath)
    {
        var documents = ReadCsv<Documents>(documentsPath);
        var items = ReadCsv<DocumentItems>(itemsPath);

        var itemsByDocId = GroupItemsByDocumentId(items);
        AssignItemsToDocuments(documents, itemsByDocId);

        foreach (var document in documents)
        {
            await _documentsService.HandleDocumentAsync(document);
        }

        await _documentsService.SaveChangesDocumentsAsync();
    }

    public void AssignItemsToDocuments(List<Documents> documents, Dictionary<int, List<DocumentItems>> itemsByDocumentId)
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

    private Dictionary<int, List<DocumentItems>> GroupItemsByDocumentId(List<DocumentItems> items)
    {
      return items
        .GroupBy(i => i.DocumentId)
        .ToDictionary(g => g.Key, g => g.ToList());
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