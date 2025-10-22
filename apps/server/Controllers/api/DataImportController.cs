using Microsoft.AspNetCore.Mvc;
using profisysApp.Services;
using profisysApp.Config;
using Microsoft.AspNetCore.Authorization;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/dataImport")]
    public class DataImportController : ControllerBase
    {
        private readonly DataImportService _importService;
        private readonly AppSettings _appSettings;

        public DataImportController(
            DataImportService importService,
            AppSettings appSettings
        )
        {
            _importService = importService;
            _appSettings = appSettings;
        }

        [HttpPost("csvFiles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ImportCsv()
        {
            try
            {
                await _importService.Import(_appSettings.PATH_TO_DOCUMENTS_CSV, _appSettings.PATH_TO_DOCUMENT_ITEMS_CSV);
                return Ok(new { message = "Dane zostały zaimportowane do bazy danych!" });
            }
            catch 
            {
                return StatusCode(500, new { message = "Błąd podcas importowania danych do bazy danych!" });
            }
        }
    }
}
