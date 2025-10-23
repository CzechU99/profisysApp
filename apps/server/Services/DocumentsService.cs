using profisysApp.Entities;
using profisysApp.Repositories;
using System.Text.Json;

namespace profisysApp.Services
{
  public class DocumentsService
  {
    private readonly IDocumentsRepository _documentsRepository;

    public DocumentsService(IDocumentsRepository documentsRepository)
    {
      _documentsRepository = documentsRepository;
    }

    public async Task<List<Documents>> GetAllDocumentsAsync()
    {
      return await _documentsRepository.GetAllDocumentsAsync();
    }

    public async Task DeleteAllDocumentsAsync()
    {
      await _documentsRepository.DeleteAllDocumentsAsync();
    }

    public async Task<Documents?> GetDocumentByIdAsync(int documentId)
    {
      return await _documentsRepository.GetDocumentByIdAsync(documentId);
    }

    public async Task DeleteDocumentAsync(Documents document)
    {
      await _documentsRepository.DeleteDocumentAsync(document);
    }

    public async Task UpdateDocumentAsync(Documents updatedDocument, Documents documentToUpdate)
    {
      await _documentsRepository.UpdateDocumentAsync(updatedDocument, documentToUpdate);
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