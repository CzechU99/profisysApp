using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Entities;
using profisysApp.Models;

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
      _context.ChangeTracker.Clear();
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

    public async Task<List<DocumentItems>> GetMissingItemsAsync(Documents incomingDoc, Documents existingDoc)
    {
      return await Task.Run(() =>
        incomingDoc.DocumentItem
          .Where(i => !existingDoc.DocumentItem.Any(ei => ei.Id == i.Id))
          .ToList()
      );
    }

    public async Task AddDocumentAsync(Documents document)
    {
      await _context.Documents.AddAsync(document);
    }

    public async Task DeleteDocumentAsync(Documents document)
    {
      _context.Documents.Remove(document);
      await _context.SaveChangesAsync();
    }

    public async Task SaveChangesDocumentsAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task UpdateDocumentAsync(DocumentDto updatedDocument, Documents documentToUpdate)
    {
      documentToUpdate.Type = updatedDocument.Type;
      documentToUpdate.Date = updatedDocument.Date;
      documentToUpdate.FirstName = updatedDocument.FirstName;
      documentToUpdate.LastName = updatedDocument.LastName;
      documentToUpdate.City = updatedDocument.City;

      await _context.SaveChangesAsync();
    }

    public async Task<int> GetLastInsertedIdAsync()
    {
      return await _context.Documents.MaxAsync(d => (int?)d.Id) ?? 0;
    }
  }
}
