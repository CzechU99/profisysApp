using profisysApp.Entities;
using profisysApp.Repositories;
using profisysApp.Models;

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

    public async Task UpdateDocumentAsync(DocumentDto updatedDocument, Documents documentToUpdate)
    {
      await _documentsRepository.UpdateDocumentAsync(updatedDocument, documentToUpdate);
    }

    public async Task HandleDocumentAsync(Documents document)
    {
      var existingDoc = await _documentsRepository.GetDocumentByIdAsync(document.Id);
      if (existingDoc != null)
      {
        var missingItems = await _documentsRepository.GetMissingItemsAsync(document, existingDoc);
        if (missingItems.Any())
        {
          await _documentsRepository.DeleteDocumentAsync(existingDoc);
        }
      }

      await _documentsRepository.AddDocumentAsync(document);
    }

    public async Task AddDocumentAsync(Documents document)
    {
      await _documentsRepository.AddDocumentAsync(document);
    }

    public async Task SaveChangesDocumentsAsync()
    {
      await _documentsRepository.SaveChangesDocumentsAsync();
    }

    public async Task<int> GetLastInsertedIdAsync()
    {
      return await _documentsRepository.GetLastInsertedIdAsync();
    }
  }
}