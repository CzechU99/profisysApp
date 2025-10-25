using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profisysApp.Services;
using profisysApp.Models;

namespace profisysApp.Controllers
{
  [ApiController]
  [Route("api/items")]
  public class ItemsController : ControllerBase
  {
    private readonly ItemsService _itemsService;
    private readonly DocumentsService _documentsService;
    private readonly AuditService _auditService;

    public ItemsController(DocumentsService documentsService, ItemsService itemsService, AuditService auditService)
    {
      _itemsService = itemsService;
      _auditService = auditService;
      _documentsService = documentsService;
    }

    [HttpDelete("{itemId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDocumentItem(int itemId)
    {
      try
      {
        var item = await _itemsService.GetItemByIdAsync(itemId);

        if (item == null)
        {
          return NotFound(new { message = "Nie znaleziono takiego itemu dokumentu!" });
        }

        await _itemsService.DeleteItemAsync(item);
        await _auditService.LogAsync(User, "Usunięcie itemu dokumentu", $"DocumentId: {itemId}");

        return Ok(new { message = "Item dokuemntu został pomyślnie usunięty!" });
      }
      catch
      {
        return StatusCode(500, new { message = "Błąd podczas usuwania itemu dokumentu!" });
      }
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateItem([FromBody] ItemDto updatedItem)
    {
      try
      {
        var itemToUpdate = await _itemsService.GetItemByIdAsync(updatedItem.Id);

        if (itemToUpdate == null)
          return NotFound(new { message = "Nie znaleziono itemu." });

        await _itemsService.UpdateItemAsync(updatedItem, itemToUpdate);
        await _auditService.LogAsync(User, "Aktualizacja itemu", $"ItemId: {itemToUpdate.Id}");

        return Ok(new { message = "Item zaktualizowany pomyślnie!" });
      }
      catch
      {
        return StatusCode(500, new { message = "Błąd podczas edytowania itemu!" });
      }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddItem([FromBody] ItemDto newItem)
    {
      try
      {
        var document = await _documentsService.GetDocumentByIdAsync(newItem.DocumentId);

        if (document == null)
          return NotFound(new { message = "Nie znaleziono dokumentu." });

        var lastOrdinal = document.DocumentItem.Any()
          ? document.DocumentItem.Max(i => i.Ordinal)
          : 0;

        newItem.Ordinal = lastOrdinal + 1;
        await _itemsService.AddItemAsync(newItem);
        await _auditService.LogAsync(User, "Dodanie itemu", $"ItemId: {newItem.Id}");

        return Ok(new { message = "Dodano nowy item do dokumentu!" });
      }
      catch
      {
        return StatusCode(500, new { message = "Błąd podczas dodawania itemu!" });
      }
    }

  }
}
