using profisysApp.Config;
using profisysApp.Entities;
using CsvHelper;
using profisysApp.Repositories;
using CsvHelper.Configuration;

namespace profisysApp.Services
{
  public class DocumentsService
  {
    private readonly DocumentsRepository _documentsRepository;

    public DocumentsService(DocumentsRepository documentsRepository)
    {
      _documentsRepository = documentsRepository;
    }

    public async Task<List<Documents>> GetAllDocuments()
    {
      return await _documentsRepository.GetAllDocuments();
    }

    public async Task DeleteAllDocuments()
    {
      await _documentsRepository.DeleteAllDocuments();
    }

    public async Task<Documents?> GetDocumentById(int documentId)
    {
      return await _documentsRepository.GetDocumentById(documentId);
    }

    public async Task DeleteDocument(Documents document)
    {
      await _documentsRepository.DeleteDocument(document);
    }

  }
}