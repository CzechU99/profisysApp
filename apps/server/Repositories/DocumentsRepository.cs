using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Entities;

namespace profisysApp.Repositories
{
  public class DocumentsRepository : IDocumentsRepository
  {
    private readonly DatabaseContext _context;

    public DocumentsRepository(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<List<Documents>> GetAllDocumentsAsync()
    {
      return await _context.Documents.Include(d => d.DocumentItem).ToListAsync();
    }

    public async Task<Documents?> GetDocumentByIdAsync(int documentId)
    {
      return await _context.Documents.Include(d => d.DocumentItem).FirstOrDefaultAsync(d => d.Id == documentId);
    }

    public async Task DeleteAllDocumentsAsync()
    {
      var allDocuments = await _context.Documents.ToListAsync();

      _context.Documents.RemoveRange(allDocuments);
      await _context.SaveChangesAsync();
    }

    public async Task AddNewDocumentsAsync(List<Documents> documents)
    {
      var existingDocumentIds = await GetExistingDocumentIdsAsync();
      var newDocuments = GetNewDocuments(documents, existingDocumentIds);

      await AddDocumentsAsync(newDocuments);
    }

    public async Task DeleteDocumentAsync(Documents document)
    {
      _context.Documents.Remove(document);
      await _context.SaveChangesAsync();
    }

    private async Task AddDocumentsAsync(List<Documents> newDocuments)
    {
      if (newDocuments.Any()) {
        await _context.Documents.AddRangeAsync(newDocuments);
      }
    }

    private List<Documents> GetNewDocuments(List<Documents> documents, List<int> existingDocumentIds)
    {
      return documents.Where(doc => !existingDocumentIds.Contains(doc.Id)).ToList();
    }

    private async Task<List<int>> GetExistingDocumentIdsAsync()
    {
      return await _context.Documents.Select(d => d.Id).ToListAsync();
    }

    public async Task SaveChangesDocumentsAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task UpdateDocumentAsync(Documents updatedDocument, Documents documentToUpdate)
    {
      documentToUpdate.Type = updatedDocument.Type;
      documentToUpdate.Date = updatedDocument.Date;
      documentToUpdate.FirstName = updatedDocument.FirstName;
      documentToUpdate.LastName = updatedDocument.LastName;
      documentToUpdate.City = updatedDocument.City;

      await _context.SaveChangesAsync();
    }
  }
}
