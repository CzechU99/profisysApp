using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Entities;
using profisysApp.Models;

namespace profisysApp.Repositories
{
  public class ItemsRepository : IItemsRepository
  {
    private readonly DatabaseContext _context;

    public ItemsRepository(DatabaseContext context)
    {
      _context = context;
    }

    public async Task<DocumentItems?> GetItemByIdAsync(int itemId)
    {
      return await _context.DocumentItems.FirstOrDefaultAsync(d => d.Id == itemId);
    }

    public async Task DeleteItemAsync(DocumentItems document)
    {
      _context.DocumentItems.Remove(document);
      await SaveChangesItemsAsync();
    }

    public async Task SaveChangesItemsAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(ItemUpdateDto updatedItem, DocumentItems itemToUpdate)
    {
      itemToUpdate.Product = updatedItem.Product;
      itemToUpdate.Quantity = updatedItem.Quantity;
      itemToUpdate.TaxRate = updatedItem.TaxRate;
      itemToUpdate.Price = updatedItem.Price;

      await SaveChangesItemsAsync();
    }

  }
}
