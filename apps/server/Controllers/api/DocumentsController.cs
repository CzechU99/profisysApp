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
                return Ok(documents);
            } catch (Microsoft.Data.Sqlite.SqliteException exception)
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

    }
}
