using profisysApp.Entities;

namespace profisysApp.Repositories
{
    public interface IDocumentsRepository
    {
        Task<List<Documents>> GetAllDocumentsAsync();
        Task<Documents?> GetDocumentByIdAsync(int documentId);
        Task AddNewDocumentsAsync(List<Documents> documents);
        Task DeleteDocumentAsync(Documents document);
        Task DeleteAllDocumentsAsync();
        Task UpdateDocumentAsync(Documents updatedDocument, Documents documentToUpdate);
        Task SaveChangesDocumentsAsync();
    }
}
