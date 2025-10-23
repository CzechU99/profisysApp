using profisysApp.Config;
using profisysApp.Entities;
using CsvHelper;
using profisysApp.Repositories;
using CsvHelper.Configuration;

namespace profisysApp.Services
{
  public class DataImportService
  {
    private readonly IDocumentsRepository _documentsRepository;
    private readonly AppSettings _settings;

    public DataImportService(IDocumentsRepository documentsRepository, AppSettings settings)
    {
      _documentsRepository = documentsRepository;
      _settings = settings;
    }

    public async Task ImportAsync(string documentsPath, string itemsPath)
    {
      var documents = ReadCsv<Documents>(documentsPath);
      var items = ReadCsv<DocumentItems>(itemsPath);

      var itemsByDocumentId = GroupItemsByDocumentId(items);
      AssignItemsToDocuments(documents, itemsByDocumentId);
      
      await _documentsRepository.AddNewDocumentsAsync(documents);
      await _documentsRepository.SaveChangesDocumentsAsync();
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