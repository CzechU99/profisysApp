using profisysApp.Entities;

namespace profisysApp.Repositories
{
    public interface IItemsRepository
    {
        Task<DocumentItems?> GetItemByIdAsync(int documentId);
        Task DeleteItemAsync(DocumentItems itemId);
        Task SaveChangesItemsAsync();
    }
}
