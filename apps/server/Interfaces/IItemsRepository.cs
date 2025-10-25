using profisysApp.Entities;
using profisysApp.Models;

namespace profisysApp.Repositories
{
    public interface IItemsRepository
    {
        Task<DocumentItems?> GetItemByIdAsync(int documentId);
        Task DeleteItemAsync(DocumentItems itemId);
        Task SaveChangesItemsAsync();
        Task UpdateItemAsync(ItemUpdateDto updatedItem, DocumentItems itemToUpdate);
    }
}
