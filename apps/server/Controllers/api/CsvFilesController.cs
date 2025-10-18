using Microsoft.AspNetCore.Mvc;
using profisysApp.Services;
using profisysApp.Config;

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
        public IActionResult ImportCsv()
        {
            try
            {
                _importService.Import(_appSettings.PATH_TO_DOCUMENTS_CSV, _appSettings.PATH_TO_DOCUMENT_ITEMS_CSV);
                return Ok(new { message = "Dane zostały zaimportowane" });
            } catch (Exception ex)
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas importu danych", error = ex.Message });
            }
        }
    }
}
