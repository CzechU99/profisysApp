using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Services;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly CsvImportService _importService;

        public DocumentsController(DatabaseContext context, CsvImportService importService)
        {
            _context = context;
            _importService = importService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? customer, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            var query = _context.Documents.Include(d => d.Id).AsQueryable();
            return Ok(await query.ToListAsync());
        }

        [HttpPost("import")]
        public IActionResult ImportCsv()
        {
            _importService.Import("Docs/Documents.csv", "Docs/DocumentItems.csv");
            return Ok(new { message = "Dane zostały zaimportowane" });
        }
    }
}
