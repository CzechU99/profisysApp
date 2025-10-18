using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using profisysApp.Data;
using profisysApp.Services;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CsvFilesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly CsvImportService _importService;

        public CsvFilesController(DatabaseContext context, CsvImportService importService)
        {
            _context = context;
            _importService = importService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var query = _context.Documents.Include(d => d.DocumentItem).AsQueryable();
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
