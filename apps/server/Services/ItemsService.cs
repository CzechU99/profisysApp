using profisysApp.Entities;
using profisysApp.Repositories;
using profisysApp.Models;

namespace profisysApp.Services
{
  public class ItemsService
  {
    private readonly IItemsRepository _itemsRepository;

    public ItemsService(IItemsRepository itemsRepository)
    {
      _itemsRepository = itemsRepository;
    }

    public async Task<DocumentItems?> GetItemByIdAsync(int itemId)
    {
      return await _itemsRepository.GetItemByIdAsync(itemId);
    }

    public async Task DeleteItemAsync(DocumentItems documentItem)
    {
      await _itemsRepository.DeleteItemAsync(documentItem);
    }

    public async Task UpdateItemAsync(ItemDto updatedItem, DocumentItems documentToItem)
    {
      await _itemsRepository.UpdateItemAsync(updatedItem, documentToItem);
    }

    public async Task AddItemAsync(DocumentItems newItem)
    {
      await _itemsRepository.AddItemAsync(newItem);
    }
  }
}