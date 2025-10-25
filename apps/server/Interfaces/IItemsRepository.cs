using profisysApp.Entities;
using profisysApp.Models;

namespace profisysApp.Repositories
{
    public interface IItemsRepository
    {
        Task<DocumentItems?> GetItemByIdAsync(int documentId);
        Task DeleteItemAsync(DocumentItems itemId);
        Task SaveChangesItemsAsync();
        Task UpdateItemAsync(ItemDto updatedItem, DocumentItems itemToUpdate);
        Task AddItemAsync(ItemDto newItem);
    }
}
