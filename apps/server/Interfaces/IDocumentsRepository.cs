using profisysApp.Entities;

namespace profisysApp.Repositories
{
    public interface IDocumentsRepository
    {
        Task<List<Documents>> GetAllDocuments();
        Task<Documents?> GetDocumentById(int documentId);
        Task AddNewDocuments(List<Documents> documents);
        Task DeleteDocument(Documents document);
        Task DeleteAllDocuments();
        Task UpdateDocument(Documents updatedDocument, Documents documentToUpdate);
        Task SaveChangesDocuments();
    }
}
