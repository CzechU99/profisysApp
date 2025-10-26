using profisysApp.Entities;
using profisysApp.Models;

namespace profisysApp.Repositories
{
    public interface IDocumentsRepository
    {
        Task<List<Documents>> GetAllDocumentsAsync();
        Task<Documents?> GetDocumentByIdAsync(int documentId);
        Task DeleteDocumentAsync(Documents document);
        Task AddDocumentAsync(Documents document);
        Task DeleteAllDocumentsAsync();
        Task<List<DocumentItems>> GetMissingItemsAsync(Documents incomingDoc, Documents existingDoc);
        Task UpdateDocumentAsync(DocumentDto updatedDocument, Documents documentToUpdate);
        Task SaveChangesDocumentsAsync();
        Task<int> GetLastInsertedIdAsync();
    }
}
