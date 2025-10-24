using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profisysApp.Services;

namespace profisysApp.Controllers
{
  [ApiController]
  [Route("api/items")]
  public class ItemsController : ControllerBase
  {
    private readonly ItemsService _itemsService;
    private readonly AuditService _auditService;

    public ItemsController(ItemsService itemsService, AuditService auditService)
    {
      _itemsService = itemsService;
      _auditService = auditService;
    }

    [HttpDelete("{itemId}")]
    // [Authorize(Roles = "Admin")]
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

  }
}
