using Microsoft.AspNetCore.Mvc;
using profisysApp.Services;
using profisysApp.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CsvFilesController : ControllerBase
    {
        private readonly CsvImportService _importService;
        private readonly AppSettings _appSettings;

        public CsvFilesController(
            CsvImportService importService,
            AppSettings appSettings
        )
        {
            _importService = importService;
            _appSettings = appSettings;
        }

        [HttpPost("ImportCsv")]
        [Authorize]
        public IActionResult ImportCsv()
        {
            try
            {
                _importService.Import(_appSettings.PATH_TO_DOCUMENTS_CSV, _appSettings.PATH_TO_DOCUMENT_ITEMS_CSV);
                return Ok(new { message = "Dane zostały zaimportowane" });
            }
            catch (Exception exception)
            {
                return StatusCode(500, new { message = "Błąd: ", error = exception.Message });
            }
        }
    }
}
