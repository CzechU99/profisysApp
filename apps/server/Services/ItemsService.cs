using profisysApp.Entities;
using profisysApp.Repositories;
using System.Text.Json;

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

    public DocumentItems? SerializeItem(JsonElement updatedItem)
    {
      return JsonSerializer.Deserialize<DocumentItems>(
        updatedItem.GetRawText(),
        new JsonSerializerOptions {
          PropertyNameCaseInsensitive = true,
          PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        }
      );
    }

    public async Task UpdateItemAsync(DocumentItems updatedItem, DocumentItems documentToItem)
    {
      await _itemsRepository.UpdateItemAsync(updatedItem, documentToItem);
    }

  }
}