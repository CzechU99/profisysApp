using profisysApp.Entities;
using profisysApp.Repositories;
using System.Text.Json;

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

    public async Task UpdateDocument(Documents updatedDocument, Documents documentToUpdate)
    {
      await _documentsRepository.UpdateDocument(updatedDocument, documentToUpdate);
    }

    public Documents? SerializeDocument(JsonElement updatedDocument)
    {
      return JsonSerializer.Deserialize<Documents>(
        updatedDocument.GetRawText(),
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
      );
    }
  }
}