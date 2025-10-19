using profisysApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public DocumentsController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllDocuments")]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _context.Documents.Include(d => d.DocumentItem).AsQueryable().ToListAsync();
                if(documents.Count == 0)
                {
                    return Ok("Brak dokumentów w bazie danych.");
                }
                return Ok(documents);
            }
            catch (Microsoft.Data.Sqlite.SqliteException exception)
            {
                return StatusCode(500, $"Błąd bazy danych: {exception.Message}");
            }
            catch (InvalidOperationException exception)
            {
                return StatusCode(500, $"Błąd odczyty z bazy danych: {exception.Message}");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Nieoczekiwny błąd: {exception.Message}");
            }
        }

        [HttpDelete("DeleteDocuments")]
        public async Task<IActionResult> DeleteDocuments()
        {
            try
            {
                var documents = await _context.Documents.Include(d => d.DocumentItem).AsQueryable().ToListAsync();

                if (documents.Count == 0)
                {
                    return Ok("Brak dokumentów do usunięcia.");
                }

                _context.Documents.RemoveRange(documents);
                await _context.SaveChangesAsync();

                return Ok("Dokumenty zostały pomyślnie usunięte.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Nieoczekiwny błąd: {exception.Message}");
            }
        }
        
        [HttpDelete("DeleteDocument/{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                var document = await _context.Documents.Include(d => d.DocumentItem).FirstOrDefaultAsync(d => d.Id == id);

                if (document == null)
                {
                    return NotFound("Nie znaleziono dokumentu do usunięcia.");
                }

                _context.Documents.Remove(document);
                await _context.SaveChangesAsync();

                return Ok("Dokument został pomyślnie usunięty.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Nieoczekiwny błąd: {exception.Message}");
            }
        }

    }
}
